using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;

namespace BoardingHouseSys.Forms
{
    public class FormBoarderDetails : Form
    {
        private User _currentUser;
        private BoarderRepository _boarderRepo;
        private PaymentRepository _paymentRepo;
        
        private Label lblNameVal = null!;
        private Label lblAddressVal = null!;
        private Label lblPhoneVal = null!;
        private Label lblRoomVal = null!;
        private Label lblRentVal = null!;
        private DataGridView dgvHistory = null!;
        private Label lblHeader;
        private GroupBox grpInfo;
        private Label lblName;
        private Label lblAddress;
        private Label lblPhone;
        private Label lblRoom;
        private Label lblRent;
        private Label lblHistory;
        private Button btnClose = null!;

        public FormBoarderDetails(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _boarderRepo = new BoarderRepository();
            _paymentRepo = new PaymentRepository();
            
            // Event Handler moved to Constructor
            btnClose.Click += (s, e) => this.Close();

            LoadDetails();
        }

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblNameVal = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblAddressVal = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPhoneVal = new System.Windows.Forms.Label();
            this.lblRoom = new System.Windows.Forms.Label();
            this.lblRoomVal = new System.Windows.Forms.Label();
            this.lblRent = new System.Windows.Forms.Label();
            this.lblRentVal = new System.Windows.Forms.Label();
            this.lblHistory = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(20, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(123, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "My Profile";
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.lblName);
            this.grpInfo.Controls.Add(this.lblNameVal);
            this.grpInfo.Controls.Add(this.lblAddress);
            this.grpInfo.Controls.Add(this.lblAddressVal);
            this.grpInfo.Controls.Add(this.lblPhone);
            this.grpInfo.Controls.Add(this.lblPhoneVal);
            this.grpInfo.Controls.Add(this.lblRoom);
            this.grpInfo.Controls.Add(this.lblRoomVal);
            this.grpInfo.Controls.Add(this.lblRent);
            this.grpInfo.Controls.Add(this.lblRentVal);
            this.grpInfo.Location = new System.Drawing.Point(20, 50);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(590, 160);
            this.grpInfo.TabIndex = 1;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Personal Information";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(20, 35);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(53, 19);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // lblNameVal
            // 
            this.lblNameVal.AutoSize = true;
            this.lblNameVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNameVal.Location = new System.Drawing.Point(100, 35);
            this.lblNameVal.Name = "lblNameVal";
            this.lblNameVal.Size = new System.Drawing.Size(70, 19);
            this.lblNameVal.TabIndex = 1;
            this.lblNameVal.Text = "Loading...";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAddress.Location = new System.Drawing.Point(20, 70);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(67, 19);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Address:";
            // 
            // lblAddressVal
            // 
            this.lblAddressVal.AutoSize = true;
            this.lblAddressVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAddressVal.Location = new System.Drawing.Point(100, 70);
            this.lblAddressVal.Name = "lblAddressVal";
            this.lblAddressVal.Size = new System.Drawing.Size(70, 19);
            this.lblAddressVal.TabIndex = 3;
            this.lblAddressVal.Text = "Loading...";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.Location = new System.Drawing.Point(20, 105);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(55, 19);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone:";
            // 
            // lblPhoneVal
            // 
            this.lblPhoneVal.AutoSize = true;
            this.lblPhoneVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhoneVal.Location = new System.Drawing.Point(100, 105);
            this.lblPhoneVal.Name = "lblPhoneVal";
            this.lblPhoneVal.Size = new System.Drawing.Size(70, 19);
            this.lblPhoneVal.TabIndex = 5;
            this.lblPhoneVal.Text = "Loading...";
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRoom.Location = new System.Drawing.Point(350, 35);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(53, 19);
            this.lblRoom.TabIndex = 6;
            this.lblRoom.Text = "Room:";
            // 
            // lblRoomVal
            // 
            this.lblRoomVal.AutoSize = true;
            this.lblRoomVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoomVal.Location = new System.Drawing.Point(410, 35);
            this.lblRoomVal.Name = "lblRoomVal";
            this.lblRoomVal.Size = new System.Drawing.Size(21, 19);
            this.lblRoomVal.TabIndex = 7;
            this.lblRoomVal.Text = "...";
            // 
            // lblRent
            // 
            this.lblRent.AutoSize = true;
            this.lblRent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRent.Location = new System.Drawing.Point(350, 70);
            this.lblRent.Name = "lblRent";
            this.lblRent.Size = new System.Drawing.Size(44, 19);
            this.lblRent.TabIndex = 8;
            this.lblRent.Text = "Rent:";
            // 
            // lblRentVal
            // 
            this.lblRentVal.AutoSize = true;
            this.lblRentVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRentVal.Location = new System.Drawing.Point(410, 70);
            this.lblRentVal.Name = "lblRentVal";
            this.lblRentVal.Size = new System.Drawing.Size(21, 19);
            this.lblRentVal.TabIndex = 9;
            this.lblRentVal.Text = "...";
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHistory.Location = new System.Drawing.Point(20, 230);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(137, 21);
            this.lblHistory.TabIndex = 2;
            this.lblHistory.Text = "Payment History";
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(20, 260);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.Size = new System.Drawing.Size(590, 230);
            this.dgvHistory.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(275, 510);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // FormBoarderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 600);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.grpInfo);
            this.Controls.Add(this.lblHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormBoarderDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "My Details";
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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
                    LoadHistory(boarderId);
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