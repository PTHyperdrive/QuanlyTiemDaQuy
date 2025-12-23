namespace QuanLyTiemDaQuy.Forms
{
    partial class MarketPriceForm
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
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lblExchangeRate = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSyncApi = new System.Windows.Forms.Button();
            this.dgvPrices = new System.Windows.Forms.DataGridView();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrices)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(900, 55);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 55);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "💎 GIÁ THỊ TRƯỜNG ĐÁ QUÝ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlActions
            // 
            this.pnlActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.pnlActions.Controls.Add(this.lblExchangeRate);
            this.pnlActions.Controls.Add(this.lblLastUpdate);
            this.pnlActions.Controls.Add(this.btnRefresh);
            this.pnlActions.Controls.Add(this.btnSyncApi);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(0, 55);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(15);
            this.pnlActions.Size = new System.Drawing.Size(900, 60);
            this.pnlActions.TabIndex = 1;
            // 
            // lblExchangeRate
            // 
            this.lblExchangeRate.AutoSize = true;
            this.lblExchangeRate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblExchangeRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            this.lblExchangeRate.Location = new System.Drawing.Point(15, 18);
            this.lblExchangeRate.Name = "lblExchangeRate";
            this.lblExchangeRate.Size = new System.Drawing.Size(200, 20);
            this.lblExchangeRate.TabIndex = 0;
            this.lblExchangeRate.Text = "💱 Tỷ giá: 1 USD = ... VND";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.ForeColor = System.Drawing.Color.Gray;
            this.lblLastUpdate.Location = new System.Drawing.Point(300, 20);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(150, 19);
            this.lblLastUpdate.TabIndex = 1;
            this.lblLastUpdate.Text = "Cập nhật: --";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(780, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSyncApi
            // 
            this.btnSyncApi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnSyncApi.FlatAppearance.BorderSize = 0;
            this.btnSyncApi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSyncApi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSyncApi.ForeColor = System.Drawing.Color.White;
            this.btnSyncApi.Location = new System.Drawing.Point(580, 13);
            this.btnSyncApi.Name = "btnSyncApi";
            this.btnSyncApi.Size = new System.Drawing.Size(190, 35);
            this.btnSyncApi.TabIndex = 2;
            this.btnSyncApi.Text = "🌐 Cập nhật từ API";
            this.btnSyncApi.UseVisualStyleBackColor = false;
            this.btnSyncApi.Click += new System.EventHandler(this.btnSyncApi_Click);
            // 
            // dgvPrices
            // 
            this.dgvPrices.AllowUserToAddRows = false;
            this.dgvPrices.AllowUserToDeleteRows = false;
            this.dgvPrices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrices.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.dgvPrices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPrices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrices.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.dgvPrices.Location = new System.Drawing.Point(0, 115);
            this.dgvPrices.MultiSelect = false;
            this.dgvPrices.Name = "dgvPrices";
            this.dgvPrices.ReadOnly = true;
            this.dgvPrices.RowHeadersVisible = false;
            this.dgvPrices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrices.Size = new System.Drawing.Size(900, 385);
            this.dgvPrices.TabIndex = 2;
            // 
            // pnlInfo
            // 
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.pnlInfo.Controls.Add(this.lblInfo);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInfo.Location = new System.Drawing.Point(0, 500);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(10);
            this.pnlInfo.Size = new System.Drawing.Size(900, 50);
            this.pnlInfo.TabIndex = 3;
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfo.ForeColor = System.Drawing.Color.LightGray;
            this.lblInfo.Location = new System.Drawing.Point(10, 10);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(880, 30);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "ℹ️ Nguồn: ExchangeRate-API (tỷ giá), GIA/Gemval (giá tham khảo). Giá trên là giá trung bình tham khảo.";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MarketPriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.dgvPrices);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MarketPriceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Giá thị trường đá quý";
            this.Load += new System.EventHandler(this.MarketPriceForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrices)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Label lblExchangeRate;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSyncApi;
        private System.Windows.Forms.DataGridView dgvPrices;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblInfo;
    }
}
