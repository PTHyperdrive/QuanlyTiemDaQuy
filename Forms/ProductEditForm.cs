using System;
using System.Windows.Forms;
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
                DisplayLocation = txtDisplayLocation.Text.Trim()
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
