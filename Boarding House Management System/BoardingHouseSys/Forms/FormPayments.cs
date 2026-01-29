using System;
using System.Drawing;
using System.Windows.Forms;
using BoardingHouseSys.Data;
using BoardingHouseSys.Models;
using System.Data;

namespace BoardingHouseSys.Forms
{
    public class FormPayments : Form
    {
        private PaymentRepository _paymentRepo;
        private BoarderRepository _boarderRepo;
        private User _currentUser;

        private DataGridView dgvPayments = null!;
        private ComboBox cmbBoarders = null!;
        private TextBox txtAmount = null!;
        private ComboBox cmbMonth = null!;
        private NumericUpDown numYear = null!;
        private ComboBox cmbStatus = null!;
        private TextBox txtNotes = null!;

        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnClear = null!;
        private Button btnBack = null!;
        private GroupBox grpInput = null!;
        private Label lblBoarder = null!;
        private Label lblAmount = null!;
        private Label lblMonth = null!;
        private Label lblYear = null!;
        private Label lblStatus = null!;
        private Label lblNotes = null!;
        private int _selectedPaymentId = 0;

        public FormPayments(User user)
        {
            InitializeComponent();
            _paymentRepo = new PaymentRepository();
            _boarderRepo = new BoarderRepository();
            _currentUser = user;

            LoadDropdowns(); // Populate static lists
            LoadBoarders();
            LoadPayments();

            // Event Handlers
            btnAdd.Click += (s, e) => AddPayment();
            btnUpdate.Click += (s, e) => UpdatePayment();
            btnDelete.Click += (s, e) => DeletePayment();
            btnClear.Click += (s, e) => ClearFields();
            btnBack.Click += (s, e) => this.Close();
            dgvPayments.CellClick += DgvPayments_CellClick;
        }

        private void LoadDropdowns()
        {
            // Months
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            cmbMonth.Items.AddRange(months);
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            // Status
            cmbStatus.Items.AddRange(new string[] { "Paid", "Pending", "Overdue" });
            cmbStatus.SelectedIndex = 0;

            // Year Default
            // InitializeComponent sets Min/Max, so this is safe now
            numYear.Value = DateTime.Now.Year;
        }

