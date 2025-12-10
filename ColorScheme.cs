using System;
using System.Drawing;

namespace weather_app
{
    public class ColorScheme
    {
        public string Name { get; set; }
        public Color FormBackColor { get; set; }
        public Color FormForeColor { get; set; }
        public Color LabelForeColor { get; set; }
        public Color TextBoxBackColor { get; set; }
        public Color TextBoxForeColor { get; set; }
        public Color RichTextBoxBackColor { get; set; }
        public Color RichTextBoxForeColor { get; set; }
        public Color ButtonBackColor { get; set; }
        public Color ButtonForeColor { get; set; }
        public Color ButtonBorderColor { get; set; }
        public Color TabControlBackColor { get; set; }
        public Color TabControlForeColor { get; set; }
        public Color TabControlBorderColor { get; set; }
        public Color TabPageBackColor { get; set; }
        public Color MenuStripBackColor { get; set; }
        public Color MenuStripForeColor { get; set; }
        public Color ChartBackColor { get; set; }
        public Color ChartForeColor { get; set; }
        public Color ChartAreaBackColor { get; set; }
        public Color ChartGridColor { get; set; }

        public static ColorScheme LightMode()
        {
            return new ColorScheme
            {
                Name = "Light",
                FormBackColor = SystemColors.Control,
                FormForeColor = SystemColors.ControlText,
                LabelForeColor = SystemColors.ControlText,
                TextBoxBackColor = Color.White,
                TextBoxForeColor = SystemColors.ControlText,
                RichTextBoxBackColor = Color.White,
                RichTextBoxForeColor = SystemColors.ControlText,
                ButtonBackColor = SystemColors.Control,
                ButtonForeColor = SystemColors.ControlText,
                ButtonBorderColor = SystemColors.ControlDark,
                TabControlBackColor = SystemColors.Control,
                TabControlForeColor = SystemColors.ControlText,
                TabControlBorderColor = SystemColors.ControlDark,
                TabPageBackColor = SystemColors.Control,
                MenuStripBackColor = SystemColors.Menu,
                MenuStripForeColor = SystemColors.MenuText,
                ChartBackColor = Color.White,
                ChartForeColor = SystemColors.ControlText,
                ChartAreaBackColor = Color.White,
                ChartGridColor = Color.LightGray
            };
        }

        public static ColorScheme DarkMode()
        {
            return new ColorScheme
            {
                Name = "Dark",
                FormBackColor = Color.FromArgb(32, 32, 32),
                FormForeColor = Color.White,
                LabelForeColor = Color.White,
                TextBoxBackColor = Color.FromArgb(45, 45, 45),
                TextBoxForeColor = Color.White,
                RichTextBoxBackColor = Color.FromArgb(45, 45, 45),
                RichTextBoxForeColor = Color.White,
                ButtonBackColor = Color.FromArgb(60, 60, 60),
                ButtonForeColor = Color.White,
                ButtonBorderColor = Color.FromArgb(100, 100, 100),
                TabControlBackColor = Color.FromArgb(32, 32, 32),
                TabControlForeColor = Color.White,
                TabControlBorderColor = Color.FromArgb(80, 80, 80),
                TabPageBackColor = Color.FromArgb(32, 32, 32),
                MenuStripBackColor = Color.FromArgb(45, 45, 45),
                MenuStripForeColor = Color.White,
                ChartBackColor = Color.FromArgb(45, 45, 45),
                ChartForeColor = Color.White,
                ChartAreaBackColor = Color.FromArgb(45, 45, 45),
                ChartGridColor = Color.FromArgb(80, 80, 80)
            };
        }
    }
}

