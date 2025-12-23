namespace QuanLyTiemDaQuy.Forms
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCancelInvoice = new System.Windows.Forms.Button();
            this.btnViewDetails = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStat1 = new System.Windows.Forms.Panel();
            this.lblTotalInvoices = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlStat2 = new System.Windows.Forms.Panel();
            this.lblCompletedCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlStat3 = new System.Windows.Forms.Panel();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlStat4 = new System.Windows.Forms.Panel();
            this.lblCancelledCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlStat5 = new System.Windows.Forms.Panel();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlStat6 = new System.Windows.Forms.Panel();
            this.lblCancelledAmount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlStat1.SuspendLayout();
            this.pnlStat2.SuspendLayout();
            this.pnlStat3.SuspendLayout();
            this.pnlStat4.SuspendLayout();
            this.pnlStat5.SuspendLayout();
            this.pnlStat6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(980, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(980, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📊 BÁO CÁO HÓA ĐƠN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlFilter.Controls.Add(this.btnRefresh);
            this.pnlFilter.Controls.Add(this.btnCancelInvoice);
            this.pnlFilter.Controls.Add(this.btnViewDetails);
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Controls.Add(this.cboStatus);
            this.pnlFilter.Controls.Add(this.lblStatus);
            this.pnlFilter.Controls.Add(this.dtpToDate);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.dtpFromDate);
            this.pnlFilter.Controls.Add(this.lblFrom);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 50);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilter.Size = new System.Drawing.Size(980, 55);
            this.pnlFilter.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(880, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 32);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCancelInvoice
            // 
            this.btnCancelInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancelInvoice.FlatAppearance.BorderSize = 0;
            this.btnCancelInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelInvoice.ForeColor = System.Drawing.Color.White;
            this.btnCancelInvoice.Location = new System.Drawing.Point(770, 12);
            this.btnCancelInvoice.Name = "btnCancelInvoice";
            this.btnCancelInvoice.Size = new System.Drawing.Size(100, 32);
            this.btnCancelInvoice.TabIndex = 8;
            this.btnCancelInvoice.Text = "🚫 Hủy HĐ";
            this.btnCancelInvoice.UseVisualStyleBackColor = false;
            this.btnCancelInvoice.Click += new System.EventHandler(this.btnCancelInvoice_Click);
            // 
            // btnViewDetails
            // 
            this.btnViewDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnViewDetails.FlatAppearance.BorderSize = 0;
            this.btnViewDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewDetails.ForeColor = System.Drawing.Color.White;
            this.btnViewDetails.Location = new System.Drawing.Point(660, 12);
            this.btnViewDetails.Name = "btnViewDetails";
            this.btnViewDetails.Size = new System.Drawing.Size(100, 32);
            this.btnViewDetails.TabIndex = 7;
            this.btnViewDetails.Text = "👁 Chi tiết";
            this.btnViewDetails.UseVisualStyleBackColor = false;
            this.btnViewDetails.Click += new System.EventHandler(this.btnViewDetails_Click);
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnFilter.FlatAppearance.BorderSize = 0;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(550, 12);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(100, 32);
            this.btnFilter.TabIndex = 6;
            this.btnFilter.Text = "🔍 Lọc";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cboStatus
            // 
            this.cboStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStatus.ForeColor = System.Drawing.Color.White;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(400, 15);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(140, 25);
            this.cboStatus.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(330, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(67, 19);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Trạng thái:";
            // 
            // dtpToDate
            // 
            this.dtpToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(220, 15);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(100, 25);
            this.dtpToDate.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.ForeColor = System.Drawing.Color.White;
            this.lblTo.Location = new System.Drawing.Point(185, 18);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(33, 19);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "đến";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(75, 15);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(100, 25);
            this.dtpFromDate.TabIndex = 1;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.ForeColor = System.Drawing.Color.White;
            this.lblFrom.Location = new System.Drawing.Point(15, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(51, 19);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Từ ngày";
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.pnlSummary.Controls.Add(this.pnlStat1);
            this.pnlSummary.Controls.Add(this.pnlStat2);
            this.pnlSummary.Controls.Add(this.pnlStat3);
            this.pnlSummary.Controls.Add(this.pnlStat4);
            this.pnlSummary.Controls.Add(this.pnlStat5);
            this.pnlSummary.Controls.Add(this.pnlStat6);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummary.Location = new System.Drawing.Point(0, 105);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(5);
            this.pnlSummary.Size = new System.Drawing.Size(980, 80);
            this.pnlSummary.TabIndex = 2;
            // 
            // pnlStat1
            // 
            this.pnlStat1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlStat1.Controls.Add(this.lblTotalInvoices);
            this.pnlStat1.Controls.Add(this.label1);
            this.pnlStat1.Location = new System.Drawing.Point(10, 10);
            this.pnlStat1.Margin = new System.Windows.Forms.Padding(5);
            this.pnlStat1.Name = "pnlStat1";
            this.pnlStat1.Size = new System.Drawing.Size(150, 60);
            this.pnlStat1.TabIndex = 0;
            // 
            // lblTotalInvoices
            // 
            this.lblTotalInvoices.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalInvoices.ForeColor = System.Drawing.Color.White;
            this.lblTotalInvoices.Location = new System.Drawing.Point(5, 25);
            this.lblTotalInvoices.Name = "lblTotalInvoices";
            this.lblTotalInvoices.Size = new System.Drawing.Size(140, 30);
            this.lblTotalInvoices.TabIndex = 1;
            this.lblTotalInvoices.Text = "0";
            this.lblTotalInvoices.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tổng HĐ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat2
            // 
            this.pnlStat2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.pnlStat2.Controls.Add(this.lblCompletedCount);
            this.pnlStat2.Controls.Add(this.label3);
            this.pnlStat2.Location = new System.Drawing.Point(170, 10);
            this.pnlStat2.Margin = new System.Windows.Forms.Padding(5);
            this.pnlStat2.Name = "pnlStat2";
            this.pnlStat2.Size = new System.Drawing.Size(150, 60);
            this.pnlStat2.TabIndex = 1;
            // 
            // lblCompletedCount
            // 
            this.lblCompletedCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCompletedCount.ForeColor = System.Drawing.Color.White;
            this.lblCompletedCount.Location = new System.Drawing.Point(5, 25);
            this.lblCompletedCount.Name = "lblCompletedCount";
            this.lblCompletedCount.Size = new System.Drawing.Size(140, 30);
            this.lblCompletedCount.TabIndex = 1;
            this.lblCompletedCount.Text = "0";
            this.lblCompletedCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "✅ Đã xuất";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat3
            // 
            this.pnlStat3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.pnlStat3.Controls.Add(this.lblPendingCount);
            this.pnlStat3.Controls.Add(this.label5);
            this.pnlStat3.Location = new System.Drawing.Point(330, 10);
            this.pnlStat3.Margin = new System.Windows.Forms.Padding(5);
            this.pnlStat3.Name = "pnlStat3";
            this.pnlStat3.Size = new System.Drawing.Size(150, 60);
            this.pnlStat3.TabIndex = 2;
            // 
            // lblPendingCount
            // 
            this.lblPendingCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblPendingCount.ForeColor = System.Drawing.Color.Black;
            this.lblPendingCount.Location = new System.Drawing.Point(5, 25);
            this.lblPendingCount.Name = "lblPendingCount";
            this.lblPendingCount.Size = new System.Drawing.Size(140, 30);
            this.lblPendingCount.TabIndex = 1;
            this.lblPendingCount.Text = "0";
            this.lblPendingCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(5, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "⏳ Chờ thanh toán";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat4
            // 
            this.pnlStat4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.pnlStat4.Controls.Add(this.lblCancelledCount);
            this.pnlStat4.Controls.Add(this.label7);
            this.pnlStat4.Location = new System.Drawing.Point(490, 10);
            this.pnlStat4.Margin = new System.Windows.Forms.Padding(5);
            this.pnlStat4.Name = "pnlStat4";
            this.pnlStat4.Size = new System.Drawing.Size(150, 60);
            this.pnlStat4.TabIndex = 3;
            // 
            // lblCancelledCount
            // 
            this.lblCancelledCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCancelledCount.ForeColor = System.Drawing.Color.White;
            this.lblCancelledCount.Location = new System.Drawing.Point(5, 25);
            this.lblCancelledCount.Name = "lblCancelledCount";
            this.lblCancelledCount.Size = new System.Drawing.Size(140, 30);
            this.lblCancelledCount.TabIndex = 1;
            this.lblCancelledCount.Text = "0";
            this.lblCancelledCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(5, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "🚫 Đã hủy";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat5
            // 
            this.pnlStat5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.pnlStat5.Controls.Add(this.lblTotalRevenue);
            this.pnlStat5.Controls.Add(this.label9);
            this.pnlStat5.Location = new System.Drawing.Point(650, 10);
            this.pnlStat5.Margin = new System.Windows.Forms.Padding(5);
            this.pnlStat5.Name = "pnlStat5";
            this.pnlStat5.Size = new System.Drawing.Size(150, 60);
            this.pnlStat5.TabIndex = 4;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.White;
            this.lblTotalRevenue.Location = new System.Drawing.Point(5, 25);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(140, 30);
            this.lblTotalRevenue.TabIndex = 1;
            this.lblTotalRevenue.Text = "0 VNĐ";
            this.lblTotalRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(5, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "💰 Doanh thu";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat6
            // 
            this.pnlStat6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.pnlStat6.Controls.Add(this.lblCancelledAmount);
            this.pnlStat6.Controls.Add(this.label11);
            this.pnlStat6.Location = new System.Drawing.Point(810, 10);
            this.pnlStat6.Margin = new System.Windows.Forms.Padding(5);
            this.pnlStat6.Name = "pnlStat6";
            this.pnlStat6.Size = new System.Drawing.Size(150, 60);
            this.pnlStat6.TabIndex = 5;
            // 
            // lblCancelledAmount
            // 
            this.lblCancelledAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCancelledAmount.ForeColor = System.Drawing.Color.White;
            this.lblCancelledAmount.Location = new System.Drawing.Point(5, 25);
            this.lblCancelledAmount.Name = "lblCancelledAmount";
            this.lblCancelledAmount.Size = new System.Drawing.Size(140, 30);
            this.lblCancelledAmount.TabIndex = 1;
            this.lblCancelledAmount.Text = "0 VNĐ";
            this.lblCancelledAmount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(5, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "Đã hủy";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoices.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.dgvInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoices.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.dgvInvoices.Location = new System.Drawing.Point(0, 185);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.ReadOnly = true;
            this.dgvInvoices.RowHeadersVisible = false;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(980, 415);
            this.dgvInvoices.TabIndex = 3;
            // 
            // ReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.dgvInvoices);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ReportForm";
            this.Text = "Báo cáo Hóa đơn";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlStat1.ResumeLayout(false);
            this.pnlStat2.ResumeLayout(false);
            this.pnlStat3.ResumeLayout(false);
            this.pnlStat4.ResumeLayout(false);
            this.pnlStat5.ResumeLayout(false);
            this.pnlStat6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnViewDetails;
        private System.Windows.Forms.Button btnCancelInvoice;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.FlowLayoutPanel pnlSummary;
        private System.Windows.Forms.Panel pnlStat1;
        private System.Windows.Forms.Label lblTotalInvoices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlStat2;
        private System.Windows.Forms.Label lblCompletedCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlStat3;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlStat4;
        private System.Windows.Forms.Label lblCancelledCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlStat5;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlStat6;
        private System.Windows.Forms.Label lblCancelledAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvInvoices;
    }
}
