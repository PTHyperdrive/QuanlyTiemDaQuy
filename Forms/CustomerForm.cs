using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class CustomerForm : Form
    {
        private readonly CustomerService _customerService;
        private List<Customer> _customers;

        public CustomerForm()
        {
            InitializeComponent();
            _customerService = new CustomerService();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void LoadCustomers()
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                
                if (string.IsNullOrEmpty(keyword))
                    _customers = _customerService.GetAllCustomers();
                else
                    _customers = _customerService.SearchCustomers(keyword);

                dgvCustomers.DataSource = null;
                dgvCustomers.DataSource = _customers;
                FormatDataGridView();
                lblStatus.Text = $"Tìm thấy {_customers.Count} khách hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvCustomers.Columns.Count == 0) return;

            if (dgvCustomers.Columns.Contains("CustomerId"))
                dgvCustomers.Columns["CustomerId"].Visible = false;
            if (dgvCustomers.Columns.Contains("DisplayText"))
                dgvCustomers.Columns["DisplayText"].Visible = false;

            if (dgvCustomers.Columns.Contains("Name"))
                dgvCustomers.Columns["Name"].HeaderText = "Tên khách hàng";
            if (dgvCustomers.Columns.Contains("Phone"))
                dgvCustomers.Columns["Phone"].HeaderText = "Điện thoại";
            if (dgvCustomers.Columns.Contains("Email"))
                dgvCustomers.Columns["Email"].HeaderText = "Email";
            if (dgvCustomers.Columns.Contains("Address"))
                dgvCustomers.Columns["Address"].HeaderText = "Địa chỉ";
            if (dgvCustomers.Columns.Contains("Tier"))
                dgvCustomers.Columns["Tier"].HeaderText = "Hạng";
            if (dgvCustomers.Columns.Contains("TotalPurchase"))
            {
                dgvCustomers.Columns["TotalPurchase"].HeaderText = "Tổng mua";
                dgvCustomers.Columns["TotalPurchase"].DefaultCellStyle.Format = "N0";
            }
            if (dgvCustomers.Columns.Contains("CreatedAt"))
                dgvCustomers.Columns["CreatedAt"].HeaderText = "Ngày tạo";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng Thêm khách hàng sẽ được phát triển!", "Thông báo");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Thông báo");
                return;
            }
            MessageBox.Show("Chức năng Sửa khách hàng sẽ được phát triển!", "Thông báo");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Thông báo");
                return;
            }

            var customer = dgvCustomers.SelectedRows[0].DataBoundItem as Customer;
            if (customer == null) return;

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng '{customer.Name}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _customerService.DeleteCustomer(customer.CustomerId);
                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
                if (result.Success) LoadCustomers();
            }
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
