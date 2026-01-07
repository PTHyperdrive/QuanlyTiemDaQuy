using System;
using System.Windows.Forms;
using System.Linq;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    /// <summary>
    /// Form để thêm/sửa sản phẩm
    /// </summary>
    public partial class ProductEditForm : Form
    {
        private readonly ProductService _productService;
        private readonly Product _product;
        private readonly bool _isEditMode;

        /// <summary>
        /// Khởi tạo form thêm sản phẩm mới
        /// </summary>
        public ProductEditForm()
        {
            InitializeComponent();
            _productService = new ProductService();
            _product = new Product();
            _isEditMode = false;
            this.Text = "✨ Thêm sản phẩm mới";
        }

        /// <summary>
        /// Khởi tạo form sửa sản phẩm
        /// </summary>
        public ProductEditForm(Product product)
        {
            InitializeComponent();
            _productService = new ProductService();
            _product = product;
            _isEditMode = true;
            this.Text = "✏️ Sửa sản phẩm: " + product.ProductCode;
        }

        private void ProductEditForm_Load(object sender, EventArgs e)
        {
            LoadStoneTypes();
            LoadCertificates(); // Load danh sách chứng chỉ

            if (_isEditMode)
            {
                LoadProductData();
                txtProductCode.Enabled = false; // Không cho sửa mã SP
            }
            else
            {
                // Tự động sinh mã sản phẩm
                txtProductCode.Text = GenerateProductCode();
                cboColor.SelectedIndex = 0;
                cboClarity.SelectedIndex = 0;
                cboCut.SelectedIndex = 0;
            }
        }

        private void LoadStoneTypes()
        {
            try
            {
                var stoneTypes = _productService.GetAllStoneTypes();
                cboStoneType.Items.Clear();
                foreach (var st in stoneTypes)
                {
                    cboStoneType.Items.Add(st);
                }
                cboStoneType.DisplayMember = "Name";
                cboStoneType.ValueMember = "StoneTypeId";

                // Khi thay đổi loại đá -> cập nhật mã SP
                cboStoneType.SelectedIndexChanged += cboStoneType_SelectedIndexChanged;

                if (cboStoneType.Items.Count > 0)
                    cboStoneType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải loại đá: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ComboBox chứng chỉ - được tạo động
        private ComboBox cboCertificate;
        private Label lblCertificate;

        private void LoadCertificates()
        {
            try
            {
                // Resize DisplayLocation to make space for Certificate
                if (txtDisplayLocation != null)
                {
                    txtDisplayLocation.Width = 200;
                }

                // Tạo label và textbox cho chứng chỉ
                lblCertificate = new Label
                {
                    AutoSize = true,
                    Font = new System.Drawing.Font("Segoe UI", 10F),
                    ForeColor = System.Drawing.Color.White,
                    Location = new System.Drawing.Point(230, 310),
                    Text = "📜 Chứng chỉ (*BẮT BUỘC):"
                };

                cboCertificate = new ComboBox
                {
                    BackColor = System.Drawing.Color.FromArgb(50, 50, 70),
                    DropDownStyle = ComboBoxStyle.DropDown, // Allow typing
                    FlatStyle = FlatStyle.Flat,
                    Font = new System.Drawing.Font("Segoe UI", 10F),
                    ForeColor = System.Drawing.Color.White,
                    Location = new System.Drawing.Point(230, 332),
                    Size = new System.Drawing.Size(200, 25),
                    DisplayMember = "DisplayName",
                    ValueMember = "CertId"
                };

                // Thêm vào form
                var pnlMainControls = this.Controls.Find("pnlMain", true);
                if (pnlMainControls.Length > 0)
                {
                    var pnlContentControls = pnlMainControls[0].Controls.Find("pnlContent", true);
                    if (pnlContentControls.Length > 0)
                    {
                        var pnlContent = pnlContentControls[0] as Panel;
                        if (pnlContent != null)
                        {
                            pnlContent.Controls.Add(lblCertificate);
                            pnlContent.Controls.Add(cboCertificate);
                        }
                    }
                }

                // Load danh sách chứng chỉ
                var certificates = _productService.GetAllCertificates();
                cboCertificate.Items.Clear();
                // Note: With DropDown style, "Choose" item might be treated as text if selected. 
                // We'll filter it out in Save logic.

                foreach (var cert in certificates)
                {
                    cboCertificate.Items.Add(new CertificateItem
                    {
                        CertId = cert.CertId,
                        DisplayName = $"{cert.CertCode} ({cert.Issuer})"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo control chứng chỉ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper class for certificate combo
        private class CertificateItem
        {
            public int CertId { get; set; }
            public string DisplayName { get; set; }
            public override string ToString() => DisplayName;
        }

        private void cboStoneType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Chỉ tự động sinh mã cho chế độ thêm mới
            if (!_isEditMode && cboStoneType.SelectedItem != null)
            {
                txtProductCode.Text = GenerateProductCode();
            }
        }

        private void LoadProductData()
        {
            txtProductCode.Text = _product.ProductCode;
            txtName.Text = _product.Name;
            nudCarat.Value = _product.Carat > 0 ? _product.Carat : 1;
            nudStockQty.Value = _product.StockQty;
            nudCostPrice.Value = _product.CostPrice;
            nudSellPrice.Value = _product.SellPrice;
            txtDisplayLocation.Text = _product.DisplayLocation;

            // Set certificate info if available
            // Set certificate info if available
            if (cboCertificate != null && !string.IsNullOrEmpty(_product.CertCode))
            {
                // Try to select if exists in list, otherwise set text
                bool found = false;
                foreach (CertificateItem item in cboCertificate.Items)
                {
                    if (item.DisplayName.StartsWith(_product.CertCode))
                    {
                        cboCertificate.SelectedItem = item;
                        found = true;
                        break;
                    }
                }
                if (!found) cboCertificate.Text = _product.CertCode;
            }

            // Select stone type
            for (int i = 0; i < cboStoneType.Items.Count; i++)
            {
                var st = cboStoneType.Items[i] as StoneType;
                if (st != null && st.StoneTypeId == _product.StoneTypeId)
                {
                    cboStoneType.SelectedIndex = i;
                    break;
                }
            }

            // Select color
            SelectComboItem(cboColor, _product.Color);
            SelectComboItem(cboClarity, _product.Clarity);
            SelectComboItem(cboCut, _product.Cut);
        }

        private void SelectComboItem(ComboBox combo, string value)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if (combo.Items[i].ToString() == value)
                {
                    combo.SelectedIndex = i;
                    return;
                }
            }
            if (combo.Items.Count > 0)
                combo.SelectedIndex = 0;
        }

        private string GenerateProductCode()
        {
            // Lấy tên loại đá đã chọn
            var selectedStoneType = cboStoneType.SelectedItem as StoneType;
            string stoneTypeName = selectedStoneType?.Name ?? "SP";

            // Sinh mã theo format: [PREFIX]-[XXX] (ví dụ: KC-001 cho Kim cương)
            return _productService.GenerateProductCode(stoneTypeName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtProductCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (cboStoneType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại đá!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboStoneType.Focus();
                return;
            }

            if (nudSellPrice.Value <= 0)
            {
                MessageBox.Show("Giá bán phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudSellPrice.Focus();
                return;
            }

            if (cboCertificate != null && string.IsNullOrWhiteSpace(cboCertificate.Text))
            {
                MessageBox.Show("Vui lòng nhập chứng chỉ (BẮT BUỘC)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCertificate.Focus();
                return;
            }

            // Xử lý chứng chỉ: Find or Create
            int certId = 0;
            if (cboCertificate != null)
            {
                // Logic: If user selected an item, use that ID. 
                // If user typed text, check if text matches an existing code, else create new.

                if (cboCertificate.SelectedItem is CertificateItem selectedItem)
                {
                    certId = selectedItem.CertId;
                }
                else
                {
                    string inputCertCode = cboCertificate.Text.Trim();

                    // Simple cleaning if user typed something like "GIA... (Issuer)" manually? 
                    // Assume user types Code mostly.

                    var allCerts = _productService.GetAllCertificates();
                    var existingCert = allCerts.FirstOrDefault(c => c.CertCode.Equals(inputCertCode, StringComparison.OrdinalIgnoreCase));

                    if (existingCert != null)
                    {
                        certId = existingCert.CertId;
                    }
                    else
                    {
                        // Create new
                        var newCert = new Certificate
                        {
                            CertCode = inputCertCode,
                            Issuer = "Nội bộ/Khác", // Default issuer 
                            IssueDate = DateTime.Now,
                            CreatedAt = DateTime.Now
                        };
                        var addResult = _productService.AddCertificate(newCert);
                        if (addResult.Success)
                        {
                            certId = addResult.CertId;
                        }
                        else
                        {
                            MessageBox.Show($"Không thể tạo chứng chỉ mới: {addResult.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

                // Build product object
                var product = new Product
                {
                    ProductCode = txtProductCode.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    StoneTypeId = ((StoneType)cboStoneType.SelectedItem).StoneTypeId,
                    Carat = nudCarat.Value,
                    Color = cboColor.SelectedItem?.ToString() ?? "",
                    Clarity = cboClarity.SelectedItem?.ToString() ?? "",
                    Cut = cboCut.SelectedItem?.ToString() ?? "",
                    StockQty = (int)nudStockQty.Value,
                    CostPrice = nudCostPrice.Value,
                    SellPrice = nudSellPrice.Value,
                    DisplayLocation = txtDisplayLocation.Text.Trim(),
                    CertId = certId  // Chứng chỉ BẮT BUỘC cho đá quý
                };

                if (_isEditMode)
                {
                    product.ProductId = _product.ProductId;
                    var result = _productService.UpdateProduct(product);

                    if (result.Success)
                    {
                        MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    var result = _productService.AddProduct(product);

                    if (result.Success)
                    {
                        MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
