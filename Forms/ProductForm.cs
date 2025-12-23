using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class ProductForm : Form
    {
        private readonly ProductService _productService;
        private List<Product> _products;

        public ProductForm()
        {
            InitializeComponent();
            _productService = new ProductService();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            LoadStoneTypes();
            LoadProducts();
        }

        private void LoadStoneTypes()
        {
            try
            {
                var stoneTypes = _productService.GetAllStoneTypes();
                cboStoneType.Items.Clear();
                cboStoneType.Items.Add(new { Name = "-- Tất cả --", StoneTypeId = 0 });
                foreach (var st in stoneTypes)
                {
                    cboStoneType.Items.Add(st);
                }
                cboStoneType.DisplayMember = "Name";
                cboStoneType.ValueMember = "StoneTypeId";
                cboStoneType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải loại đá: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProducts()
        {
            try
            {
                // Lấy giá trị bộ lọc
                string keyword = txtSearch.Text.Trim();
                int? stoneTypeId = null;
                if (cboStoneType.SelectedIndex > 0)
                {
                    var selected = cboStoneType.SelectedItem as StoneType;
                    if (selected != null) stoneTypeId = selected.StoneTypeId;
                }

                decimal? minPrice = null, maxPrice = null;
                if (decimal.TryParse(txtMinPrice.Text, out decimal min)) minPrice = min;
                if (decimal.TryParse(txtMaxPrice.Text, out decimal max)) maxPrice = max;

                // Tìm kiếm sản phẩm
                _products = _productService.SearchProducts(
                    keyword: string.IsNullOrEmpty(keyword) ? null : keyword,
                    stoneTypeId: stoneTypeId,
                    minPrice: minPrice,
                    maxPrice: maxPrice
                );

                // Gán dữ liệu vào DataGridView
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = _products;

                // Định dạng các cột
                FormatDataGridView();

                lblStatus.Text = $"Tìm thấy {_products.Count} sản phẩm";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvProducts.Columns.Count == 0) return;

            // Ẩn một số cột
            string[] hiddenColumns = { "ProductId", "StoneTypeId", "CertId", "ImagePath", "UpdatedAt" };
            foreach (var col in hiddenColumns)
            {
                if (dgvProducts.Columns.Contains(col))
                    dgvProducts.Columns[col].Visible = false;
            }

            // Thiết lập tiêu đề cột
            if (dgvProducts.Columns.Contains("ProductCode"))
                dgvProducts.Columns["ProductCode"].HeaderText = "Mã SP";
            if (dgvProducts.Columns.Contains("Name"))
                dgvProducts.Columns["Name"].HeaderText = "Tên sản phẩm";
            if (dgvProducts.Columns.Contains("StoneTypeName"))
                dgvProducts.Columns["StoneTypeName"].HeaderText = "Loại đá";
            if (dgvProducts.Columns.Contains("Carat"))
                dgvProducts.Columns["Carat"].HeaderText = "Carat";
            if (dgvProducts.Columns.Contains("Color"))
                dgvProducts.Columns["Color"].HeaderText = "Màu";
            if (dgvProducts.Columns.Contains("Clarity"))
                dgvProducts.Columns["Clarity"].HeaderText = "Độ tinh khiết";
            if (dgvProducts.Columns.Contains("Cut"))
                dgvProducts.Columns["Cut"].HeaderText = "Giác cắt";
            if (dgvProducts.Columns.Contains("CostPrice"))
            {
                dgvProducts.Columns["CostPrice"].HeaderText = "Giá vốn";
                dgvProducts.Columns["CostPrice"].DefaultCellStyle.Format = "N0";
                dgvProducts.Columns["CostPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProducts.Columns.Contains("SellPrice"))
            {
                dgvProducts.Columns["SellPrice"].HeaderText = "Giá bán";
                dgvProducts.Columns["SellPrice"].DefaultCellStyle.Format = "N0";
                dgvProducts.Columns["SellPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvProducts.Columns.Contains("StockQty"))
                dgvProducts.Columns["StockQty"].HeaderText = "Tồn kho";
            if (dgvProducts.Columns.Contains("Status"))
                dgvProducts.Columns["Status"].HeaderText = "Trạng thái";

            // Đánh dấu sản phẩm tồn kho thấp
            foreach (DataGridViewRow row in dgvProducts.Rows)
            {
                if (row.DataBoundItem is Product product)
                {
                    if (product.IsOutOfStock)
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200);
                    else if (product.IsLowStock)
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtMinPrice.Clear();
            txtMaxPrice.Clear();
            cboStoneType.SelectedIndex = 0;
            LoadProducts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thêm sản phẩm sẽ được phát triển!", "Thông báo");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Chức năng Sửa sản phẩm sẽ được phát triển!", "Thông báo");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = dgvProducts.SelectedRows[0].DataBoundItem as Product;
            if (product == null) return;

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm '{product.Name}'?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _productService.DeleteProduct(product.ProductId);
                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.Success)
                    LoadProducts();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
