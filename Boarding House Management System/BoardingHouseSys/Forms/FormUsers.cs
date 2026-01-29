using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormUsers : Form
    {
        private UserRepository _repository;
        private User _currentUser;

        // Controls
        private DataGridView dgvUsers = null!;
        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private ComboBox cmbRole = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnRefresh = null!;
        private Button btnBack = null!;
        private GroupBox grpInput = null!;
        private Label lblUser = null!;
        private Label lblPass = null!;
        private Label lblRole = null!;
        private int _selectedUserId = 0;

        public FormUsers(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _repository = new UserRepository();

            // Populate Roles
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Boarder");

            // Only SuperAdmin can create other SuperAdmins
            if (_currentUser.Role == "SuperAdmin")
            {
                cmbRole.Items.Add("SuperAdmin");
            }
            cmbRole.SelectedIndex = 0;

            // Event Handlers
            this.btnAdd.Click += (s, e) => AddUser();
            this.btnUpdate.Click += (s, e) => UpdateUser();
            this.btnDelete.Click += (s, e) => DeleteUser();
            this.btnRefresh.Click += (s, e) => LoadUsers();
            this.btnBack.Click += (s, e) => this.Close();
            this.dgvUsers.CellClick += DgvUsers_CellClick;
            this.Load += (s, e) => LoadUsers();
        }

        private void InitializeComponent()
        {
            dgvUsers = new DataGridView();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            cmbRole = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            grpInput = new GroupBox();
            lblUser = new Label();
            lblPass = new Label();
            lblRole = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            grpInput.SuspendLayout();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.ColumnHeadersHeight = 34;
            dgvUsers.Location = new Point(340, 20);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 62;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(746, 599);
            dgvUsers.TabIndex = 1;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(20, 58);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(250, 31);
            txtUsername.TabIndex = 1;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(20, 143);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(250, 31);
            txtPassword.TabIndex = 3;
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Location = new Point(20, 252);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(250, 33);
            cmbRole.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Location = new Point(20, 292);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Create";
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(105, 292);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Location = new Point(190, 292);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(20, 332);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 30);
            btnRefresh.TabIndex = 9;
            btnRefresh.Text = "Refresh List";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(150, 332);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(120, 30);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            // 
            // grpInput
            // 
            grpInput.Controls.Add(lblUser);
            grpInput.Controls.Add(txtUsername);
            grpInput.Controls.Add(lblPass);
            grpInput.Controls.Add(txtPassword);
            grpInput.Controls.Add(lblRole);
            grpInput.Controls.Add(cmbRole);
            grpInput.Controls.Add(btnAdd);
            grpInput.Controls.Add(btnUpdate);
            grpInput.Controls.Add(btnDelete);
            grpInput.Controls.Add(btnRefresh);
            grpInput.Controls.Add(btnBack);
            grpInput.Location = new Point(20, 20);
            grpInput.Name = "grpInput";
            grpInput.Size = new Size(300, 414);
            grpInput.TabIndex = 0;
            grpInput.TabStop = false;
            grpInput.Text = "User Details";
            grpInput.Enter += grpInput_Enter;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(20, 30);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(95, 25);
            lblUser.TabIndex = 0;
            lblUser.Text = "Username:";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(-2, 115);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(302, 25);
            lblPass.TabIndex = 2;
            lblPass.Text = "Password (leave blank if unchanged):";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Location = new Point(20, 226);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(50, 25);
            lblRole.TabIndex = 4;
            lblRole.Text = "Role:";
            // 
            // FormUsers
            // 
            ClientSize = new Size(1104, 623);
            Controls.Add(grpInput);
            Controls.Add(dgvUsers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormUsers";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manage Users";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            grpInput.ResumeLayout(false);
            grpInput.PerformLayout();
            ResumeLayout(false);
        }

        private void DgvUsers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
                _selectedUserId = Convert.ToInt32(row.Cells["Id"].Value);
                txtUsername.Text = row.Cells["Username"].Value.ToString();

                string role = row.Cells["Role"].Value.ToString()!;
                if (cmbRole.Items.Contains(role))
                {
                    cmbRole.SelectedItem = role;
                }
                txtPassword.Clear(); // Don't show hash
            }
        }

        private void UpdateUser()
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Please select a user to update.");
                return;
            }

            try
            {
                var user = new User
                {
                    Id = _selectedUserId,
                    Username = txtUsername.Text.Trim(),
                    Role = cmbRole.SelectedItem?.ToString() ?? "Boarder"
                };

                // Pass password only if user typed something
                string? newPass = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text.Trim();

                _repository.UpdateUser(user, newPass);
                MessageBox.Show("User updated successfully!");
                LoadUsers();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message);
            }
        }

        private void DeleteUser()
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Please select a user to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _repository.DeleteUser(_selectedUserId);
                    MessageBox.Show("User deleted successfully!");
                    LoadUsers();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting user: " + ex.Message);
                }
            }
        }

        private void ClearFields()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = 0;
            _selectedUserId = 0;
        }

        private void LoadUsers()
        {
            try
            {
                var users = _repository.GetAllUsers();
                dgvUsers.DataSource = users;

                // Hide sensitive or internal columns
                if (dgvUsers.Columns["PasswordHash"] != null)
                    dgvUsers.Columns["PasswordHash"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }

        private void AddUser()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                var user = new User
                {
                    Username = txtUsername.Text.Trim(),
                    Role = cmbRole.SelectedItem?.ToString() ?? "Boarder"
                };

                _repository.CreateUser(user, txtPassword.Text.Trim());

                MessageBox.Show("User created successfully!");
                LoadUsers();
                txtUsername.Clear();
                txtPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating user: " + ex.Message);
            }
        }

        private void grpInput_Enter(object? sender, EventArgs e)
        {

        }
    }
}
