using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class SalesForm : Form
    {
        private readonly SalesService _salesService;
        private readonly CustomerService _customerService;
        private readonly ProductService _productService;
        private List<InvoiceDetail> _cart;
        private Invoice _currentInvoice;

        // Các thành phần autocomplete
        private ListBox lstProductSuggestions;
        private List<Product> _allProducts;

        public SalesForm()
        {
            InitializeComponent();
            _salesService = new SalesService();
            _customerService = new CustomerService();
            _productService = new ProductService();
            _cart = new List<InvoiceDetail>();
            _currentInvoice = new Invoice();
            
            SetupAutocomplete();
        }

        private void SetupAutocomplete()
        {
            // Tạo listbox gợi ý
            lstProductSuggestions = new ListBox();
            lstProductSuggestions.BackColor = Color.FromArgb(50, 50, 70);
            lstProductSuggestions.ForeColor = Color.White;
            lstProductSuggestions.Font = new Font("Segoe UI", 10F);
            lstProductSuggestions.BorderStyle = BorderStyle.FixedSingle;
            lstProductSuggestions.Visible = false;
            lstProductSuggestions.Height = 150;
            lstProductSuggestions.Width = txtProductCode.Width + 150;
            lstProductSuggestions.Click += LstProductSuggestions_Click;
            lstProductSuggestions.KeyDown += LstProductSuggestions_KeyDown;
            
            // Thêm vào form
            this.Controls.Add(lstProductSuggestions);
            lstProductSuggestions.BringToFront();

            // Kết nối sự kiện textbox
            txtProductCode.TextChanged += TxtProductCode_TextChanged;
            txtProductCode.Leave += TxtProductCode_Leave;
            txtProductCode.KeyDown += TxtProductCode_KeyDown;
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadProducts();
            UpdateTotals();
            txtProductCode.Focus();
        }

        private void LoadProducts()
        {
            try
            {
                _allProducts = _productService.GetAllProducts();
            }
            catch (Exception ex)
            {
                _allProducts = new List<Product>();
                MessageBox.Show($"Lỗi tải sản phẩm: {ex.Message}", "Lỗi");
            }
        }

        private void TxtProductCode_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtProductCode.Text.Trim().ToUpper();
            
            if (string.IsNullOrEmpty(searchText) || searchText.Length < 1)
            {
                lstProductSuggestions.Visible = false;
                return;
            }

            // Lọc sản phẩm bắt đầu với từ khóa tìm kiếm
            var matches = new List<Product>();
            foreach (var p in _allProducts)
            {
                if (p.ProductCode.ToUpper().StartsWith(searchText) || 
                    p.Name.ToUpper().Contains(searchText))
                {
                    if (p.StockQty > 0) // Chỉ hiển thị sản phẩm còn hàng
                        matches.Add(p);
                }
            }

            if (matches.Count > 0)
            {
                lstProductSuggestions.Items.Clear();
                foreach (var p in matches)
                {
                    lstProductSuggestions.Items.Add($"{p.ProductCode} - {p.Name} ({p.StockQty} còn) - {p.SellPrice:N0}đ");
                }

                // Đặt vị trí listbox bên dưới textbox
                Point txtLocation = txtProductCode.Parent.PointToScreen(txtProductCode.Location);
                Point formLocation = this.PointToClient(txtLocation);
                lstProductSuggestions.Location = new Point(formLocation.X, formLocation.Y + txtProductCode.Height + 2);
                lstProductSuggestions.Visible = true;
                lstProductSuggestions.BringToFront();
            }
            else
            {
                lstProductSuggestions.Visible = false;
            }
        }

        private void TxtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (lstProductSuggestions.Visible)
            {
                if (e.KeyCode == Keys.Down)
                {
                    lstProductSuggestions.Focus();
                    if (lstProductSuggestions.Items.Count > 0)
                        lstProductSuggestions.SelectedIndex = 0;
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    lstProductSuggestions.Visible = false;
                    e.Handled = true;
                }
            }
        }

        private void LstProductSuggestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectSuggestion();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lstProductSuggestions.Visible = false;
                txtProductCode.Focus();
                e.Handled = true;
            }
        }

        private void LstProductSuggestions_Click(object sender, EventArgs e)
        {
            SelectSuggestion();
        }

        private void SelectSuggestion()
        {
            if (lstProductSuggestions.SelectedItem != null)
            {
                string selectedText = lstProductSuggestions.SelectedItem.ToString();
                // Trích xuất mã sản phẩm (trước dấu " - " đầu tiên)
                int dashIndex = selectedText.IndexOf(" - ");
                if (dashIndex > 0)
                {
                    string productCode = selectedText.Substring(0, dashIndex);
                    
                    // Tạm thời bỏ event handler để tránh lặp vô hạn
                    txtProductCode.TextChanged -= TxtProductCode_TextChanged;
                    txtProductCode.Text = productCode;
                    txtProductCode.TextChanged += TxtProductCode_TextChanged;
                }
                
                lstProductSuggestions.Visible = false;
                txtProductCode.Focus();
                txtProductCode.SelectionStart = txtProductCode.Text.Length;
            }
        }

        private void TxtProductCode_Leave(object sender, EventArgs e)
        {
            // Ẩn gợi ý sau một khoảng ngắn (để cho phép click vào list)
            Timer hideTimer = new Timer();
            hideTimer.Interval = 200;
            hideTimer.Tick += (s, args) =>
            {
                if (!lstProductSuggestions.Focused)
                    lstProductSuggestions.Visible = false;
                hideTimer.Stop();
                hideTimer.Dispose();
            };
            hideTimer.Start();
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();
                cboCustomer.Items.Clear();
                cboCustomer.Items.Add(new Customer { CustomerId = 0, Name = "-- Khách lẻ --" });
                foreach (var c in customers)
                {
                    cboCustomer.Items.Add(c);
                }
                cboCustomer.DisplayMember = "Name";
                cboCustomer.ValueMember = "CustomerId";
                cboCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải khách hàng: {ex.Message}", "Lỗi");
            }
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            lstProductSuggestions.Visible = false;
            
            string productCode = txtProductCode.Text.Trim();
            int qty = (int)nudQty.Value;

            var result = _salesService.AddToCart(productCode, qty, _cart);

            if (result.Success && result.Detail != null)
            {
                // Kiểm tra sản phẩm đã có trong giỏ chưa
                bool found = false;
                foreach (var item in _cart)
                {
                    if (item.ProductId == result.Detail.ProductId)
                    {
                        item.Qty += result.Detail.Qty;
                        item.LineTotal = item.Qty * item.UnitPrice;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    _cart.Add(result.Detail);
                }

                RefreshCart();
                UpdateTotals();
                
                // Xóa và focus lại
                txtProductCode.TextChanged -= TxtProductCode_TextChanged;
                txtProductCode.Clear();
                txtProductCode.TextChanged += TxtProductCode_TextChanged;
                txtProductCode.Focus();
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshCart()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart;
            FormatCartGrid();
        }

        private void FormatCartGrid()
        {
            if (dgvCart.Columns.Count == 0) return;

            if (dgvCart.Columns.Contains("InvoiceDetailId"))
                dgvCart.Columns["InvoiceDetailId"].Visible = false;
            if (dgvCart.Columns.Contains("InvoiceId"))
                dgvCart.Columns["InvoiceId"].Visible = false;
            if (dgvCart.Columns.Contains("ProductId"))
                dgvCart.Columns["ProductId"].Visible = false;

            if (dgvCart.Columns.Contains("ProductCode"))
                dgvCart.Columns["ProductCode"].HeaderText = "Mã SP";
            if (dgvCart.Columns.Contains("ProductName"))
                dgvCart.Columns["ProductName"].HeaderText = "Tên sản phẩm";
            if (dgvCart.Columns.Contains("Qty"))
                dgvCart.Columns["Qty"].HeaderText = "SL";
            if (dgvCart.Columns.Contains("UnitPrice"))
            {
                dgvCart.Columns["UnitPrice"].HeaderText = "Đơn giá";
                dgvCart.Columns["UnitPrice"].DefaultCellStyle.Format = "N0";
            }
            if (dgvCart.Columns.Contains("LineTotal"))
            {
                dgvCart.Columns["LineTotal"].HeaderText = "Thành tiền";
                dgvCart.Columns["LineTotal"].DefaultCellStyle.Format = "N0";
            }
        }

        private void UpdateTotals()
        {
            decimal discount = (decimal)nudDiscount.Value;
            decimal vat = (decimal)nudVAT.Value;

            var invoice = _salesService.CalculateInvoiceTotals(_cart, discount, vat);

            lblSubtotal.Text = invoice.Subtotal.ToString("N0") + " VNĐ";
            lblDiscount.Text = "-" + invoice.DiscountAmount.ToString("N0") + " VNĐ";
            lblVAT.Text = "+" + invoice.VATAmount.ToString("N0") + " VNĐ";
            lblTotal.Text = invoice.Total.ToString("N0") + " VNĐ";

            _currentInvoice = invoice;
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo");
                return;
            }

            var item = dgvCart.SelectedRows[0].DataBoundItem as InvoiceDetail;
            if (item != null)
            {
                _cart.Remove(item);
                RefreshCart();
                UpdateTotals();
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0) return;

            var confirm = MessageBox.Show("Xóa toàn bộ giỏ hàng?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _cart.Clear();
                RefreshCart();
                UpdateTotals();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!", "Thông báo");
                return;
            }

            // Chuẩn bị hoá đơn
            var invoice = new Invoice
            {
                Details = new List<InvoiceDetail>(_cart),
                DiscountPercent = (decimal)nudDiscount.Value,
                VAT = (decimal)nudVAT.Value,
                PaymentMethod = cboPaymentMethod.SelectedItem?.ToString() ?? "Tiền mặt",
                EmployeeId = EmployeeService.CurrentEmployee?.EmployeeId ?? 1
            };

            // Đặt khách hàng
            var selectedCustomer = cboCustomer.SelectedItem as Customer;
            if (selectedCustomer != null && selectedCustomer.CustomerId > 0)
            {
                invoice.CustomerId = selectedCustomer.CustomerId;
            }

            // Tạo hoá đơn
            var result = _salesService.CreateInvoice(invoice);

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa giỏ hàng
                _cart.Clear();
                RefreshCart();
                UpdateTotals();
                cboCustomer.SelectedIndex = 0;
                
                // Tải lại sản phẩm để cập nhật tồn kho
                LoadProducts();

                // TODO: In hoá đơn
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void nudVAT_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (lstProductSuggestions.Visible && lstProductSuggestions.SelectedIndex >= 0)
                {
                    SelectSuggestion();
                }
                else
                {
                    btnAddToCart_Click(sender, e);
                }
                e.Handled = true;
            }
        }

        #region Quy trình Hoá đơn chờ thanh toán

        /// <summary>
        /// Lưu hoá đơn đang chờ thanh toán (mỗi chi nhánh chỉ 1 HĐ pending)
        /// </summary>
        private void btnSavePending_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống!", "Thông báo");
                return;
            }

            int branchId = EmployeeService.CurrentEmployee?.BranchId ?? 1;

            // Kiểm tra đã có HĐ pending chưa
            if (_salesService.HasPendingInvoice(branchId))
            {
                MessageBox.Show("Chi nhánh đã có 1 hoá đơn đang chờ thanh toán!\nVui lòng hoàn thành hoặc huỷ hoá đơn đó trước.", 
                    "Không thể lưu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo hoá đơn pending (không auto complete)
            var invoice = new Invoice
            {
                Details = new List<InvoiceDetail>(_cart),
                DiscountPercent = (decimal)nudDiscount.Value,
                VAT = (decimal)nudVAT.Value,
                PaymentMethod = cboPaymentMethod.SelectedItem?.ToString() ?? "Tiền mặt",
                EmployeeId = EmployeeService.CurrentEmployee?.EmployeeId ?? 1,
                BranchId = branchId,
                Status = InvoiceStatus.Pending
            };

            var selectedCustomer = cboCustomer.SelectedItem as Customer;
            if (selectedCustomer != null && selectedCustomer.CustomerId > 0)
            {
                invoice.CustomerId = selectedCustomer.CustomerId;
            }

            var result = _salesService.CreateInvoice(invoice, autoComplete: false);

            if (result.Success)
            {
                MessageBox.Show($"Đã lưu hoá đơn chờ thanh toán!\nMã HĐ: {result.Message}", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                _cart.Clear();
                RefreshCart();
                UpdateTotals();
                cboCustomer.SelectedIndex = 0;
                LoadProducts();
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy lại hoá đơn đang chờ thanh toán của chi nhánh
        /// </summary>
        private void btnResumePending_Click(object sender, EventArgs e)
        {
            int branchId = EmployeeService.CurrentEmployee?.BranchId ?? 1;
            
            var pendingInvoice = _salesService.GetPendingInvoiceByBranch(branchId);
            if (pendingInvoice == null)
            {
                MessageBox.Show("Không có hoá đơn đang chờ thanh toán!", "Thông báo");
                return;
            }

            // Tải chi tiết hoá đơn vào giỏ hàng
            _cart.Clear();
            if (pendingInvoice.Details != null)
            {
                foreach (var detail in pendingInvoice.Details)
                {
                    _cart.Add(detail);
                }
            }

            // Đặt giá trị form
            nudDiscount.Value = pendingInvoice.DiscountPercent;
            nudVAT.Value = pendingInvoice.VAT;
            
            // Tìm và chọn khách hàng
            for (int i = 0; i < cboCustomer.Items.Count; i++)
            {
                var customer = cboCustomer.Items[i] as Customer;
                if (customer != null && customer.CustomerId == pendingInvoice.CustomerId)
                {
                    cboCustomer.SelectedIndex = i;
                    break;
                }
            }

            RefreshCart();
            UpdateTotals();

            MessageBox.Show($"Đã lấy lại hoá đơn: {pendingInvoice.InvoiceCode}", "Thông tin", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Huỷ hoá đơn đang chờ thanh toán
        /// </summary>
        private void btnCancelPending_Click(object sender, EventArgs e)
        {
            int branchId = EmployeeService.CurrentEmployee?.BranchId ?? 1;
            
            var pendingInvoice = _salesService.GetPendingInvoiceByBranch(branchId);
            if (pendingInvoice == null)
            {
                MessageBox.Show("Không có hoá đơn đang chờ thanh toán để huỷ!", "Thông báo");
                return;
            }

            var confirm = MessageBox.Show(
                $"Huỷ hoá đơn chờ thanh toán?\nMã HĐ: {pendingInvoice.InvoiceCode}\nTổng: {pendingInvoice.Total:N0} VNĐ",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _salesService.CancelInvoice(pendingInvoice.InvoiceId, "Khách không thanh toán");
                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.Success)
                {
                    LoadProducts(); // Refresh stock
                }
            }
        }

        #endregion
    }
}
