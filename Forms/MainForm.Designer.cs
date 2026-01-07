namespace QuanLyTiemDaQuy.Forms
{
    partial class MainForm
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnMarketPrice = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnImportReport = new System.Windows.Forms.Button();
            this.btnDiscounts = new System.Windows.Forms.Button(); // Added instantiation
            this.btnSales = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblCurrentDate = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.pnlStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStat1 = new System.Windows.Forms.Panel();
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlStat2 = new System.Windows.Forms.Panel();
            this.lblLowStock = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlStat3 = new System.Windows.Forms.Panel();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlStat4 = new System.Windows.Forms.Panel();
            this.lblTodayRevenue = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlStat5 = new System.Windows.Forms.Panel();
            this.lblTodayInvoices = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlStat6 = new System.Windows.Forms.Panel();
            this.lblMonthRevenue = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlStat1.SuspendLayout();
            this.pnlStat2.SuspendLayout();
            this.pnlStat3.SuspendLayout();
            this.pnlStat4.SuspendLayout();
            this.pnlStat5.SuspendLayout();
            this.pnlStat6.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnUsers);
            this.pnlSidebar.Controls.Add(this.btnReport);
            this.pnlSidebar.Controls.Add(this.btnMarketPrice);
            this.pnlSidebar.Controls.Add(this.btnImport);
            this.pnlSidebar.Controls.Add(this.btnImportReport);
            this.pnlSidebar.Controls.Add(this.btnDiscounts); // Added
            this.pnlSidebar.Controls.Add(this.btnSales);
            this.pnlSidebar.Controls.Add(this.btnSuppliers);
            this.pnlSidebar.Controls.Add(this.btnCustomers);
            this.pnlSidebar.Controls.Add(this.btnProducts);
            this.pnlSidebar.Controls.Add(this.btnDashboard);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(220, 700);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnLogout.ForeColor = System.Drawing.Color.Salmon;
            this.btnLogout.Location = new System.Drawing.Point(0, 655);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(220, 45);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "🚪  Đăng xuất";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUsers.FlatAppearance.BorderSize = 0;
            this.btnUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUsers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUsers.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnUsers.Location = new System.Drawing.Point(0, 465);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnUsers.Size = new System.Drawing.Size(220, 45);
            this.btnUsers.TabIndex = 9;
            this.btnUsers.Text = "⚙️  Quản lý hệ thống";
            this.btnUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnReport
            // 
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReport.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnReport.Location = new System.Drawing.Point(0, 420);
            this.btnReport.Name = "btnReport";
            this.btnReport.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnReport.Size = new System.Drawing.Size(220, 45);
            this.btnReport.TabIndex = 8;
            this.btnReport.Text = "📊  Báo cáo";
            this.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnMarketPrice
            // 
            this.btnMarketPrice.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMarketPrice.FlatAppearance.BorderSize = 0;
            this.btnMarketPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMarketPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnMarketPrice.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMarketPrice.Location = new System.Drawing.Point(0, 375);
            this.btnMarketPrice.Name = "btnMarketPrice";
            this.btnMarketPrice.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnMarketPrice.Size = new System.Drawing.Size(220, 45);
            this.btnMarketPrice.TabIndex = 11;
            this.btnMarketPrice.Text = "💱  Giá thị trường";
            this.btnMarketPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMarketPrice.UseVisualStyleBackColor = true;
            this.btnMarketPrice.Click += new System.EventHandler(this.btnMarketPrice_Click);
            // 
            // btnDiscounts
            // 
            this.btnDiscounts.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDiscounts.FlatAppearance.BorderSize = 0;
            this.btnDiscounts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDiscounts.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDiscounts.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDiscounts.Location = new System.Drawing.Point(0, 375); // Adjusted location flow if needed, but Dock=Top handles order
            this.btnDiscounts.Name = "btnDiscounts";
            this.btnDiscounts.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDiscounts.Size = new System.Drawing.Size(220, 45);
            this.btnDiscounts.TabIndex = 7;
            this.btnDiscounts.Text = "🏷️  Giảm giá";
            this.btnDiscounts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDiscounts.UseVisualStyleBackColor = true;
            this.btnDiscounts.Click += new System.EventHandler(this.btnDiscounts_Click);
            // 
            // btnImport
            // 
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnImport.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnImport.Location = new System.Drawing.Point(0, 330);
            this.btnImport.Name = "btnImport";
            this.btnImport.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnImport.Size = new System.Drawing.Size(220, 45);
            this.btnImport.TabIndex = 6;
            this.btnImport.Text = "📥  Nhập hàng";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnImportReport
            // 
            this.btnImportReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnImportReport.FlatAppearance.BorderSize = 0;
            this.btnImportReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportReport.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnImportReport.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnImportReport.Location = new System.Drawing.Point(0, 330);
            this.btnImportReport.Name = "btnImportReport";
            this.btnImportReport.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnImportReport.Size = new System.Drawing.Size(220, 45);
            this.btnImportReport.TabIndex = 12;
            this.btnImportReport.Text = "📦  BC Nhập kho";
            this.btnImportReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportReport.UseVisualStyleBackColor = true;
            this.btnImportReport.Click += new System.EventHandler(this.btnImportReport_Click);
            // 
            // btnSales
            // 
            this.btnSales.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSales.FlatAppearance.BorderSize = 0;
            this.btnSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSales.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSales.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSales.Location = new System.Drawing.Point(0, 285);
            this.btnSales.Name = "btnSales";
            this.btnSales.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSales.Size = new System.Drawing.Size(220, 45);
            this.btnSales.TabIndex = 5;
            this.btnSales.Text = "🛒  Bán hàng";
            this.btnSales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSales.UseVisualStyleBackColor = true;
            this.btnSales.Click += new System.EventHandler(this.btnSales_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSuppliers.FlatAppearance.BorderSize = 0;
            this.btnSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuppliers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSuppliers.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSuppliers.Location = new System.Drawing.Point(0, 240);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSuppliers.Size = new System.Drawing.Size(220, 45);
            this.btnSuppliers.TabIndex = 4;
            this.btnSuppliers.Text = "🏭  Nhà cung cấp";
            this.btnSuppliers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSuppliers.UseVisualStyleBackColor = true;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCustomers.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnCustomers.Location = new System.Drawing.Point(0, 195);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCustomers.Size = new System.Drawing.Size(220, 45);
            this.btnCustomers.TabIndex = 3;
            this.btnCustomers.Text = "👥  Khách hàng";
            this.btnCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.UseVisualStyleBackColor = true;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProducts.FlatAppearance.BorderSize = 0;
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnProducts.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnProducts.Location = new System.Drawing.Point(0, 150);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnProducts.Size = new System.Drawing.Size(220, 45);
            this.btnProducts.TabIndex = 2;
            this.btnProducts.Text = "💎  Sản phẩm";
            this.btnProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(0, 105);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(220, 45);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "🏠  Dashboard";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(55)))));
            this.pnlLogo.Controls.Add(this.lblAppName);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(220, 105);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            this.lblAppName.Location = new System.Drawing.Point(0, 0);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(220, 105);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "💎\r\nTIỆM ĐÁ QUÝ";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(78)))));
            this.pnlHeader.Controls.Add(this.lblCurrentDate);
            this.pnlHeader.Controls.Add(this.lblUserRole);
            this.pnlHeader.Controls.Add(this.lblUserName);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(220, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(980, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblCurrentDate
            // 
            this.lblCurrentDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCurrentDate.ForeColor = System.Drawing.Color.Silver;
            this.lblCurrentDate.Location = new System.Drawing.Point(710, 35);
            this.lblCurrentDate.Name = "lblCurrentDate";
            this.lblCurrentDate.Size = new System.Drawing.Size(260, 20);
            this.lblCurrentDate.TabIndex = 3;
            this.lblCurrentDate.Text = "Ngày hiện tại";
            this.lblCurrentDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserRole
            // 
            this.lblUserRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblUserRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            this.lblUserRole.Location = new System.Drawing.Point(860, 8);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(110, 20);
            this.lblUserRole.TabIndex = 2;
            this.lblUserRole.Text = "Role";
            this.lblUserRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(710, 5);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(150, 25);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Tên người dùng";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(126, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.pnlContent.Controls.Add(this.pnlDashboard);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(220, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(980, 640);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.Controls.Add(this.pnlStats);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlDashboard.Size = new System.Drawing.Size(980, 640);
            this.pnlDashboard.TabIndex = 0;
            // 
            // pnlStats
            // 
            this.pnlStats.Controls.Add(this.pnlStat1);
            this.pnlStats.Controls.Add(this.pnlStat2);
            this.pnlStats.Controls.Add(this.pnlStat3);
            this.pnlStats.Controls.Add(this.pnlStat4);
            this.pnlStats.Controls.Add(this.pnlStat5);
            this.pnlStats.Controls.Add(this.pnlStat6);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Location = new System.Drawing.Point(20, 20);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(940, 250);
            this.pnlStats.TabIndex = 0;
            this.pnlStats.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlStats_Paint);
            // 
            // pnlStat1
            // 
            this.pnlStat1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.pnlStat1.Controls.Add(this.lblTotalProducts);
            this.pnlStat1.Controls.Add(this.label1);
            this.pnlStat1.Location = new System.Drawing.Point(10, 10);
            this.pnlStat1.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStat1.Name = "pnlStat1";
            this.pnlStat1.Size = new System.Drawing.Size(280, 100);
            this.pnlStat1.TabIndex = 0;
            // 
            // lblTotalProducts
            // 
            this.lblTotalProducts.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalProducts.ForeColor = System.Drawing.Color.White;
            this.lblTotalProducts.Location = new System.Drawing.Point(10, 35);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Size = new System.Drawing.Size(260, 55);
            this.lblTotalProducts.TabIndex = 1;
            this.lblTotalProducts.Text = "0";
            this.lblTotalProducts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "💎 Tổng sản phẩm";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat2
            // 
            this.pnlStat2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.pnlStat2.Controls.Add(this.lblLowStock);
            this.pnlStat2.Controls.Add(this.label3);
            this.pnlStat2.Location = new System.Drawing.Point(310, 10);
            this.pnlStat2.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStat2.Name = "pnlStat2";
            this.pnlStat2.Size = new System.Drawing.Size(280, 100);
            this.pnlStat2.TabIndex = 1;
            // 
            // lblLowStock
            // 
            this.lblLowStock.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblLowStock.ForeColor = System.Drawing.Color.White;
            this.lblLowStock.Location = new System.Drawing.Point(10, 35);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(260, 55);
            this.lblLowStock.TabIndex = 1;
            this.lblLowStock.Text = "0";
            this.lblLowStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLowStock.Click += new System.EventHandler(this.lblLowStock_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "⚠️ Tồn kho thấp";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // pnlStat3
            // 
            this.pnlStat3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.pnlStat3.Controls.Add(this.lblTotalCustomers);
            this.pnlStat3.Controls.Add(this.label5);
            this.pnlStat3.Location = new System.Drawing.Point(610, 10);
            this.pnlStat3.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStat3.Name = "pnlStat3";
            this.pnlStat3.Size = new System.Drawing.Size(280, 100);
            this.pnlStat3.TabIndex = 2;
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.White;
            this.lblTotalCustomers.Location = new System.Drawing.Point(10, 35);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(260, 55);
            this.lblTotalCustomers.TabIndex = 1;
            this.lblTotalCustomers.Text = "0";
            this.lblTotalCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(260, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "👥 Khách hàng";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat4
            // 
            this.pnlStat4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.pnlStat4.Controls.Add(this.lblTodayRevenue);
            this.pnlStat4.Controls.Add(this.label7);
            this.pnlStat4.Location = new System.Drawing.Point(10, 130);
            this.pnlStat4.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStat4.Name = "pnlStat4";
            this.pnlStat4.Size = new System.Drawing.Size(280, 100);
            this.pnlStat4.TabIndex = 3;
            // 
            // lblTodayRevenue
            // 
            this.lblTodayRevenue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTodayRevenue.ForeColor = System.Drawing.Color.Black;
            this.lblTodayRevenue.Location = new System.Drawing.Point(10, 35);
            this.lblTodayRevenue.Name = "lblTodayRevenue";
            this.lblTodayRevenue.Size = new System.Drawing.Size(260, 55);
            this.lblTodayRevenue.TabIndex = 1;
            this.lblTodayRevenue.Text = "0 VNĐ";
            this.lblTodayRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(10, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(260, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "💰 Doanh thu hôm nay";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat5
            // 
            this.pnlStat5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.pnlStat5.Controls.Add(this.lblTodayInvoices);
            this.pnlStat5.Controls.Add(this.label9);
            this.pnlStat5.Location = new System.Drawing.Point(310, 130);
            this.pnlStat5.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStat5.Name = "pnlStat5";
            this.pnlStat5.Size = new System.Drawing.Size(280, 100);
            this.pnlStat5.TabIndex = 4;
            // 
            // lblTodayInvoices
            // 
            this.lblTodayInvoices.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTodayInvoices.ForeColor = System.Drawing.Color.White;
            this.lblTodayInvoices.Location = new System.Drawing.Point(10, 35);
            this.lblTodayInvoices.Name = "lblTodayInvoices";
            this.lblTodayInvoices.Size = new System.Drawing.Size(260, 55);
            this.lblTodayInvoices.TabIndex = 1;
            this.lblTodayInvoices.Text = "0";
            this.lblTodayInvoices.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(10, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(260, 25);
            this.label9.TabIndex = 0;
            this.label9.Text = "🧾 Hóa đơn hôm nay";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat6
            // 
            this.pnlStat6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(66)))), ((int)(((byte)(193)))));
            this.pnlStat6.Controls.Add(this.lblMonthRevenue);
            this.pnlStat6.Controls.Add(this.label11);
            this.pnlStat6.Location = new System.Drawing.Point(610, 130);
            this.pnlStat6.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStat6.Name = "pnlStat6";
            this.pnlStat6.Size = new System.Drawing.Size(280, 100);
            this.pnlStat6.TabIndex = 5;
            // 
            // lblMonthRevenue
            // 
            this.lblMonthRevenue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMonthRevenue.ForeColor = System.Drawing.Color.White;
            this.lblMonthRevenue.Location = new System.Drawing.Point(10, 35);
            this.lblMonthRevenue.Name = "lblMonthRevenue";
            this.lblMonthRevenue.Size = new System.Drawing.Size(260, 55);
            this.lblMonthRevenue.TabIndex = 1;
            this.lblMonthRevenue.Text = "0 VNĐ";
            this.lblMonthRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(10, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(260, 25);
            this.label11.TabIndex = 0;
            this.label11.Text = "📈 Doanh thu tháng này";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Tiệm Đá Quý";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.pnlStat1.ResumeLayout(false);
            this.pnlStat2.ResumeLayout(false);
            this.pnlStat3.ResumeLayout(false);
            this.pnlStat4.ResumeLayout(false);
            this.pnlStat5.ResumeLayout(false);
            this.pnlStat6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnSales;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnMarketPrice;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnDiscounts; // Added
        private System.Windows.Forms.Button btnImportReport;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.Label lblCurrentDate;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.FlowLayoutPanel pnlStats;
        private System.Windows.Forms.Panel pnlStat1;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlStat2;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlStat3;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlStat4;
        private System.Windows.Forms.Label lblTodayRevenue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnlStat5;
        private System.Windows.Forms.Label lblTodayInvoices;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnlStat6;
        private System.Windows.Forms.Label lblMonthRevenue;
        private System.Windows.Forms.Label label11;
    }
}
