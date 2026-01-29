using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;
using System.Collections.Generic;
using System.Data;

namespace BoardingHouseSys.Forms
{
    public class FormBoarders : Form
    {
        private BoarderRepository _boarderRepo;
        private RoomRepository _roomRepo;
        private UserRepository _userRepo;
        private User _currentUser;

        private DataGridView dgvBoarders = null!;

        // Input Fields
        private TextBox txtFullName = null!;
        private TextBox txtAddress = null!;
        private TextBox txtPhone = null!;
        private ComboBox cmbRooms = null!;

        // Account Fields
        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private CheckBox chkCreateAccount = null!;
        private GroupBox grpAccount = null!;
        private GroupBox grpDetails = null!;

        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnRefresh = null!;
        private Button btnBack = null!;

        private Label lblFullName = null!;
        private Label lblAddress = null!;
        private Label lblPhone = null!;
        private Label lblRoom = null!;
        private Label lblUsername = null!;
        private Label lblPassword = null!;

        private int _selectedBoarderId = 0;

        public FormBoarders(User user)
        {
            InitializeComponent();
            _boarderRepo = new BoarderRepository();
            _roomRepo = new RoomRepository();
            _userRepo = new UserRepository();
            _currentUser = user;

            // Event Handlers
            this.chkCreateAccount.CheckedChanged += (s, e) => ToggleAccountFields(chkCreateAccount.Checked);
            this.btnAdd.Click += (s, e) => AddBoarder();
            this.btnUpdate.Click += (s, e) => UpdateBoarder();
            this.btnDelete.Click += (s, e) => DeleteBoarder();
            this.btnRefresh.Click += (s, e) => LoadBoarders();
            this.btnBack.Click += (s, e) => this.Close();
            this.dgvBoarders.CellClick += DgvBoarders_CellClick;

            this.Load += (s, e) =>
            {
                LoadRooms(); // Load rooms first
                LoadBoarders();
            };
        }

