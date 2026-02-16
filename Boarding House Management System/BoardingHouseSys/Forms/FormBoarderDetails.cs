#nullable enable
#pragma warning disable CS8618
#pragma warning disable CS8622
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;
using BoardingHouseSys.UI;
using System.IO;

namespace BoardingHouseSys.Forms
{
    public class FormBoarderDetails : Form
    {
        private User _currentUser;
        private BoarderRepository _boarderRepo;
        private PaymentRepository _paymentRepo;
        private int _currentBoarderId;
        
        // UI Controls - Exposed for Designer
        private System.ComponentModel.IContainer? components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnBackTop;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.PictureBox picProfile;
        private System.Windows.Forms.Button btnUploadPhoto;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblNameVal;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblAddressVal;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPhoneVal;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Label lblRoomVal;
        private System.Windows.Forms.Label lblRent;
        private System.Windows.Forms.Label lblRentVal;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.FlowLayoutPanel actionPanel;
        private System.Windows.Forms.Button btnClose;

        // Parameterless constructor for Designer support
        public FormBoarderDetails()
        {
            InitializeComponent();
            WireEvents();
            _currentUser = new User();
            _boarderRepo = new BoarderRepository();
            _paymentRepo = new PaymentRepository();
        }

        public FormBoarderDetails(User user)
        {
            InitializeComponent();
            WireEvents();
            
            _currentUser = user;
            _boarderRepo = new BoarderRepository();
            _paymentRepo = new PaymentRepository();
            
            LoadDetails();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            btnBackTop = new Button();
            mainLayout = new TableLayoutPanel();
            lblHeader = new Label();
            grpInfo = new GroupBox();
            infoPanel = new Panel();
            picProfile = new PictureBox();
            btnUploadPhoto = new Button();
            lblName = new Label();
            lblNameVal = new Label();
            lblAddress = new Label();
            lblAddressVal = new Label();
            lblPhone = new Label();
            lblPhoneVal = new Label();
            lblRoom = new Label();
            lblRoomVal = new Label();
            lblRent = new Label();
            lblRentVal = new Label();
            rightPanel = new Panel();
            dgvHistory = new DataGridView();
            lblHistory = new Label();
            actionPanel = new FlowLayoutPanel();
            btnClose = new Button();
            pnlTop.SuspendLayout();
            mainLayout.SuspendLayout();
            grpInfo.SuspendLayout();
            infoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picProfile).BeginInit();
            rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            actionPanel.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(50, 50, 50);
            pnlTop.Controls.Add(btnBackTop);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(10);
            pnlTop.Size = new Size(1200, 50);
            pnlTop.TabIndex = 1;
            // 
            // btnBackTop
            // 
            btnBackTop.BackColor = Color.White;
            btnBackTop.Dock = DockStyle.Left;
            btnBackTop.FlatStyle = FlatStyle.Flat;
            btnBackTop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBackTop.Location = new Point(10, 10);
            btnBackTop.Name = "btnBackTop";
            btnBackTop.Size = new Size(180, 30);
            btnBackTop.TabIndex = 0;
            btnBackTop.Text = "← Back to Dashboard";
            btnBackTop.TextAlign = ContentAlignment.MiddleLeft;
            btnBackTop.UseVisualStyleBackColor = false;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 2;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainLayout.Controls.Add(lblHeader, 0, 0);
            mainLayout.Controls.Add(grpInfo, 0, 1);
            mainLayout.Controls.Add(rightPanel, 1, 1);
            mainLayout.Controls.Add(actionPanel, 1, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 50);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(20);
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            mainLayout.Size = new Size(1200, 750);
            mainLayout.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            mainLayout.SetColumnSpan(lblHeader, 2);
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblHeader.Location = new Point(23, 20);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(1154, 60);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "My Profile";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(infoPanel);
            grpInfo.Dock = DockStyle.Fill;
            grpInfo.Font = new Font("Segoe UI", 12F);
            grpInfo.Location = new Point(23, 83);
            grpInfo.Name = "grpInfo";
            grpInfo.Padding = new Padding(20);
            grpInfo.Size = new Size(458, 594);
            grpInfo.TabIndex = 1;
            grpInfo.TabStop = false;
            grpInfo.Text = "Personal Information";
            // 
            // infoPanel
            // 
            infoPanel.Controls.Add(picProfile);
            infoPanel.Controls.Add(btnUploadPhoto);
            infoPanel.Controls.Add(lblName);
            infoPanel.Controls.Add(lblNameVal);
            infoPanel.Controls.Add(lblAddress);
            infoPanel.Controls.Add(lblAddressVal);
            infoPanel.Controls.Add(lblPhone);
            infoPanel.Controls.Add(lblPhoneVal);
            infoPanel.Controls.Add(lblRoom);
            infoPanel.Controls.Add(lblRoomVal);
            infoPanel.Controls.Add(lblRent);
            infoPanel.Controls.Add(lblRentVal);
            infoPanel.Dock = DockStyle.Fill;
            infoPanel.Location = new Point(20, 52);
            infoPanel.Name = "infoPanel";
            infoPanel.Size = new Size(418, 522);
            infoPanel.TabIndex = 0;
            // 
            // picProfile
            // 
            picProfile.BorderStyle = BorderStyle.FixedSingle;
            picProfile.Location = new Point(20, 30);
            picProfile.Name = "picProfile";
            picProfile.Size = new Size(150, 150);
            picProfile.SizeMode = PictureBoxSizeMode.Zoom;
            picProfile.TabIndex = 0;
            picProfile.TabStop = false;
            // 
            // btnUploadPhoto
            // 
            btnUploadPhoto.BackColor = UITheme.PrimaryColor;
            btnUploadPhoto.FlatStyle = FlatStyle.Flat;
            btnUploadPhoto.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUploadPhoto.ForeColor = Color.White;
            btnUploadPhoto.Location = new Point(20, 190);
            btnUploadPhoto.Name = "btnUploadPhoto";
            btnUploadPhoto.Size = new Size(150, 45);
            btnUploadPhoto.TabIndex = 1;
            btnUploadPhoto.Text = "Change Photo";
            btnUploadPhoto.UseVisualStyleBackColor = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.Location = new Point(200, 30);
            lblName.Name = "lblName";
            lblName.Size = new Size(73, 28);
            lblName.TabIndex = 2;
            lblName.Text = "Name:";
            // 
            // lblNameVal
            // 
            lblNameVal.AutoEllipsis = true;
            lblNameVal.Font = new Font("Segoe UI", 10F);
            lblNameVal.Location = new Point(200, 60);
            lblNameVal.Name = "lblNameVal";
            lblNameVal.Size = new Size(200, 28);
            lblNameVal.TabIndex = 3;
            lblNameVal.Text = "...";
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAddress.Location = new Point(20, 240);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(92, 28);
            lblAddress.TabIndex = 4;
            lblAddress.Text = "Address:";
            // 
            // lblAddressVal
            // 
            lblAddressVal.AutoEllipsis = true;
            lblAddressVal.Font = new Font("Segoe UI", 10F);
            lblAddressVal.Location = new Point(20, 270);
            lblAddressVal.Name = "lblAddressVal";
            lblAddressVal.Size = new Size(380, 60);
            lblAddressVal.TabIndex = 5;
            lblAddressVal.Text = "...";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhone.Location = new Point(20, 340);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(76, 28);
            lblPhone.TabIndex = 6;
            lblPhone.Text = "Phone:";
            // 
            // lblPhoneVal
            // 
            lblPhoneVal.AutoEllipsis = true;
            lblPhoneVal.Font = new Font("Segoe UI", 10F);
            lblPhoneVal.Location = new Point(110, 340);
            lblPhoneVal.Name = "lblPhoneVal";
            lblPhoneVal.Size = new Size(200, 28);
            lblPhoneVal.TabIndex = 7;
            lblPhoneVal.Text = "...";
            // 
            // lblRoom
            // 
            lblRoom.AutoSize = true;
            lblRoom.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRoom.Location = new Point(20, 380);
            lblRoom.Name = "lblRoom";
            lblRoom.Size = new Size(72, 28);
            lblRoom.TabIndex = 8;
            lblRoom.Text = "Room:";
            // 
            // lblRoomVal
            // 
            lblRoomVal.AutoEllipsis = true;
            lblRoomVal.Font = new Font("Segoe UI", 10F);
            lblRoomVal.Location = new Point(110, 380);
            lblRoomVal.Name = "lblRoomVal";
            lblRoomVal.Size = new Size(200, 28);
            lblRoomVal.TabIndex = 9;
            lblRoomVal.Text = "...";
            // 
            // lblRent
            // 
            lblRent.AutoSize = true;
            lblRent.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRent.Location = new Point(20, 420);
            lblRent.Name = "lblRent";
            lblRent.Size = new Size(61, 28);
            lblRent.TabIndex = 10;
            lblRent.Text = "Rent:";
            // 
            // lblRentVal
            // 
            lblRentVal.AutoEllipsis = true;
            lblRentVal.Font = new Font("Segoe UI", 10F);
            lblRentVal.Location = new Point(110, 420);
            lblRentVal.Name = "lblRentVal";
            lblRentVal.Size = new Size(200, 28);
            lblRentVal.TabIndex = 11;
            lblRentVal.Text = "...";
            // 
            // rightPanel
            // 
            rightPanel.Controls.Add(dgvHistory);
            rightPanel.Controls.Add(lblHistory);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(487, 83);
            rightPanel.Name = "rightPanel";
            rightPanel.Padding = new Padding(10);
            rightPanel.Size = new Size(690, 594);
            rightPanel.TabIndex = 2;
            // 
            // dgvHistory
            // 
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.ColumnHeadersHeight = 34;
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.Location = new Point(10, 50);
            dgvHistory.Name = "dgvHistory";
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowHeadersWidth = 62;
            dgvHistory.Size = new Size(670, 534);
            dgvHistory.TabIndex = 0;
            // 
            // lblHistory
            // 
            lblHistory.Dock = DockStyle.Top;
            lblHistory.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHistory.Location = new Point(10, 10);
            lblHistory.Name = "lblHistory";
            lblHistory.Size = new Size(670, 40);
            lblHistory.TabIndex = 1;
            lblHistory.Text = "Payment History";
            // 
            // actionPanel
            // 
            actionPanel.Controls.Add(btnClose);
            actionPanel.Dock = DockStyle.Fill;
            actionPanel.FlowDirection = FlowDirection.RightToLeft;
            actionPanel.Location = new Point(487, 683);
            actionPanel.Name = "actionPanel";
            actionPanel.Size = new Size(690, 44);
            actionPanel.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.LightSlateGray;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(487, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(200, 40);
            btnClose.TabIndex = 0;
            btnClose.Text = "Back to Dashboard";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // FormBoarderDetails
            // 
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1200, 800);
            Controls.Add(mainLayout);
            Controls.Add(pnlTop);
            Name = "FormBoarderDetails";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "My Details";
            WindowState = FormWindowState.Maximized;
            pnlTop.ResumeLayout(false);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            grpInfo.ResumeLayout(false);
            infoPanel.ResumeLayout(false);
            infoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picProfile).EndInit();
            rightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            actionPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void WireEvents()
        {
            this.btnClose.Click += (s, e) => this.Close();
            this.btnBackTop.Click += (s, e) => this.Close();
            this.btnUploadPhoto.Click += BtnUploadPhoto_Click;
        }

