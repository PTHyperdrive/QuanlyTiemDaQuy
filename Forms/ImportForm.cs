using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class ImportForm : Form
    {
        private readonly PricingService _pricingService;
        private readonly ImportService _importService;
        private readonly ProductService _productService;

        private List<ImportDetail> _cartItems;
        private PurchasePriceResult _currentPriceResult;
        private int _currentStoneTypeId;

        public ImportForm()
        {
            InitializeComponent();
            _pricingService = new PricingService();
            _importService = new ImportService();
            _productService = new ProductService();
            _cartItems = new List<ImportDetail>();
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            LoadStoneTypes();
            LoadColorGrades();
            LoadClarityGrades();
            LoadCutGrades();
            LoadSuppliers();
            LoadCertIssuers(); // Load danh sách đơn vị cấp chứng chỉ
            ClearGemstoneInput();
            UpdateCartDisplay();
        }

        #region Load Dropdown Data

        private void LoadStoneTypes()
        {
            var prices = _pricingService.GetAllMarketPrices();
            cboStoneType.DataSource = prices;
            cboStoneType.DisplayMember = "StoneTypeName";
            cboStoneType.ValueMember = "StoneTypeId";
            
            if (prices.Count > 0)
            {
                UpdateBasePriceDisplay(prices[0]);
            }
        }

        private void LoadColorGrades()
        {
            var grades = _pricingService.GetAllColorGrades();
            cboColor.DataSource = grades;
            cboColor.DisplayMember = "Grade";
            cboColor.ValueMember = "Grade";
        }

        private void LoadClarityGrades()
        {
            var grades = _pricingService.GetAllClarityGrades();
            cboClarity.DataSource = grades;
            cboClarity.DisplayMember = "Grade";
            cboClarity.ValueMember = "Grade";
        }

        private void LoadCutGrades()
        {
            var grades = _pricingService.GetAllCutGrades();
            cboCut.DataSource = grades;
            cboCut.DisplayMember = "Grade";
            cboCut.ValueMember = "Grade";
        }

        private void LoadSuppliers()
        {
            cboSupplier.Items.Clear();
            cboSupplier.Items.Add("Khách hàng bán lại");
            cboSupplier.Items.Add("Nhà cung cấp");
            cboSupplier.SelectedIndex = 0;
        }

        /// <summary>
        /// Load danh sách tổ chức cấp chứng chỉ quốc tế
        /// </summary>
        private void LoadCertIssuers()
        {
            // Thay txtCertIssuer bằng ComboBox động
            if (cboCertIssuer == null)
            {
                cboCertIssuer = new ComboBox
                {
                    BackColor = System.Drawing.Color.FromArgb(65, 65, 88),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = System.Drawing.Color.White,
                    Location = txtCertIssuer.Location,
                    Size = txtCertIssuer.Size,
                    Font = txtCertIssuer.Font,
                    TabIndex = txtCertIssuer.TabIndex
                };
                
                // Thay thế txtCertIssuer bằng cboCertIssuer
                grpCert.Controls.Remove(txtCertIssuer);
                grpCert.Controls.Add(cboCertIssuer);
            }

            cboCertIssuer.Items.Clear();
            cboCertIssuer.Items.Add("GIA - Gemological Institute of America");
            cboCertIssuer.Items.Add("IGI - International Gemological Institute");
            cboCertIssuer.Items.Add("HRD - Hoge Raad voor Diamant (Antwerp)");
            cboCertIssuer.Items.Add("AGS - American Gem Society");
            cboCertIssuer.Items.Add("Gübelin - Gübelin Gem Lab (Switzerland)");
            cboCertIssuer.SelectedIndex = 0;
        }

        // Field cho ComboBox thêm vào động
        private ComboBox cboCertIssuer;

        #endregion

        #region Stone Type Selection

        private void cboStoneType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cboStoneType.SelectedItem as GemstoneMarketPrice;
            if (selected != null)
            {
                _currentStoneTypeId = selected.StoneTypeId;
                UpdateBasePriceDisplay(selected);
                CalculatePrice();
            }
        }

        private void UpdateBasePriceDisplay(GemstoneMarketPrice marketPrice)
        {
            lblBasePrice.Text = $"Giá cơ sở: {marketPrice.BasePricePerCarat:N0} VNĐ/carat";
            lblLastUpdate.Text = $"Cập nhật: {marketPrice.LastUpdated:dd/MM/yyyy}";
        }

        #endregion

        #region Auto Price Calculation

        private void CalculatePrice()
        {
            if (cboStoneType.SelectedItem == null || 
                cboColor.SelectedItem == null || 
                cboClarity.SelectedItem == null || 
                cboCut.SelectedItem == null)
            {
                return;
            }

            decimal carat;
            if (!decimal.TryParse(txtCarat.Text, out carat) || carat <= 0)
            {
                lblSuggestedPrice.ForeColor = System.Drawing.Color.Red;
                lblSuggestedPrice.Text = "Nhập số carat hợp lệ";
                return;
            }

            // Giới hạn carat tối đa là 8
            if (carat > 8)
            {
                lblSuggestedPrice.ForeColor = System.Drawing.Color.Red;
                lblSuggestedPrice.Text = "Carat tối đa là 8";
                return;
            }

            string color = cboColor.SelectedValue?.ToString() ?? "";
            string clarity = cboClarity.SelectedValue?.ToString() ?? "";
            string cut = cboCut.SelectedValue?.ToString() ?? "";

            _currentPriceResult = _pricingService.CalculatePurchasePrice(
                _currentStoneTypeId, carat, color, clarity, cut);

            // Display results - màu xanh cho giá đề xuất
            lblSuggestedPrice.ForeColor = System.Drawing.Color.FromArgb(0, 255, 150);
            lblSuggestedPrice.Text = $"{_currentPriceResult.SuggestedPrice:N0} VNĐ";
            lblPriceRange.Text = $"Cho phép: {_currentPriceResult.MinPrice:N0} - {_currentPriceResult.MaxPrice:N0} VNĐ";
            txtPriceBreakdown.Text = _currentPriceResult.PriceBreakdown;
            
            // Set suggested price as default
            numFinalPrice.Value = _currentPriceResult.SuggestedPrice;
            numFinalPrice.Minimum = _currentPriceResult.MinPrice;
            numFinalPrice.Maximum = _currentPriceResult.MaxPrice;
        }

        private void txtCarat_TextChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void cboColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void cboClarity_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void cboCut_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        #endregion

        #region Certificate Validation

        private bool ValidateCertificate()
        {
            if (string.IsNullOrWhiteSpace(txtCertCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã chứng chỉ!\nĐá quý bắt buộc phải có chứng chỉ.", 
                    "Thiếu chứng chỉ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCertCode.Focus();
                return false;
            }

            // Đơn vị cấp bây giờ là ComboBox, luôn có giá trị được chọn
            if (cboCertIssuer == null || cboCertIssuer.SelectedIndex < 0)
            {
                MessageBox.Show("Vui lòng chọn đơn vị cấp chứng chỉ (GIA, IGI, AGS...)!", 
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCertIssuer?.Focus();
                return false;
            }

            // Kiểm tra format GIA: tối đa 10 chữ số
            string selectedIssuer = cboCertIssuer.SelectedItem?.ToString() ?? "";
            if (selectedIssuer.StartsWith("GIA"))
            {
                string certCode = txtCertCode.Text.Trim();
                
                // GIA report number chỉ chứa số, tối đa 10 chữ số
                if (!System.Text.RegularExpressions.Regex.IsMatch(certCode, @"^\d{1,10}$"))
                {
                    MessageBox.Show(
                        "Mã chứng chỉ GIA phải là số và tối đa 10 chữ số!\n\n" +
                        "💡 Bạn có thể kiểm tra chứng chỉ GIA tại:\n" +
                        "https://www.gia.edu/report-check-landing", 
                        "Mã GIA không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCertCode.Focus();
                    return false;
                }

                // Hỏi người dùng có muốn kiểm tra trực tuyến không
                var checkOnline = MessageBox.Show(
                    $"Mã GIA: {certCode}\n\n" +
                    "⚠️ BẮT BUỘC XÁC MINH CHỨNG CHỈ GIA TRƯỚC KHI THU MUA!\n\n" +
                    "• Yes = Mở browser xác minh trên gia.edu\n" +
                    "• No = Hủy bỏ, nhập lại thông tin",
                    "Xác minh chứng chỉ GIA (BẮT BUỘC)",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (checkOnline == DialogResult.Yes)
                {
                    // Mở browser với GIA Report Check - KHÓA APP CHO ĐẾN KHI TẮT BROWSER
                    try
                    {
                        this.Enabled = false; // Disable form
                        this.Cursor = Cursors.WaitCursor;
                        
                        var process = System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = $"https://www.gia.edu/report-check-landing?reportno={certCode}",
                            UseShellExecute = true
                        });
                        
                        // Chờ browser đóng
                        if (process != null)
                        {
                            process.WaitForExit();
                        }
                        
                        this.Cursor = Cursors.Default;
                        this.Enabled = true;
                        
                        // Sau khi đóng browser, hỏi kết quả xác minh
                        var verified = MessageBox.Show(
                            "Bạn đã xác minh chứng chỉ GIA thành công?\n\n" +
                            "• Yes = Chứng chỉ hợp lệ, tiếp tục thêm vào phiếu\n" +
                            "• No = Chứng chỉ không hợp lệ, hủy bỏ",
                            "Kết quả xác minh GIA",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (verified == DialogResult.Yes)
                        {
                            return true; // Đã xác minh - cho phép thêm
                        }
                        else
                        {
                            ClearGemstoneInput();
                            return false;
                        }
                    }
                    catch
                    {
                        this.Cursor = Cursors.Default;
                        this.Enabled = true;
                        System.Diagnostics.Process.Start("https://www.gia.edu/report-check-landing");
                        MessageBox.Show(
                            "Vui lòng xác minh chứng chỉ trên trang GIA.\n\n" +
                            "Sau khi xác minh xong, quay lại nhập thông tin và thêm vào phiếu.",
                            "Đang xác minh...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // No = Hủy bỏ, reset hết dữ liệu
                    MessageBox.Show(
                        "❌ KHÔNG THỂ THU MUA KHI CHƯA XÁC MINH CHỨNG CHỈ GIA!\n\n" +
                        "Vui lòng xác minh chứng chỉ trước khi thêm vào phiếu nhập.",
                        "Yêu cầu xác minh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearGemstoneInput();
                }
                
                return false; // LUÔN return false - bắt buộc xác minh GIA
            }

            // Các loại chứng chỉ khác (IGI, HRD, AGS, Gübelin) - cho phép thêm
            return true;
        }

        #endregion

        #region Add to Cart

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Validate certificate
            if (!ValidateCertificate())
                return;

            // Validate carat
            decimal carat;
            if (!decimal.TryParse(txtCarat.Text, out carat) || carat <= 0)
            {
                MessageBox.Show("Vui lòng nhập số carat hợp lệ!", "Lỗi");
                txtCarat.Focus();
                return;
            }

            // Validate price calculated
            if (_currentPriceResult == null || _currentPriceResult.SuggestedPrice <= 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin 4C để tính giá!", "Lỗi");
                return;
            }

            // Validate final price within bounds
            decimal finalPrice = numFinalPrice.Value;
            var validation = _pricingService.ValidatePurchasePrice(finalPrice, _currentPriceResult.SuggestedPrice);
            if (!validation.IsValid)
            {
                MessageBox.Show(validation.Message, "Giá không hợp lệ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get stone type info
            var stoneType = cboStoneType.SelectedItem as GemstoneMarketPrice;
            
            // Create product name
            string productName = $"{stoneType?.StoneTypeName ?? "Đá quý"} {carat}ct " +
                                 $"{cboColor.Text}/{cboClarity.Text}/{cboCut.Text}";

            // Add to cart
            var item = new ImportDetail
            {
                ProductCode = txtCertCode.Text.Trim(),
                ProductName = productName,
                Qty = 1,
                UnitCost = finalPrice,
                LineTotal = finalPrice
            };
            _cartItems.Add(item);

            UpdateCartDisplay();
            ClearGemstoneInput();
            MessageBox.Show($"Đã thêm: {productName}\nGiá thu mua: {finalPrice:N0} VNĐ", 
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cartItems;
            
            if (dgvCart.Columns.Count > 0)
            {
                if (dgvCart.Columns.Contains("ImportDetailId"))
                    dgvCart.Columns["ImportDetailId"].Visible = false;
                if (dgvCart.Columns.Contains("ImportId"))
                    dgvCart.Columns["ImportId"].Visible = false;
                if (dgvCart.Columns.Contains("ProductId"))
                    dgvCart.Columns["ProductId"].Visible = false;
                
                if (dgvCart.Columns.Contains("ProductCode"))
                    dgvCart.Columns["ProductCode"].HeaderText = "Mã CC";
                if (dgvCart.Columns.Contains("ProductName"))
                    dgvCart.Columns["ProductName"].HeaderText = "Sản phẩm";
                if (dgvCart.Columns.Contains("Qty"))
                    dgvCart.Columns["Qty"].HeaderText = "SL";
                if (dgvCart.Columns.Contains("UnitCost"))
                    dgvCart.Columns["UnitCost"].HeaderText = "Giá thu mua";
                if (dgvCart.Columns.Contains("LineTotal"))
                    dgvCart.Columns["LineTotal"].HeaderText = "Thành tiền";
            }

            // Update total
            decimal total = 0;
            foreach (var item in _cartItems)
            {
                total += item.LineTotal;
            }
            lblTotal.Text = $"TỔNG: {total:N0} VNĐ";
        }

        private void ClearGemstoneInput()
        {
            txtCarat.Clear();
            txtCertCode.Clear();
            
            // Reset cboCertIssuer thay vì txtCertIssuer
            if (cboCertIssuer != null && cboCertIssuer.Items.Count > 0)
                cboCertIssuer.SelectedIndex = 0;
            
            dtpCertDate.Value = DateTime.Today;
            txtPriceBreakdown.Clear();
            lblSuggestedPrice.Text = "---";
            lblPriceRange.Text = "";
            
            // QUAN TRỌNG: Reset Minimum và Maximum trước khi set Value = 0
            numFinalPrice.Minimum = 0;
            numFinalPrice.Maximum = decimal.MaxValue;
            numFinalPrice.Value = 0;
            
            _currentPriceResult = null;
            
            if (cboStoneType.Items.Count > 0) cboStoneType.SelectedIndex = 0;
            if (cboColor.Items.Count > 0) cboColor.SelectedIndex = 0;
            if (cboClarity.Items.Count > 0) cboClarity.SelectedIndex = 0;
            if (cboCut.Items.Count > 0) cboCut.SelectedIndex = 0;
        }

        #endregion

        #region Remove from Cart

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo");
                return;
            }

            int index = dgvCart.SelectedRows[0].Index;
            if (index >= 0 && index < _cartItems.Count)
            {
                _cartItems.RemoveAt(index);
                UpdateCartDisplay();
            }
        }

        #endregion

        #region Save Import

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm!", "Thông báo");
                return;
            }

            var confirm = MessageBox.Show(
                $"Xác nhận lưu phiếu nhập với {_cartItems.Count} sản phẩm?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Create import receipt
                    var receipt = new ImportReceipt
                    {
                        ImportCode = _importService.GenerateImportCode(),
                        SupplierId = 1, // Default supplier
                        EmployeeId = EmployeeService.CurrentEmployee?.EmployeeId ?? 0,
                        ImportDate = DateTime.Now,
                        Note = $"Nguồn: {cboSupplier.Text}",
                        Details = _cartItems
                    };
                    receipt.CalculateTotal();

                    var result = _importService.CreateImportReceipt(receipt);
                    
                    MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                        MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                    if (result.Success)
                    {
                        _cartItems.Clear();
                        UpdateCartDisplay();
                        ClearGemstoneInput();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearGemstoneInput();
        }
    }
}
