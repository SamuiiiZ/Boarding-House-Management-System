using System.Drawing;
using System.Windows.Forms;

namespace BoardingHouseSys.UI
{
    public static class UITheme
    {
        // Color Palette
        public static Color PrimaryColor = Color.FromArgb(0, 123, 255);      // Blue
        public static Color SecondaryColor = Color.FromArgb(108, 117, 125);   // Gray
        public static Color SuccessColor = Color.FromArgb(40, 167, 69);       // Green
        public static Color DangerColor = Color.FromArgb(220, 53, 69);        // Red
        public static Color WarningColor = Color.FromArgb(255, 193, 7);        // Yellow
        public static Color InfoColor = Color.FromArgb(23, 162, 184);          // Teal
        public static Color LightColor = Color.FromArgb(248, 249, 250);       // Light Gray
        public static Color DarkColor = Color.FromArgb(52, 58, 64);           // Dark Gray
        
        // Fonts
        public static Font TitleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        public static Font HeaderFont = new Font("Segoe UI", 14, FontStyle.Bold);
        public static Font NormalFont = new Font("Segoe UI", 11);
        public static Font SmallFont = new Font("Segoe UI", 9);
        
        // Button Styles
        public static void ApplyButtonStyle(Button button, Color bgColor, int width = 120, int height = 40)
        {
            button.BackColor = bgColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.ForeColor = Color.White;
            button.Font = NormalFont;
            button.Size = new Size(width, height);
            button.UseVisualStyleBackColor = false;
            
            // Hover effects
            button.MouseEnter += (s, e) => 
            {
                button.BackColor = ControlPaint.Light(bgColor, 0.1f);
            };
            
            button.MouseLeave += (s, e) => 
            {
                button.BackColor = bgColor;
            };
        }
        
        public static void ApplyPrimaryButton(Button button, int width = 120, int height = 40)
        {
            ApplyButtonStyle(button, PrimaryColor, width, height);
        }
        
        public static void ApplySecondaryButton(Button button, int width = 120, int height = 40)
        {
            ApplyButtonStyle(button, SecondaryColor, width, height);
        }
        
        public static void ApplySuccessButton(Button button, int width = 120, int height = 40)
        {
            ApplyButtonStyle(button, SuccessColor, width, height);
        }
        
        public static void ApplyDangerButton(Button button, int width = 120, int height = 40)
        {
            ApplyButtonStyle(button, DangerColor, width, height);
        }
        
        // TextBox Styles
        public static void ApplyTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = Color.White;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = NormalFont;
            textBox.ForeColor = DarkColor;
        }
        
        // Label Styles
        public static void ApplyLabelStyle(Label label, bool isBold = false)
        {
            label.Font = isBold ? new Font(NormalFont, FontStyle.Bold) : NormalFont;
            label.ForeColor = DarkColor;
        }
        
        public static void ApplyHeaderLabelStyle(Label label)
        {
            label.Font = HeaderFont;
            label.ForeColor = PrimaryColor;
        }
        
        // Form Styles
        public static void ApplyFormStyle(Form form)
        {
            form.BackColor = Color.White;
            form.Font = NormalFont;
            form.StartPosition = FormStartPosition.CenterScreen;
        }
        
        // Panel Styles
        public static void ApplyPanelStyle(Panel panel, Color? backgroundColor = null)
        {
            panel.BackColor = backgroundColor ?? LightColor;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Padding = new Padding(10);
        }
        
        // DataGridView Styles
        public static void ApplyDataGridViewStyle(DataGridView gridView)
        {
            gridView.BackgroundColor = LightColor;
            gridView.BorderStyle = BorderStyle.None;
            gridView.EnableHeadersVisualStyles = false;
            gridView.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            gridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gridView.ColumnHeadersDefaultCellStyle.Font = new Font(NormalFont, FontStyle.Bold);
            gridView.RowHeadersVisible = false;
            gridView.DefaultCellStyle.Font = NormalFont;
            gridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            gridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}