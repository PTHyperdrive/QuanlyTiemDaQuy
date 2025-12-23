namespace QuanLyTiemDaQuy.Forms
{
    partial class ProductEditForm
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            
            // Row 1: Mã SP & Tên SP
            this.lblProductCode = new System.Windows.Forms.Label();
            this.txtProductCode = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            
            // Row 2: Loại đá & Carat
            this.lblStoneType = new System.Windows.Forms.Label();
            this.cboStoneType = new System.Windows.Forms.ComboBox();
            this.lblCarat = new System.Windows.Forms.Label();
            this.nudCarat = new System.Windows.Forms.NumericUpDown();
            
            // Row 3: Color & Clarity
            this.lblColor = new System.Windows.Forms.Label();
            this.cboColor = new System.Windows.Forms.ComboBox();
            this.lblClarity = new System.Windows.Forms.Label();
            this.cboClarity = new System.Windows.Forms.ComboBox();
            
            // Row 4: Cut & Tồn kho
            this.lblCut = new System.Windows.Forms.Label();
            this.cboCut = new System.Windows.Forms.ComboBox();
            this.lblStockQty = new System.Windows.Forms.Label();
            this.nudStockQty = new System.Windows.Forms.NumericUpDown();
            
            // Row 5: Giá vốn & Giá bán
            this.lblCostPrice = new System.Windows.Forms.Label();
            this.nudCostPrice = new System.Windows.Forms.NumericUpDown();
            this.lblSellPrice = new System.Windows.Forms.Label();
            this.nudSellPrice = new System.Windows.Forms.NumericUpDown();
            
            // Row 6: Vị trí trưng bày
            this.lblDisplayLocation = new System.Windows.Forms.Label();
            this.txtDisplayLocation = new System.Windows.Forms.TextBox();

            ((System.ComponentModel.ISupportInitialize)(this.nudCarat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCostPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSellPrice)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(520, 480);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(20, 420);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(480, 40);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(360, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "❌ Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "💾 Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(20, 20);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(480, 400);
            this.pnlContent.TabIndex = 0;
            // 
            // lblProductCode
            // 
            this.lblProductCode.AutoSize = true;
            this.lblProductCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProductCode.ForeColor = System.Drawing.Color.White;
            this.lblProductCode.Location = new System.Drawing.Point(10, 10);
            this.lblProductCode.Name = "lblProductCode";
            this.lblProductCode.Size = new System.Drawing.Size(52, 19);
            this.lblProductCode.TabIndex = 0;
            this.lblProductCode.Text = "Mã SP:";
            // 
            // txtProductCode
            // 
            this.txtProductCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtProductCode.ForeColor = System.Drawing.Color.White;
            this.txtProductCode.Location = new System.Drawing.Point(10, 32);
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.Size = new System.Drawing.Size(200, 25);
            this.txtProductCode.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(230, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 19);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Tên sản phẩm:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(230, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(240, 25);
            this.txtName.TabIndex = 2;
            // 
            // lblStoneType
            // 
            this.lblStoneType.AutoSize = true;
            this.lblStoneType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStoneType.ForeColor = System.Drawing.Color.White;
            this.lblStoneType.Location = new System.Drawing.Point(10, 70);
            this.lblStoneType.Name = "lblStoneType";
            this.lblStoneType.Size = new System.Drawing.Size(57, 19);
            this.lblStoneType.TabIndex = 4;
            this.lblStoneType.Text = "Loại đá:";
            // 
            // cboStoneType
            // 
            this.cboStoneType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.cboStoneType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStoneType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStoneType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStoneType.ForeColor = System.Drawing.Color.White;
            this.cboStoneType.FormattingEnabled = true;
            this.cboStoneType.Location = new System.Drawing.Point(10, 92);
            this.cboStoneType.Name = "cboStoneType";
            this.cboStoneType.Size = new System.Drawing.Size(200, 25);
            this.cboStoneType.TabIndex = 3;
            // 
            // lblCarat
            // 
            this.lblCarat.AutoSize = true;
            this.lblCarat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCarat.ForeColor = System.Drawing.Color.White;
            this.lblCarat.Location = new System.Drawing.Point(230, 70);
            this.lblCarat.Name = "lblCarat";
            this.lblCarat.Size = new System.Drawing.Size(43, 19);
            this.lblCarat.TabIndex = 6;
            this.lblCarat.Text = "Carat:";
            // 
            // nudCarat
            // 
            this.nudCarat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.nudCarat.DecimalPlaces = 2;
            this.nudCarat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCarat.ForeColor = System.Drawing.Color.White;
            this.nudCarat.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            this.nudCarat.Location = new System.Drawing.Point(230, 92);
            this.nudCarat.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudCarat.Name = "nudCarat";
            this.nudCarat.Size = new System.Drawing.Size(120, 25);
            this.nudCarat.TabIndex = 4;
            this.nudCarat.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblColor.ForeColor = System.Drawing.Color.White;
            this.lblColor.Location = new System.Drawing.Point(10, 130);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(40, 19);
            this.lblColor.TabIndex = 8;
            this.lblColor.Text = "Màu:";
            // 
            // cboColor
            // 
            this.cboColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.cboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboColor.ForeColor = System.Drawing.Color.White;
            this.cboColor.FormattingEnabled = true;
            this.cboColor.Items.AddRange(new object[] { "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" });
            this.cboColor.Location = new System.Drawing.Point(10, 152);
            this.cboColor.Name = "cboColor";
            this.cboColor.Size = new System.Drawing.Size(200, 25);
            this.cboColor.TabIndex = 5;
            // 
            // lblClarity
            // 
            this.lblClarity.AutoSize = true;
            this.lblClarity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblClarity.ForeColor = System.Drawing.Color.White;
            this.lblClarity.Location = new System.Drawing.Point(230, 130);
            this.lblClarity.Name = "lblClarity";
            this.lblClarity.Size = new System.Drawing.Size(94, 19);
            this.lblClarity.TabIndex = 10;
            this.lblClarity.Text = "Độ tinh khiết:";
            // 
            // cboClarity
            // 
            this.cboClarity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.cboClarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClarity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboClarity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboClarity.ForeColor = System.Drawing.Color.White;
            this.cboClarity.FormattingEnabled = true;
            this.cboClarity.Items.AddRange(new object[] { "FL", "IF", "VVS1", "VVS2", "VS1", "VS2", "SI1", "SI2", "I1", "I2", "I3" });
            this.cboClarity.Location = new System.Drawing.Point(230, 152);
            this.cboClarity.Name = "cboClarity";
            this.cboClarity.Size = new System.Drawing.Size(120, 25);
            this.cboClarity.TabIndex = 6;
            // 
            // lblCut
            // 
            this.lblCut.AutoSize = true;
            this.lblCut.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCut.ForeColor = System.Drawing.Color.White;
            this.lblCut.Location = new System.Drawing.Point(10, 190);
            this.lblCut.Name = "lblCut";
            this.lblCut.Size = new System.Drawing.Size(64, 19);
            this.lblCut.TabIndex = 12;
            this.lblCut.Text = "Giác cắt:";
            // 
            // cboCut
            // 
            this.cboCut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.cboCut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCut.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCut.ForeColor = System.Drawing.Color.White;
            this.cboCut.FormattingEnabled = true;
            this.cboCut.Items.AddRange(new object[] { "Xuất sắc", "Rất tốt", "Tốt", "Trung bình", "Kém" });
            this.cboCut.Location = new System.Drawing.Point(10, 212);
            this.cboCut.Name = "cboCut";
            this.cboCut.Size = new System.Drawing.Size(200, 25);
            this.cboCut.TabIndex = 7;
            // 
            // lblStockQty
            // 
            this.lblStockQty.AutoSize = true;
            this.lblStockQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStockQty.ForeColor = System.Drawing.Color.White;
            this.lblStockQty.Location = new System.Drawing.Point(230, 190);
            this.lblStockQty.Name = "lblStockQty";
            this.lblStockQty.Size = new System.Drawing.Size(65, 19);
            this.lblStockQty.TabIndex = 14;
            this.lblStockQty.Text = "Tồn kho:";
            // 
            // nudStockQty
            // 
            this.nudStockQty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.nudStockQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudStockQty.ForeColor = System.Drawing.Color.White;
            this.nudStockQty.Location = new System.Drawing.Point(230, 212);
            this.nudStockQty.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            this.nudStockQty.Name = "nudStockQty";
            this.nudStockQty.Size = new System.Drawing.Size(120, 25);
            this.nudStockQty.TabIndex = 8;
            this.nudStockQty.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCostPrice
            // 
            this.lblCostPrice.AutoSize = true;
            this.lblCostPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCostPrice.ForeColor = System.Drawing.Color.White;
            this.lblCostPrice.Location = new System.Drawing.Point(10, 250);
            this.lblCostPrice.Name = "lblCostPrice";
            this.lblCostPrice.Size = new System.Drawing.Size(56, 19);
            this.lblCostPrice.TabIndex = 16;
            this.lblCostPrice.Text = "Giá vốn:";
            // 
            // nudCostPrice
            // 
            this.nudCostPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.nudCostPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudCostPrice.ForeColor = System.Drawing.Color.White;
            this.nudCostPrice.Location = new System.Drawing.Point(10, 272);
            this.nudCostPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudCostPrice.Name = "nudCostPrice";
            this.nudCostPrice.Size = new System.Drawing.Size(200, 25);
            this.nudCostPrice.TabIndex = 9;
            this.nudCostPrice.ThousandsSeparator = true;
            // 
            // lblSellPrice
            // 
            this.lblSellPrice.AutoSize = true;
            this.lblSellPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSellPrice.ForeColor = System.Drawing.Color.White;
            this.lblSellPrice.Location = new System.Drawing.Point(230, 250);
            this.lblSellPrice.Name = "lblSellPrice";
            this.lblSellPrice.Size = new System.Drawing.Size(58, 19);
            this.lblSellPrice.TabIndex = 18;
            this.lblSellPrice.Text = "Giá bán:";
            // 
            // nudSellPrice
            // 
            this.nudSellPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.nudSellPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudSellPrice.ForeColor = System.Drawing.Color.White;
            this.nudSellPrice.Location = new System.Drawing.Point(230, 272);
            this.nudSellPrice.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.nudSellPrice.Name = "nudSellPrice";
            this.nudSellPrice.Size = new System.Drawing.Size(200, 25);
            this.nudSellPrice.TabIndex = 10;
            this.nudSellPrice.ThousandsSeparator = true;
            // 
            // lblDisplayLocation
            // 
            this.lblDisplayLocation.AutoSize = true;
            this.lblDisplayLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDisplayLocation.ForeColor = System.Drawing.Color.White;
            this.lblDisplayLocation.Location = new System.Drawing.Point(10, 310);
            this.lblDisplayLocation.Name = "lblDisplayLocation";
            this.lblDisplayLocation.Size = new System.Drawing.Size(110, 19);
            this.lblDisplayLocation.TabIndex = 20;
            this.lblDisplayLocation.Text = "Vị trí trưng bày:";
            // 
            // txtDisplayLocation
            // 
            this.txtDisplayLocation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.txtDisplayLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDisplayLocation.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDisplayLocation.ForeColor = System.Drawing.Color.White;
            this.txtDisplayLocation.Location = new System.Drawing.Point(10, 332);
            this.txtDisplayLocation.Name = "txtDisplayLocation";
            this.txtDisplayLocation.Size = new System.Drawing.Size(420, 25);
            this.txtDisplayLocation.TabIndex = 11;
            // 
            // Add controls to pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblProductCode);
            this.pnlContent.Controls.Add(this.txtProductCode);
            this.pnlContent.Controls.Add(this.lblName);
            this.pnlContent.Controls.Add(this.txtName);
            this.pnlContent.Controls.Add(this.lblStoneType);
            this.pnlContent.Controls.Add(this.cboStoneType);
            this.pnlContent.Controls.Add(this.lblCarat);
            this.pnlContent.Controls.Add(this.nudCarat);
            this.pnlContent.Controls.Add(this.lblColor);
            this.pnlContent.Controls.Add(this.cboColor);
            this.pnlContent.Controls.Add(this.lblClarity);
            this.pnlContent.Controls.Add(this.cboClarity);
            this.pnlContent.Controls.Add(this.lblCut);
            this.pnlContent.Controls.Add(this.cboCut);
            this.pnlContent.Controls.Add(this.lblStockQty);
            this.pnlContent.Controls.Add(this.nudStockQty);
            this.pnlContent.Controls.Add(this.lblCostPrice);
            this.pnlContent.Controls.Add(this.nudCostPrice);
            this.pnlContent.Controls.Add(this.lblSellPrice);
            this.pnlContent.Controls.Add(this.nudSellPrice);
            this.pnlContent.Controls.Add(this.lblDisplayLocation);
            this.pnlContent.Controls.Add(this.txtDisplayLocation);
            // 
            // ProductEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(520, 480);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm sản phẩm";
            this.Load += new System.EventHandler(this.ProductEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCarat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCostPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSellPrice)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblProductCode;
        private System.Windows.Forms.TextBox txtProductCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblStoneType;
        private System.Windows.Forms.ComboBox cboStoneType;
        private System.Windows.Forms.Label lblCarat;
        private System.Windows.Forms.NumericUpDown nudCarat;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cboColor;
        private System.Windows.Forms.Label lblClarity;
        private System.Windows.Forms.ComboBox cboClarity;
        private System.Windows.Forms.Label lblCut;
        private System.Windows.Forms.ComboBox cboCut;
        private System.Windows.Forms.Label lblStockQty;
        private System.Windows.Forms.NumericUpDown nudStockQty;
        private System.Windows.Forms.Label lblCostPrice;
        private System.Windows.Forms.NumericUpDown nudCostPrice;
        private System.Windows.Forms.Label lblSellPrice;
        private System.Windows.Forms.NumericUpDown nudSellPrice;
        private System.Windows.Forms.Label lblDisplayLocation;
        private System.Windows.Forms.TextBox txtDisplayLocation;
    }
}
