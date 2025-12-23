using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    /// <summary>
    /// Form quản lý hệ thống: Tài khoản và Chi nhánh
    /// </summary>
    public partial class SystemManagementForm : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly BranchService _branchService;
        private List<Employee> _employees;
        private List<Branch> _branches;
        private Employee _selectedEmployee;
        private Branch _selectedBranch;

        public SystemManagementForm()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _branchService = new BranchService();
        }

        private void SystemManagementForm_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền
            if (!CanAccessSystemManagement())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!\nChỉ Admin và Manager mới có thể quản lý hệ thống.", 
                    "Không có quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadBranchesCombo();
            LoadRoles();
            LoadEmployees();
            LoadBranches();
            ClearEmployeeForm();
            ClearBranchForm();
        }

        private bool CanAccessSystemManagement()
        {
            var current = EmployeeService.CurrentEmployee;
            return current != null && (current.IsAdmin || current.IsManager);
        }

        #region Tab Quản lý Tài khoản

        private void LoadRoles()
        {
            cboEmpRole.Items.Clear();
            cboEmpRole.Items.Add(EmployeeRoles.Admin);
            cboEmpRole.Items.Add(EmployeeRoles.Manager);
            cboEmpRole.Items.Add(EmployeeRoles.Sales);
            cboEmpRole.SelectedIndex = 2;
        }

        private void LoadBranchesCombo()
        {
            try
            {
                var branches = _branchService.GetActiveBranches();
                cboEmpBranch.Items.Clear();
                foreach (var b in branches)
                {
                    cboEmpBranch.Items.Add(b);
                }
                cboEmpBranch.DisplayMember = "Name";
                cboEmpBranch.ValueMember = "BranchId";
                if (cboEmpBranch.Items.Count > 0)
                    cboEmpBranch.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi nhánh: {ex.Message}", "Lỗi");
            }
        }

        private void LoadEmployees()
        {
            try
            {
                _employees = _employeeService.GetAllEmployees();
                dgvEmployees.DataSource = null;
                dgvEmployees.DataSource = _employees;
                FormatEmployeeGrid();
                lblEmpStatus.Text = $"Tổng: {_employees.Count} tài khoản";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}", "Lỗi");
            }
        }

        private void FormatEmployeeGrid()
        {
            if (dgvEmployees.Columns.Count == 0) return;

            var hiddenCols = new[] { "EmployeeId", "PasswordHash", "IsAdmin", "IsManager", "IsSales", "HasFullBranchAccess", "MustChangePassword" };
            foreach (var col in hiddenCols)
            {
                if (dgvEmployees.Columns.Contains(col))
                    dgvEmployees.Columns[col].Visible = false;
            }

            if (dgvEmployees.Columns.Contains("Name"))
                dgvEmployees.Columns["Name"].HeaderText = "Họ tên";
            if (dgvEmployees.Columns.Contains("Username"))
                dgvEmployees.Columns["Username"].HeaderText = "Username";
            if (dgvEmployees.Columns.Contains("Role"))
                dgvEmployees.Columns["Role"].HeaderText = "Vai trò";
            if (dgvEmployees.Columns.Contains("Phone"))
                dgvEmployees.Columns["Phone"].HeaderText = "SĐT";
            if (dgvEmployees.Columns.Contains("Email"))
                dgvEmployees.Columns["Email"].HeaderText = "Email";
            if (dgvEmployees.Columns.Contains("BranchName"))
                dgvEmployees.Columns["BranchName"].HeaderText = "Chi nhánh";
            if (dgvEmployees.Columns.Contains("BranchId"))
                dgvEmployees.Columns["BranchId"].Visible = false;
            if (dgvEmployees.Columns.Contains("IsActive"))
                dgvEmployees.Columns["IsActive"].HeaderText = "Hoạt động";
            if (dgvEmployees.Columns.Contains("CreatedAt"))
                dgvEmployees.Columns["CreatedAt"].HeaderText = "Ngày tạo";

            // Đánh dấu tài khoản không hoạt động
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

        private void ClearEmployeeForm()
        {
            _selectedEmployee = null;
            txtEmpName.Clear();
            txtEmpUsername.Clear();
            txtEmpPhone.Clear();
            txtEmpEmail.Clear();
            cboEmpRole.SelectedIndex = 2;
            if (cboEmpBranch.Items.Count > 0)
                cboEmpBranch.SelectedIndex = 0;
            chkMustChangePassword.Checked = false;
            chkEmpActive.Checked = true;
            
            btnEmpAdd.Enabled = true;
            btnEmpUpdate.Enabled = false;
            btnEmpToggleActive.Enabled = false;
            btnEmpSetPassword.Enabled = false;
            txtEmpUsername.Enabled = true;
            
            lblEmpStatus.Text = "Sẵn sàng thêm mới";
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count == 0) return;

            _selectedEmployee = dgvEmployees.SelectedRows[0].DataBoundItem as Employee;
            if (_selectedEmployee == null) return;

            txtEmpName.Text = _selectedEmployee.Name;
            txtEmpUsername.Text = _selectedEmployee.Username;
            txtEmpPhone.Text = _selectedEmployee.Phone;
            txtEmpEmail.Text = _selectedEmployee.Email;
            cboEmpRole.SelectedItem = _selectedEmployee.Role;
            chkMustChangePassword.Checked = _selectedEmployee.MustChangePassword;
            chkEmpActive.Checked = _selectedEmployee.IsActive;

            // Chọn chi nhánh
            for (int i = 0; i < cboEmpBranch.Items.Count; i++)
            {
                var branch = cboEmpBranch.Items[i] as Branch;
                if (branch != null && branch.BranchId == _selectedEmployee.BranchId)
                {
                    cboEmpBranch.SelectedIndex = i;
                    break;
                }
            }

            btnEmpAdd.Enabled = true;
            btnEmpUpdate.Enabled = true;
            btnEmpToggleActive.Enabled = true;
            btnEmpSetPassword.Enabled = EmployeeService.CurrentEmployee?.IsAdmin ?? false;
            txtEmpUsername.Enabled = false; // Không cho đổi username khi edit

            // Toggle button text
            btnEmpToggleActive.Text = _selectedEmployee.IsActive ? "🚫 Vô hiệu hóa" : "✅ Kích hoạt";
            btnEmpToggleActive.BackColor = _selectedEmployee.IsActive 
                ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69);

            lblEmpStatus.Text = $"Đang xem: {_selectedEmployee.Name}";
        }

        private void btnEmpAdd_Click(object sender, EventArgs e)
        {
            // Validate
            var validation = InputValidator.ValidateEmployee(
                txtEmpName.Text, txtEmpUsername.Text, "123456", 
                txtEmpEmail.Text, txtEmpPhone.Text);

            if (!validation.IsValid)
            {
                MessageBox.Show(validation.Message, "Dữ liệu không hợp lệ", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedBranch = cboEmpBranch.SelectedItem as Branch;
            var employee = new Employee
            {
                Name = InputValidator.Sanitize(txtEmpName.Text.Trim()),
                Username = txtEmpUsername.Text.Trim().ToLower(),
                Phone = InputValidator.Sanitize(txtEmpPhone.Text.Trim()),
                Email = InputValidator.Sanitize(txtEmpEmail.Text.Trim()),
                Role = cboEmpRole.SelectedItem?.ToString() ?? EmployeeRoles.Sales,
                IsActive = chkEmpActive.Checked,
                BranchId = selectedBranch?.BranchId ?? 1,
                MustChangePassword = chkMustChangePassword.Checked
            };

            // Mật khẩu mặc định
            string defaultPassword = "123456";
            var result = _employeeService.AddEmployee(employee, defaultPassword);

            MessageBox.Show(result.Success 
                ? $"{result.Message}\nMật khẩu mặc định: {defaultPassword}" 
                : result.Message, 
                result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                // Set MustChangePassword nếu được chọn
                if (chkMustChangePassword.Checked)
                {
                    _employeeService.SetMustChangePassword(result.EmployeeId, true);
                }
                LoadEmployees();
                ClearEmployeeForm();
            }
        }

        private void btnEmpUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!", "Thông báo");
                return;
            }

            // Validate
            var nameVal = InputValidator.ValidateName(txtEmpName.Text);
            if (!nameVal.IsValid) { MessageBox.Show(nameVal.Message, "Lỗi"); return; }

            _selectedEmployee.Name = InputValidator.Sanitize(txtEmpName.Text.Trim());
            _selectedEmployee.Phone = InputValidator.Sanitize(txtEmpPhone.Text.Trim());
            _selectedEmployee.Email = InputValidator.Sanitize(txtEmpEmail.Text.Trim());
            _selectedEmployee.Role = cboEmpRole.SelectedItem?.ToString() ?? EmployeeRoles.Sales;
            _selectedEmployee.IsActive = chkEmpActive.Checked;

            var selectedBranch = cboEmpBranch.SelectedItem as Branch;
            if (selectedBranch != null)
                _selectedEmployee.BranchId = selectedBranch.BranchId;

            var result = _employeeService.UpdateEmployee(_selectedEmployee);

            // Cập nhật MustChangePassword
            if (result.Success)
            {
                _employeeService.SetMustChangePassword(_selectedEmployee.EmployeeId, chkMustChangePassword.Checked);
            }

            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
                LoadEmployees();
        }

        private void btnEmpToggleActive_Click(object sender, EventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo");
                return;
            }

            string action = _selectedEmployee.IsActive ? "vô hiệu hóa" : "kích hoạt";
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn {action} tài khoản '{_selectedEmployee.Username}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _selectedEmployee.IsActive 
                    ? _employeeService.DeactivateEmployee(_selectedEmployee.EmployeeId)
                    : _employeeService.ActivateEmployee(_selectedEmployee.EmployeeId);

                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.Success)
                {
                    LoadEmployees();
                    ClearEmployeeForm();
                }
            }
        }

        private void btnEmpSetPassword_Click(object sender, EventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo");
                return;
            }

            if (!(EmployeeService.CurrentEmployee?.IsAdmin ?? false))
            {
                MessageBox.Show("Chỉ Admin mới có quyền đặt mật khẩu!", "Không có quyền");
                return;
            }

            // Dialog nhập mật khẩu mới
            string newPassword = ShowPasswordInputDialog();
            if (string.IsNullOrEmpty(newPassword)) return;

            var result = _employeeService.SetPassword(_selectedEmployee.EmployeeId, newPassword, chkMustChangePassword.Checked);

            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private string ShowPasswordInputDialog()
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Đặt mật khẩu mới",
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(45, 45, 68)
            };
            Label textLabel = new Label() { 
                Left = 20, Top = 20, Width = 300, Text = "Nhập mật khẩu mới (ít nhất 6 ký tự):",
                ForeColor = Color.White
            };
            TextBox textBox = new TextBox() { 
                Left = 20, Top = 50, Width = 290, Height = 30,
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(60, 60, 80),
                ForeColor = Color.White
            };
            Button confirmation = new Button() { 
                Text = "OK", Left = 130, Width = 80, Top = 90, DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };
            Button cancel = new Button() { 
                Text = "Hủy", Left = 220, Width = 80, Top = 90, DialogResult = DialogResult.Cancel,
                BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
            };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }

        private void btnEmpClear_Click(object sender, EventArgs e)
        {
            LoadEmployees();
            ClearEmployeeForm();
        }

        #endregion

        #region Tab Quản lý Chi nhánh

        private void LoadBranches()
        {
            try
            {
                _branches = _branchService.GetAllBranches();
                dgvBranches.DataSource = null;
                dgvBranches.DataSource = _branches;
                FormatBranchGrid();
                lblBranchStatus.Text = $"Tổng: {_branches.Count} chi nhánh";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi nhánh: {ex.Message}", "Lỗi");
            }
        }

        private void FormatBranchGrid()
        {
            if (dgvBranches.Columns.Count == 0) return;

            if (dgvBranches.Columns.Contains("BranchId"))
                dgvBranches.Columns["BranchId"].Visible = false;
            if (dgvBranches.Columns.Contains("BranchCode"))
                dgvBranches.Columns["BranchCode"].HeaderText = "Mã";
            if (dgvBranches.Columns.Contains("Name"))
                dgvBranches.Columns["Name"].HeaderText = "Tên chi nhánh";
            if (dgvBranches.Columns.Contains("Address"))
                dgvBranches.Columns["Address"].HeaderText = "Địa chỉ";
            if (dgvBranches.Columns.Contains("Phone"))
                dgvBranches.Columns["Phone"].HeaderText = "SĐT";
            if (dgvBranches.Columns.Contains("IsActive"))
                dgvBranches.Columns["IsActive"].HeaderText = "Hoạt động";
            if (dgvBranches.Columns.Contains("CreatedAt"))
                dgvBranches.Columns["CreatedAt"].Visible = false;

            foreach (DataGridViewRow row in dgvBranches.Rows)
            {
                var branch = row.DataBoundItem as Branch;
                if (branch != null && !branch.IsActive)
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                }
            }
        }

        private void ClearBranchForm()
        {
            _selectedBranch = null;
            txtBranchCode.Clear();
            txtBranchName.Clear();
            txtBranchAddress.Clear();
            txtBranchPhone.Clear();
            chkBranchActive.Checked = true;
            
            btnBranchAdd.Enabled = true;
            btnBranchUpdate.Enabled = false;
            btnBranchToggleActive.Enabled = false;
            txtBranchCode.Enabled = true;

            dgvBranchEmployees.DataSource = null;
            lblBranchEmployees.Text = "👥 Nhân viên chi nhánh:";
            lblBranchStatus.Text = "Sẵn sàng";
        }

        private void dgvBranches_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBranches.SelectedRows.Count == 0) return;

            _selectedBranch = dgvBranches.SelectedRows[0].DataBoundItem as Branch;
            if (_selectedBranch == null) return;

            txtBranchCode.Text = _selectedBranch.BranchCode;
            txtBranchName.Text = _selectedBranch.Name;
            txtBranchAddress.Text = _selectedBranch.Address;
            txtBranchPhone.Text = _selectedBranch.Phone;
            chkBranchActive.Checked = _selectedBranch.IsActive;

            btnBranchAdd.Enabled = true;
            btnBranchUpdate.Enabled = true;
            btnBranchToggleActive.Enabled = true;
            txtBranchCode.Enabled = false;

            btnBranchToggleActive.Text = _selectedBranch.IsActive ? "🚫" : "✅";
            btnBranchToggleActive.BackColor = _selectedBranch.IsActive 
                ? Color.FromArgb(220, 53, 69) : Color.FromArgb(40, 167, 69);

            // Load nhân viên của chi nhánh
            LoadBranchEmployees(_selectedBranch.BranchId);
            lblBranchStatus.Text = $"Đang xem: {_selectedBranch.Name}";
        }

        private void LoadBranchEmployees(int branchId)
        {
            try
            {
                var employees = _branchService.GetEmployeesByBranch(branchId);
                dgvBranchEmployees.DataSource = null;
                dgvBranchEmployees.DataSource = employees;
                FormatBranchEmployeesGrid();
                lblBranchEmployees.Text = $"👥 Nhân viên chi nhánh: {employees.Count} người";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải nhân viên: {ex.Message}", "Lỗi");
            }
        }

        private void FormatBranchEmployeesGrid()
        {
            if (dgvBranchEmployees.Columns.Count == 0) return;

            var hiddenCols = new[] { "EmployeeId", "PasswordHash", "IsAdmin", "IsManager", "IsSales", 
                "HasFullBranchAccess", "MustChangePassword", "BranchId", "BranchName" };
            foreach (var col in hiddenCols)
            {
                if (dgvBranchEmployees.Columns.Contains(col))
                    dgvBranchEmployees.Columns[col].Visible = false;
            }

            if (dgvBranchEmployees.Columns.Contains("Name"))
                dgvBranchEmployees.Columns["Name"].HeaderText = "Họ tên";
            if (dgvBranchEmployees.Columns.Contains("Username"))
                dgvBranchEmployees.Columns["Username"].HeaderText = "Username";
            if (dgvBranchEmployees.Columns.Contains("Role"))
                dgvBranchEmployees.Columns["Role"].HeaderText = "Vai trò";
            if (dgvBranchEmployees.Columns.Contains("Phone"))
                dgvBranchEmployees.Columns["Phone"].HeaderText = "SĐT";
            if (dgvBranchEmployees.Columns.Contains("IsActive"))
                dgvBranchEmployees.Columns["IsActive"].HeaderText = "Hoạt động";
        }

        private void btnBranchAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBranchName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên chi nhánh!", "Lỗi");
                return;
            }

            var branch = new Branch
            {
                BranchCode = txtBranchCode.Text.Trim(),
                Name = txtBranchName.Text.Trim(),
                Address = txtBranchAddress.Text.Trim(),
                Phone = txtBranchPhone.Text.Trim(),
                IsActive = chkBranchActive.Checked
            };

            var result = _branchService.AddBranch(branch);

            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                LoadBranches();
                LoadBranchesCombo();
                ClearBranchForm();
            }
        }

        private void btnBranchUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedBranch == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh!", "Thông báo");
                return;
            }

            _selectedBranch.Name = txtBranchName.Text.Trim();
            _selectedBranch.Address = txtBranchAddress.Text.Trim();
            _selectedBranch.Phone = txtBranchPhone.Text.Trim();
            _selectedBranch.IsActive = chkBranchActive.Checked;

            var result = _branchService.UpdateBranch(_selectedBranch);

            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                LoadBranches();
                LoadBranchesCombo();
            }
        }

        private void btnBranchToggleActive_Click(object sender, EventArgs e)
        {
            if (_selectedBranch == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh!", "Thông báo");
                return;
            }

            string action = _selectedBranch.IsActive ? "vô hiệu hóa" : "kích hoạt";
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn {action} chi nhánh '{_selectedBranch.Name}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _selectedBranch.IsActive 
                    ? _branchService.DeactivateBranch(_selectedBranch.BranchId)
                    : _branchService.ActivateBranch(_selectedBranch.BranchId);

                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.Success)
                {
                    LoadBranches();
                    LoadBranchesCombo();
                    ClearBranchForm();
                }
            }
        }

        private void btnBranchClear_Click(object sender, EventArgs e)
        {
            LoadBranches();
            ClearBranchForm();
        }

        private void btnTransferEmployee_Click(object sender, EventArgs e)
        {
            if (dgvBranchEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần chuyển chi nhánh!", "Thông báo");
                return;
            }

            var employee = dgvBranchEmployees.SelectedRows[0].DataBoundItem as Employee;
            if (employee == null) return;

            // Dialog chọn chi nhánh mới
            var branches = _branchService.GetActiveBranches();
            if (branches.Count <= 1)
            {
                MessageBox.Show("Không có chi nhánh khác để chuyển!", "Thông báo");
                return;
            }

            using (Form dialog = new Form())
            {
                dialog.Text = "Chuyển chi nhánh";
                dialog.Size = new Size(350, 180);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.BackColor = Color.FromArgb(45, 45, 68);

                Label lbl = new Label { 
                    Text = $"Chuyển '{employee.Name}' đến chi nhánh:", 
                    Location = new Point(20, 20), Size = new Size(300, 25),
                    ForeColor = Color.White
                };

                ComboBox cbo = new ComboBox { 
                    Location = new Point(20, 50), Size = new Size(290, 30),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    BackColor = Color.FromArgb(60, 60, 80),
                    ForeColor = Color.White
                };
                foreach (var b in branches)
                {
                    if (b.BranchId != _selectedBranch?.BranchId)
                        cbo.Items.Add(b);
                }
                cbo.DisplayMember = "Name";
                if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;

                Button btnOk = new Button { 
                    Text = "Chuyển", Location = new Point(130, 100), Size = new Size(80, 35),
                    DialogResult = DialogResult.OK,
                    BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
                };
                Button btnCancel = new Button { 
                    Text = "Hủy", Location = new Point(220, 100), Size = new Size(80, 35),
                    DialogResult = DialogResult.Cancel,
                    BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat
                };

                dialog.Controls.AddRange(new Control[] { lbl, cbo, btnOk, btnCancel });
                dialog.AcceptButton = btnOk;
                dialog.CancelButton = btnCancel;

                if (dialog.ShowDialog() == DialogResult.OK && cbo.SelectedItem != null)
                {
                    var newBranch = cbo.SelectedItem as Branch;
                    if (newBranch != null)
                    {
                        var result = _employeeService.UpdateEmployeeBranch(employee.EmployeeId, newBranch.BranchId);
                        MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                            MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (result.Success && _selectedBranch != null)
                        {
                            LoadBranchEmployees(_selectedBranch.BranchId);
                            LoadEmployees();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
