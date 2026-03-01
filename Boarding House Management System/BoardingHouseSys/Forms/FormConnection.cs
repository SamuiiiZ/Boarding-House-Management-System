#nullable enable
#pragma warning disable CS8618
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using BoardingHouseSys.Data;
using BoardingHouseSys.UI;

namespace BoardingHouseSys.Forms
{
    public class FormConnection : Form
    {
        private Panel contentPanel;
        private TextBox txtServer;
        private TextBox txtDatabase;
        private TextBox txtUser;
        private TextBox txtPassword;
        private TextBox txtSuperUser;
        private TextBox txtSuperPass;
        private TextBox txtSuperPassConfirm;
        private Button btnSave;
        private Button btnTest;
        private Button btnCancel;
        private Button btnInstall;
        private RadioButton rbLocal;
        private RadioButton rbRemote;

        public FormConnection()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void InitializeComponent()
        {
            Text = "Database Connection Settings";
            StartPosition = FormStartPosition.CenterScreen;
            UITheme.ApplyFormStyle(this);
            WindowState = FormWindowState.Maximized;

            contentPanel = new Panel();
            contentPanel.Size = new Size(460, 540);
            contentPanel.BackColor = Color.White;
            contentPanel.Anchor = AnchorStyles.None;
            Controls.Add(contentPanel);
            Resize += (s, e) =>
            {
                contentPanel.Left = (ClientSize.Width - contentPanel.Width) / 2;
                contentPanel.Top = (ClientSize.Height - contentPanel.Height) / 2;
            };

            Label lblTitle = new Label();
            lblTitle.Text = "Connection Setup";
            UITheme.ApplyHeaderLabelStyle(lblTitle);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            contentPanel.Controls.Add(lblTitle);

            rbLocal = new RadioButton();
            rbLocal.Text = "Local (This PC)";
            rbLocal.Location = new Point(40, 70);
            rbLocal.Checked = true;
            rbLocal.CheckedChanged += RbType_CheckedChanged;
            contentPanel.Controls.Add(rbLocal);

            rbRemote = new RadioButton();
            rbRemote.Text = "Remote (Network)";
            rbRemote.Location = new Point(200, 70);
            rbRemote.CheckedChanged += RbType_CheckedChanged;
            contentPanel.Controls.Add(rbRemote);

            Label lblServer = new Label();
            lblServer.Text = "Server IP / Host:";
            UITheme.ApplyLabelStyle(lblServer);
            lblServer.Location = new Point(20, 110);
            contentPanel.Controls.Add(lblServer);

            txtServer = new TextBox();
            UITheme.ApplyTextBoxStyle(txtServer);
            txtServer.Location = new Point(180, 108);
            txtServer.Width = 240;
            txtServer.Text = "localhost";
            contentPanel.Controls.Add(txtServer);

            Label lblDb = new Label();
            lblDb.Text = "Database Name:";
            UITheme.ApplyLabelStyle(lblDb);
            lblDb.Location = new Point(20, 150);
            contentPanel.Controls.Add(lblDb);

            txtDatabase = new TextBox();
            UITheme.ApplyTextBoxStyle(txtDatabase);
            txtDatabase.Location = new Point(180, 148);
            txtDatabase.Width = 240;
            txtDatabase.Text = "BoardingHouseDB";
            contentPanel.Controls.Add(txtDatabase);

            Label lblUser = new Label();
            lblUser.Text = "DB Username:";
            UITheme.ApplyLabelStyle(lblUser);
            lblUser.Location = new Point(20, 190);
            contentPanel.Controls.Add(lblUser);

            txtUser = new TextBox();
            UITheme.ApplyTextBoxStyle(txtUser);
            txtUser.Location = new Point(180, 188);
            txtUser.Width = 240;
            txtUser.Text = "root";
            contentPanel.Controls.Add(txtUser);

            Label lblPass = new Label();
            lblPass.Text = "DB Password:";
            UITheme.ApplyLabelStyle(lblPass);
            lblPass.Location = new Point(20, 230);
            contentPanel.Controls.Add(lblPass);

            txtPassword = new TextBox();
            UITheme.ApplyTextBoxStyle(txtPassword);
            txtPassword.Location = new Point(180, 228);
            txtPassword.Width = 240;
            txtPassword.PasswordChar = '*';
            txtPassword.Text = "root";
            contentPanel.Controls.Add(txtPassword);

            Label lblSuperUser = new Label();
            lblSuperUser.Text = "Super Admin Username:";
            UITheme.ApplyLabelStyle(lblSuperUser);
            lblSuperUser.Location = new Point(20, 270);
            contentPanel.Controls.Add(lblSuperUser);

            txtSuperUser = new TextBox();
            UITheme.ApplyTextBoxStyle(txtSuperUser);
            txtSuperUser.Location = new Point(180, 268);
            txtSuperUser.Width = 240;
            txtSuperUser.Text = "superadmin";
            contentPanel.Controls.Add(txtSuperUser);

            Label lblSuperPass = new Label();
            lblSuperPass.Text = "Super Admin Password:";
            UITheme.ApplyLabelStyle(lblSuperPass);
            lblSuperPass.Location = new Point(20, 310);
            contentPanel.Controls.Add(lblSuperPass);

            txtSuperPass = new TextBox();
            UITheme.ApplyTextBoxStyle(txtSuperPass);
            txtSuperPass.Location = new Point(180, 308);
            txtSuperPass.Width = 240;
            txtSuperPass.PasswordChar = '*';
            contentPanel.Controls.Add(txtSuperPass);

            Label lblSuperPassConfirm = new Label();
            lblSuperPassConfirm.Text = "Confirm Password:";
            UITheme.ApplyLabelStyle(lblSuperPassConfirm);
            lblSuperPassConfirm.Location = new Point(20, 350);
            contentPanel.Controls.Add(lblSuperPassConfirm);

            txtSuperPassConfirm = new TextBox();
            UITheme.ApplyTextBoxStyle(txtSuperPassConfirm);
            txtSuperPassConfirm.Location = new Point(180, 348);
            txtSuperPassConfirm.Width = 240;
            txtSuperPassConfirm.PasswordChar = '*';
            contentPanel.Controls.Add(txtSuperPassConfirm);

            btnTest = new Button();
            btnTest.Text = "Test Connection";
            UITheme.ApplySecondaryButton(btnTest, 160, 38);
            btnTest.Location = new Point(20, 395);
            btnTest.Click += BtnTest_Click;
            contentPanel.Controls.Add(btnTest);

            btnSave = new Button();
            btnSave.Text = "Save && Apply";
            UITheme.ApplyPrimaryButton(btnSave, 160, 38);
            btnSave.Location = new Point(260, 395);
            btnSave.Click += BtnSave_Click;
            contentPanel.Controls.Add(btnSave);

            btnInstall = new Button();
            btnInstall.Text = "Install DB + SuperAdmin";
            UITheme.ApplyPrimaryButton(btnInstall, 260, 40);
            btnInstall.Location = new Point(100, 440);
            btnInstall.Click += BtnInstall_Click;
            contentPanel.Controls.Add(btnInstall);

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            UITheme.ApplyDangerButton(btnCancel, 120, 36);
            btnCancel.Location = new Point(170, 485);
            btnCancel.Click += BtnCancel_Click;
            contentPanel.Controls.Add(btnCancel);

            contentPanel.Left = (ClientSize.Width - contentPanel.Width) / 2;
            contentPanel.Top = (ClientSize.Height - contentPanel.Height) / 2;
        }

