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
        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private Button btnLogin = null!;
        private Button btnExit = null!;
        private Label lblUsername = null!;
        private Label lblPassword = null!;
        private Label label1 = null!;
        private Label lblTitle = null!;

        public FormLogin()
        {
            InitializeComponent();
            _userRepository = new UserRepository();

            // Event Handlers
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
        }

        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnExit = new Button();
            lblUsername = new Label();
            lblPassword = new Label();
            lblTitle = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(477, 150);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(252, 31);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(477, 190);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(252, 31);
            txtPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(477, 230);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 40);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            // 
            // btnExit
            // 
            btnExit.Location = new Point(580, 230);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 40);
            btnExit.TabIndex = 6;
            btnExit.Text = "Exit";
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(377, 150);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 23);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(377, 190);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password:";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(377, 87);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(512, 38);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Boarding House Management System";
            lblTitle.Click += lblTitle_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(377, 49);
            label1.Name = "label1";
            label1.Size = new Size(66, 38);
            label1.TabIndex = 7;
            label1.Text = "RJK";
            // 
            // FormLogin
            // 
            ClientSize = new Size(1028, 514);
            Controls.Add(label1);
            Controls.Add(lblTitle);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnExit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        private void btnLogin_Click(object? sender, EventArgs e)
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
                User? user = _userRepository.Authenticate(username, password);
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

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblTitle_Click(object? sender, EventArgs e)
        {

        }
    }
}