        private void InitializeComponent()
        {
            this.grpInput = new System.Windows.Forms.GroupBox();
            this.lblBoarder = new System.Windows.Forms.Label();
            this.cmbBoarders = new System.Windows.Forms.ComboBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.grpInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // grpInput
            // 
            this.grpInput.Controls.Add(this.lblBoarder);
            this.grpInput.Controls.Add(this.cmbBoarders);
            this.grpInput.Controls.Add(this.lblAmount);
            this.grpInput.Controls.Add(this.txtAmount);
            this.grpInput.Controls.Add(this.lblMonth);
            this.grpInput.Controls.Add(this.cmbMonth);
            this.grpInput.Controls.Add(this.lblYear);
            this.grpInput.Controls.Add(this.numYear);
            this.grpInput.Controls.Add(this.lblStatus);
            this.grpInput.Controls.Add(this.cmbStatus);
            this.grpInput.Controls.Add(this.lblNotes);
            this.grpInput.Controls.Add(this.txtNotes);
            this.grpInput.Controls.Add(this.btnAdd);
            this.grpInput.Controls.Add(this.btnUpdate);
            this.grpInput.Controls.Add(this.btnDelete);
            this.grpInput.Controls.Add(this.btnClear);
            this.grpInput.Location = new System.Drawing.Point(20, 20);
            this.grpInput.Name = "grpInput";
            this.grpInput.Size = new System.Drawing.Size(380, 420);
            this.grpInput.TabIndex = 0;
            this.grpInput.TabStop = false;
            this.grpInput.Text = "Payment Details";
            // 
            // lblBoarder
            // 
            this.lblBoarder.AutoSize = true;
            this.lblBoarder.Location = new System.Drawing.Point(20, 35);
            this.lblBoarder.Name = "lblBoarder";
            this.lblBoarder.Size = new System.Drawing.Size(50, 15);
            this.lblBoarder.TabIndex = 0;
            this.lblBoarder.Text = "Boarder:";
            // 
            // cmbBoarders
            // 
            this.cmbBoarders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoarders.FormattingEnabled = true;
            this.cmbBoarders.Location = new System.Drawing.Point(100, 32);
            this.cmbBoarders.Name = "cmbBoarders";
            this.cmbBoarders.Size = new System.Drawing.Size(250, 25);
            this.cmbBoarders.TabIndex = 1;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(20, 75);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(54, 15);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "Amount:";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(100, 72);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(250, 23);
            this.txtAmount.TabIndex = 3;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(20, 115);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(46, 15);
            this.lblMonth.TabIndex = 4;
            this.lblMonth.Text = "Month:";
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.FormattingEnabled = true;
            this.cmbMonth.Location = new System.Drawing.Point(100, 112);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(250, 25);
            this.cmbMonth.TabIndex = 5;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(20, 155);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(32, 15);
            this.lblYear.TabIndex = 6;
            this.lblYear.Text = "Year:";
            // 
            // numYear
            // 
            this.numYear.Location = new System.Drawing.Point(100, 152);
            this.numYear.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(250, 23);
            this.numYear.TabIndex = 7;
            this.numYear.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 195);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(100, 192);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(250, 25);
            this.cmbStatus.TabIndex = 9;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(20, 235);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(41, 15);
            this.lblNotes.TabIndex = 10;
            this.lblNotes.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(100, 232);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(250, 80);
            this.txtNotes.TabIndex = 11;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.LightGreen;
            this.btnAdd.Location = new System.Drawing.Point(20, 340);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 35);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add Payment";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(140, 340);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(90, 35);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightCoral;
            this.btnDelete.Location = new System.Drawing.Point(240, 340);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 35);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(20, 385);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(310, 30);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(20, 460);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(380, 40);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back to Dashboard";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayments.BackgroundColor = System.Drawing.Color.White;
            this.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayments.Location = new System.Drawing.Point(420, 20);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersVisible = false;
            this.dgvPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayments.Size = new System.Drawing.Size(600, 580);
            this.dgvPayments.TabIndex = 2;
            // 
            // FormPayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 650);
            this.Controls.Add(this.dgvPayments);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.grpInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormPayments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Payments";
            this.grpInput.ResumeLayout(false);
            this.grpInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.ResumeLayout(false);

        }

        private void LoadBoarders()
        {
            try
            {
                DataTable dt = _boarderRepo.GetAllBoarders();
                cmbBoarders.DisplayMember = "FullName";
                cmbBoarders.ValueMember = "Id";
                cmbBoarders.DataSource = dt;
                cmbBoarders.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading boarders: " + ex.Message);
            }
        }

        private void LoadPayments()
        {
            try
            {
                dgvPayments.DataSource = _paymentRepo.GetAllPayments();
                
                // Format Columns
                if (dgvPayments.Columns["Id"] != null) dgvPayments.Columns["Id"].Visible = false;
                if (dgvPayments.Columns["BoarderId"] != null) dgvPayments.Columns["BoarderId"].Visible = false;
                
                if (dgvPayments.Columns["BoarderName"] != null) dgvPayments.Columns["BoarderName"].HeaderText = "Boarder";
                if (dgvPayments.Columns["RoomNumber"] != null) dgvPayments.Columns["RoomNumber"].HeaderText = "Room";
                if (dgvPayments.Columns["Amount"] != null) dgvPayments.Columns["Amount"].DefaultCellStyle.Format = "C"; // Currency
                if (dgvPayments.Columns["PaymentDate"] != null) dgvPayments.Columns["PaymentDate"].HeaderText = "Date Recorded";
                if (dgvPayments.Columns["MonthPaid"] != null) dgvPayments.Columns["MonthPaid"].HeaderText = "Month";
                if (dgvPayments.Columns["YearPaid"] != null) dgvPayments.Columns["YearPaid"].HeaderText = "Year";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payments: " + ex.Message);
            }
        }

        private void DgvPayments_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPayments.Rows[e.RowIndex];
                _selectedPaymentId = Convert.ToInt32(row.Cells["Id"].Value);

                // Set Boarder
                int boarderId = Convert.ToInt32(row.Cells["BoarderId"].Value);
                cmbBoarders.SelectedValue = boarderId;

                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                cmbMonth.SelectedItem = row.Cells["MonthPaid"].Value.ToString();
                numYear.Value = Convert.ToDecimal(row.Cells["YearPaid"].Value);
                cmbStatus.SelectedItem = row.Cells["Status"].Value.ToString();
                txtNotes.Text = row.Cells["Notes"].Value.ToString();
            }
        }

        private void AddPayment()
        {
            if (cmbBoarders.SelectedValue == null || string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please select a boarder and enter an amount.");
                return;
            }

            try
            {
                var payment = new Payment
                {
                    BoarderId = Convert.ToInt32(cmbBoarders.SelectedValue),
                    Amount = decimal.Parse(txtAmount.Text),
                    PaymentDate = DateTime.Now,
                    MonthPaid = cmbMonth.SelectedItem?.ToString() ?? "January",
                    YearPaid = (int)numYear.Value,
                    Status = cmbStatus.SelectedItem?.ToString() ?? "Pending",
                    Notes = txtNotes.Text
                };

                _paymentRepo.AddPayment(payment);
                MessageBox.Show("Payment recorded successfully!");
                LoadPayments();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding payment: " + ex.Message);
            }
        }

        private void UpdatePayment()
        {
            if (_selectedPaymentId == 0)
            {
                MessageBox.Show("Please select a payment to update.");
                return;
            }

            try
            {
                var payment = new Payment
                {
                    Id = _selectedPaymentId,
                    BoarderId = Convert.ToInt32(cmbBoarders.SelectedValue),
                    Amount = decimal.Parse(txtAmount.Text),
                    MonthPaid = cmbMonth.SelectedItem?.ToString() ?? "January",
                    YearPaid = (int)numYear.Value,
                    Status = cmbStatus.SelectedItem?.ToString() ?? "Pending",
                    Notes = txtNotes.Text
                };

                _paymentRepo.UpdatePayment(payment);
                MessageBox.Show("Payment updated successfully!");
                LoadPayments();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating payment: " + ex.Message);
            }
        }

        private void DeletePayment()
        {
            if (_selectedPaymentId == 0)
            {
                MessageBox.Show("Please select a payment to delete.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this payment record?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _paymentRepo.DeletePayment(_selectedPaymentId);
                    MessageBox.Show("Payment deleted successfully!");
                    LoadPayments();
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting payment: " + ex.Message);
                }
            }
        }

        private void ClearFields()
        {
            cmbBoarders.SelectedIndex = -1;
            txtAmount.Clear();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            numYear.Value = DateTime.Now.Year;
            cmbStatus.SelectedIndex = 0;
            txtNotes.Clear();
            _selectedPaymentId = 0;
        }

        private void grpInput_Enter(object? sender, EventArgs e)
        {

        }
    }
}