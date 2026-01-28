using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormLogin : Form
    {
        private UserRepository _userRepository;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblTitle;

        public FormLogin()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private void InitializeComponent()
        {
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnExit = new Button();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblTitle = new Label();
            this.SuspendLayout();

            // Title
            this.lblTitle.Text = "Boarding House Management";
            this.lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(50, 20);

            // Username
            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new Point(50, 80);
            this.txtUsername.Location = new Point(150, 80);
            this.txtUsername.Size = new Size(200, 25);

            // Password
            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new Point(50, 120);
            this.txtPassword.Location = new Point(150, 120);
            this.txtPassword.Size = new Size(200, 25);
            this.txtPassword.PasswordChar = '*';

            // Buttons
            this.btnLogin.Text = "Login";
            this.btnLogin.Location = new Point(150, 160);
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            this.btnExit.Text = "Exit";
            this.btnExit.Location = new Point(250, 160);
            this.btnExit.Click += new EventHandler(this.btnExit_Click);

            // Form
            this.ClientSize = new Size(420, 250);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);
            this.Text = "Login";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try 
            {
                User user = _userRepository.Authenticate(username, password);
                if (user != null)
                {
                    // Hide login, show dashboard
                    this.Hide();
                    FormDashboard dashboard = new FormDashboard(user);
                    dashboard.FormClosed += (s, args) => 
                    {
                        // Ensure app exits if login is also closed or handle logout logic
                        // If logout clicked, show login. If X clicked, show login.
                        this.Show(); 
                        this.txtPassword.Clear();
                    };
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to database: " + ex.Message + "\n\nMake sure you have run the 'database_schema.sql' script and updated the connection string in Data/DatabaseHelper.cs.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
