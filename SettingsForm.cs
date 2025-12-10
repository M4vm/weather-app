using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace weather_app
{
    public partial class SettingsForm : Form
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string ColorScheme { get; private set; }

        public SettingsForm(double latitude, double longitude, string currentColorScheme)
        {
            InitializeComponent();
            Latitude = latitude;
            Longitude = longitude;
            ColorScheme = currentColorScheme;
            txtLatitude.Text = latitude.ToString();
            txtLongitude.Text = longitude.ToString();
            
            // Set the color scheme radio buttons
            if (currentColorScheme == "Dark")
                rbDarkMode.Checked = true;
            else
                rbLightMode.Checked = true;
            
            // Apply color scheme to settings form
            ApplyColorScheme(currentColorScheme);
        }

        private void ApplyColorScheme(string schemeName)
        {
            weather_app.ColorScheme scheme = schemeName == "Dark" ? weather_app.ColorScheme.DarkMode() : weather_app.ColorScheme.LightMode();
            
            this.BackColor = scheme.FormBackColor;
            this.ForeColor = scheme.FormForeColor;
            
            lblLat.ForeColor = scheme.LabelForeColor;
            lblLon.ForeColor = scheme.LabelForeColor;
            lblColorScheme.ForeColor = scheme.LabelForeColor;
            
            txtLatitude.BackColor = scheme.TextBoxBackColor;
            txtLatitude.ForeColor = scheme.TextBoxForeColor;
            txtLongitude.BackColor = scheme.TextBoxBackColor;
            txtLongitude.ForeColor = scheme.TextBoxForeColor;
            
            btnSave.BackColor = scheme.ButtonBackColor;
            btnSave.ForeColor = scheme.ButtonForeColor;
            btnCancel.BackColor = scheme.ButtonBackColor;
            btnCancel.ForeColor = scheme.ButtonForeColor;
            
            rbLightMode.ForeColor = scheme.LabelForeColor;
            rbDarkMode.ForeColor = scheme.LabelForeColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtLatitude.Text, out double lat) && 
                double.TryParse(txtLongitude.Text, out double lon))
            {
                Latitude = lat;
                Longitude = lon;
                ColorScheme = rbDarkMode.Checked ? "Dark" : "Light";
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values for latitude and longitude.", 
                    "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbColorScheme_CheckedChanged(object sender, EventArgs e)
        {
            // Preview color scheme when radio button changes
            if (rbDarkMode.Checked)
                ApplyColorScheme("Dark");
            else if (rbLightMode.Checked)
                ApplyColorScheme("Light");
        }
    }
}

