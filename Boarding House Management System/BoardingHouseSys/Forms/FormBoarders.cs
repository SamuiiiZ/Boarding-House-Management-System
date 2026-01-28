using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;
using System.Collections.Generic;

namespace BoardingHouseSys.Forms
{
    public class FormBoarders : Form
    {
        private BoarderRepository _boarderRepo;
        private RoomRepository _roomRepo;
        private UserRepository _userRepo;
        
        private DataGridView dgvBoarders;
        
        // Input Fields
        private TextBox txtFullName;
        private TextBox txtAddress;
        private TextBox txtPhone;
        private ComboBox cmbRooms;
        
        // Account Fields
        private TextBox txtUsername;
        private TextBox txtPassword;
        private CheckBox chkCreateAccount;
        private GroupBox grpAccount;

        private Button btnAdd;
        private Button btnRefresh;

        public FormBoarders(User user)
        {
            InitializeComponent();
            _boarderRepo = new BoarderRepository();
            _roomRepo = new RoomRepository();
            _userRepo = new UserRepository();
            
            this.Load += (s, e) => {
                LoadBoarders();
                LoadRooms();
            };
        }

        private void InitializeComponent()
        {
            this.Size = new Size(1000, 700);
            this.Text = "Manage Boarders";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Main Layout Panel
            int padding = 20;
            int inputGap = 40;
            int labelW = 100;
            int inputW = 220;

            // --- 1. Boarder Details Group ---
            GroupBox grpDetails = new GroupBox
            {
                Text = "Boarder Information",
                Location = new Point(padding, padding),
                Size = new Size(350, 220)
            };
            this.Controls.Add(grpDetails);

            // Full Name
            CreateLabel(grpDetails, "Full Name:", 20, 30);
            txtFullName = CreateTextBox(grpDetails, 20 + labelW, 30, inputW);

            // Address
            CreateLabel(grpDetails, "Address:", 20, 30 + inputGap);
            txtAddress = CreateTextBox(grpDetails, 20 + labelW, 30 + inputGap, inputW);

            // Phone
            CreateLabel(grpDetails, "Phone:", 20, 30 + inputGap * 2);
            txtPhone = CreateTextBox(grpDetails, 20 + labelW, 30 + inputGap * 2, inputW);

            // Room
            CreateLabel(grpDetails, "Assign Room:", 20, 30 + inputGap * 3);
            cmbRooms = new ComboBox
            {
                Location = new Point(20 + labelW, 30 + inputGap * 3),
                Size = new Size(inputW, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            grpDetails.Controls.Add(cmbRooms);

            // --- 2. User Account Group ---
            grpAccount = new GroupBox
            {
                Text = "User Account (Login Access)",
                Location = new Point(grpDetails.Right + padding, padding),
                Size = new Size(350, 160)
            };
            this.Controls.Add(grpAccount);

            chkCreateAccount = new CheckBox
            {
                Text = "Create Login Account?",
                Location = new Point(20, 30),
                AutoSize = true,
                Checked = true
            };
            chkCreateAccount.CheckedChanged += (s, e) => ToggleAccountFields(chkCreateAccount.Checked);
            grpAccount.Controls.Add(chkCreateAccount);

            CreateLabel(grpAccount, "Username:", 20, 70);
            txtUsername = CreateTextBox(grpAccount, 100, 70, 200);

            CreateLabel(grpAccount, "Password:", 20, 110);
            txtPassword = CreateTextBox(grpAccount, 100, 110, 200);
            txtPassword.PasswordChar = '*';

            // --- 3. Action Buttons ---
            // Placed below the Account Group
            btnAdd = new Button
            {
                Text = "Register Boarder",
                Location = new Point(grpAccount.Left, grpAccount.Bottom + 10),
                Size = new Size(160, 40),
                BackColor = Color.LightGreen
            };
            btnAdd.Click += (s, e) => AddBoarder();
            this.Controls.Add(btnAdd);

            btnRefresh = new Button
            {
                Text = "Refresh List",
                Location = new Point(btnAdd.Right + 10, grpAccount.Bottom + 10),
                Size = new Size(160, 40)
            };
            btnRefresh.Click += (s, e) => LoadBoarders();
            this.Controls.Add(btnRefresh);

            // --- 4. Data Grid ---
            int gridY = grpDetails.Bottom + padding + 40; // Spacing below inputs
            dgvBoarders = new DataGridView
            {
                Location = new Point(padding, gridY),
                Size = new Size(this.ClientSize.Width - (padding * 2), this.ClientSize.Height - gridY - padding),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White
            };
            this.Controls.Add(dgvBoarders);
        }

        private void CreateLabel(Control parent, string text, int x, int y)
        {
            Label lbl = new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true };
            parent.Controls.Add(lbl);
        }

        private TextBox CreateTextBox(Control parent, int x, int y, int w)
        {
            TextBox txt = new TextBox { Location = new Point(x, y), Size = new Size(w, 25) };
            parent.Controls.Add(txt);
            return txt;
        }

        private void ToggleAccountFields(bool enabled)
        {
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
        }

        private void LoadRooms()
        {
            try 
            {
                var dt = _roomRepo.GetAllRooms();
                cmbRooms.DisplayMember = "RoomNumber";
                cmbRooms.ValueMember = "Id";
                cmbRooms.DataSource = dt;
                cmbRooms.SelectedIndex = -1; // Default empty
            }
            catch { }
        }

        private void LoadBoarders()
        {
            try
            {
                dgvBoarders.DataSource = _boarderRepo.GetAllBoarders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading boarders: " + ex.Message);
            }
        }

        private void AddBoarder()
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required.");
                return;
            }

            int? newUserId = null;

            try
            {
                // 1. Create User Account if checked
                if (chkCreateAccount.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        MessageBox.Show("Username and Password are required for account creation.");
                        return;
                    }

                    var newUser = new User
                    {
                        Username = txtUsername.Text,
                        Role = "Boarder"
                    };
                    
                    // CreateUser returns the new ID
                    newUserId = _userRepo.CreateUser(newUser, txtPassword.Text);
                }

                // 2. Create Boarder Profile
                var boarder = new Boarder
                {
                    FullName = txtFullName.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text,
                    RoomId = cmbRooms.SelectedValue != null ? (int?)Convert.ToInt32(cmbRooms.SelectedValue) : null,
                    UserId = newUserId
                };

                _boarderRepo.AddBoarder(boarder);

                MessageBox.Show("Boarder registered successfully!");
                LoadBoarders();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ClearInputs()
        {
            txtFullName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRooms.SelectedIndex = -1;
        }
    }
}
