namespace QuanLyTiemDaQuy.Forms
{
    partial class CustomerEditForm
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
            
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblTier = new System.Windows.Forms.Label();
            this.cboTier = new System.Windows.Forms.ComboBox();
            this.lblTotalPurchase = new System.Windows.Forms.Label();
            this.lblTotalPurchaseValue = new System.Windows.Forms.Label();

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
            this.pnlMain.Size = new System.Drawing.Size(450, 380);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnSave);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(20, 320);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(410, 40);
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
            this.btnCancel.Location = new System.Drawing.Point(290, 0);
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
            this.pnlContent.Size = new System.Drawing.Size(410, 300);
            this.pnlContent.TabIndex = 0;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(10, 10);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(110, 19);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên khách hàng:";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(10, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(380, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhone.ForeColor = System.Drawing.Color.White;
            this.lblPhone.Location = new System.Drawing.Point(10, 70);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(78, 19);
            this.lblPhone.TabIndex = 2;
            this.lblPhone.Text = "Điện thoại:";
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.ForeColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(10, 92);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(180, 25);
            this.txtPhone.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.ForeColor = System.Drawing.Color.White;
            this.lblEmail.Location = new System.Drawing.Point(210, 70);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(44, 19);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(210, 92);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(180, 25);
            this.txtEmail.TabIndex = 3;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAddress.ForeColor = System.Drawing.Color.White;
            this.lblAddress.Location = new System.Drawing.Point(10, 130);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(51, 19);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Địa chỉ:";
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.ForeColor = System.Drawing.Color.White;
            this.txtAddress.Location = new System.Drawing.Point(10, 152);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(380, 50);
            this.txtAddress.TabIndex = 4;
            // 
            // lblTier
            // 
            this.lblTier.AutoSize = true;
            this.lblTier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTier.ForeColor = System.Drawing.Color.White;
            this.lblTier.Location = new System.Drawing.Point(10, 215);
            this.lblTier.Name = "lblTier";
            this.lblTier.Size = new System.Drawing.Size(105, 19);
            this.lblTier.TabIndex = 8;
            this.lblTier.Text = "Hạng khách hàng:";
            // 
            // cboTier
            // 
            this.cboTier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.cboTier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTier.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTier.ForeColor = System.Drawing.Color.White;
            this.cboTier.FormattingEnabled = true;
            this.cboTier.Items.AddRange(new object[] { "Thường", "VIP", "VVIP" });
            this.cboTier.Location = new System.Drawing.Point(10, 237);
            this.cboTier.Name = "cboTier";
            this.cboTier.Size = new System.Drawing.Size(150, 25);
            this.cboTier.TabIndex = 5;
            // 
            // lblTotalPurchase
            // 
            this.lblTotalPurchase.AutoSize = true;
            this.lblTotalPurchase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalPurchase.ForeColor = System.Drawing.Color.LightGray;
            this.lblTotalPurchase.Location = new System.Drawing.Point(180, 215);
            this.lblTotalPurchase.Name = "lblTotalPurchase";
            this.lblTotalPurchase.Size = new System.Drawing.Size(100, 19);
            this.lblTotalPurchase.TabIndex = 10;
            this.lblTotalPurchase.Text = "Tổng mua hàng:";
            // 
            // lblTotalPurchaseValue
            // 
            this.lblTotalPurchaseValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalPurchaseValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(150)))));
            this.lblTotalPurchaseValue.Location = new System.Drawing.Point(180, 237);
            this.lblTotalPurchaseValue.Name = "lblTotalPurchaseValue";
            this.lblTotalPurchaseValue.Size = new System.Drawing.Size(200, 25);
            this.lblTotalPurchaseValue.TabIndex = 11;
            this.lblTotalPurchaseValue.Text = "0 VNĐ";
            // 
            // Add controls to pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblName);
            this.pnlContent.Controls.Add(this.txtName);
            this.pnlContent.Controls.Add(this.lblPhone);
            this.pnlContent.Controls.Add(this.txtPhone);
            this.pnlContent.Controls.Add(this.lblEmail);
            this.pnlContent.Controls.Add(this.txtEmail);
            this.pnlContent.Controls.Add(this.lblAddress);
            this.pnlContent.Controls.Add(this.txtAddress);
            this.pnlContent.Controls.Add(this.lblTier);
            this.pnlContent.Controls.Add(this.cboTier);
            this.pnlContent.Controls.Add(this.lblTotalPurchase);
            this.pnlContent.Controls.Add(this.lblTotalPurchaseValue);
            // 
            // CustomerEditForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(450, 380);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm khách hàng";
            this.Load += new System.EventHandler(this.CustomerEditForm_Load);
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
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblTier;
        private System.Windows.Forms.ComboBox cboTier;
        private System.Windows.Forms.Label lblTotalPurchase;
        private System.Windows.Forms.Label lblTotalPurchaseValue;
    }
}