        private void BtnUploadPhoto_Click(object? sender, EventArgs e)
        {
            if (_currentBoarderId == 0)
            {
                MessageBox.Show("Profile not loaded correctly.");
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select Profile Picture";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string sourcePath = ofd.FileName;
                        string folderPath = Path.Combine(Application.StartupPath, "ProfilePictures");
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(sourcePath);
                        string destPath = Path.Combine(folderPath, fileName);
                        
                        File.Copy(sourcePath, destPath, true);
                        string relativePath = "ProfilePictures/" + fileName;

                        // Update DB via Repository
                        _boarderRepo.UpdateProfilePicture(_currentBoarderId, relativePath);

                        // Update UI
                        picProfile.Image = Image.FromFile(destPath);
                        MessageBox.Show("Profile picture updated!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating photo: " + ex.Message);
                    }
                }
            }
        }

        private void LoadDetails()
        {
            try
            {
                DataTable dt = _boarderRepo.GetBoarderDetailsByUserId(_currentUser.Id);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblNameVal.Text = row["FullName"].ToString();
                    lblAddressVal.Text = row["Address"].ToString();
                    lblPhoneVal.Text = row["Phone"].ToString();
                    
                    object roomNum = row["RoomNumber"];
                    lblRoomVal.Text = (roomNum != DBNull.Value) ? roomNum.ToString() : "Not Assigned";
                    
                    object rate = row["MonthlyRate"];
                    lblRentVal.Text = (rate != DBNull.Value) ? Convert.ToDecimal(rate).ToString("C") : "N/A";

                    int boarderId = Convert.ToInt32(row["Id"]);
                    _currentBoarderId = boarderId; // Store ID
                    LoadHistory(boarderId);
                    
                    // Load Profile Picture
                    string? profilePath = row["ProfilePicturePath"] as string;
                    if (!string.IsNullOrEmpty(profilePath))
                    {
                        string fullPath = Path.Combine(Application.StartupPath, profilePath);
                        if (File.Exists(fullPath))
                        {
                            picProfile.Image = Image.FromFile(fullPath);
                        }
                    }
                }
                else
                {
                    lblNameVal.Text = "Profile not found.";
                    MessageBox.Show("No boarder profile linked to this account. Please contact Admin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading details: " + ex.Message);
            }
        }

        private void LoadHistory(int boarderId)
        {
            try
            {
                dgvHistory.DataSource = _paymentRepo.GetPaymentsByBoarderId(boarderId);

                // Format Columns
                if (dgvHistory.Columns["Id"] != null) dgvHistory.Columns["Id"].Visible = false;
                if (dgvHistory.Columns["Amount"] != null) dgvHistory.Columns["Amount"].DefaultCellStyle.Format = "C";
                if (dgvHistory.Columns["PaymentDate"] != null) dgvHistory.Columns["PaymentDate"].HeaderText = "Date";
                if (dgvHistory.Columns["MonthPaid"] != null) dgvHistory.Columns["MonthPaid"].HeaderText = "Month";
                if (dgvHistory.Columns["YearPaid"] != null) dgvHistory.Columns["YearPaid"].HeaderText = "Year";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payment history: " + ex.Message);
            }
        }
    }
}
