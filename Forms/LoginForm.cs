using System;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class LoginForm : Form
    {
        private readonly EmployeeService _employeeService;
        private List<Branch> _branches;

        public LoginForm()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            LoadBranches();
            txtUsername.Focus();
        }

        private void LoadBranches()
        {
            try
            {
                _branches = _employeeService.GetAllBranches();
                cboBranch.Items.Clear();
                foreach (var branch in _branches)
                {
                    if (branch.IsActive)
                        cboBranch.Items.Add(branch);
                }
                cboBranch.DisplayMember = "Name";
                cboBranch.ValueMember = "BranchId";
                if (cboBranch.Items.Count > 0)
                    cboBranch.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi nhánh: {ex.Message}", "Lỗi");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (cboBranch.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBranch.Focus();
                return;
            }

            var selectedBranch = cboBranch.SelectedItem as Branch;
            int selectedBranchId = selectedBranch?.BranchId ?? 0;

            // Attempt login
            var result = _employeeService.Login(username, password);

            if (result.Success)
            {
                // Kiểm tra quyền truy cập chi nhánh
                if (result.Employee != null)
                {
                    // Admin có quyền truy cập tất cả chi nhánh
                    if (!result.Employee.HasFullBranchAccess)
                    {
                        // NV thường chỉ được đăng nhập vào chi nhánh của mình
                        if (result.Employee.BranchId != selectedBranchId)
                        {
                            MessageBox.Show(
                                $"Bạn không có quyền đăng nhập vào chi nhánh này!\nChi nhánh của bạn: {result.Employee.BranchName}",
                                "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        // Admin: cập nhật BranchId theo chi nhánh đã chọn
                        result.Employee.BranchId = selectedBranchId;
                        result.Employee.BranchName = selectedBranch?.Name ?? "";
                    }
                }

                // Kiểm tra nếu cần đổi mật khẩu (sau khi bị reset)
                if (result.Employee != null && result.Employee.MustChangePassword)
                {
                    ShowChangePasswordDialog();
                }
                
                // Login thành công - đặt DialogResult và đóng form
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Đăng nhập thất bại", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }

        private void ShowChangePasswordDialog()
        {
            MessageBox.Show(
                "Mật khẩu của bạn đã được reset.\nVui lòng đổi mật khẩu mới để tiếp tục sử dụng.",
                "Yêu cầu đổi mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Hiển thị dialog đổi mật khẩu
            using (var changePassForm = new ChangePasswordForm())
            {
                if (changePassForm.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
