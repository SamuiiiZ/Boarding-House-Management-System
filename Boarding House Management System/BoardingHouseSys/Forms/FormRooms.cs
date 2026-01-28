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
        private DataGridView dgvRooms;
        private TextBox txtRoomNumber;
        private TextBox txtCapacity;
        private TextBox txtRate;
        private Button btnAdd;
        private Button btnRefresh;

        public FormRooms(User user)
        {
            InitializeComponent();
            _repository = new RoomRepository();
            // Load data only if connection is likely to work, or handle error gracefully
            this.Load += (s, e) => LoadRooms();
        }

        private void InitializeComponent()
        {
            this.dgvRooms = new DataGridView();
            this.txtRoomNumber = new TextBox();
            this.txtCapacity = new TextBox();
            this.txtRate = new TextBox();
            this.btnAdd = new Button();
            this.btnRefresh = new Button();
            
            Label lblRoom = new Label { Text = "Room #:", Location = new Point(20, 20), AutoSize = true };
            this.txtRoomNumber.Location = new Point(100, 20);
            
            Label lblCap = new Label { Text = "Capacity:", Location = new Point(20, 50), AutoSize = true };
            this.txtCapacity.Location = new Point(100, 50);

            Label lblRate = new Label { Text = "Rate:", Location = new Point(20, 80), AutoSize = true };
            this.txtRate.Location = new Point(100, 80);

            this.btnAdd.Text = "Add Room";
            this.btnAdd.Location = new Point(220, 20);
            this.btnAdd.Size = new Size(100, 30);
            this.btnAdd.Click += (s, e) => AddRoom();

            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new Point(220, 60);
            this.btnRefresh.Size = new Size(100, 30);
            this.btnRefresh.Click += (s, e) => LoadRooms();

            this.dgvRooms.Location = new Point(20, 120);
            this.dgvRooms.Size = new Size(540, 220);
            this.dgvRooms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRooms.AllowUserToAddRows = false;
            this.dgvRooms.ReadOnly = true;

            this.Controls.Add(lblRoom);
            this.Controls.Add(this.txtRoomNumber);
            this.Controls.Add(lblCap);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(lblRate);
            this.Controls.Add(this.txtRate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvRooms);

            this.Text = "Manage Rooms";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void LoadRooms()
        {
            try
            {
                dgvRooms.DataSource = _repository.GetAllRooms();
            }
            catch (Exception ex)
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
