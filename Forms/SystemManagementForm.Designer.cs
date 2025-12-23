namespace QuanLyTiemDaQuy.Forms
{
    partial class SystemManagementForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabEmployees = new System.Windows.Forms.TabPage();
            this.tabBranches = new System.Windows.Forms.TabPage();
            
            // Tab Employees Controls
            this.pnlEmployeeDetails = new System.Windows.Forms.Panel();
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.lblEmpName = new System.Windows.Forms.Label();
            this.txtEmpName = new System.Windows.Forms.TextBox();
            this.lblEmpUsername = new System.Windows.Forms.Label();
            this.txtEmpUsername = new System.Windows.Forms.TextBox();
            this.lblEmpRole = new System.Windows.Forms.Label();
            this.cboEmpRole = new System.Windows.Forms.ComboBox();
            this.lblEmpPhone = new System.Windows.Forms.Label();
            this.txtEmpPhone = new System.Windows.Forms.TextBox();
            this.lblEmpEmail = new System.Windows.Forms.Label();
            this.txtEmpEmail = new System.Windows.Forms.TextBox();
            this.lblEmpBranch = new System.Windows.Forms.Label();
            this.cboEmpBranch = new System.Windows.Forms.ComboBox();
            this.chkMustChangePassword = new System.Windows.Forms.CheckBox();
            this.chkEmpActive = new System.Windows.Forms.CheckBox();
            this.btnEmpAdd = new System.Windows.Forms.Button();
            this.btnEmpUpdate = new System.Windows.Forms.Button();
            this.btnEmpToggleActive = new System.Windows.Forms.Button();
            this.btnEmpSetPassword = new System.Windows.Forms.Button();
            this.btnEmpClear = new System.Windows.Forms.Button();
            this.lblEmpStatus = new System.Windows.Forms.Label();
            
            // Tab Branches Controls
            this.splitBranches = new System.Windows.Forms.SplitContainer();
            this.pnlBranchDetails = new System.Windows.Forms.Panel();
            this.dgvBranches = new System.Windows.Forms.DataGridView();
            this.dgvBranchEmployees = new System.Windows.Forms.DataGridView();
            this.lblBranchCode = new System.Windows.Forms.Label();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.lblBranchName = new System.Windows.Forms.Label();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.lblBranchAddress = new System.Windows.Forms.Label();
            this.txtBranchAddress = new System.Windows.Forms.TextBox();
            this.lblBranchPhone = new System.Windows.Forms.Label();
            this.txtBranchPhone = new System.Windows.Forms.TextBox();
            this.chkBranchActive = new System.Windows.Forms.CheckBox();
            this.btnBranchAdd = new System.Windows.Forms.Button();
            this.btnBranchUpdate = new System.Windows.Forms.Button();
            this.btnBranchToggleActive = new System.Windows.Forms.Button();
            this.btnBranchClear = new System.Windows.Forms.Button();
            this.btnTransferEmployee = new System.Windows.Forms.Button();
            this.lblBranchStatus = new System.Windows.Forms.Label();
            this.lblBranchEmployees = new System.Windows.Forms.Label();
            
            this.tabControl.SuspendLayout();
            this.tabEmployees.SuspendLayout();
            this.tabBranches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranchEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitBranches)).BeginInit();
            this.splitBranches.Panel1.SuspendLayout();
            this.splitBranches.Panel2.SuspendLayout();
            this.splitBranches.SuspendLayout();
            this.pnlEmployeeDetails.SuspendLayout();
            this.pnlBranchDetails.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabEmployees);
            this.tabControl.Controls.Add(this.tabBranches);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(980, 640);
            this.tabControl.TabIndex = 0;
            
            // 
            // tabEmployees
            // 
            this.tabEmployees.BackColor = System.Drawing.Color.FromArgb(32, 32, 48);
            this.tabEmployees.Controls.Add(this.dgvEmployees);
            this.tabEmployees.Controls.Add(this.pnlEmployeeDetails);
            this.tabEmployees.Location = new System.Drawing.Point(4, 29);
            this.tabEmployees.Name = "tabEmployees";
            this.tabEmployees.Padding = new System.Windows.Forms.Padding(10);
            this.tabEmployees.Size = new System.Drawing.Size(972, 607);
            this.tabEmployees.TabIndex = 0;
            this.tabEmployees.Text = "👤 Quản lý Tài khoản";
            
            // 
            // dgvEmployees
            // 
            this.dgvEmployees.AllowUserToAddRows = false;
            this.dgvEmployees.AllowUserToDeleteRows = false;
            this.dgvEmployees.BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 68);
            this.dgvEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.Location = new System.Drawing.Point(10, 10);
            this.dgvEmployees.MultiSelect = false;
            this.dgvEmployees.Name = "dgvEmployees";
            this.dgvEmployees.ReadOnly = true;
            this.dgvEmployees.RowHeadersWidth = 51;
            this.dgvEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEmployees.Size = new System.Drawing.Size(622, 587);
            this.dgvEmployees.TabIndex = 0;
            this.dgvEmployees.SelectionChanged += new System.EventHandler(this.dgvEmployees_SelectionChanged);
            
            // 
            // pnlEmployeeDetails
            // 
            this.pnlEmployeeDetails.BackColor = System.Drawing.Color.FromArgb(45, 45, 68);
            this.pnlEmployeeDetails.Controls.Add(this.lblEmpName);
            this.pnlEmployeeDetails.Controls.Add(this.txtEmpName);
            this.pnlEmployeeDetails.Controls.Add(this.lblEmpUsername);
            this.pnlEmployeeDetails.Controls.Add(this.txtEmpUsername);
            this.pnlEmployeeDetails.Controls.Add(this.lblEmpRole);
            this.pnlEmployeeDetails.Controls.Add(this.cboEmpRole);
            this.pnlEmployeeDetails.Controls.Add(this.lblEmpPhone);
            this.pnlEmployeeDetails.Controls.Add(this.txtEmpPhone);
            this.pnlEmployeeDetails.Controls.Add(this.lblEmpEmail);
            this.pnlEmployeeDetails.Controls.Add(this.txtEmpEmail);
            this.pnlEmployeeDetails.Controls.Add(this.lblEmpBranch);
            this.pnlEmployeeDetails.Controls.Add(this.cboEmpBranch);
            this.pnlEmployeeDetails.Controls.Add(this.chkMustChangePassword);
            this.pnlEmployeeDetails.Controls.Add(this.chkEmpActive);
            this.pnlEmployeeDetails.Controls.Add(this.btnEmpAdd);
            this.pnlEmployeeDetails.Controls.Add(this.btnEmpUpdate);
            this.pnlEmployeeDetails.Controls.Add(this.btnEmpToggleActive);
            this.pnlEmployeeDetails.Controls.Add(this.btnEmpSetPassword);
            this.pnlEmployeeDetails.Controls.Add(this.btnEmpClear);
            this.pnlEmployeeDetails.Controls.Add(this.lblEmpStatus);
            this.pnlEmployeeDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlEmployeeDetails.Location = new System.Drawing.Point(642, 10);
            this.pnlEmployeeDetails.Name = "pnlEmployeeDetails";
            this.pnlEmployeeDetails.Padding = new System.Windows.Forms.Padding(15);
            this.pnlEmployeeDetails.Size = new System.Drawing.Size(320, 587);
            this.pnlEmployeeDetails.TabIndex = 1;
            
            // Employee Detail Labels and Inputs
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.ForeColor = System.Drawing.Color.White;
            this.lblEmpName.Location = new System.Drawing.Point(15, 20);
            this.lblEmpName.Text = "Họ tên: *";
            
            this.txtEmpName.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpName.ForeColor = System.Drawing.Color.White;
            this.txtEmpName.Location = new System.Drawing.Point(15, 40);
            this.txtEmpName.Size = new System.Drawing.Size(290, 27);
            
            this.lblEmpUsername.AutoSize = true;
            this.lblEmpUsername.ForeColor = System.Drawing.Color.White;
            this.lblEmpUsername.Location = new System.Drawing.Point(15, 75);
            this.lblEmpUsername.Text = "Tên đăng nhập: *";
            
            this.txtEmpUsername.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtEmpUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpUsername.ForeColor = System.Drawing.Color.White;
            this.txtEmpUsername.Location = new System.Drawing.Point(15, 95);
            this.txtEmpUsername.Size = new System.Drawing.Size(290, 27);
            
            this.lblEmpRole.AutoSize = true;
            this.lblEmpRole.ForeColor = System.Drawing.Color.White;
            this.lblEmpRole.Location = new System.Drawing.Point(15, 130);
            this.lblEmpRole.Text = "Vai trò:";
            
            this.cboEmpRole.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.cboEmpRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpRole.ForeColor = System.Drawing.Color.White;
            this.cboEmpRole.Location = new System.Drawing.Point(15, 150);
            this.cboEmpRole.Size = new System.Drawing.Size(290, 27);
            
            this.lblEmpPhone.AutoSize = true;
            this.lblEmpPhone.ForeColor = System.Drawing.Color.White;
            this.lblEmpPhone.Location = new System.Drawing.Point(15, 185);
            this.lblEmpPhone.Text = "Số điện thoại:";
            
            this.txtEmpPhone.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtEmpPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpPhone.ForeColor = System.Drawing.Color.White;
            this.txtEmpPhone.Location = new System.Drawing.Point(15, 205);
            this.txtEmpPhone.Size = new System.Drawing.Size(290, 27);
            
            this.lblEmpEmail.AutoSize = true;
            this.lblEmpEmail.ForeColor = System.Drawing.Color.White;
            this.lblEmpEmail.Location = new System.Drawing.Point(15, 240);
            this.lblEmpEmail.Text = "Email:";
            
            this.txtEmpEmail.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtEmpEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmpEmail.Location = new System.Drawing.Point(15, 260);
            this.txtEmpEmail.Size = new System.Drawing.Size(290, 27);
            
            this.lblEmpBranch.AutoSize = true;
            this.lblEmpBranch.ForeColor = System.Drawing.Color.White;
            this.lblEmpBranch.Location = new System.Drawing.Point(15, 295);
            this.lblEmpBranch.Text = "Chi nhánh:";
            
            this.cboEmpBranch.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.cboEmpBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmpBranch.ForeColor = System.Drawing.Color.White;
            this.cboEmpBranch.Location = new System.Drawing.Point(15, 315);
            this.cboEmpBranch.Size = new System.Drawing.Size(290, 27);
            
            this.chkMustChangePassword.AutoSize = true;
            this.chkMustChangePassword.ForeColor = System.Drawing.Color.Orange;
            this.chkMustChangePassword.Location = new System.Drawing.Point(15, 355);
            this.chkMustChangePassword.Text = "Yêu cầu đổi MK khi đăng nhập";
            
            this.chkEmpActive.AutoSize = true;
            this.chkEmpActive.Checked = true;
            this.chkEmpActive.ForeColor = System.Drawing.Color.LightGreen;
            this.chkEmpActive.Location = new System.Drawing.Point(15, 380);
            this.chkEmpActive.Text = "Đang hoạt động";
            
            // Employee Buttons
            this.btnEmpAdd.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnEmpAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpAdd.ForeColor = System.Drawing.Color.White;
            this.btnEmpAdd.Location = new System.Drawing.Point(15, 415);
            this.btnEmpAdd.Size = new System.Drawing.Size(140, 35);
            this.btnEmpAdd.Text = "➕ Thêm mới";
            this.btnEmpAdd.Click += new System.EventHandler(this.btnEmpAdd_Click);
            
            this.btnEmpUpdate.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnEmpUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpUpdate.ForeColor = System.Drawing.Color.White;
            this.btnEmpUpdate.Location = new System.Drawing.Point(165, 415);
            this.btnEmpUpdate.Size = new System.Drawing.Size(140, 35);
            this.btnEmpUpdate.Text = "💾 Cập nhật";
            this.btnEmpUpdate.Click += new System.EventHandler(this.btnEmpUpdate_Click);
            
            this.btnEmpSetPassword.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.btnEmpSetPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpSetPassword.ForeColor = System.Drawing.Color.Black;
            this.btnEmpSetPassword.Location = new System.Drawing.Point(15, 460);
            this.btnEmpSetPassword.Size = new System.Drawing.Size(140, 35);
            this.btnEmpSetPassword.Text = "🔑 Đặt mật khẩu";
            this.btnEmpSetPassword.Click += new System.EventHandler(this.btnEmpSetPassword_Click);
            
            this.btnEmpToggleActive.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnEmpToggleActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpToggleActive.ForeColor = System.Drawing.Color.White;
            this.btnEmpToggleActive.Location = new System.Drawing.Point(165, 460);
            this.btnEmpToggleActive.Size = new System.Drawing.Size(140, 35);
            this.btnEmpToggleActive.Text = "🚫 Vô hiệu hóa";
            this.btnEmpToggleActive.Click += new System.EventHandler(this.btnEmpToggleActive_Click);
            
            this.btnEmpClear.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnEmpClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpClear.ForeColor = System.Drawing.Color.White;
            this.btnEmpClear.Location = new System.Drawing.Point(15, 505);
            this.btnEmpClear.Size = new System.Drawing.Size(290, 35);
            this.btnEmpClear.Text = "🔄 Làm mới";
            this.btnEmpClear.Click += new System.EventHandler(this.btnEmpClear_Click);
            
            this.lblEmpStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblEmpStatus.ForeColor = System.Drawing.Color.Silver;
            this.lblEmpStatus.Location = new System.Drawing.Point(15, 555);
            this.lblEmpStatus.Size = new System.Drawing.Size(290, 25);
            this.lblEmpStatus.Text = "Sẵn sàng";
            
            // 
            // tabBranches
            // 
            this.tabBranches.BackColor = System.Drawing.Color.FromArgb(32, 32, 48);
            this.tabBranches.Controls.Add(this.splitBranches);
            this.tabBranches.Location = new System.Drawing.Point(4, 29);
            this.tabBranches.Name = "tabBranches";
            this.tabBranches.Padding = new System.Windows.Forms.Padding(10);
            this.tabBranches.Size = new System.Drawing.Size(972, 607);
            this.tabBranches.TabIndex = 1;
            this.tabBranches.Text = "🏢 Quản lý Chi nhánh";
            
            // 
            // splitBranches
            // 
            this.splitBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitBranches.Location = new System.Drawing.Point(10, 10);
            this.splitBranches.Name = "splitBranches";
            this.splitBranches.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitBranches.Size = new System.Drawing.Size(952, 587);
            this.splitBranches.SplitterDistance = 280;
            
            // splitBranches.Panel1 - Danh sách chi nhánh + Form
            this.splitBranches.Panel1.Controls.Add(this.dgvBranches);
            this.splitBranches.Panel1.Controls.Add(this.pnlBranchDetails);
            
            // splitBranches.Panel2 - Nhân viên của chi nhánh
            this.splitBranches.Panel2.Controls.Add(this.dgvBranchEmployees);
            this.splitBranches.Panel2.Controls.Add(this.lblBranchEmployees);
            this.splitBranches.Panel2.Controls.Add(this.btnTransferEmployee);
            
            // 
            // dgvBranches
            // 
            this.dgvBranches.AllowUserToAddRows = false;
            this.dgvBranches.AllowUserToDeleteRows = false;
            this.dgvBranches.BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 68);
            this.dgvBranches.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBranches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBranches.Location = new System.Drawing.Point(0, 0);
            this.dgvBranches.MultiSelect = false;
            this.dgvBranches.Name = "dgvBranches";
            this.dgvBranches.ReadOnly = true;
            this.dgvBranches.RowHeadersWidth = 51;
            this.dgvBranches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBranches.Size = new System.Drawing.Size(602, 280);
            this.dgvBranches.TabIndex = 0;
            this.dgvBranches.SelectionChanged += new System.EventHandler(this.dgvBranches_SelectionChanged);
            
            // 
            // pnlBranchDetails
            // 
            this.pnlBranchDetails.BackColor = System.Drawing.Color.FromArgb(45, 45, 68);
            this.pnlBranchDetails.Controls.Add(this.lblBranchCode);
            this.pnlBranchDetails.Controls.Add(this.txtBranchCode);
            this.pnlBranchDetails.Controls.Add(this.lblBranchName);
            this.pnlBranchDetails.Controls.Add(this.txtBranchName);
            this.pnlBranchDetails.Controls.Add(this.lblBranchAddress);
            this.pnlBranchDetails.Controls.Add(this.txtBranchAddress);
            this.pnlBranchDetails.Controls.Add(this.lblBranchPhone);
            this.pnlBranchDetails.Controls.Add(this.txtBranchPhone);
            this.pnlBranchDetails.Controls.Add(this.chkBranchActive);
            this.pnlBranchDetails.Controls.Add(this.btnBranchAdd);
            this.pnlBranchDetails.Controls.Add(this.btnBranchUpdate);
            this.pnlBranchDetails.Controls.Add(this.btnBranchToggleActive);
            this.pnlBranchDetails.Controls.Add(this.btnBranchClear);
            this.pnlBranchDetails.Controls.Add(this.lblBranchStatus);
            this.pnlBranchDetails.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBranchDetails.Location = new System.Drawing.Point(612, 0);
            this.pnlBranchDetails.Name = "pnlBranchDetails";
            this.pnlBranchDetails.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBranchDetails.Size = new System.Drawing.Size(340, 280);
            this.pnlBranchDetails.TabIndex = 1;
            
            // Branch Detail Labels and Inputs
            this.lblBranchCode.AutoSize = true;
            this.lblBranchCode.ForeColor = System.Drawing.Color.White;
            this.lblBranchCode.Location = new System.Drawing.Point(10, 10);
            this.lblBranchCode.Text = "Mã chi nhánh:";
            
            this.txtBranchCode.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtBranchCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchCode.ForeColor = System.Drawing.Color.White;
            this.txtBranchCode.Location = new System.Drawing.Point(10, 30);
            this.txtBranchCode.Size = new System.Drawing.Size(150, 27);
            
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.ForeColor = System.Drawing.Color.White;
            this.lblBranchName.Location = new System.Drawing.Point(170, 10);
            this.lblBranchName.Text = "Tên chi nhánh: *";
            
            this.txtBranchName.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtBranchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchName.ForeColor = System.Drawing.Color.White;
            this.txtBranchName.Location = new System.Drawing.Point(170, 30);
            this.txtBranchName.Size = new System.Drawing.Size(160, 27);
            
            this.lblBranchAddress.AutoSize = true;
            this.lblBranchAddress.ForeColor = System.Drawing.Color.White;
            this.lblBranchAddress.Location = new System.Drawing.Point(10, 65);
            this.lblBranchAddress.Text = "Địa chỉ:";
            
            this.txtBranchAddress.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtBranchAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchAddress.ForeColor = System.Drawing.Color.White;
            this.txtBranchAddress.Location = new System.Drawing.Point(10, 85);
            this.txtBranchAddress.Size = new System.Drawing.Size(320, 27);
            
            this.lblBranchPhone.AutoSize = true;
            this.lblBranchPhone.ForeColor = System.Drawing.Color.White;
            this.lblBranchPhone.Location = new System.Drawing.Point(10, 120);
            this.lblBranchPhone.Text = "Điện thoại:";
            
            this.txtBranchPhone.BackColor = System.Drawing.Color.FromArgb(60, 60, 80);
            this.txtBranchPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchPhone.ForeColor = System.Drawing.Color.White;
            this.txtBranchPhone.Location = new System.Drawing.Point(10, 140);
            this.txtBranchPhone.Size = new System.Drawing.Size(150, 27);
            
            this.chkBranchActive.AutoSize = true;
            this.chkBranchActive.Checked = true;
            this.chkBranchActive.ForeColor = System.Drawing.Color.LightGreen;
            this.chkBranchActive.Location = new System.Drawing.Point(170, 145);
            this.chkBranchActive.Text = "Hoạt động";
            
            // Branch Buttons
            this.btnBranchAdd.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnBranchAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBranchAdd.ForeColor = System.Drawing.Color.White;
            this.btnBranchAdd.Location = new System.Drawing.Point(10, 180);
            this.btnBranchAdd.Size = new System.Drawing.Size(75, 32);
            this.btnBranchAdd.Text = "➕ Thêm";
            this.btnBranchAdd.Click += new System.EventHandler(this.btnBranchAdd_Click);
            
            this.btnBranchUpdate.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            this.btnBranchUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBranchUpdate.ForeColor = System.Drawing.Color.White;
            this.btnBranchUpdate.Location = new System.Drawing.Point(90, 180);
            this.btnBranchUpdate.Size = new System.Drawing.Size(75, 32);
            this.btnBranchUpdate.Text = "💾 Sửa";
            this.btnBranchUpdate.Click += new System.EventHandler(this.btnBranchUpdate_Click);
            
            this.btnBranchToggleActive.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnBranchToggleActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBranchToggleActive.ForeColor = System.Drawing.Color.White;
            this.btnBranchToggleActive.Location = new System.Drawing.Point(170, 180);
            this.btnBranchToggleActive.Size = new System.Drawing.Size(75, 32);
            this.btnBranchToggleActive.Text = "🚫";
            this.btnBranchToggleActive.Click += new System.EventHandler(this.btnBranchToggleActive_Click);
            
            this.btnBranchClear.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnBranchClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBranchClear.ForeColor = System.Drawing.Color.White;
            this.btnBranchClear.Location = new System.Drawing.Point(250, 180);
            this.btnBranchClear.Size = new System.Drawing.Size(80, 32);
            this.btnBranchClear.Text = "🔄 Mới";
            this.btnBranchClear.Click += new System.EventHandler(this.btnBranchClear_Click);
            
            this.lblBranchStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBranchStatus.ForeColor = System.Drawing.Color.Silver;
            this.lblBranchStatus.Location = new System.Drawing.Point(10, 250);
            this.lblBranchStatus.Size = new System.Drawing.Size(320, 20);
            this.lblBranchStatus.Text = "Sẵn sàng";
            
            // 
            // lblBranchEmployees
            // 
            this.lblBranchEmployees.AutoSize = true;
            this.lblBranchEmployees.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBranchEmployees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBranchEmployees.ForeColor = System.Drawing.Color.White;
            this.lblBranchEmployees.Location = new System.Drawing.Point(0, 0);
            this.lblBranchEmployees.Size = new System.Drawing.Size(200, 25);
            this.lblBranchEmployees.Text = "👥 Nhân viên chi nhánh:";
            
            // 
            // dgvBranchEmployees
            // 
            this.dgvBranchEmployees.AllowUserToAddRows = false;
            this.dgvBranchEmployees.AllowUserToDeleteRows = false;
            this.dgvBranchEmployees.BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 68);
            this.dgvBranchEmployees.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBranchEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBranchEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBranchEmployees.Location = new System.Drawing.Point(0, 25);
            this.dgvBranchEmployees.MultiSelect = false;
            this.dgvBranchEmployees.Name = "dgvBranchEmployees";
            this.dgvBranchEmployees.ReadOnly = true;
            this.dgvBranchEmployees.RowHeadersWidth = 51;
            this.dgvBranchEmployees.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBranchEmployees.Size = new System.Drawing.Size(802, 258);
            this.dgvBranchEmployees.TabIndex = 1;
            
            // 
            // btnTransferEmployee
            // 
            this.btnTransferEmployee.BackColor = System.Drawing.Color.FromArgb(111, 66, 193);
            this.btnTransferEmployee.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTransferEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransferEmployee.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTransferEmployee.ForeColor = System.Drawing.Color.White;
            this.btnTransferEmployee.Location = new System.Drawing.Point(812, 25);
            this.btnTransferEmployee.Name = "btnTransferEmployee";
            this.btnTransferEmployee.Size = new System.Drawing.Size(140, 258);
            this.btnTransferEmployee.TabIndex = 2;
            this.btnTransferEmployee.Text = "🔀 Chuyển\r\nchi nhánh";
            this.btnTransferEmployee.Click += new System.EventHandler(this.btnTransferEmployee_Click);
            
            // 
            // SystemManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(32, 32, 48);
            this.ClientSize = new System.Drawing.Size(980, 640);
            this.Controls.Add(this.tabControl);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "SystemManagementForm";
            this.Text = "Quản lý hệ thống";
            this.Load += new System.EventHandler(this.SystemManagementForm_Load);
            
            this.tabControl.ResumeLayout(false);
            this.tabEmployees.ResumeLayout(false);
            this.tabBranches.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBranchEmployees)).EndInit();
            this.splitBranches.Panel1.ResumeLayout(false);
            this.splitBranches.Panel2.ResumeLayout(false);
            this.splitBranches.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitBranches)).EndInit();
            this.splitBranches.ResumeLayout(false);
            this.pnlEmployeeDetails.ResumeLayout(false);
            this.pnlEmployeeDetails.PerformLayout();
            this.pnlBranchDetails.ResumeLayout(false);
            this.pnlBranchDetails.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabEmployees;
        private System.Windows.Forms.TabPage tabBranches;
        
        // Tab Employees
        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.Panel pnlEmployeeDetails;
        private System.Windows.Forms.Label lblEmpName;
        private System.Windows.Forms.TextBox txtEmpName;
        private System.Windows.Forms.Label lblEmpUsername;
        private System.Windows.Forms.TextBox txtEmpUsername;
        private System.Windows.Forms.Label lblEmpRole;
        private System.Windows.Forms.ComboBox cboEmpRole;
        private System.Windows.Forms.Label lblEmpPhone;
        private System.Windows.Forms.TextBox txtEmpPhone;
        private System.Windows.Forms.Label lblEmpEmail;
        private System.Windows.Forms.TextBox txtEmpEmail;
        private System.Windows.Forms.Label lblEmpBranch;
        private System.Windows.Forms.ComboBox cboEmpBranch;
        private System.Windows.Forms.CheckBox chkMustChangePassword;
        private System.Windows.Forms.CheckBox chkEmpActive;
        private System.Windows.Forms.Button btnEmpAdd;
        private System.Windows.Forms.Button btnEmpUpdate;
        private System.Windows.Forms.Button btnEmpToggleActive;
        private System.Windows.Forms.Button btnEmpSetPassword;
        private System.Windows.Forms.Button btnEmpClear;
        private System.Windows.Forms.Label lblEmpStatus;
        
        // Tab Branches
        private System.Windows.Forms.SplitContainer splitBranches;
        private System.Windows.Forms.DataGridView dgvBranches;
        private System.Windows.Forms.Panel pnlBranchDetails;
        private System.Windows.Forms.DataGridView dgvBranchEmployees;
        private System.Windows.Forms.Label lblBranchCode;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.Label lblBranchName;
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.Label lblBranchAddress;
        private System.Windows.Forms.TextBox txtBranchAddress;
        private System.Windows.Forms.Label lblBranchPhone;
        private System.Windows.Forms.TextBox txtBranchPhone;
        private System.Windows.Forms.CheckBox chkBranchActive;
        private System.Windows.Forms.Button btnBranchAdd;
        private System.Windows.Forms.Button btnBranchUpdate;
        private System.Windows.Forms.Button btnBranchToggleActive;
        private System.Windows.Forms.Button btnBranchClear;
        private System.Windows.Forms.Button btnTransferEmployee;
        private System.Windows.Forms.Label lblBranchStatus;
        private System.Windows.Forms.Label lblBranchEmployees;
    }
}
