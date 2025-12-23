using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class UserManagementForm : Form
    {
        private readonly EmployeeService _employeeService;
        private List<Employee> _employees;
        private Employee _selectedEmployee;

        public UserManagementForm()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền truy cập
            if (!CanAccessUserManagement())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!\nChỉ Admin và Manager mới có thể quản lý tài khoản.", 
                    "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadRoles();
            LoadEmployees();
            ClearForm();
        }

        private bool CanAccessUserManagement()
        {
            var current = EmployeeService.CurrentEmployee;
            return current != null && (current.IsAdmin || current.IsManager);
        }

        private bool CanAddEmployee()
        {
            var current = EmployeeService.CurrentEmployee;
            return current != null && (current.IsAdmin || current.IsManager);
        }

        private void LoadRoles()
        {
            cboRole.Items.Clear();
            cboRole.Items.Add(EmployeeRoles.Admin);
            cboRole.Items.Add(EmployeeRoles.Manager);
            cboRole.Items.Add(EmployeeRoles.Sales);
            cboRole.SelectedIndex = 2; // Default: Sales
        }

        private void LoadEmployees()
        {
            try
            {
                _employees = _employeeService.GetAllEmployees();
                dgvEmployees.DataSource = null;
                dgvEmployees.DataSource = _employees;
                FormatDataGridView();
                lblStatus.Text = $"Tổng: {_employees.Count} nhân viên";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}", "Lỗi");
            }
        }

        private void FormatDataGridView()
        {
            if (dgvEmployees.Columns.Count == 0) return;

            if (dgvEmployees.Columns.Contains("EmployeeId"))
                dgvEmployees.Columns["EmployeeId"].Visible = false;
            if (dgvEmployees.Columns.Contains("PasswordHash"))
                dgvEmployees.Columns["PasswordHash"].Visible = false;
            if (dgvEmployees.Columns.Contains("IsAdmin"))
                dgvEmployees.Columns["IsAdmin"].Visible = false;
            if (dgvEmployees.Columns.Contains("IsManager"))
                dgvEmployees.Columns["IsManager"].Visible = false;
            if (dgvEmployees.Columns.Contains("IsSales"))
                dgvEmployees.Columns["IsSales"].Visible = false;

            if (dgvEmployees.Columns.Contains("Name"))
                dgvEmployees.Columns["Name"].HeaderText = "Họ tên";
            if (dgvEmployees.Columns.Contains("Username"))
                dgvEmployees.Columns["Username"].HeaderText = "Tên đăng nhập";
            if (dgvEmployees.Columns.Contains("Role"))
                dgvEmployees.Columns["Role"].HeaderText = "Vai trò";
            if (dgvEmployees.Columns.Contains("Phone"))
                dgvEmployees.Columns["Phone"].HeaderText = "Điện thoại";
            if (dgvEmployees.Columns.Contains("Email"))
                dgvEmployees.Columns["Email"].HeaderText = "Email";
            if (dgvEmployees.Columns.Contains("IsActive"))
                dgvEmployees.Columns["IsActive"].HeaderText = "Hoạt động";
            if (dgvEmployees.Columns.Contains("CreatedAt"))
                dgvEmployees.Columns["CreatedAt"].HeaderText = "Ngày tạo";

            // Đánh dấu các dòng không hoạt động
            foreach (DataGridViewRow row in dgvEmployees.Rows)
            {
                var emp = row.DataBoundItem as Employee;
                if (emp != null && !emp.IsActive)
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.DefaultCellStyle.Font = new Font(dgvEmployees.Font, FontStyle.Strikeout);
                }
            }
        }

        private void ClearForm()
        {
            _selectedEmployee = null;
            txtName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            cboRole.SelectedIndex = 2;
            chkActive.Checked = true;
            
            btnAdd.Enabled = CanAddEmployee();
            btnUpdate.Enabled = false;
            btnDeactivate.Enabled = false;
            btnResetPassword.Enabled = false;
            
            txtName.Focus();
            grpDetails.Text = "Thông tin nhân viên - Thêm mới";
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;

            _selectedEmployee = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;
            if (_selectedEmployee == null) return;

            txtName.Text = _selectedEmployee.Name;
            txtUsername.Text = _selectedEmployee.Username;
            txtPassword.Clear();
            txtPhone.Text = _selectedEmployee.Phone;
            txtEmail.Text = _selectedEmployee.Email;
            cboRole.SelectedItem = _selectedEmployee.Role;
            chkActive.Checked = _selectedEmployee.IsActive;

            btnAdd.Enabled = CanAddEmployee();
            btnUpdate.Enabled = CanAddEmployee();
            btnDeactivate.Enabled = CanAddEmployee() && _selectedEmployee.IsActive;
            btnResetPassword.Enabled = EmployeeService.CurrentEmployee?.IsAdmin ?? false;
            
            grpDetails.Text = $"Thông tin nhân viên - {_selectedEmployee.Name}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CanAddEmployee())
            {
                MessageBox.Show("Bạn không có quyền thêm nhân viên!", "Không có quyền");
                return;
            }

            // Validate inputs
            var validation = InputValidator.ValidateEmployee(
                txtName.Text, txtUsername.Text, txtPassword.Text, 
                txtEmail.Text, txtPhone.Text);

            if (!validation.IsValid)
            {
                MessageBox.Show(validation.Message, "Dữ liệu không hợp lệ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var employee = new Employee
            {
                Name = InputValidator.Sanitize(txtName.Text.Trim()),
                Username = txtUsername.Text.Trim().ToLower(),
                Phone = InputValidator.Sanitize(txtPhone.Text.Trim()),
                Email = InputValidator.Sanitize(txtEmail.Text.Trim()),
                Role = cboRole.SelectedItem?.ToString() ?? EmployeeRoles.Sales,
                IsActive = chkActive.Checked
            };

            var result = _employeeService.AddEmployee(employee, txtPassword.Text);
            
            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                LoadEmployees();
                ClearForm();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo");
                return;
            }

            if (!CanAddEmployee())
            {
                MessageBox.Show("Bạn không có quyền sửa thông tin nhân viên!", "Không có quyền");
                return;
            }

            // Validate inputs (password không bắt buộc khi update)
            var nameVal = InputValidator.ValidateName(txtName.Text);
            if (!nameVal.IsValid) { MessageBox.Show(nameVal.Message, "Lỗi"); return; }

            var usernameVal = InputValidator.ValidateUsername(txtUsername.Text);
            if (!usernameVal.IsValid) { MessageBox.Show(usernameVal.Message, "Lỗi"); return; }

            var emailVal = InputValidator.ValidateEmail(txtEmail.Text);
            if (!emailVal.IsValid) { MessageBox.Show(emailVal.Message, "Lỗi"); return; }

            var phoneVal = InputValidator.ValidatePhone(txtPhone.Text);
            if (!phoneVal.IsValid) { MessageBox.Show(phoneVal.Message, "Lỗi"); return; }

            _selectedEmployee.Name = InputValidator.Sanitize(txtName.Text.Trim());
            _selectedEmployee.Username = txtUsername.Text.Trim().ToLower();
            _selectedEmployee.Phone = InputValidator.Sanitize(txtPhone.Text.Trim());
            _selectedEmployee.Email = InputValidator.Sanitize(txtEmail.Text.Trim());
            _selectedEmployee.Role = cboRole.SelectedItem?.ToString() ?? EmployeeRoles.Sales;
            _selectedEmployee.IsActive = chkActive.Checked;

            var result = _employeeService.UpdateEmployee(_selectedEmployee);
            
            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                LoadEmployees();
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần vô hiệu hóa!", "Thông báo");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn vô hiệu hóa tài khoản '{_selectedEmployee.Username}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _employeeService.DeactivateEmployee(_selectedEmployee.EmployeeId);
                
                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.Success)
                {
                    LoadEmployees();
                    ClearForm();
                }
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần reset mật khẩu!", "Thông báo");
                return;
            }

            if (!(EmployeeService.CurrentEmployee?.IsAdmin ?? false))
            {
                MessageBox.Show("Chỉ Admin mới có quyền reset mật khẩu!", "Không có quyền");
                return;
            }

            string newPassword = "123456"; // Mật khẩu mặc định
            
            var confirm = MessageBox.Show(
                $"Reset mật khẩu cho '{_selectedEmployee.Username}' về '{newPassword}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _employeeService.ResetPassword(_selectedEmployee.EmployeeId, newPassword);
                
                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