        private void InitializeComponent()
        {
            grpDetails = new GroupBox();
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblRoom = new Label();
            cmbRooms = new ComboBox();
            grpAccount = new GroupBox();
            chkCreateAccount = new CheckBox();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            dgvBoarders = new DataGridView();
            grpDetails.SuspendLayout();
            grpAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBoarders).BeginInit();
            SuspendLayout();
            // 
            // grpDetails
            // 
            grpDetails.Controls.Add(lblFullName);
            grpDetails.Controls.Add(txtFullName);
            grpDetails.Controls.Add(lblAddress);
            grpDetails.Controls.Add(txtAddress);
            grpDetails.Controls.Add(lblPhone);
            grpDetails.Controls.Add(txtPhone);
            grpDetails.Controls.Add(lblRoom);
            grpDetails.Controls.Add(cmbRooms);
            grpDetails.Location = new Point(6, 20);
            grpDetails.Name = "grpDetails";
            grpDetails.Size = new Size(385, 220);
            grpDetails.TabIndex = 0;
            grpDetails.TabStop = false;
            grpDetails.Text = "Boarder Information";
            grpDetails.Enter += grpDetails_Enter;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(20, 33);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(95, 25);
            lblFullName.TabIndex = 0;
            lblFullName.Text = "Full Name:";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(128, 30);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(220, 31);
            txtFullName.TabIndex = 1;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(20, 73);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(81, 25);
            lblAddress.TabIndex = 2;
            lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(128, 70);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(220, 31);
            txtAddress.TabIndex = 3;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(20, 113);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(66, 25);
            lblPhone.TabIndex = 4;
            lblPhone.Text = "Phone:";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(128, 110);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(220, 31);
            txtPhone.TabIndex = 5;
            // 
            // lblRoom
            // 
            lblRoom.AutoSize = true;
            lblRoom.Location = new Point(0, 158);
            lblRoom.Name = "lblRoom";
            lblRoom.Size = new Size(122, 25);
            lblRoom.TabIndex = 6;
            lblRoom.Text = "Assign Room:";
            // 
            // cmbRooms
            // 
            cmbRooms.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRooms.Location = new Point(128, 155);
            cmbRooms.Name = "cmbRooms";
            cmbRooms.Size = new Size(220, 33);
            cmbRooms.TabIndex = 7;
            // 
            // grpAccount
            // 
            grpAccount.Controls.Add(chkCreateAccount);
            grpAccount.Controls.Add(lblUsername);
            grpAccount.Controls.Add(txtUsername);
            grpAccount.Controls.Add(lblPassword);
            grpAccount.Controls.Add(txtPassword);
            grpAccount.Location = new Point(429, 20);
            grpAccount.Name = "grpAccount";
            grpAccount.Size = new Size(350, 160);
            grpAccount.TabIndex = 1;
            grpAccount.TabStop = false;
            grpAccount.Text = "User Account (Login Access)";
            // 
            // chkCreateAccount
            // 
            chkCreateAccount.AutoSize = true;
            chkCreateAccount.Checked = true;
            chkCreateAccount.CheckState = CheckState.Checked;
            chkCreateAccount.Location = new Point(20, 30);
            chkCreateAccount.Name = "chkCreateAccount";
            chkCreateAccount.Size = new Size(215, 29);
            chkCreateAccount.TabIndex = 0;
            chkCreateAccount.Text = "Create Login Account?";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(20, 73);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(95, 25);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(121, 73);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 31);
            txtUsername.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(20, 113);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(91, 25);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(121, 110);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(200, 31);
            txtPassword.TabIndex = 4;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Location = new Point(419, 190);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(86, 40);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Register";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(511, 190);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 40);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Location = new Point(597, 190);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 40);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(683, 190);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 40);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(769, 190);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 40);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            // 
            // dgvBoarders
            // 
            dgvBoarders.AllowUserToAddRows = false;
            dgvBoarders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvBoarders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBoarders.BackgroundColor = Color.White;
            dgvBoarders.ColumnHeadersHeight = 34;
            dgvBoarders.Location = new Point(20, 300);
            dgvBoarders.Name = "dgvBoarders";
            dgvBoarders.ReadOnly = true;
            dgvBoarders.RowHeadersWidth = 62;
            dgvBoarders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBoarders.Size = new Size(1040, 340);
            dgvBoarders.TabIndex = 7;
            // 
            // FormBoarders
            // 
            ClientSize = new Size(1078, 644);
            Controls.Add(grpDetails);
            Controls.Add(grpAccount);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(btnBack);
            Controls.Add(dgvBoarders);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormBoarders";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manage Boarders";
            grpDetails.ResumeLayout(false);
            grpDetails.PerformLayout();
            grpAccount.ResumeLayout(false);
            grpAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBoarders).EndInit();
            ResumeLayout(false);
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

        private void DgvBoarders_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBoarders.Rows[e.RowIndex];
                _selectedBoarderId = Convert.ToInt32(row.Cells["Id"].Value);
                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();

                // Select Room
                if (row.Cells["RoomId"].Value != DBNull.Value)
                {
                    cmbRooms.SelectedValue = Convert.ToInt32(row.Cells["RoomId"].Value);
                }
                else
                {
                    cmbRooms.SelectedIndex = -1;
                }

                // Disable Account creation when editing
                chkCreateAccount.Checked = false;
                chkCreateAccount.Enabled = false;
                grpAccount.Enabled = false;
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

        private void UpdateBoarder()
        {
            if (_selectedBoarderId == 0)
            {
                MessageBox.Show("Please select a boarder to update.");
                return;
            }

            try
            {
                var boarder = new Boarder
                {
                    Id = _selectedBoarderId,
                    FullName = txtFullName.Text,
                    Address = txtAddress.Text,
                    Phone = txtPhone.Text,
                    RoomId = cmbRooms.SelectedValue != null ? (int?)Convert.ToInt32(cmbRooms.SelectedValue) : null
                };

                _boarderRepo.UpdateBoarder(boarder);
                MessageBox.Show("Boarder updated successfully!");
                LoadBoarders();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating boarder: " + ex.Message);
            }
        }

        private void DeleteBoarder()
        {
            if (_selectedBoarderId == 0)
            {
                MessageBox.Show("Please select a boarder to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this boarder?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _boarderRepo.DeleteBoarder(_selectedBoarderId);
                    MessageBox.Show("Boarder deleted successfully!");
                    LoadBoarders();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting boarder: " + ex.Message);
                }
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

            chkCreateAccount.Enabled = true;
            chkCreateAccount.Checked = true;
            grpAccount.Enabled = true;

            _selectedBoarderId = 0;
        }

        private void grpDetails_Enter(object? sender, EventArgs e)
        {

        }
    }
}