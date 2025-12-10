namespace weather_app
{
    partial class SettingsForm
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
            this.lblLat = new System.Windows.Forms.Label();
            this.lblLon = new System.Windows.Forms.Label();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblColorScheme = new System.Windows.Forms.Label();
            this.rbLightMode = new System.Windows.Forms.RadioButton();
            this.rbDarkMode = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblLat
            // 
            this.lblLat.AutoSize = true;
            this.lblLat.Location = new System.Drawing.Point(12, 15);
            this.lblLat.Name = "lblLat";
            this.lblLat.Size = new System.Drawing.Size(48, 13);
            this.lblLat.TabIndex = 0;
            this.lblLat.Text = "Latitude:";
            // 
            // lblLon
            // 
            this.lblLon.AutoSize = true;
            this.lblLon.Location = new System.Drawing.Point(12, 41);
            this.lblLon.Name = "lblLon";
            this.lblLon.Size = new System.Drawing.Size(57, 13);
            this.lblLon.TabIndex = 1;
            this.lblLon.Text = "Longitude:";
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(75, 12);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(150, 20);
            this.txtLatitude.TabIndex = 2;
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(75, 38);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(150, 20);
            this.txtLongitude.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(69, 95);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(150, 95);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblColorScheme
            // 
            this.lblColorScheme.AutoSize = true;
            this.lblColorScheme.Location = new System.Drawing.Point(12, 67);
            this.lblColorScheme.Name = "lblColorScheme";
            this.lblColorScheme.Size = new System.Drawing.Size(75, 13);
            this.lblColorScheme.TabIndex = 6;
            this.lblColorScheme.Text = "Color Scheme:";
            // 
            // rbLightMode
            // 
            this.rbLightMode.AutoSize = true;
            this.rbLightMode.Checked = true;
            this.rbLightMode.Location = new System.Drawing.Point(93, 65);
            this.rbLightMode.Name = "rbLightMode";
            this.rbLightMode.Size = new System.Drawing.Size(48, 17);
            this.rbLightMode.TabIndex = 7;
            this.rbLightMode.TabStop = true;
            this.rbLightMode.Text = "Light";
            this.rbLightMode.UseVisualStyleBackColor = true;
            this.rbLightMode.CheckedChanged += new System.EventHandler(this.rbColorScheme_CheckedChanged);
            // 
            // rbDarkMode
            // 
            this.rbDarkMode.AutoSize = true;
            this.rbDarkMode.Location = new System.Drawing.Point(147, 65);
            this.rbDarkMode.Name = "rbDarkMode";
            this.rbDarkMode.Size = new System.Drawing.Size(48, 17);
            this.rbDarkMode.TabIndex = 8;
            this.rbDarkMode.Text = "Dark";
            this.rbDarkMode.UseVisualStyleBackColor = true;
            this.rbDarkMode.CheckedChanged += new System.EventHandler(this.rbColorScheme_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(237, 130);
            this.Controls.Add(this.rbDarkMode);
            this.Controls.Add(this.rbLightMode);
            this.Controls.Add(this.lblColorScheme);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtLongitude);
            this.Controls.Add(this.txtLatitude);
            this.Controls.Add(this.lblLon);
            this.Controls.Add(this.lblLat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblLat;
        private System.Windows.Forms.Label lblLon;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblColorScheme;
        private System.Windows.Forms.RadioButton rbLightMode;
        private System.Windows.Forms.RadioButton rbDarkMode;
    }
}

