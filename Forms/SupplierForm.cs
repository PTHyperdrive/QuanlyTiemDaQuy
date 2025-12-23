using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.BLL;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class SupplierForm : Form
    {
        private readonly SupplierService _supplierService;
        private Supplier _selectedSupplier;
        private List<Supplier> _suppliers;

        public SupplierForm()
        {
            InitializeComponent();
            _supplierService = new SupplierService();
        }

        private void SupplierForm_Load(object sender, EventArgs e)
        {
            LoadSuppliers();
            LoadCertificates();
            ClearForm();
        }

        #region Load Data

        private void LoadSuppliers()
        {
            try
            {
                _suppliers = _supplierService.GetAllSuppliers();
                dgvSuppliers.DataSource = null;
                dgvSuppliers.DataSource = _suppliers;
                FormatSupplierGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải nhà cung cấp: {ex.Message}", "Lỗi");
            }
        }

        private void LoadCertificates()
        {
            try
            {
                var certs = _supplierService.GetAllCertificates();
                dgvCertificates.DataSource = null;
                dgvCertificates.DataSource = certs;
                FormatCertificateGrid();

                lblCertCount.Text = $"Tổng: {certs.Count} chứng chỉ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chứng chỉ: {ex.Message}", "Lỗi");
            }
        }

        private void FormatSupplierGrid()
        {
            if (dgvSuppliers.Columns.Count == 0) return;

            if (dgvSuppliers.Columns.Contains("SupplierId"))
                dgvSuppliers.Columns["SupplierId"].Visible = false;
            if (dgvSuppliers.Columns.Contains("Name"))
                dgvSuppliers.Columns["Name"].HeaderText = "Tên NCC";
            if (dgvSuppliers.Columns.Contains("ContactPerson"))
                dgvSuppliers.Columns["ContactPerson"].HeaderText = "Người liên hệ";
            if (dgvSuppliers.Columns.Contains("Phone"))
                dgvSuppliers.Columns["Phone"].HeaderText = "SĐT";
            if (dgvSuppliers.Columns.Contains("Email"))
                dgvSuppliers.Columns["Email"].HeaderText = "Email";
            if (dgvSuppliers.Columns.Contains("Address"))
                dgvSuppliers.Columns["Address"].HeaderText = "Địa chỉ";
            if (dgvSuppliers.Columns.Contains("TaxCode"))
                dgvSuppliers.Columns["TaxCode"].Visible = false;
            if (dgvSuppliers.Columns.Contains("BankAccount"))
                dgvSuppliers.Columns["BankAccount"].Visible = false;
            if (dgvSuppliers.Columns.Contains("Note"))
                dgvSuppliers.Columns["Note"].Visible = false;
            if (dgvSuppliers.Columns.Contains("IsActive"))
                dgvSuppliers.Columns["IsActive"].Visible = false;
            if (dgvSuppliers.Columns.Contains("CreatedAt"))
                dgvSuppliers.Columns["CreatedAt"].Visible = false;
        }

        private void FormatCertificateGrid()
        {
            if (dgvCertificates.Columns.Count == 0) return;

            if (dgvCertificates.Columns.Contains("CertId"))
                dgvCertificates.Columns["CertId"].Visible = false;
            if (dgvCertificates.Columns.Contains("CertCode"))
                dgvCertificates.Columns["CertCode"].HeaderText = "Mã chứng chỉ";
            if (dgvCertificates.Columns.Contains("Issuer"))
                dgvCertificates.Columns["Issuer"].HeaderText = "Đơn vị cấp";
            if (dgvCertificates.Columns.Contains("IssueDate"))
                dgvCertificates.Columns["IssueDate"].HeaderText = "Ngày cấp";
            if (dgvCertificates.Columns.Contains("CreatedAt"))
                dgvCertificates.Columns["CreatedAt"].Visible = false;
        }

        #endregion

        #region Supplier CRUD

        private void dgvSuppliers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSuppliers.SelectedRows.Count == 0) return;

            _selectedSupplier = dgvSuppliers.SelectedRows[0].DataBoundItem as Supplier;
            if (_selectedSupplier == null) return;

            txtName.Text = _selectedSupplier.Name;
            txtContact.Text = _selectedSupplier.ContactPerson;
            txtPhone.Text = _selectedSupplier.Phone;
            txtEmail.Text = _selectedSupplier.Email;
            txtAddress.Text = _selectedSupplier.Address;

            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            grpDetails.Text = $"Nhà cung cấp - {_selectedSupplier.Name}";
        }

        private void ClearForm()
        {
            _selectedSupplier = null;
            txtName.Clear();
            txtContact.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            grpDetails.Text = "Nhà cung cấp - Thêm mới";
            txtName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateSupplierInput()) return;

            var supplier = new Supplier
            {
                Name = txtName.Text.Trim(),
                ContactPerson = txtContact.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };

            var result = _supplierService.AddSupplier(supplier);
            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                LoadSuppliers();
                ClearForm();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedSupplier == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo");
                return;
            }

            if (!ValidateSupplierInput()) return;

            _selectedSupplier.Name = txtName.Text.Trim();
            _selectedSupplier.ContactPerson = txtContact.Text.Trim();
            _selectedSupplier.Phone = txtPhone.Text.Trim();
            _selectedSupplier.Email = txtEmail.Text.Trim();
            _selectedSupplier.Address = txtAddress.Text.Trim();

            var result = _supplierService.UpdateSupplier(_selectedSupplier);
            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                LoadSuppliers();
                ClearForm();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedSupplier == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Thông báo");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa nhà cung cấp '{_selectedSupplier.Name}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _supplierService.DeleteSupplier(_selectedSupplier.SupplierId);
                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.Success)
                {
                    LoadSuppliers();
                    ClearForm();
                }
            }
        }

        private bool ValidateSupplierInput()
        {
            var nameResult = InputValidator.ValidateName(txtName.Text);
            if (!nameResult.IsValid)
            {
                MessageBox.Show(nameResult.Message, "Lỗi");
                txtName.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                var emailResult = InputValidator.ValidateEmail(txtEmail.Text);
                if (!emailResult.IsValid)
                {
                    MessageBox.Show(emailResult.Message, "Lỗi");
                    txtEmail.Focus();
                    return false;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                var phoneResult = InputValidator.ValidatePhone(txtPhone.Text);
                if (!phoneResult.IsValid)
                {
                    MessageBox.Show(phoneResult.Message, "Lỗi");
                    txtPhone.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadSuppliers();
            }
            else
            {
                _suppliers = _supplierService.SearchSuppliers(keyword);
                dgvSuppliers.DataSource = null;
                dgvSuppliers.DataSource = _suppliers;
                FormatSupplierGrid();
            }
        }

        #endregion

        #region Certificate Validation

        private void btnCheckCert_Click(object sender, EventArgs e)
        {
            string certCode = txtCertCheck.Text.Trim();
            if (string.IsNullOrEmpty(certCode))
            {
                MessageBox.Show("Vui lòng nhập mã chứng chỉ để kiểm tra!", "Thông báo");
                txtCertCheck.Focus();
                return;
            }

            var result = _supplierService.ValidateCertificate(certCode);
            if (result.IsValid)
            {
                lblCertStatus.ForeColor = Color.LightGreen;
                lblCertStatus.Text = $"✅ HỢP LỆ - {result.Message}";
            }
            else
            {
                lblCertStatus.ForeColor = Color.Red;
                lblCertStatus.Text = $"❌ KHÔNG HỢP LỆ - {result.Message}";
            }
        }

        private void btnAddCert_Click(object sender, EventArgs e)
        {
            var cert = new Certificate
            {
                CertCode = txtNewCertCode.Text.Trim(),
                Issuer = txtNewCertIssuer.Text.Trim(),
                IssueDate = dtpCertDate.Value
            };

            var result = _supplierService.AddCertificate(cert);
            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                txtNewCertCode.Clear();
                txtNewCertIssuer.Clear();
                dtpCertDate.Value = DateTime.Today;
                LoadCertificates();
            }
        }

        private void btnDeleteCert_Click(object sender, EventArgs e)
        {
            if (dgvCertificates.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn chứng chỉ để xóa!", "Thông báo");
                return;
            }

            var cert = dgvCertificates.SelectedRows[0].DataBoundItem as Certificate;
            if (cert == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa chứng chỉ '{cert.CertCode}'?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                var result = _supplierService.DeleteCertificate(cert.CertId);
                MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                if (result.Success)
                {
                    LoadCertificates();
                }
            }
        }

        #endregion
    }
}
