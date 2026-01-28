using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormDashboard : Form
    {
        private User _currentUser;
        private Label lblWelcome;
        private Button btnLogout;
        
        // Admin Buttons
        private Button btnManageBoarders;
        private Button btnManageRooms;
        private Button btnManagePayments;
        
        // SuperAdmin Buttons
        private Button btnManageUsers;
        
        // Boarder Buttons
        private Button btnMyDetails;

        // Dashboard Stats Labels
        private Label lblStats;

        public FormDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            lblWelcome.Text = $"Welcome, {user.Username} ({user.Role})";
            SetupRoleBasedAccess();
        }
        
        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.btnLogout = new Button();
            this.btnManageBoarders = new Button();
            this.btnManageRooms = new Button();
            this.btnManagePayments = new Button();
            this.btnManageUsers = new Button();
            this.btnMyDetails = new Button();
            this.lblStats = new Label();
            
            this.SuspendLayout();
            
            // Header
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            this.lblWelcome.Location = new Point(20, 20);
            
            this.btnLogout.Text = "Logout";
            this.btnLogout.Location = new Point(650, 20);
            this.btnLogout.Click += (s, e) => this.Close();

            // Navigation Buttons (Left Panel simulation)
            int yPos = 80;
            
            // Manage Boarders
            this.btnManageBoarders.Text = "Manage Boarders";
            this.btnManageBoarders.Size = new Size(150, 40);
            this.btnManageBoarders.Location = new Point(20, yPos);
            this.btnManageBoarders.Click += (s, e) => OpenForm(new FormBoarders(_currentUser));
            yPos += 50;

            // Manage Rooms
            this.btnManageRooms.Text = "Manage Rooms";
            this.btnManageRooms.Size = new Size(150, 40);
            this.btnManageRooms.Location = new Point(20, yPos);
            this.btnManageRooms.Click += (s, e) => OpenForm(new FormRooms(_currentUser));
            yPos += 50;

            // Manage Payments
            this.btnManagePayments.Text = "Manage Payments";
            this.btnManagePayments.Size = new Size(150, 40);
            this.btnManagePayments.Location = new Point(20, yPos);
            this.btnManagePayments.Click += (s, e) => OpenForm(new FormPayments(_currentUser));
            yPos += 50;

            // Manage Users
            this.btnManageUsers.Text = "Manage Users";
            this.btnManageUsers.Size = new Size(150, 40);
            this.btnManageUsers.Location = new Point(20, yPos);
            this.btnManageUsers.Click += (s, e) => OpenForm(new FormUsers(_currentUser));
            yPos += 50;
            
            // My Details
            this.btnMyDetails.Text = "My Details";
            this.btnMyDetails.Size = new Size(150, 40);
            this.btnMyDetails.Location = new Point(20, yPos);
            this.btnMyDetails.Click += (s, e) => MessageBox.Show("Boarder details view coming soon!", "Info");
            
            // Stats Area
            this.lblStats.Text = "Dashboard Stats will appear here.";
            this.lblStats.Location = new Point(200, 80);
            this.lblStats.Size = new Size(500, 300);
            this.lblStats.BorderStyle = BorderStyle.FixedSingle;
            this.lblStats.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStats.Font = new Font("Segoe UI", 10);

            // Form
            this.ClientSize = new Size(800, 500);
            this.Controls.Add(lblWelcome);
            this.Controls.Add(btnLogout);
            this.Controls.Add(btnManageBoarders);
            this.Controls.Add(btnManageRooms);
            this.Controls.Add(btnManagePayments);
            this.Controls.Add(btnManageUsers);
            this.Controls.Add(btnMyDetails);
            this.Controls.Add(lblStats);
            
            this.Text = "Boarding House Management System - Dashboard";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void SetupRoleBasedAccess()
        {
            // Default: Hide all
            btnManageBoarders.Visible = false;
            btnManageRooms.Visible = false;
            btnManagePayments.Visible = false;
            btnManageUsers.Visible = false;
            btnMyDetails.Visible = false;

            if (_currentUser.Role == "SuperAdmin")
            {
                btnManageUsers.Visible = true;
                btnManageBoarders.Visible = true;
                btnManageRooms.Visible = true;
                btnManagePayments.Visible = true;
                lblStats.Text = "Super Admin Dashboard\n\nOverview:\n- Manage Users\n- Full System Access";
            }
            else if (_currentUser.Role == "Admin")
            {
                btnManageBoarders.Visible = true;
                btnManageRooms.Visible = true;
                btnManagePayments.Visible = true;
                lblStats.Text = "Admin Dashboard\n\nOverview:\n- Manage Boarders\n- Manage Rooms\n- Manage Payments";
            }
            else if (_currentUser.Role == "Boarder")
            {
                btnMyDetails.Visible = true;
                lblStats.Text = "Boarder Dashboard\n\n- View Rent\n- View Payments";
            }
        }

        private void OpenForm(Form form)
        {
            form.ShowDialog(); // Show as modal
        }
    }
}
