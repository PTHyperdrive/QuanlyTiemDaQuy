namespace QuanLyTiemDaQuy.Forms
{
    partial class SupplierForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSuppliers = new System.Windows.Forms.TabPage();
            this.splitSupplier = new System.Windows.Forms.SplitContainer();
            this.pnlSupplierList = new System.Windows.Forms.Panel();
            this.dgvSuppliers = new System.Windows.Forms.DataGridView();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tabCertificates = new System.Windows.Forms.TabPage();
            this.splitCert = new System.Windows.Forms.SplitContainer();
            this.pnlCertList = new System.Windows.Forms.Panel();
            this.dgvCertificates = new System.Windows.Forms.DataGridView();
            this.pnlCertHeader = new System.Windows.Forms.Panel();
            this.btnDeleteCert = new System.Windows.Forms.Button();
            this.lblCertCount = new System.Windows.Forms.Label();
            this.grpCertCheck = new System.Windows.Forms.GroupBox();
            this.lblCertStatus = new System.Windows.Forms.Label();
            this.btnCheckCert = new System.Windows.Forms.Button();
            this.txtCertCheck = new System.Windows.Forms.TextBox();
            this.lblCertCheck = new System.Windows.Forms.Label();
            this.grpAddCert = new System.Windows.Forms.GroupBox();
            this.btnAddCert = new System.Windows.Forms.Button();
            this.dtpCertDate = new System.Windows.Forms.DateTimePicker();
            this.lblCertDate = new System.Windows.Forms.Label();
            this.txtNewCertIssuer = new System.Windows.Forms.TextBox();
            this.lblNewCertIssuer = new System.Windows.Forms.Label();
            this.txtNewCertCode = new System.Windows.Forms.TextBox();
            this.lblNewCertCode = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabSuppliers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitSupplier)).BeginInit();
            this.splitSupplier.Panel1.SuspendLayout();
            this.splitSupplier.Panel2.SuspendLayout();
            this.splitSupplier.SuspendLayout();
            this.pnlSupplierList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).BeginInit();
            this.pnlSearch.SuspendLayout();
            this.grpDetails.SuspendLayout();
            this.tabCertificates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitCert)).BeginInit();
            this.splitCert.Panel1.SuspendLayout();
            this.splitCert.Panel2.SuspendLayout();
            this.splitCert.SuspendLayout();
            this.pnlCertList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificates)).BeginInit();
            this.pnlCertHeader.SuspendLayout();
            this.grpCertCheck.SuspendLayout();
            this.grpAddCert.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(900, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🏢 NHÀ CUNG CẤP & CHỨNG CHỈ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSuppliers);
            this.tabControl.Controls.Add(this.tabCertificates);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 50);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(900, 500);
            this.tabControl.TabIndex = 1;
            // 
            // tabSuppliers
            // 
            this.tabSuppliers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.tabSuppliers.Controls.Add(this.splitSupplier);
            this.tabSuppliers.Location = new System.Drawing.Point(4, 28);
            this.tabSuppliers.Name = "tabSuppliers";
            this.tabSuppliers.Padding = new System.Windows.Forms.Padding(3);
            this.tabSuppliers.Size = new System.Drawing.Size(892, 468);
            this.tabSuppliers.TabIndex = 0;
            this.tabSuppliers.Text = "🏢 Nhà cung cấp";
            // 
            // splitSupplier
            // 
            this.splitSupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitSupplier.Location = new System.Drawing.Point(3, 3);
            this.splitSupplier.Name = "splitSupplier";
            // 
            // splitSupplier.Panel1
            // 
            this.splitSupplier.Panel1.Controls.Add(this.pnlSupplierList);
            this.splitSupplier.Panel1.Controls.Add(this.pnlSearch);
            // 
            // splitSupplier.Panel2
            // 
            this.splitSupplier.Panel2.Controls.Add(this.grpDetails);
            this.splitSupplier.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitSupplier.Size = new System.Drawing.Size(886, 462);
            this.splitSupplier.SplitterDistance = 550;
            this.splitSupplier.TabIndex = 0;
            // 
            // pnlSupplierList
            // 
            this.pnlSupplierList.Controls.Add(this.dgvSuppliers);
            this.pnlSupplierList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSupplierList.Location = new System.Drawing.Point(0, 40);
            this.pnlSupplierList.Name = "pnlSupplierList";
            this.pnlSupplierList.Padding = new System.Windows.Forms.Padding(5);
            this.pnlSupplierList.Size = new System.Drawing.Size(550, 422);
            this.pnlSupplierList.TabIndex = 1;
            // 
            // dgvSuppliers
            // 
            this.dgvSuppliers.AllowUserToAddRows = false;
            this.dgvSuppliers.AllowUserToDeleteRows = false;
            this.dgvSuppliers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSuppliers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.dgvSuppliers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSuppliers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSuppliers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.dgvSuppliers.Location = new System.Drawing.Point(5, 5);
            this.dgvSuppliers.MultiSelect = false;
            this.dgvSuppliers.Name = "dgvSuppliers";
            this.dgvSuppliers.ReadOnly = true;
            this.dgvSuppliers.RowHeadersVisible = false;
            this.dgvSuppliers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSuppliers.Size = new System.Drawing.Size(540, 412);
            this.dgvSuppliers.TabIndex = 0;
            this.dgvSuppliers.SelectionChanged += new System.EventHandler(this.dgvSuppliers_SelectionChanged);
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.lblSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(5);
            this.pnlSearch.Size = new System.Drawing.Size(550, 40);
            this.pnlSearch.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(80, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 25);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(10, 11);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(63, 19);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "🔍 Tìm:";
            // 
            // grpDetails
            // 
            this.grpDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.grpDetails.Controls.Add(this.btnClear);
            this.grpDetails.Controls.Add(this.btnDelete);
            this.grpDetails.Controls.Add(this.btnUpdate);
            this.grpDetails.Controls.Add(this.btnAdd);
            this.grpDetails.Controls.Add(this.txtAddress);
            this.grpDetails.Controls.Add(this.lblAddress);
            this.grpDetails.Controls.Add(this.txtEmail);
            this.grpDetails.Controls.Add(this.lblEmail);
            this.grpDetails.Controls.Add(this.txtPhone);
            this.grpDetails.Controls.Add(this.lblPhone);
            this.grpDetails.Controls.Add(this.txtContact);
            this.grpDetails.Controls.Add(this.lblContact);
            this.grpDetails.Controls.Add(this.txtName);
            this.grpDetails.Controls.Add(this.lblName);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.ForeColor = System.Drawing.Color.White;
            this.grpDetails.Location = new System.Drawing.Point(10, 10);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(312, 442);
            this.grpDetails.TabIndex = 0;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Nhà cung cấp - Thêm mới";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(15, 390);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(280, 35);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "🔄 Làm mới";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(200, 340);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(95, 35);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "🗑 Xóa";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(105, 340);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(85, 35);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "💾 Sửa";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(15, 340);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 35);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "➕ Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.ForeColor = System.Drawing.Color.White;
            this.txtAddress.Location = new System.Drawing.Point(15, 260);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(280, 60);
            this.txtAddress.TabIndex = 9;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(15, 240);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(54, 19);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "Địa chỉ:";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(15, 205);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(280, 25);
            this.txtEmail.TabIndex = 7;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(15, 185);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 19);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.ForeColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(15, 150);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(280, 25);
            this.txtPhone.TabIndex = 5;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(15, 130);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(33, 19);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "SĐT:";
            // 
            // txtContact
            // 
            this.txtContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.ForeColor = System.Drawing.Color.White;
            this.txtContact.Location = new System.Drawing.Point(15, 95);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(280, 25);
            this.txtContact.TabIndex = 3;
            // 
            // lblContact
            // 
            this.lblContact.AutoSize = true;
            this.lblContact.Location = new System.Drawing.Point(15, 75);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new System.Drawing.Size(94, 19);
            this.lblContact.TabIndex = 2;
            this.lblContact.Text = "Người liên hệ:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(15, 45);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(280, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 19);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên NCC: *";
            // 
            // tabCertificates
            // 
            this.tabCertificates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.tabCertificates.Controls.Add(this.splitCert);
            this.tabCertificates.Location = new System.Drawing.Point(4, 28);
            this.tabCertificates.Name = "tabCertificates";
            this.tabCertificates.Padding = new System.Windows.Forms.Padding(3);
            this.tabCertificates.Size = new System.Drawing.Size(892, 468);
            this.tabCertificates.TabIndex = 1;
            this.tabCertificates.Text = "📜 Chứng chỉ đá quý";
            // 
            // splitCert
            // 
            this.splitCert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCert.Location = new System.Drawing.Point(3, 3);
            this.splitCert.Name = "splitCert";
            // 
            // splitCert.Panel1
            // 
            this.splitCert.Panel1.Controls.Add(this.pnlCertList);
            this.splitCert.Panel1.Controls.Add(this.pnlCertHeader);
            // 
            // splitCert.Panel2
            // 
            this.splitCert.Panel2.Controls.Add(this.grpCertCheck);
            this.splitCert.Panel2.Controls.Add(this.grpAddCert);
            this.splitCert.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitCert.Size = new System.Drawing.Size(886, 462);
            this.splitCert.SplitterDistance = 500;
            this.splitCert.TabIndex = 0;
            // 
            // pnlCertList
            // 
            this.pnlCertList.Controls.Add(this.dgvCertificates);
            this.pnlCertList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCertList.Location = new System.Drawing.Point(0, 40);
            this.pnlCertList.Name = "pnlCertList";
            this.pnlCertList.Padding = new System.Windows.Forms.Padding(5);
            this.pnlCertList.Size = new System.Drawing.Size(500, 422);
            this.pnlCertList.TabIndex = 1;
            // 
            // dgvCertificates
            // 
            this.dgvCertificates.AllowUserToAddRows = false;
            this.dgvCertificates.AllowUserToDeleteRows = false;
            this.dgvCertificates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCertificates.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.dgvCertificates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCertificates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCertificates.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.dgvCertificates.Location = new System.Drawing.Point(5, 5);
            this.dgvCertificates.MultiSelect = false;
            this.dgvCertificates.Name = "dgvCertificates";
            this.dgvCertificates.ReadOnly = true;
            this.dgvCertificates.RowHeadersVisible = false;
            this.dgvCertificates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCertificates.Size = new System.Drawing.Size(490, 412);
            this.dgvCertificates.TabIndex = 0;
            // 
            // pnlCertHeader
            // 
            this.pnlCertHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlCertHeader.Controls.Add(this.btnDeleteCert);
            this.pnlCertHeader.Controls.Add(this.lblCertCount);
            this.pnlCertHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCertHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlCertHeader.Name = "pnlCertHeader";
            this.pnlCertHeader.Size = new System.Drawing.Size(500, 40);
            this.pnlCertHeader.TabIndex = 0;
            // 
            // btnDeleteCert
            // 
            this.btnDeleteCert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDeleteCert.FlatAppearance.BorderSize = 0;
            this.btnDeleteCert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteCert.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCert.Location = new System.Drawing.Point(400, 5);
            this.btnDeleteCert.Name = "btnDeleteCert";
            this.btnDeleteCert.Size = new System.Drawing.Size(90, 30);
            this.btnDeleteCert.TabIndex = 1;
            this.btnDeleteCert.Text = "🗑 Xóa";
            this.btnDeleteCert.UseVisualStyleBackColor = false;
            this.btnDeleteCert.Click += new System.EventHandler(this.btnDeleteCert_Click);
            // 
            // lblCertCount
            // 
            this.lblCertCount.AutoSize = true;
            this.lblCertCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCertCount.ForeColor = System.Drawing.Color.White;
            this.lblCertCount.Location = new System.Drawing.Point(10, 10);
            this.lblCertCount.Name = "lblCertCount";
            this.lblCertCount.Size = new System.Drawing.Size(114, 19);
            this.lblCertCount.TabIndex = 0;
            this.lblCertCount.Text = "📜 Danh sách CC";
            // 
            // grpCertCheck
            // 
            this.grpCertCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.grpCertCheck.Controls.Add(this.lblCertStatus);
            this.grpCertCheck.Controls.Add(this.btnCheckCert);
            this.grpCertCheck.Controls.Add(this.txtCertCheck);
            this.grpCertCheck.Controls.Add(this.lblCertCheck);
            this.grpCertCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpCertCheck.ForeColor = System.Drawing.Color.White;
            this.grpCertCheck.Location = new System.Drawing.Point(10, 10);
            this.grpCertCheck.Name = "grpCertCheck";
            this.grpCertCheck.Size = new System.Drawing.Size(362, 140);
            this.grpCertCheck.TabIndex = 0;
            this.grpCertCheck.TabStop = false;
            this.grpCertCheck.Text = "🔍 Kiểm tra chứng chỉ";
            // 
            // lblCertStatus
            // 
            this.lblCertStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCertStatus.ForeColor = System.Drawing.Color.Silver;
            this.lblCertStatus.Location = new System.Drawing.Point(15, 90);
            this.lblCertStatus.Name = "lblCertStatus";
            this.lblCertStatus.Size = new System.Drawing.Size(330, 40);
            this.lblCertStatus.TabIndex = 3;
            this.lblCertStatus.Text = "Nhập mã chứng chỉ và nhấn Kiểm tra";
            // 
            // btnCheckCert
            // 
            this.btnCheckCert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCheckCert.FlatAppearance.BorderSize = 0;
            this.btnCheckCert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckCert.Location = new System.Drawing.Point(250, 50);
            this.btnCheckCert.Name = "btnCheckCert";
            this.btnCheckCert.Size = new System.Drawing.Size(95, 30);
            this.btnCheckCert.TabIndex = 2;
            this.btnCheckCert.Text = "🔍 Kiểm tra";
            this.btnCheckCert.UseVisualStyleBackColor = false;
            this.btnCheckCert.Click += new System.EventHandler(this.btnCheckCert_Click);
            // 
            // txtCertCheck
            // 
            this.txtCertCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtCertCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCertCheck.ForeColor = System.Drawing.Color.White;
            this.txtCertCheck.Location = new System.Drawing.Point(15, 53);
            this.txtCertCheck.Name = "txtCertCheck";
            this.txtCertCheck.Size = new System.Drawing.Size(225, 25);
            this.txtCertCheck.TabIndex = 1;
            // 
            // lblCertCheck
            // 
            this.lblCertCheck.AutoSize = true;
            this.lblCertCheck.Location = new System.Drawing.Point(15, 30);
            this.lblCertCheck.Name = "lblCertCheck";
            this.lblCertCheck.Size = new System.Drawing.Size(107, 19);
            this.lblCertCheck.TabIndex = 0;
            this.lblCertCheck.Text = "Nhập mã CC để kiểm tra:";
            // 
            // grpAddCert
            // 
            this.grpAddCert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.grpAddCert.Controls.Add(this.btnAddCert);
            this.grpAddCert.Controls.Add(this.dtpCertDate);
            this.grpAddCert.Controls.Add(this.lblCertDate);
            this.grpAddCert.Controls.Add(this.txtNewCertIssuer);
            this.grpAddCert.Controls.Add(this.lblNewCertIssuer);
            this.grpAddCert.Controls.Add(this.txtNewCertCode);
            this.grpAddCert.Controls.Add(this.lblNewCertCode);
            this.grpAddCert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpAddCert.ForeColor = System.Drawing.Color.White;
            this.grpAddCert.Location = new System.Drawing.Point(10, 260);
            this.grpAddCert.Name = "grpAddCert";
            this.grpAddCert.Size = new System.Drawing.Size(362, 192);
            this.grpAddCert.TabIndex = 1;
            this.grpAddCert.TabStop = false;
            this.grpAddCert.Text = "➕ Thêm chứng chỉ mới";
            // 
            // btnAddCert
            // 
            this.btnAddCert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddCert.FlatAppearance.BorderSize = 0;
            this.btnAddCert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCert.Location = new System.Drawing.Point(15, 145);
            this.btnAddCert.Name = "btnAddCert";
            this.btnAddCert.Size = new System.Drawing.Size(330, 35);
            this.btnAddCert.TabIndex = 6;
            this.btnAddCert.Text = "✅ Đăng ký chứng chỉ";
            this.btnAddCert.UseVisualStyleBackColor = false;
            this.btnAddCert.Click += new System.EventHandler(this.btnAddCert_Click);
            // 
            // dtpCertDate
            // 
            this.dtpCertDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCertDate.Location = new System.Drawing.Point(105, 110);
            this.dtpCertDate.Name = "dtpCertDate";
            this.dtpCertDate.Size = new System.Drawing.Size(120, 25);
            this.dtpCertDate.TabIndex = 5;
            // 
            // lblCertDate
            // 
            this.lblCertDate.AutoSize = true;
            this.lblCertDate.Location = new System.Drawing.Point(15, 113);
            this.lblCertDate.Name = "lblCertDate";
            this.lblCertDate.Size = new System.Drawing.Size(60, 19);
            this.lblCertDate.TabIndex = 4;
            this.lblCertDate.Text = "Ngày cấp:";
            // 
            // txtNewCertIssuer
            // 
            this.txtNewCertIssuer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtNewCertIssuer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewCertIssuer.ForeColor = System.Drawing.Color.White;
            this.txtNewCertIssuer.Location = new System.Drawing.Point(105, 75);
            this.txtNewCertIssuer.Name = "txtNewCertIssuer";
            this.txtNewCertIssuer.Size = new System.Drawing.Size(240, 25);
            this.txtNewCertIssuer.TabIndex = 3;
            // 
            // lblNewCertIssuer
            // 
            this.lblNewCertIssuer.AutoSize = true;
            this.lblNewCertIssuer.Location = new System.Drawing.Point(15, 78);
            this.lblNewCertIssuer.Name = "lblNewCertIssuer";
            this.lblNewCertIssuer.Size = new System.Drawing.Size(79, 19);
            this.lblNewCertIssuer.TabIndex = 2;
            this.lblNewCertIssuer.Text = "Đơn vị cấp: *";
            // 
            // txtNewCertCode
            // 
            this.txtNewCertCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtNewCertCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewCertCode.ForeColor = System.Drawing.Color.White;
            this.txtNewCertCode.Location = new System.Drawing.Point(105, 40);
            this.txtNewCertCode.Name = "txtNewCertCode";
            this.txtNewCertCode.Size = new System.Drawing.Size(240, 25);
            this.txtNewCertCode.TabIndex = 1;
            // 
            // lblNewCertCode
            // 
            this.lblNewCertCode.AutoSize = true;
            this.lblNewCertCode.Location = new System.Drawing.Point(15, 43);
            this.lblNewCertCode.Name = "lblNewCertCode";
            this.lblNewCertCode.Size = new System.Drawing.Size(54, 19);
            this.lblNewCertCode.TabIndex = 0;
            this.lblNewCertCode.Text = "Mã CC: *";
            // 
            // SupplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "SupplierForm";
            this.Text = "Quản lý Nhà cung cấp & Chứng chỉ";
            this.Load += new System.EventHandler(this.SupplierForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabSuppliers.ResumeLayout(false);
            this.splitSupplier.Panel1.ResumeLayout(false);
            this.splitSupplier.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitSupplier)).EndInit();
            this.splitSupplier.ResumeLayout(false);
            this.pnlSupplierList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSuppliers)).EndInit();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.tabCertificates.ResumeLayout(false);
            this.splitCert.Panel1.ResumeLayout(false);
            this.splitCert.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCert)).EndInit();
            this.splitCert.ResumeLayout(false);
            this.pnlCertList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificates)).EndInit();
            this.pnlCertHeader.ResumeLayout(false);
            this.pnlCertHeader.PerformLayout();
            this.grpCertCheck.ResumeLayout(false);
            this.grpCertCheck.PerformLayout();
            this.grpAddCert.ResumeLayout(false);
            this.grpAddCert.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSuppliers;
        private System.Windows.Forms.TabPage tabCertificates;
        private System.Windows.Forms.SplitContainer splitSupplier;
        private System.Windows.Forms.Panel pnlSupplierList;
        private System.Windows.Forms.DataGridView dgvSuppliers;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.SplitContainer splitCert;
        private System.Windows.Forms.Panel pnlCertList;
        private System.Windows.Forms.DataGridView dgvCertificates;
        private System.Windows.Forms.Panel pnlCertHeader;
        private System.Windows.Forms.Label lblCertCount;
        private System.Windows.Forms.Button btnDeleteCert;
        private System.Windows.Forms.GroupBox grpCertCheck;
        private System.Windows.Forms.TextBox txtCertCheck;
        private System.Windows.Forms.Label lblCertCheck;
        private System.Windows.Forms.Button btnCheckCert;
        private System.Windows.Forms.Label lblCertStatus;
        private System.Windows.Forms.GroupBox grpAddCert;
        private System.Windows.Forms.TextBox txtNewCertCode;
        private System.Windows.Forms.Label lblNewCertCode;
        private System.Windows.Forms.TextBox txtNewCertIssuer;
        private System.Windows.Forms.Label lblNewCertIssuer;
        private System.Windows.Forms.DateTimePicker dtpCertDate;
        private System.Windows.Forms.Label lblCertDate;
        private System.Windows.Forms.Button btnAddCert;
    }
}