        private void BtnCancel_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void RbType_CheckedChanged(object? sender, EventArgs e)
        {
            if (rbLocal.Checked)
            {
                txtServer.Text = "localhost";
                txtServer.Enabled = false;
            }
            else
            {
                txtServer.Enabled = true;
                if (txtServer.Text == "localhost") txtServer.Text = "";
            }
        }

        private void LoadSettings()
        {
            string path = AppConfig.GetConfigPath();
            if (File.Exists(path))
            {
                try
                {
                    string[] lines = File.ReadAllLines(path);
                    if (lines.Length >= 4)
                    {
                        txtServer.Text = lines[0].Split('=')[1];
                        txtDatabase.Text = lines[1].Split('=')[1];
                        txtUser.Text = lines[2].Split('=')[1];
                        txtPassword.Text = lines[3].Split('=')[1];
                        
                        if (txtServer.Text.ToLower() == "localhost")
                            rbLocal.Checked = true;
                        else
                            rbRemote.Checked = true;
                    }
                }
                catch { }
            }
        }

        private string BuildConnectionString()
        {
            return $"Server={txtServer.Text};Database={txtDatabase.Text};Uid={txtUser.Text};Pwd={txtPassword.Text};";
        }

        private string BuildBaseConnection()
        {
            return $"Server={txtServer.Text};Uid={txtUser.Text};Pwd={txtPassword.Text};";
        }

        private void BtnTest_Click(object? sender, EventArgs e)
        {
            string connStr = BuildConnectionString();
            try
            {
                DatabaseHelper tempHelper = new DatabaseHelper(connStr);
                // Try simple query
                tempHelper.ExecuteScalar("SELECT 1");
                MessageBox.Show("Connection Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                string[] lines = new string[]
                {
                    $"Server={txtServer.Text}",
                    $"Database={txtDatabase.Text}",
                    $"Uid={txtUser.Text}",
                    $"Pwd={txtPassword.Text}"
                };
                string path = AppConfig.GetConfigPath();
                File.WriteAllLines(path, lines);

                DatabaseHelper.SetConnectionString(BuildConnectionString());

                MessageBox.Show("Settings Saved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving settings: " + ex.Message);
            }
        }

        private void BtnInstall_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSuperUser.Text) || string.IsNullOrWhiteSpace(txtSuperPass.Text))
            {
                MessageBox.Show("Super Admin username and password are required.");
                return;
            }

            if (txtSuperPass.Text != txtSuperPassConfirm.Text)
            {
                MessageBox.Show("Super Admin passwords do not match.");
                return;
            }

            try
            {
                string baseConn = BuildBaseConnection();
                string dbName = txtDatabase.Text.Trim();

                DatabaseBootstrap.Install(baseConn, dbName, txtSuperUser.Text.Trim(), txtSuperPass.Text.Trim());

                string[] lines = new string[]
                {
                    $"Server={txtServer.Text}",
                    $"Database={txtDatabase.Text}",
                    $"Uid={txtUser.Text}",
                    $"Pwd={txtPassword.Text}"
                };
                string path = AppConfig.GetConfigPath();
                File.WriteAllLines(path, lines);

                DatabaseHelper.SetConnectionString(BuildConnectionString());

                MessageBox.Show("Installation complete. You can now log in with the Super Admin account.", "Installer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Installer failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
