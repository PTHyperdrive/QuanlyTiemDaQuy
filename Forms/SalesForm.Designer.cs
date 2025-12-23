namespace QuanLyTiemDaQuy.Forms
{
    partial class SalesForm
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.dgvCart = new System.Windows.Forms.DataGridView();
            this.pnlCartButtons = new System.Windows.Forms.Panel();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.pnlAddProduct = new System.Windows.Forms.Panel();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.nudQty = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblVAT = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudVAT = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudDiscount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).BeginInit();
            this.pnlCartButtons.SuspendLayout();
            this.pnlAddProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.dgvCart);
            this.pnlLeft.Controls.Add(this.pnlCartButtons);
            this.pnlLeft.Controls.Add(this.pnlAddProduct);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(630, 640);
            this.pnlLeft.TabIndex = 0;
            // 
            // dgvCart
            // 
            this.dgvCart.AllowUserToAddRows = false;
            this.dgvCart.AllowUserToDeleteRows = false;
            this.dgvCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCart.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.dgvCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.dgvCart.Location = new System.Drawing.Point(0, 70);
            this.dgvCart.MultiSelect = false;
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.ReadOnly = true;
            this.dgvCart.RowHeadersVisible = false;
            this.dgvCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCart.Size = new System.Drawing.Size(630, 520);
            this.dgvCart.TabIndex = 2;
            // 
            // pnlCartButtons
            // 
            this.pnlCartButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlCartButtons.Controls.Add(this.btnCancelPending);
            this.pnlCartButtons.Controls.Add(this.btnResumePending);
            this.pnlCartButtons.Controls.Add(this.btnSavePending);
            this.pnlCartButtons.Controls.Add(this.btnClearCart);
            this.pnlCartButtons.Controls.Add(this.btnRemoveItem);
            this.pnlCartButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCartButtons.Location = new System.Drawing.Point(0, 540);
            this.pnlCartButtons.Name = "pnlCartButtons";
            this.pnlCartButtons.Size = new System.Drawing.Size(630, 100);
            this.pnlCartButtons.TabIndex = 1;
            // 
            // btnClearCart
            // 
            this.btnClearCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClearCart.FlatAppearance.BorderSize = 0;
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.ForeColor = System.Drawing.Color.White;
            this.btnClearCart.Location = new System.Drawing.Point(150, 10);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(130, 32);
            this.btnClearCart.TabIndex = 1;
            this.btnClearCart.Text = "🗑️ Xóa tất cả";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.btnClearCart_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnRemoveItem.FlatAppearance.BorderSize = 0;
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(10, 10);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(130, 32);
            this.btnRemoveItem.TabIndex = 0;
            this.btnRemoveItem.Text = "❌ Xóa dòng";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnSavePending
            // 
            this.btnSavePending = new System.Windows.Forms.Button();
            this.btnSavePending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSavePending.FlatAppearance.BorderSize = 0;
            this.btnSavePending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePending.ForeColor = System.Drawing.Color.Black;
            this.btnSavePending.Location = new System.Drawing.Point(10, 55);
            this.btnSavePending.Name = "btnSavePending";
            this.btnSavePending.Size = new System.Drawing.Size(180, 35);
            this.btnSavePending.TabIndex = 2;
            this.btnSavePending.Text = "⏳ Lưu chờ thanh toán";
            this.btnSavePending.UseVisualStyleBackColor = false;
            this.btnSavePending.Click += new System.EventHandler(this.btnSavePending_Click);
            // 
            // btnResumePending
            // 
            this.btnResumePending = new System.Windows.Forms.Button();
            this.btnResumePending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnResumePending.FlatAppearance.BorderSize = 0;
            this.btnResumePending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResumePending.ForeColor = System.Drawing.Color.White;
            this.btnResumePending.Location = new System.Drawing.Point(200, 55);
            this.btnResumePending.Name = "btnResumePending";
            this.btnResumePending.Size = new System.Drawing.Size(180, 35);
            this.btnResumePending.TabIndex = 3;
            this.btnResumePending.Text = "📋 Lấy lại HĐ chờ";
            this.btnResumePending.UseVisualStyleBackColor = false;
            this.btnResumePending.Click += new System.EventHandler(this.btnResumePending_Click);
            // 
            // btnCancelPending
            // 
            this.btnCancelPending = new System.Windows.Forms.Button();
            this.btnCancelPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancelPending.FlatAppearance.BorderSize = 0;
            this.btnCancelPending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelPending.ForeColor = System.Drawing.Color.White;
            this.btnCancelPending.Location = new System.Drawing.Point(390, 55);
            this.btnCancelPending.Name = "btnCancelPending";
            this.btnCancelPending.Size = new System.Drawing.Size(180, 35);
            this.btnCancelPending.TabIndex = 4;
            this.btnCancelPending.Text = "🚫 Huỷ HĐ chờ";
            this.btnCancelPending.UseVisualStyleBackColor = false;
            this.btnCancelPending.Click += new System.EventHandler(this.btnCancelPending_Click);
            // 
            // pnlAddProduct
            // 
            this.pnlAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlAddProduct.Controls.Add(this.btnAddToCart);
            this.pnlAddProduct.Controls.Add(this.nudQty);
            this.pnlAddProduct.Controls.Add(this.label2);
            this.pnlAddProduct.Controls.Add(this.txtProductCode);
            this.pnlAddProduct.Controls.Add(this.label1);
            this.pnlAddProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddProduct.Location = new System.Drawing.Point(0, 0);
            this.pnlAddProduct.Name = "pnlAddProduct";
            this.pnlAddProduct.Size = new System.Drawing.Size(630, 70);
            this.pnlAddProduct.TabIndex = 0;
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnAddToCart.FlatAppearance.BorderSize = 0;
            this.btnAddToCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToCart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(445, 20);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(150, 32);
            this.btnAddToCart.TabIndex = 4;
            this.btnAddToCart.Text = "➕ Thêm vào giỏ";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // nudQty
            // 
            this.nudQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.nudQty.ForeColor = System.Drawing.Color.White;
            this.nudQty.Location = new System.Drawing.Point(345, 22);
            this.nudQty.Minimum = new decimal(new int[] {1, 0, 0, 0});
            this.nudQty.Name = "nudQty";
            this.nudQty.Size = new System.Drawing.Size(80, 25);
            this.nudQty.TabIndex = 3;
            this.nudQty.Value = new decimal(new int[] {1, 0, 0, 0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(305, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "SL:";
            // 
            // txtProductCode
            // 
            this.txtProductCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductCode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtProductCode.ForeColor = System.Drawing.Color.White;
            this.txtProductCode.Location = new System.Drawing.Point(85, 20);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(210, 27);
            this.txtProductCode.TabIndex = 1;
            this.txtProductCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductCode_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã SP:";
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlRight.Controls.Add(this.btnCheckout);
            this.pnlRight.Controls.Add(this.pnlTotals);
            this.pnlRight.Controls.Add(this.pnlSettings);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(630, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(350, 640);
            this.pnlRight.TabIndex = 1;
            // 
            // btnCheckout
            // 
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnCheckout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Location = new System.Drawing.Point(0, 580);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(350, 60);
            this.btnCheckout.TabIndex = 2;
            this.btnCheckout.Text = "💳 THANH TOÁN";
            this.btnCheckout.UseVisualStyleBackColor = false;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // pnlTotals
            // 
            this.pnlTotals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(55)))));
            this.pnlTotals.Controls.Add(this.lblTotal);
            this.pnlTotals.Controls.Add(this.label8);
            this.pnlTotals.Controls.Add(this.lblVAT);
            this.pnlTotals.Controls.Add(this.label6);
            this.pnlTotals.Controls.Add(this.lblDiscount);
            this.pnlTotals.Controls.Add(this.label4);
            this.pnlTotals.Controls.Add(this.lblSubtotal);
            this.pnlTotals.Controls.Add(this.label3);
            this.pnlTotals.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTotals.Location = new System.Drawing.Point(0, 220);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(350, 200);
            this.pnlTotals.TabIndex = 1;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            this.lblTotal.Location = new System.Drawing.Point(150, 140);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(190, 40);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "0 VNĐ";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(15, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 25);
            this.label8.TabIndex = 6;
            this.label8.Text = "TỔNG CỘNG:";
            // 
            // lblVAT
            // 
            this.lblVAT.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblVAT.ForeColor = System.Drawing.Color.LightGray;
            this.lblVAT.Location = new System.Drawing.Point(180, 90);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(160, 25);
            this.lblVAT.TabIndex = 5;
            this.lblVAT.Text = "+0 VNĐ";
            this.lblVAT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label6.ForeColor = System.Drawing.Color.LightGray;
            this.label6.Location = new System.Drawing.Point(15, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 4;
            this.label6.Text = "VAT:";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDiscount.ForeColor = System.Drawing.Color.Salmon;
            this.lblDiscount.Location = new System.Drawing.Point(180, 55);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(160, 25);
            this.lblDiscount.TabIndex = 3;
            this.lblDiscount.Text = "-0 VNĐ";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label4.ForeColor = System.Drawing.Color.LightGray;
            this.label4.Location = new System.Drawing.Point(15, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Giảm giá:";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtotal.ForeColor = System.Drawing.Color.White;
            this.lblSubtotal.Location = new System.Drawing.Point(180, 20);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(160, 25);
            this.lblSubtotal.TabIndex = 1;
            this.lblSubtotal.Text = "0 VNĐ";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.label3.ForeColor = System.Drawing.Color.LightGray;
            this.label3.Location = new System.Drawing.Point(15, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tạm tính:";
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.cboPaymentMethod);
            this.pnlSettings.Controls.Add(this.label9);
            this.pnlSettings.Controls.Add(this.nudVAT);
            this.pnlSettings.Controls.Add(this.label7);
            this.pnlSettings.Controls.Add(this.nudDiscount);
            this.pnlSettings.Controls.Add(this.label5);
            this.pnlSettings.Controls.Add(this.cboCustomer);
            this.pnlSettings.Controls.Add(this.lblCustomer);
            this.pnlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSettings.Location = new System.Drawing.Point(0, 0);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(350, 220);
            this.pnlSettings.TabIndex = 0;
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPaymentMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPaymentMethod.ForeColor = System.Drawing.Color.White;
            this.cboPaymentMethod.FormattingEnabled = true;
            this.cboPaymentMethod.Items.AddRange(new object[] {"Tiền mặt", "Thẻ", "Chuyển khoản"});
            this.cboPaymentMethod.Location = new System.Drawing.Point(130, 170);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(200, 25);
            this.cboPaymentMethod.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.LightGray;
            this.label9.Location = new System.Drawing.Point(15, 173);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 19);
            this.label9.TabIndex = 6;
            this.label9.Text = "Phương thức TT:";
            // 
            // nudVAT
            // 
            this.nudVAT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.nudVAT.DecimalPlaces = 1;
            this.nudVAT.ForeColor = System.Drawing.Color.White;
            this.nudVAT.Location = new System.Drawing.Point(130, 125);
            this.nudVAT.Name = "nudVAT";
            this.nudVAT.Size = new System.Drawing.Size(100, 25);
            this.nudVAT.TabIndex = 5;
            this.nudVAT.Value = new decimal(new int[] {10, 0, 0, 0});
            this.nudVAT.ValueChanged += new System.EventHandler(this.nudVAT_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.LightGray;
            this.label7.Location = new System.Drawing.Point(15, 127);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 19);
            this.label7.TabIndex = 4;
            this.label7.Text = "VAT (%):";
            // 
            // nudDiscount
            // 
            this.nudDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.nudDiscount.DecimalPlaces = 1;
            this.nudDiscount.ForeColor = System.Drawing.Color.White;
            this.nudDiscount.Location = new System.Drawing.Point(130, 80);
            this.nudDiscount.Name = "nudDiscount";
            this.nudDiscount.Size = new System.Drawing.Size(100, 25);
            this.nudDiscount.TabIndex = 3;
            this.nudDiscount.ValueChanged += new System.EventHandler(this.nudDiscount_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.LightGray;
            this.label5.Location = new System.Drawing.Point(15, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Giảm giá (%):";
            // 
            // cboCustomer
            // 
            this.cboCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(88)))));
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCustomer.ForeColor = System.Drawing.Color.White;
            this.cboCustomer.FormattingEnabled = true;
            this.cboCustomer.Location = new System.Drawing.Point(130, 30);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(200, 25);
            this.cboCustomer.TabIndex = 1;
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.ForeColor = System.Drawing.Color.LightGray;
            this.lblCustomer.Location = new System.Drawing.Point(15, 33);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(82, 19);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Khách hàng:";
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(980, 640);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlRight);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "SalesForm";
            this.Text = "Bán hàng (POS)";
            this.Load += new System.EventHandler(this.SalesForm_Load);
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCart)).EndInit();
            this.pnlCartButtons.ResumeLayout(false);
            this.pnlAddProduct.ResumeLayout(false);
            this.pnlAddProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQty)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotals.PerformLayout();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.DataGridView dgvCart;
        private System.Windows.Forms.Panel pnlCartButtons;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Panel pnlAddProduct;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.NumericUpDown nudQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblVAT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudVAT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudDiscount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Button btnSavePending;
        private System.Windows.Forms.Button btnResumePending;
        private System.Windows.Forms.Button btnCancelPending;
    }
}
