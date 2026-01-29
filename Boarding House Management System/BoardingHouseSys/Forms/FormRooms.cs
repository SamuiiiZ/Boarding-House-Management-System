using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormRooms : Form
    {
        private RoomRepository _repository;
        private DataGridView dgvRooms = null!;
        private TextBox txtRoomNumber = null!;
        private TextBox txtCapacity = null!;
        private TextBox txtRate = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnRefresh = null!;
        private Button btnBack = null!;
        private Label lblRoom = null!;
        private Label lblCap = null!;
        private Label lblRate = null!;
        private int _selectedRoomId = 0;

        public FormRooms(User user)
        {
            InitializeComponent();
            _repository = new RoomRepository();
            
            // Event Handlers
            this.btnAdd.Click += (s, e) => AddRoom();
            this.btnUpdate.Click += (s, e) => UpdateRoom();
            this.btnDelete.Click += (s, e) => DeleteRoom();
            this.btnRefresh.Click += (s, e) => LoadRooms();
            this.btnBack.Click += (s, e) => this.Close();
            this.dgvRooms.CellClick += DgvRooms_CellClick;
            this.Load += (s, e) => LoadRooms();
        }

        private void InitializeComponent()
        {
            dgvRooms = new DataGridView();
            txtRoomNumber = new TextBox();
            txtCapacity = new TextBox();
            txtRate = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            lblRoom = new Label();
            lblCap = new Label();
            lblRate = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRooms).BeginInit();
            SuspendLayout();
            // 
            // dgvRooms
            // 
            dgvRooms.AllowUserToAddRows = false;
            dgvRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRooms.ColumnHeadersHeight = 34;
            dgvRooms.Location = new Point(20, 120);
            dgvRooms.Name = "dgvRooms";
            dgvRooms.ReadOnly = true;
            dgvRooms.RowHeadersWidth = 62;
            dgvRooms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRooms.Size = new Size(540, 220);
            dgvRooms.TabIndex = 11;
            // 
            // txtRoomNumber
            // 
            txtRoomNumber.Location = new Point(100, 20);
            txtRoomNumber.Name = "txtRoomNumber";
            txtRoomNumber.Size = new Size(100, 31);
            txtRoomNumber.TabIndex = 1;
            // 
            // txtCapacity
            // 
            txtCapacity.Location = new Point(100, 50);
            txtCapacity.Name = "txtCapacity";
            txtCapacity.Size = new Size(100, 31);
            txtCapacity.TabIndex = 3;
            // 
            // txtRate
            // 
            txtRate.Location = new Point(100, 80);
            txtRate.Name = "txtRate";
            txtRate.Size = new Size(100, 31);
            txtRate.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(220, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(310, 20);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.Location = new Point(400, 20);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(220, 60);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 30);
            btnRefresh.TabIndex = 9;
            btnRefresh.Text = "Refresh";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(400, 60);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 30);
            btnBack.TabIndex = 10;
            btnBack.Text = "Back";
            // 
            // lblRoom
            // 
            lblRoom.AutoSize = true;
            lblRoom.Location = new Point(20, 20);
            lblRoom.Name = "lblRoom";
            lblRoom.Size = new Size(80, 25);
            lblRoom.TabIndex = 0;
            lblRoom.Text = "Room #:";
            // 
            // lblCap
            // 
            lblCap.AutoSize = true;
            lblCap.Location = new Point(12, 50);
            lblCap.Name = "lblCap";
            lblCap.Size = new Size(83, 25);
            lblCap.TabIndex = 2;
            lblCap.Text = "Capacity:";
            // 
            // lblRate
            // 
            lblRate.AutoSize = true;
            lblRate.Location = new Point(20, 80);
            lblRate.Name = "lblRate";
            lblRate.Size = new Size(51, 25);
            lblRate.TabIndex = 4;
            lblRate.Text = "Rate:";
            // 
            // FormRooms
            // 
            ClientSize = new Size(837, 508);
            Controls.Add(lblRoom);
            Controls.Add(txtRoomNumber);
            Controls.Add(lblCap);
            Controls.Add(txtCapacity);
            Controls.Add(lblRate);
            Controls.Add(txtRate);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(btnBack);
            Controls.Add(dgvRooms);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FormRooms";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manage Rooms";
            ((System.ComponentModel.ISupportInitialize)dgvRooms).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void DgvRooms_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvRooms.Rows[e.RowIndex];
                _selectedRoomId = Convert.ToInt32(row.Cells["Id"].Value);
                txtRoomNumber.Text = row.Cells["RoomNumber"].Value.ToString();
                txtCapacity.Text = row.Cells["Capacity"].Value.ToString();
                txtRate.Text = row.Cells["MonthlyRate"].Value.ToString();
            }
        }

        private void UpdateRoom()
        {
            if (_selectedRoomId == 0)
            {
                MessageBox.Show("Please select a room to update.");
                return;
            }
            
            try
            {
                var room = new Room
                {
                    Id = _selectedRoomId,
                    RoomNumber = txtRoomNumber.Text,
                    Capacity = int.Parse(txtCapacity.Text),
                    MonthlyRate = decimal.Parse(txtRate.Text)
                };
                _repository.UpdateRoom(room);
                MessageBox.Show("Room updated successfully!");
                LoadRooms();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating room: " + ex.Message);
            }
        }

        private void DeleteRoom()
        {
            if (_selectedRoomId == 0)
            {
                MessageBox.Show("Please select a room to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this room?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _repository.DeleteRoom(_selectedRoomId);
                    MessageBox.Show("Room deleted successfully!");
                    LoadRooms();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting room: " + ex.Message);
                }
            }
        }

        private void ClearFields()
        {
            txtRoomNumber.Clear();
            txtCapacity.Clear();
            txtRate.Clear();
            _selectedRoomId = 0;
        }

        private void LoadRooms()
        {
            try
            {
                dgvRooms.DataSource = _repository.GetAllRooms();
            }
            catch (Exception)
            {
                // Don't spam message box on load if DB not ready
                // MessageBox.Show("Error loading rooms: " + ex.Message);
            }
        }

        private void AddRoom()
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) || string.IsNullOrWhiteSpace(txtCapacity.Text) || string.IsNullOrWhiteSpace(txtRate.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            try
            {
                var room = new Room
                {
                    RoomNumber = txtRoomNumber.Text,
                    Capacity = int.Parse(txtCapacity.Text),
                    MonthlyRate = decimal.Parse(txtRate.Text)
                };
                _repository.AddRoom(room);
                MessageBox.Show("Room added successfully!");
                LoadRooms();
                txtRoomNumber.Clear();
                txtCapacity.Clear();
                txtRate.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding room: " + ex.Message);
            }
        }
    }
}
