namespace QuanLyTiemDaQuy.Forms
{
    partial class ImportForm
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grpGemstone = new System.Windows.Forms.GroupBox();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.lblBasePrice = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.grpPrice = new System.Windows.Forms.GroupBox();
            this.lblPriceRange = new System.Windows.Forms.Label();
            this.numFinalPrice = new System.Windows.Forms.NumericUpDown();
            this.lblFinalPrice = new System.Windows.Forms.Label();
            this.lblSuggestedPrice = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPriceBreakdown = new System.Windows.Forms.TextBox();
            this.grpCert = new System.Windows.Forms.GroupBox();
            this.dtpCertDate = new System.Windows.Forms.DateTimePicker();
            this.lblCertDate = new System.Windows.Forms.Label();
            this.txtCertIssuer = new System.Windows.Forms.TextBox();
            this.lblCertIssuer = new System.Windows.Forms.Label();
            this.txtCertCode = new System.Windows.Forms.TextBox();
            this.lblCertCode = new System.Windows.Forms.Label();
            this.grp4C = new System.Windows.Forms.GroupBox();
            this.cboCut = new System.Windows.Forms.ComboBox();
            this.lblCut = new System.Windows.Forms.Label();
            this.cboClarity = new System.Windows.Forms.ComboBox();
            this.lblClarity = new System.Windows.Forms.Label();
            this.cboColor = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtCarat = new System.Windows.Forms.TextBox();
            this.lblCarat = new System.Windows.Forms.Label();
            this.cboStoneType = new System.Windows.Forms.ComboBox();
            this.lblStoneType = new System.Windows.Forms.Label();
            this.pnlCart = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.cboSupplier = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.grpGemstone.SuspendLayout();
            this.grpPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFinalPrice)).BeginInit();
            this.grpCert.SuspendLayout();
            this.grp4C.SuspendLayout();
            this.pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
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
            this.lblTitle.Text = "📥 NHẬP HÀNG - THU MUA ĐÁ QUÝ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 50);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grpGemstone);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlCart);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitContainer.Size = new System.Drawing.Size(980, 550);
            this.splitContainer.SplitterDistance = 500;
            this.splitContainer.TabIndex = 1;
            // 
            // grpGemstone
            // 
            this.grpGemstone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.grpGemstone.Controls.Add(this.lblSupplier);
            this.grpGemstone.Controls.Add(this.cboSupplier);
            this.grpGemstone.Controls.Add(this.lblLastUpdate);
            this.grpGemstone.Controls.Add(this.lblBasePrice);
            this.grpGemstone.Controls.Add(this.btnClear);
            this.grpGemstone.Controls.Add(this.btnAddToCart);
            this.grpGemstone.Controls.Add(this.grpPrice);
            this.grpGemstone.Controls.Add(this.grpCert);
            this.grpGemstone.Controls.Add(this.grp4C);
            this.grpGemstone.Controls.Add(this.cboStoneType);
            this.grpGemstone.Controls.Add(this.lblStoneType);
            this.grpGemstone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGemstone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpGemstone.ForeColor = System.Drawing.Color.White;
            this.grpGemstone.Location = new System.Drawing.Point(10, 10);
            this.grpGemstone.Name = "grpGemstone";
            this.grpGemstone.Size = new System.Drawing.Size(480, 530);
            this.grpGemstone.TabIndex = 0;
            this.grpGemstone.TabStop = false;
            this.grpGemstone.Text = "💎 Thông tin đá quý";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(250, 28);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(56, 19);
            this.lblSupplier.TabIndex = 12;
            this.lblSupplier.Text = "Nguồn:";
            // 
            // cboSupplier
            // 
            this.cboSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSupplier.ForeColor = System.Drawing.Color.White;
            this.cboSupplier.Location = new System.Drawing.Point(310, 25);
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.Size = new System.Drawing.Size(155, 25);
            this.cboSupplier.TabIndex = 11;
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLastUpdate.ForeColor = System.Drawing.Color.Silver;
            this.lblLastUpdate.Location = new System.Drawing.Point(15, 75);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(200, 15);
            this.lblLastUpdate.TabIndex = 10;
            this.lblLastUpdate.Text = "Cập nhật: --";
            // 
            // lblBasePrice
            // 
            this.lblBasePrice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBasePrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            this.lblBasePrice.Location = new System.Drawing.Point(15, 55);
            this.lblBasePrice.Name = "lblBasePrice";
            this.lblBasePrice.Size = new System.Drawing.Size(300, 20);
            this.lblBasePrice.TabIndex = 9;
            this.lblBasePrice.Text = "Giá cơ sở: --- VNĐ/carat";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(350, 485);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(115, 35);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "🔄 Làm mới";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddToCart.FlatAppearance.BorderSize = 0;
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddToCart.Location = new System.Drawing.Point(15, 485);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(320, 35);
            this.btnAddToCart.TabIndex = 7;
            this.btnAddToCart.Text = "➕ THÊM VÀO PHIẾU NHẬP";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // grpPrice
            // 
            this.grpPrice.Controls.Add(this.lblPriceRange);
            this.grpPrice.Controls.Add(this.numFinalPrice);
            this.grpPrice.Controls.Add(this.lblFinalPrice);
            this.grpPrice.Controls.Add(this.lblSuggestedPrice);
            this.grpPrice.Controls.Add(this.label10);
            this.grpPrice.Controls.Add(this.txtPriceBreakdown);
            this.grpPrice.ForeColor = System.Drawing.Color.White;
            this.grpPrice.Location = new System.Drawing.Point(15, 325);
            this.grpPrice.Name = "grpPrice";
            this.grpPrice.Size = new System.Drawing.Size(450, 150);
            this.grpPrice.TabIndex = 6;
            this.grpPrice.TabStop = false;
            this.grpPrice.Text = "💰 Giá thu mua tự động";
            // 
            // lblPriceRange
            // 
            this.lblPriceRange.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblPriceRange.ForeColor = System.Drawing.Color.Silver;
            this.lblPriceRange.Location = new System.Drawing.Point(240, 118);
            this.lblPriceRange.Name = "lblPriceRange";
            this.lblPriceRange.Size = new System.Drawing.Size(200, 20);
            this.lblPriceRange.TabIndex = 5;
            this.lblPriceRange.Text = "Cho phép: --- VNĐ";
            // 
            // numFinalPrice
            // 
            this.numFinalPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.numFinalPrice.ForeColor = System.Drawing.Color.White;
            this.numFinalPrice.Increment = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numFinalPrice.Location = new System.Drawing.Point(240, 90);
            this.numFinalPrice.Maximum = new decimal(new int[] { 2000000000, 0, 0, 0 });
            this.numFinalPrice.Name = "numFinalPrice";
            this.numFinalPrice.Size = new System.Drawing.Size(200, 25);
            this.numFinalPrice.TabIndex = 4;
            this.numFinalPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFinalPrice.ThousandsSeparator = true;
            // 
            // lblFinalPrice
            // 
            this.lblFinalPrice.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFinalPrice.Location = new System.Drawing.Point(240, 68);
            this.lblFinalPrice.Name = "lblFinalPrice";
            this.lblFinalPrice.Size = new System.Drawing.Size(200, 20);
            this.lblFinalPrice.TabIndex = 3;
            this.lblFinalPrice.Text = "Giá thu mua thực tế:";
            // 
            // lblSuggestedPrice
            // 
            this.lblSuggestedPrice.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSuggestedPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(150)))));
            this.lblSuggestedPrice.Location = new System.Drawing.Point(240, 35);
            this.lblSuggestedPrice.Name = "lblSuggestedPrice";
            this.lblSuggestedPrice.Size = new System.Drawing.Size(200, 30);
            this.lblSuggestedPrice.TabIndex = 2;
            this.lblSuggestedPrice.Text = "--- VNĐ";
            this.lblSuggestedPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(240, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 19);
            this.label10.TabIndex = 1;
            this.label10.Text = "💡 Giá đề xuất:";
            // 
            // txtPriceBreakdown
            // 
            this.txtPriceBreakdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.txtPriceBreakdown.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPriceBreakdown.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtPriceBreakdown.ForeColor = System.Drawing.Color.LightGray;
            this.txtPriceBreakdown.Location = new System.Drawing.Point(10, 20);
            this.txtPriceBreakdown.Multiline = true;
            this.txtPriceBreakdown.Name = "txtPriceBreakdown";
            this.txtPriceBreakdown.ReadOnly = true;
            this.txtPriceBreakdown.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPriceBreakdown.Size = new System.Drawing.Size(220, 120);
            this.txtPriceBreakdown.TabIndex = 0;
            // 
            // grpCert
            // 
            this.grpCert.Controls.Add(this.dtpCertDate);
            this.grpCert.Controls.Add(this.lblCertDate);
            this.grpCert.Controls.Add(this.txtCertIssuer);
            this.grpCert.Controls.Add(this.lblCertIssuer);
            this.grpCert.Controls.Add(this.txtCertCode);
            this.grpCert.Controls.Add(this.lblCertCode);
            this.grpCert.ForeColor = System.Drawing.Color.White;
            this.grpCert.Location = new System.Drawing.Point(15, 235);
            this.grpCert.Name = "grpCert";
            this.grpCert.Size = new System.Drawing.Size(450, 85);
            this.grpCert.TabIndex = 5;
            this.grpCert.TabStop = false;
            this.grpCert.Text = "📜 Chứng chỉ (Bắt buộc)";
            // 
            // dtpCertDate
            // 
            this.dtpCertDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCertDate.Location = new System.Drawing.Point(340, 50);
            this.dtpCertDate.Name = "dtpCertDate";
            this.dtpCertDate.Size = new System.Drawing.Size(100, 25);
            this.dtpCertDate.TabIndex = 5;
            // 
            // lblCertDate
            // 
            this.lblCertDate.AutoSize = true;
            this.lblCertDate.Location = new System.Drawing.Point(275, 53);
            this.lblCertDate.Name = "lblCertDate";
            this.lblCertDate.Size = new System.Drawing.Size(60, 19);
            this.lblCertDate.TabIndex = 4;
            this.lblCertDate.Text = "Ngày cấp:";
            // 
            // txtCertIssuer
            // 
            this.txtCertIssuer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtCertIssuer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCertIssuer.ForeColor = System.Drawing.Color.White;
            this.txtCertIssuer.Location = new System.Drawing.Point(90, 50);
            this.txtCertIssuer.Name = "txtCertIssuer";
            this.txtCertIssuer.Size = new System.Drawing.Size(175, 25);
            this.txtCertIssuer.TabIndex = 3;
            // 
            // lblCertIssuer
            // 
            this.lblCertIssuer.AutoSize = true;
            this.lblCertIssuer.Location = new System.Drawing.Point(10, 53);
            this.lblCertIssuer.Name = "lblCertIssuer";
            this.lblCertIssuer.Size = new System.Drawing.Size(74, 19);
            this.lblCertIssuer.TabIndex = 2;
            this.lblCertIssuer.Text = "Đơn vị cấp:";
            // 
            // txtCertCode
            // 
            this.txtCertCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtCertCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCertCode.ForeColor = System.Drawing.Color.White;
            this.txtCertCode.Location = new System.Drawing.Point(90, 22);
            this.txtCertCode.Name = "txtCertCode";
            this.txtCertCode.Size = new System.Drawing.Size(350, 25);
            this.txtCertCode.TabIndex = 1;
            // 
            // lblCertCode
            // 
            this.lblCertCode.AutoSize = true;
            this.lblCertCode.Location = new System.Drawing.Point(10, 25);
            this.lblCertCode.Name = "lblCertCode";
            this.lblCertCode.Size = new System.Drawing.Size(56, 19);
            this.lblCertCode.TabIndex = 0;
            this.lblCertCode.Text = "Mã CC: *";
            // 
            // grp4C
            // 
            this.grp4C.Controls.Add(this.cboCut);
            this.grp4C.Controls.Add(this.lblCut);
            this.grp4C.Controls.Add(this.cboClarity);
            this.grp4C.Controls.Add(this.lblClarity);
            this.grp4C.Controls.Add(this.cboColor);
            this.grp4C.Controls.Add(this.lblColor);
            this.grp4C.Controls.Add(this.txtCarat);
            this.grp4C.Controls.Add(this.lblCarat);
            this.grp4C.ForeColor = System.Drawing.Color.White;
            this.grp4C.Location = new System.Drawing.Point(15, 95);
            this.grp4C.Name = "grp4C";
            this.grp4C.Size = new System.Drawing.Size(450, 135);
            this.grp4C.TabIndex = 4;
            this.grp4C.TabStop = false;
            this.grp4C.Text = "📐 Thông số 4C (Tự động tính giá)";
            // 
            // cboCut
            // 
            this.cboCut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboCut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCut.ForeColor = System.Drawing.Color.White;
            this.cboCut.Location = new System.Drawing.Point(295, 95);
            this.cboCut.Name = "cboCut";
            this.cboCut.Size = new System.Drawing.Size(145, 25);
            this.cboCut.TabIndex = 7;
            this.cboCut.SelectedIndexChanged += new System.EventHandler(this.cboCut_SelectedIndexChanged);
            // 
            // lblCut
            // 
            this.lblCut.AutoSize = true;
            this.lblCut.Location = new System.Drawing.Point(235, 98);
            this.lblCut.Name = "lblCut";
            this.lblCut.Size = new System.Drawing.Size(33, 19);
            this.lblCut.TabIndex = 6;
            this.lblCut.Text = "Cut:";
            // 
            // cboClarity
            // 
            this.cboClarity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboClarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClarity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboClarity.ForeColor = System.Drawing.Color.White;
            this.cboClarity.Location = new System.Drawing.Point(80, 95);
            this.cboClarity.Name = "cboClarity";
            this.cboClarity.Size = new System.Drawing.Size(145, 25);
            this.cboClarity.TabIndex = 5;
            this.cboClarity.SelectedIndexChanged += new System.EventHandler(this.cboClarity_SelectedIndexChanged);
            // 
            // lblClarity
            // 
            this.lblClarity.AutoSize = true;
            this.lblClarity.Location = new System.Drawing.Point(10, 98);
            this.lblClarity.Name = "lblClarity";
            this.lblClarity.Size = new System.Drawing.Size(51, 19);
            this.lblClarity.TabIndex = 4;
            this.lblClarity.Text = "Clarity:";
            // 
            // cboColor
            // 
            this.cboColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboColor.ForeColor = System.Drawing.Color.White;
            this.cboColor.Location = new System.Drawing.Point(295, 58);
            this.cboColor.Name = "cboColor";
            this.cboColor.Size = new System.Drawing.Size(145, 25);
            this.cboColor.TabIndex = 3;
            this.cboColor.SelectedIndexChanged += new System.EventHandler(this.cboColor_SelectedIndexChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(235, 61);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(45, 19);
            this.lblColor.TabIndex = 2;
            this.lblColor.Text = "Color:";
            // 
            // txtCarat
            // 
            this.txtCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtCarat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCarat.ForeColor = System.Drawing.Color.White;
            this.txtCarat.Location = new System.Drawing.Point(80, 25);
            this.txtCarat.Name = "txtCarat";
            this.txtCarat.Size = new System.Drawing.Size(145, 25);
            this.txtCarat.TabIndex = 1;
            this.txtCarat.TextChanged += new System.EventHandler(this.txtCarat_TextChanged);
            // 
            // lblCarat
            // 
            this.lblCarat.AutoSize = true;
            this.lblCarat.Location = new System.Drawing.Point(10, 28);
            this.lblCarat.Name = "lblCarat";
            this.lblCarat.Size = new System.Drawing.Size(43, 19);
            this.lblCarat.TabIndex = 0;
            this.lblCarat.Text = "Carat:";
            // 
            // cboStoneType
            // 
            this.cboStoneType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboStoneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStoneType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStoneType.ForeColor = System.Drawing.Color.White;
            this.cboStoneType.Location = new System.Drawing.Point(90, 25);
            this.cboStoneType.Name = "cboStoneType";
            this.cboStoneType.Size = new System.Drawing.Size(150, 25);
            this.cboStoneType.TabIndex = 1;
            this.cboStoneType.SelectedIndexChanged += new System.EventHandler(this.cboStoneType_SelectedIndexChanged);
            // 
            // lblStoneType
            // 
            this.lblStoneType.AutoSize = true;
            this.lblStoneType.Location = new System.Drawing.Point(15, 28);
            this.lblStoneType.Name = "lblStoneType";
            this.lblStoneType.Size = new System.Drawing.Size(55, 19);
            this.lblStoneType.TabIndex = 0;
            this.lblStoneType.Text = "Loại đá:";
            // 
            // pnlCart
            // 
            this.pnlCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlCart.Controls.Add(this.btnSave);
            this.pnlCart.Controls.Add(this.btnRemoveItem);
            this.pnlCart.Controls.Add(this.lblTotal);
            this.pnlCart.Controls.Add(this.dgvCart);
            this.pnlCart.Controls.Add(this.lblCartTitle);
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCart.Location = new System.Drawing.Point(10, 10);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCart.Size = new System.Drawing.Size(456, 530);
            this.pnlCart.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(10, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(436, 45);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "💾 LƯU PHIẾU NHẬP";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRemoveItem.FlatAppearance.BorderSize = 0;
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(366, 10);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(80, 30);
            this.btnRemoveItem.TabIndex = 3;
            this.btnRemoveItem.Text = "🗑 Xóa";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(150)))));
            this.lblTotal.Location = new System.Drawing.Point(10, 435);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(436, 40);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "TỔNG: 0 VNĐ";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.dgvCart.Location = new System.Drawing.Point(10, 45);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(436, 385);
            this.dgvCart.TabIndex = 1;
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.AutoSize = true;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.ForeColor = System.Drawing.Color.White;
            this.lblCartTitle.Location = new System.Drawing.Point(10, 15);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(170, 21);
            this.lblCartTitle.TabIndex = 0;
            this.lblCartTitle.Text = "🛒 Danh sách thu mua";
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "ImportForm";
            this.Text = "Nhập hàng / Thu mua đá quý";
            this.Load += new System.EventHandler(this.ImportForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.grpGemstone.ResumeLayout(false);
            this.grpGemstone.PerformLayout();
            this.grpPrice.ResumeLayout(false);
            this.grpPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFinalPrice)).EndInit();
            this.grpCert.ResumeLayout(false);
            this.grpCert.PerformLayout();
            this.grp4C.ResumeLayout(false);
            this.grp4C.PerformLayout();
            this.pnlCart.ResumeLayout(false);
            this.pnlCart.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox grpGemstone;
        private System.Windows.Forms.ComboBox cboStoneType;
        private System.Windows.Forms.Label lblStoneType;
        private System.Windows.Forms.GroupBox grp4C;
        private System.Windows.Forms.TextBox txtCarat;
        private System.Windows.Forms.Label lblCarat;
        private System.Windows.Forms.ComboBox cboColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cboClarity;
        private System.Windows.Forms.Label lblClarity;
        private System.Windows.Forms.ComboBox cboCut;
        private System.Windows.Forms.Label lblCut;
        private System.Windows.Forms.GroupBox grpCert;
        private System.Windows.Forms.TextBox txtCertCode;
        private System.Windows.Forms.Label lblCertCode;
        private System.Windows.Forms.TextBox txtCertIssuer;
        private System.Windows.Forms.Label lblCertIssuer;
        private System.Windows.Forms.DateTimePicker dtpCertDate;
        private System.Windows.Forms.Label lblCertDate;
        private System.Windows.Forms.GroupBox grpPrice;
        private System.Windows.Forms.TextBox txtPriceBreakdown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblSuggestedPrice;
        private System.Windows.Forms.Label lblFinalPrice;
        private System.Windows.Forms.NumericUpDown numFinalPrice;
        private System.Windows.Forms.Label lblPriceRange;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblBasePrice;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Panel pnlCart;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.ComboBox cboSupplier;
    }
}
