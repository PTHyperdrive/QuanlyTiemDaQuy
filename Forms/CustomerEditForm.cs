using System;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    /// <summary>
    /// Form để thêm/sửa khách hàng
    /// </summary>
    public partial class CustomerEditForm : Form
    {
        private readonly CustomerService _customerService;
        private readonly Customer _customer;
        private readonly bool _isEditMode;

        /// <summary>
        /// Khởi tạo form thêm khách hàng mới
        /// </summary>
        public CustomerEditForm()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _customer = new Customer();
            _isEditMode = false;
            this.Text = "✨ Thêm khách hàng mới";
        }

        /// <summary>
        /// Khởi tạo form sửa khách hàng
        /// </summary>
        public CustomerEditForm(Customer customer)
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _customer = customer;
            _isEditMode = true;
            this.Text = "✏️ Sửa khách hàng: " + customer.Name;
        }

        private void CustomerEditForm_Load(object sender, EventArgs e)
        {
            cboTier.SelectedIndex = 0; // Mặc định "Thường"
            
            if (_isEditMode)
            {
                LoadCustomerData();
            }
            else
            {
                lblTotalPurchaseValue.Text = "0 VNĐ";
            }
        }

        private void LoadCustomerData()
        {
            txtName.Text = _customer.Name;
            txtPhone.Text = _customer.Phone;
            txtEmail.Text = _customer.Email;
            txtAddress.Text = _customer.Address;
            
            // Select tier
            for (int i = 0; i < cboTier.Items.Count; i++)
            {
                if (cboTier.Items[i].ToString() == _customer.Tier)
                {
                    cboTier.SelectedIndex = i;
                    break;
                }
            }
            
            // Hiển thị tổng mua hàng
            lblTotalPurchaseValue.Text = _customer.TotalPurchase.ToString("N0") + " VNĐ";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            // Validate phone format
            if (!IsValidPhone(txtPhone.Text.Trim()))
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            // Validate email if provided
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Email không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            // Build customer object
            var customer = new Customer
            {
                Name = txtName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                Tier = cboTier.SelectedItem?.ToString() ?? "Thường"
            };

            if (_isEditMode)
            {
                customer.CustomerId = _customer.CustomerId;
                customer.TotalPurchase = _customer.TotalPurchase; // Giữ nguyên tổng mua
                
                var result = _customerService.UpdateCustomer(customer);
                
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
                var result = _customerService.AddCustomer(customer);
                
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

        private bool IsValidPhone(string phone)
        {
            // Simple validation for Vietnamese phone numbers
            if (phone.Length < 9 || phone.Length > 15)
                return false;
            
            foreach (char c in phone)
            {
                if (!char.IsDigit(c) && c != '+' && c != '-' && c != ' ')
                    return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
