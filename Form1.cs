using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace weather_app
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;
        private double latitude = 40.73;
        private double longitude = -73.93;
        private string colorScheme = "Light";

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30);
            
            // Load saved color scheme preference
            try
            {
                colorScheme = Properties.Settings.Default.ColorScheme;
            }
            catch
            {
                colorScheme = "Light";
            }
            
            InitializeCharts();
            ApplyColorScheme();
            UpdateStatus("Ready. Click Refresh to get weather data.");
        }

        private void InitializeCharts()
        {
            // Initialize Combined Weather Chart
            chartTemperature.ChartAreas.Clear();
            chartTemperature.Series.Clear();
            chartTemperature.Legends.Clear();
            
            ChartArea weatherArea = new ChartArea("WeatherArea");
            weatherArea.AxisX.Title = "Time";
            weatherArea.AxisY.Title = "Temperature (°C)";
            weatherArea.AxisX.MajorGrid.Enabled = true;
            weatherArea.AxisY.MajorGrid.Enabled = true;
            weatherArea.AxisX.LabelStyle.Angle = -45;
            weatherArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            
            // Add secondary Y-axis for precipitation probability
            weatherArea.AxisY2.Enabled = AxisEnabled.True;
            weatherArea.AxisY2.Title = "Precipitation Probability (%)";
            weatherArea.AxisY2.Maximum = 100;
            weatherArea.AxisY2.Minimum = 0;
            weatherArea.AxisY2.MajorGrid.Enabled = false; // Don't show grid for secondary axis to avoid clutter
            
            chartTemperature.ChartAreas.Add(weatherArea);

            // Temperature series (left Y-axis)
            Series tempSeries = new Series("Temperature");
            tempSeries.ChartType = SeriesChartType.SplineArea;
            tempSeries.Color = Color.FromArgb(150, Color.Red); // Semi-transparent red for area fill
            tempSeries.BorderColor = Color.Red;
            tempSeries.BorderWidth = 2;
            tempSeries.YAxisType = AxisType.Primary;
            chartTemperature.Series.Add(tempSeries);

            // Apparent Temperature series (left Y-axis)
            Series apparentTempSeries = new Series("Apparent Temperature");
            apparentTempSeries.ChartType = SeriesChartType.SplineArea;
            apparentTempSeries.Color = Color.FromArgb(150, Color.Orange); // Semi-transparent orange for area fill
            apparentTempSeries.BorderColor = Color.Orange;
            apparentTempSeries.BorderWidth = 2;
            apparentTempSeries.YAxisType = AxisType.Primary;
            chartTemperature.Series.Add(apparentTempSeries);

            // Precipitation Probability series (right Y-axis) - Histogram style
            Series precipSeries = new Series("Precipitation Probability");
            precipSeries.ChartType = SeriesChartType.Column;
            precipSeries.Color = Color.Blue;
            precipSeries.YAxisType = AxisType.Secondary;
            // Histogram-style settings: bars touch each other with no gaps
            precipSeries["PointWidth"] = "1";
            precipSeries["DrawSideBySide"] = "True";
            precipSeries.BorderColor = Color.DarkBlue;
            precipSeries.BorderWidth = 1;
            chartTemperature.Series.Add(precipSeries);

            Legend weatherLegend = new Legend("WeatherLegend");
            weatherLegend.Docking = Docking.Top;
            chartTemperature.Legends.Add(weatherLegend);

            chartTemperature.Titles.Clear();
            chartTemperature.Titles.Add("Weather Forecast (3 Days)");
            chartTemperature.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);

            // Initialize Hourly Forecast Spline Chart
            chartHourlyForecast.ChartAreas.Clear();
            chartHourlyForecast.Series.Clear();
            chartHourlyForecast.Legends.Clear();

            ChartArea hourlyArea = new ChartArea("HourlyArea");
            hourlyArea.AxisX.Title = "Time";
            hourlyArea.AxisY.Title = "Temperature (°C)";
            hourlyArea.AxisX.MajorGrid.Enabled = true;
            hourlyArea.AxisY.MajorGrid.Enabled = true;
            hourlyArea.AxisX.LabelStyle.Angle = -45;
            hourlyArea.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartHourlyForecast.ChartAreas.Add(hourlyArea);

            // Temperature spline series
            Series hourlyTempSeries = new Series("Temperature");
            hourlyTempSeries.ChartType = SeriesChartType.Spline;
            hourlyTempSeries.Color = Color.Red;
            hourlyTempSeries.BorderWidth = 2;
            hourlyTempSeries.MarkerStyle = MarkerStyle.Circle;
            hourlyTempSeries.MarkerSize = 5;
            chartHourlyForecast.Series.Add(hourlyTempSeries);

            // Apparent Temperature spline series
            Series hourlyApparentTempSeries = new Series("Apparent Temperature");
            hourlyApparentTempSeries.ChartType = SeriesChartType.Spline;
            hourlyApparentTempSeries.Color = Color.Orange;
            hourlyApparentTempSeries.BorderWidth = 2;
            hourlyApparentTempSeries.MarkerStyle = MarkerStyle.Circle;
            hourlyApparentTempSeries.MarkerSize = 5;
            chartHourlyForecast.Series.Add(hourlyApparentTempSeries);

            Legend hourlyLegend = new Legend("HourlyLegend");
            hourlyLegend.Docking = Docking.Top;
            chartHourlyForecast.Legends.Add(hourlyLegend);

            chartHourlyForecast.Titles.Clear();
            chartHourlyForecast.Titles.Add("Hourly Forecast (Today)");
            chartHourlyForecast.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            UpdateStatus("Fetching weather data...");
            txtWeatherInfo.Clear();

            try
            {
                // Get city name from coordinates (reverse geocoding)
                await UpdateLocationName(latitude, longitude);

                // Build Open Meteo API URL
                string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&daily=sunset,sunrise&hourly=temperature_2m,precipitation_probability,apparent_temperature&timezone=auto&forecast_days=3";

                // Make API call
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();
                
                // Parse and display weather data
                DisplayWeatherData(jsonResponse, latitude, longitude);
                UpdateCurrentTemperature(jsonResponse);
                PopulateCharts(jsonResponse);
                PopulateHourlyForecastChart(jsonResponse);
                UpdateStatus("Weather data updated successfully.");
            }
            catch (HttpRequestException ex)
            {
                UpdateStatus("Error: Failed to fetch weather data.");
                txtWeatherInfo.Text = $"Network error: {ex.Message}\n\nPlease check your internet connection and try again.";
            }
            catch (Exception ex)
            {
                UpdateStatus("Error: An unexpected error occurred.");
                txtWeatherInfo.Text = $"Error: {ex.Message}\n\n{ex.StackTrace}";
            }
            finally
            {
                btnRefresh.Enabled = true;
            }
        }

        private void DisplayWeatherData(string jsonResponse, double latitude, double longitude)
        {
            try
            {
                JObject json = JObject.Parse(jsonResponse);
                JObject hourly = json["hourly"] as JObject;
                JObject daily = json["daily"] as JObject;

                if (hourly == null || daily == null)
                {
                    txtWeatherInfo.Text = "Error: Could not parse weather data from API response.";
                    return;
                }

                StringBuilder weatherText = new StringBuilder();
                weatherText.AppendLine("═══════════════════════════════════════");
                weatherText.AppendLine("         FORECAST INFO");
                weatherText.AppendLine("═══════════════════════════════════════");
                weatherText.AppendLine();
                weatherText.AppendLine($"Location: {latitude:F4}°N, {longitude:F4}°E");
                
                // Get timezone info
                if (json["timezone"] != null)
                {
                    weatherText.AppendLine($"Timezone: {json["timezone"].Value<string>()}");
                }
                weatherText.AppendLine();

                // Display Hourly Data (Next 12 hours)
                if (hourly["time"] != null && hourly["temperature_2m"] != null)
                {
                    weatherText.AppendLine("═══════════════════════════════════════");
                    weatherText.AppendLine("         HOURLY FORECAST (NEXT 12 HOURS)");
                    weatherText.AppendLine("═══════════════════════════════════════");
                    weatherText.AppendLine();

                    JArray timeArray = hourly["time"] as JArray;
                    JArray tempArray = hourly["temperature_2m"] as JArray;
                    JArray apparentTempArray = hourly["apparent_temperature"] as JArray;
                    JArray precipProbArray = hourly["precipitation_probability"] as JArray;

                    int hoursToShow = Math.Min(12, timeArray.Count);

                    for (int i = 0; i < hoursToShow; i++)
                    {
                        string time = timeArray[i].Value<string>();
                        double temp = tempArray[i].Value<double>();
                        double apparentTemp = apparentTempArray != null && i < apparentTempArray.Count 
                            ? apparentTempArray[i].Value<double>() 
                            : temp;
                        int precipProb = precipProbArray != null && i < precipProbArray.Count 
                            ? precipProbArray[i].Value<int>() 
                            : 0;

                        // Format time (show only time part)
                        string timeOnly = time.Contains("T") ? time.Split('T')[1] : time;
                        if (timeOnly.Length > 5) timeOnly = timeOnly.Substring(0, 5); // Keep only HH:MM

                        weatherText.AppendLine($"{timeOnly} - Temp: {temp:F1}°C (Feels like: {apparentTemp:F1}°C), Precip: {precipProb}%");
                    }
                }

                weatherText.AppendLine();
                weatherText.AppendLine("═══════════════════════════════════════");

                txtWeatherInfo.Text = weatherText.ToString();
            }
            catch (JsonException ex)
            {
                txtWeatherInfo.Text = $"Error parsing weather data: {ex.Message}\n\nRaw response:\n{jsonResponse}";
            }
        }

        private string GetWeatherDescription(int weatherCode)
        {
            // WMO Weather interpretation codes (WW)
            switch (weatherCode)
            {
                case 0: return "Clear sky";
                case 1: return "Mainly clear";
                case 2: return "Partly cloudy";
                case 3: return "Overcast";
                case 45: return "Fog";
                case 48: return "Depositing rime fog";
                case 51: return "Light drizzle";
                case 53: return "Moderate drizzle";
                case 55: return "Dense drizzle";
                case 56: return "Light freezing drizzle";
                case 57: return "Dense freezing drizzle";
                case 61: return "Slight rain";
                case 63: return "Moderate rain";
                case 65: return "Heavy rain";
                case 66: return "Light freezing rain";
                case 67: return "Heavy freezing rain";
                case 71: return "Slight snow fall";
                case 73: return "Moderate snow fall";
                case 75: return "Heavy snow fall";
                case 77: return "Snow grains";
                case 80: return "Slight rain showers";
                case 81: return "Moderate rain showers";
                case 82: return "Violent rain showers";
                case 85: return "Slight snow showers";
                case 86: return "Heavy snow showers";
                case 95: return "Thunderstorm";
                case 96: return "Thunderstorm with slight hail";
                case 99: return "Thunderstorm with heavy hail";
                default: return $"Unknown (Code: {weatherCode})";
            }
        }

        private void PopulateCharts(string jsonResponse)
        {
            try
            {
                JObject json = JObject.Parse(jsonResponse);
                JObject hourly = json["hourly"] as JObject;

                if (hourly == null) return;

                JArray timeArray = hourly["time"] as JArray;
                JArray tempArray = hourly["temperature_2m"] as JArray;
                JArray apparentTempArray = hourly["apparent_temperature"] as JArray;
                JArray precipProbArray = hourly["precipitation_probability"] as JArray;

                if (timeArray == null || tempArray == null) return;

                // Clear existing data
                chartTemperature.Series["Temperature"].Points.Clear();
                chartTemperature.Series["Apparent Temperature"].Points.Clear();
                chartTemperature.Series["Precipitation Probability"].Points.Clear();

                // Populate combined chart (show all 72 hours)
                for (int i = 0; i < timeArray.Count && i < tempArray.Count; i++)
                {
                    string time = timeArray[i].Value<string>();
                    double temp = tempArray[i].Value<double>();
                    double apparentTemp = apparentTempArray != null && i < apparentTempArray.Count
                        ? apparentTempArray[i].Value<double>()
                        : temp;
                    int precipProb = precipProbArray != null && i < precipProbArray.Count
                        ? precipProbArray[i].Value<int>()
                        : 0;

                    // Format time label (show date and hour)
                    DateTime dateTime = DateTime.Parse(time);
                    string timeLabel = dateTime.ToString("MM/dd HH:mm");

                    // Add temperature data (left Y-axis)
                    chartTemperature.Series["Temperature"].Points.AddXY(timeLabel, temp);
                    chartTemperature.Series["Apparent Temperature"].Points.AddXY(timeLabel, apparentTemp);
                    
                    // Add precipitation data (right Y-axis)
                    chartTemperature.Series["Precipitation Probability"].Points.AddXY(timeLabel, precipProb);
                }

                // Adjust X-axis interval for better readability (show every 6 hours)
                int pointCount = chartTemperature.Series["Temperature"].Points.Count;
                if (pointCount > 0)
                {
                    chartTemperature.ChartAreas[0].AxisX.Interval = Math.Max(1, pointCount / 12);
                }
            }
            catch (Exception ex)
            {
                // Silently fail chart population - text view will still work
                System.Diagnostics.Debug.WriteLine($"Error populating charts: {ex.Message}");
            }
        }

        private void PopulateHourlyForecastChart(string jsonResponse)
        {
            try
            {
                JObject json = JObject.Parse(jsonResponse);
                JObject hourly = json["hourly"] as JObject;

                if (hourly == null) return;

                JArray timeArray = hourly["time"] as JArray;
                JArray tempArray = hourly["temperature_2m"] as JArray;
                JArray apparentTempArray = hourly["apparent_temperature"] as JArray;

                if (timeArray == null || tempArray == null) return;

                // Clear existing data
                chartHourlyForecast.Series["Temperature"].Points.Clear();
                chartHourlyForecast.Series["Apparent Temperature"].Points.Clear();

                // Populate hourly forecast chart (show next 24 hours)
                int hoursToShow = Math.Min(24, timeArray.Count);

                for (int i = 0; i < hoursToShow && i < tempArray.Count; i++)
                {
                    string time = timeArray[i].Value<string>();
                    double temp = tempArray[i].Value<double>();
                    double apparentTemp = apparentTempArray != null && i < apparentTempArray.Count
                        ? apparentTempArray[i].Value<double>()
                        : temp;

                    // Format time label (show only time part for better readability)
                    DateTime dateTime = DateTime.Parse(time);
                    string timeLabel = dateTime.ToString("HH:mm");

                    // Add temperature data as spline (normal orientation)
                    chartHourlyForecast.Series["Temperature"].Points.AddXY(timeLabel, temp);
                    chartHourlyForecast.Series["Apparent Temperature"].Points.AddXY(timeLabel, apparentTemp);
                }

                // Adjust X-axis interval for better readability
                int pointCount = chartHourlyForecast.Series["Temperature"].Points.Count;
                if (pointCount > 0)
                {
                    chartHourlyForecast.ChartAreas[0].AxisX.Interval = Math.Max(1, pointCount / 8);
                }
            }
            catch (Exception ex)
            {
                // Silently fail chart population
                System.Diagnostics.Debug.WriteLine($"Error populating hourly forecast chart: {ex.Message}");
            }
        }

        private async Task UpdateLocationName(double latitude, double longitude)
        {
            try
            {
                // Use OpenStreetMap Nominatim API for reverse geocoding (free, no API key required)
                string geocodeUrl = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude}&lon={longitude}&addressdetails=1";
                
                using (HttpClient geocodeClient = new HttpClient())
                {
                    // Nominatim requires a User-Agent header
                    geocodeClient.DefaultRequestHeaders.Add("User-Agent", "WeatherApp/1.0");
                    geocodeClient.Timeout = TimeSpan.FromSeconds(10);
                    
                    HttpResponseMessage response = await geocodeClient.GetAsync(geocodeUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject json = JObject.Parse(jsonResponse);
                        
                        // Try to get city name from address
                        string cityName = "Unknown Location";
                        if (json["address"] != null)
                        {
                            JObject address = json["address"] as JObject;
                            
                            // Try different address fields in order of preference
                            if (address["city"] != null)
                                cityName = address["city"].Value<string>();
                            else if (address["town"] != null)
                                cityName = address["town"].Value<string>();
                            else if (address["village"] != null)
                                cityName = address["village"].Value<string>();
                            else if (address["municipality"] != null)
                                cityName = address["municipality"].Value<string>();
                            else if (address["county"] != null)
                                cityName = address["county"].Value<string>();
                            else if (address["state"] != null)
                                cityName = address["state"].Value<string>();
                            else if (address["display_name"] != null)
                            {
                                // Fallback: use display_name and extract first part
                                string displayName = json["display_name"].Value<string>();
                                if (!string.IsNullOrEmpty(displayName))
                                {
                                    string[] parts = displayName.Split(',');
                                    cityName = parts[0].Trim();
                                }
                            }
                        }
                        
                        lblLocation.Text = cityName;
                    }
                }
            }
            catch (Exception ex)
            {
                // If geocoding fails, just show coordinates
                lblLocation.Text = $"{latitude:F2}°N, {longitude:F2}°E";
                System.Diagnostics.Debug.WriteLine($"Geocoding error: {ex.Message}");
            }
        }

        private void UpdateCurrentTemperature(string jsonResponse)
        {
            try
            {
                JObject json = JObject.Parse(jsonResponse);
                JObject hourly = json["hourly"] as JObject;

                if (hourly != null && hourly["temperature_2m"] != null)
                {
                    JArray tempArray = hourly["temperature_2m"] as JArray;
                    if (tempArray != null && tempArray.Count > 0)
                    {
                        // Get the first (current) temperature
                        double currentTemp = tempArray[0].Value<double>();
                        lblTemp.Text = $"{currentTemp:F0}°C";
                    }
                }
            }
            catch (Exception ex)
            {
                lblTemp.Text = "--";
                System.Diagnostics.Debug.WriteLine($"Error updating temperature: {ex.Message}");
            }
        }

        private void UpdateStatus(string status)
        {
            lblStatus.Text = $"Status: {status}";
            Application.DoEvents();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(latitude, longitude, colorScheme);
            if (settingsForm.ShowDialog(this) == DialogResult.OK)
            {
                latitude = settingsForm.Latitude;
                longitude = settingsForm.Longitude;
                
                // Apply new color scheme if changed
                if (settingsForm.ColorScheme != colorScheme)
                {
                    colorScheme = settingsForm.ColorScheme;
                    ApplyColorScheme();
                    
                    // Save color scheme preference
                    try
                    {
                        Properties.Settings.Default.ColorScheme = colorScheme;
                        Properties.Settings.Default.Save();
                    }
                    catch { }
                }
                
                UpdateStatus("Settings saved. Click Refresh to update weather data.");
            }
        }

        private void ApplyColorScheme()
        {
            weather_app.ColorScheme scheme = colorScheme == "Dark" ? weather_app.ColorScheme.DarkMode() : weather_app.ColorScheme.LightMode();
            
            // Apply to form
            this.BackColor = scheme.FormBackColor;
            this.ForeColor = scheme.FormForeColor;
            
            // Apply to labels
            lblStatus.ForeColor = scheme.LabelForeColor;
            lblLocation.ForeColor = scheme.LabelForeColor;
            lblTemp.ForeColor = scheme.LabelForeColor;
            
            // Apply to text boxes
            txtWeatherInfo.BackColor = scheme.RichTextBoxBackColor;
            txtWeatherInfo.ForeColor = scheme.RichTextBoxForeColor;
            
            // Apply to buttons
            btnRefresh.BackColor = scheme.ButtonBackColor;
            btnRefresh.ForeColor = scheme.ButtonForeColor;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.FlatAppearance.BorderColor = scheme.ButtonBorderColor;
            btnRefresh.FlatAppearance.BorderSize = 1;
            
            // Apply to tab control
            tabControl.BackColor = scheme.TabControlBackColor;
            tabControl.ForeColor = scheme.TabControlForeColor;
            tabCharts.BackColor = scheme.TabPageBackColor;
            tabTextView.BackColor = scheme.TabPageBackColor;
            
            // Handle TabControl border with Paint event
            tabControl.Paint -= TabControl_Paint;
            tabControl.Paint += TabControl_Paint;
            tabControl.Invalidate(); // Force redraw to show border changes
            
            // Apply to menu strip
            menuStrip1.BackColor = scheme.MenuStripBackColor;
            menuStrip1.ForeColor = scheme.MenuStripForeColor;
            settingsToolStripMenuItem.ForeColor = scheme.MenuStripForeColor;
            infoToolStripMenuItem.ForeColor = scheme.MenuStripForeColor;
            
            // Apply to charts
            ApplyChartColors(chartTemperature, scheme);
            ApplyChartColors(chartHourlyForecast, scheme);
        }

        private void ApplyChartColors(Chart chart, weather_app.ColorScheme scheme)
        {
            chart.BackColor = scheme.ChartBackColor;
            chart.ForeColor = scheme.ChartForeColor;
            
            foreach (ChartArea area in chart.ChartAreas)
            {
                area.BackColor = scheme.ChartAreaBackColor;
                area.AxisX.LineColor = scheme.ChartForeColor;
                area.AxisY.LineColor = scheme.ChartForeColor;
                area.AxisX.TitleForeColor = scheme.ChartForeColor;
                area.AxisY.TitleForeColor = scheme.ChartForeColor;
                area.AxisX.LabelStyle.ForeColor = scheme.ChartForeColor;
                area.AxisY.LabelStyle.ForeColor = scheme.ChartForeColor;
                area.AxisX.MajorGrid.LineColor = scheme.ChartGridColor;
                area.AxisY.MajorGrid.LineColor = scheme.ChartGridColor;
                
                if (area.AxisY2.Enabled == AxisEnabled.True)
                {
                    area.AxisY2.LineColor = scheme.ChartForeColor;
                    area.AxisY2.TitleForeColor = scheme.ChartForeColor;
                    area.AxisY2.LabelStyle.ForeColor = scheme.ChartForeColor;
                }
            }
            
            foreach (Legend legend in chart.Legends)
            {
                legend.BackColor = scheme.ChartBackColor;
                legend.ForeColor = scheme.ChartForeColor;
            }
            
            foreach (Title title in chart.Titles)
            {
                title.ForeColor = scheme.ChartForeColor;
            }
        }

        private void TabControl_Paint(object sender, PaintEventArgs e)
        {
            TabControl tc = sender as TabControl;
            if (tc == null) return;
            
            weather_app.ColorScheme scheme = colorScheme == "Dark" ? weather_app.ColorScheme.DarkMode() : weather_app.ColorScheme.LightMode();
            using (Pen borderPen = new Pen(scheme.TabControlBorderColor, 1))
            {
                Rectangle rect = tc.ClientRectangle;
                // Draw border around the entire tab control
                e.Graphics.DrawRectangle(borderPen, 0, 0, rect.Width - 1, rect.Height - 1);
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by M.S.\nUsing Open-Meteo and OpenStreetMap", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            httpClient?.Dispose();
            base.OnFormClosing(e);
        }

    }
}
