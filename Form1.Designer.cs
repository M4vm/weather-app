namespace weather_app
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRefresh = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCharts = new System.Windows.Forms.TabPage();
            this.chartTemperature = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartHourlyForecast = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabTextView = new System.Windows.Forms.TabPage();
            this.txtWeatherInfo = new System.Windows.Forms.RichTextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperature)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHourlyForecast)).BeginInit();
            this.tabTextView.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(618, 523);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(154, 26);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh Weather";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabCharts);
            this.tabControl.Controls.Add(this.tabTextView);
            this.tabControl.Location = new System.Drawing.Point(12, 142);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(760, 370);
            this.tabControl.TabIndex = 8;
            // 
            // tabCharts
            // 
            this.tabCharts.Controls.Add(this.chartTemperature);
            this.tabCharts.Controls.Add(this.chartHourlyForecast);
            this.tabCharts.Location = new System.Drawing.Point(4, 22);
            this.tabCharts.Name = "tabCharts";
            this.tabCharts.Padding = new System.Windows.Forms.Padding(3);
            this.tabCharts.Size = new System.Drawing.Size(752, 344);
            this.tabCharts.TabIndex = 0;
            this.tabCharts.Text = "Charts";
            this.tabCharts.UseVisualStyleBackColor = true;
            // 
            // chartTemperature
            // 
            this.chartTemperature.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.chartTemperature.Location = new System.Drawing.Point(6, 6);
            this.chartTemperature.Name = "chartTemperature";
            this.chartTemperature.Size = new System.Drawing.Size(367, 332);
            this.chartTemperature.TabIndex = 0;
            this.chartTemperature.Text = "Weather Chart";
            // 
            // chartHourlyForecast
            // 
            this.chartHourlyForecast.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartHourlyForecast.Location = new System.Drawing.Point(379, 6);
            this.chartHourlyForecast.Name = "chartHourlyForecast";
            this.chartHourlyForecast.Size = new System.Drawing.Size(367, 332);
            this.chartHourlyForecast.TabIndex = 1;
            this.chartHourlyForecast.Text = "Hourly Forecast Chart";
            // 
            // tabTextView
            // 
            this.tabTextView.Controls.Add(this.txtWeatherInfo);
            this.tabTextView.Location = new System.Drawing.Point(4, 22);
            this.tabTextView.Name = "tabTextView";
            this.tabTextView.Padding = new System.Windows.Forms.Padding(3);
            this.tabTextView.Size = new System.Drawing.Size(752, 344);
            this.tabTextView.TabIndex = 1;
            this.tabTextView.Text = "Text View";
            this.tabTextView.UseVisualStyleBackColor = true;
            // 
            // txtWeatherInfo
            // 
            this.txtWeatherInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWeatherInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeatherInfo.Location = new System.Drawing.Point(3, 3);
            this.txtWeatherInfo.Name = "txtWeatherInfo";
            this.txtWeatherInfo.ReadOnly = true;
            this.txtWeatherInfo.Size = new System.Drawing.Size(746, 338);
            this.txtWeatherInfo.TabIndex = 0;
            this.txtWeatherInfo.Text = "";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(19, 515);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status:";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(300, 48);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(37, 37);
            this.lblLocation.TabIndex = 9;
            this.lblLocation.Text = "~";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.Location = new System.Drawing.Point(345, 97);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(53, 55);
            this.lblTemp.TabIndex = 10;
            this.lblTemp.Text = "~";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.infoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRefresh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "Form1";
            this.Text = "Weather App";
            this.tabControl.ResumeLayout(false);
            this.tabCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartTemperature)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartHourlyForecast)).EndInit();
            this.tabTextView.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RichTextBox txtWeatherInfo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCharts;
        private System.Windows.Forms.TabPage tabTextView;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemperature;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHourlyForecast;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
    }
}

