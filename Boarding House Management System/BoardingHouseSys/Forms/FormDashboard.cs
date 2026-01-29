using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormDashboard : Form
    {
        private User _currentUser;
        private Label lblWelcome = null!;
        private Button btnLogout = null!;
        
        // Admin Buttons
        private Button btnManageBoarders = null!;
        private Button btnManageRooms = null!;
        private Button btnManagePayments = null!;
        
        // SuperAdmin Buttons
        private Button btnManageUsers = null!;
        
        // Boarder Buttons
        private Button btnMyDetails = null!;

        // Dashboard Stats Labels
        private Label lblStats = null!;

        public FormDashboard(User user)
        {
            InitializeComponent();
            _currentUser = user;
            lblWelcome.Text = $"Welcome, {user.Username} ({user.Role})";
            
            // Event Handlers
            this.btnLogout.Click += (s, e) => this.Close();
            this.btnManageBoarders.Click += (s, e) => OpenForm(new FormBoarders(_currentUser));
            this.btnManageRooms.Click += (s, e) => OpenForm(new FormRooms(_currentUser));
            this.btnManagePayments.Click += (s, e) => OpenForm(new FormPayments(_currentUser));
            this.btnManageUsers.Click += (s, e) => OpenForm(new FormUsers(_currentUser));
            this.btnMyDetails.Click += (s, e) => OpenForm(new FormBoarderDetails(_currentUser));

            SetupRoleBasedAccess();
        }

        private void InitializeComponent()
        {
            lblWelcome = new Label();
            btnLogout = new Button();
            btnManageBoarders = new Button();
            btnManageRooms = new Button();
            btnManagePayments = new Button();
            btnManageUsers = new Button();
            btnMyDetails = new Button();
            lblStats = new Label();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblWelcome.Location = new Point(20, 20);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(0, 32);
            lblWelcome.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(650, 20);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(83, 32);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "Logout";
            // 
            // btnManageBoarders
            // 
            btnManageBoarders.Location = new Point(20, 80);
            btnManageBoarders.Name = "btnManageBoarders";
            btnManageBoarders.Size = new Size(150, 40);
            btnManageBoarders.TabIndex = 2;
            btnManageBoarders.Text = "Manage Boarders";
            // 
            // btnManageRooms
            // 
            btnManageRooms.Location = new Point(20, 130);
            btnManageRooms.Name = "btnManageRooms";
            btnManageRooms.Size = new Size(150, 40);
            btnManageRooms.TabIndex = 3;
            btnManageRooms.Text = "Manage Rooms";
            // 
            // btnManagePayments
            // 
            btnManagePayments.Location = new Point(20, 180);
            btnManagePayments.Name = "btnManagePayments";
            btnManagePayments.Size = new Size(150, 40);
            btnManagePayments.TabIndex = 4;
            btnManagePayments.Text = "Manage Payments";
            // 
            // btnManageUsers
            // 
            btnManageUsers.Location = new Point(20, 230);
            btnManageUsers.Name = "btnManageUsers";
            btnManageUsers.Size = new Size(150, 40);
            btnManageUsers.TabIndex = 5;
            btnManageUsers.Text = "Manage Users";
            // 
            // btnMyDetails
            // 
            btnMyDetails.Location = new Point(20, 280);
            btnMyDetails.Name = "btnMyDetails";
            btnMyDetails.Size = new Size(150, 40);
            btnMyDetails.TabIndex = 6;
            btnMyDetails.Text = "My Details";
            // 
            // lblStats
            // 
            lblStats.BorderStyle = BorderStyle.FixedSingle;
            lblStats.Font = new Font("Segoe UI", 10F);
            lblStats.Location = new Point(200, 80);
            lblStats.Name = "lblStats";
            lblStats.Size = new Size(500, 300);
            lblStats.TabIndex = 7;
            lblStats.Text = "Dashboard Stats will appear here.";
            lblStats.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormDashboard
            // 
            ClientSize = new Size(800, 500);
            Controls.Add(lblWelcome);
            Controls.Add(btnLogout);
            Controls.Add(btnManageBoarders);
            Controls.Add(btnManageRooms);
            Controls.Add(btnManagePayments);
            Controls.Add(btnManageUsers);
            Controls.Add(btnMyDetails);
            Controls.Add(lblStats);
            Name = "FormDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Boarding House Management System - Dashboard";
            ResumeLayout(false);
            PerformLayout();
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
