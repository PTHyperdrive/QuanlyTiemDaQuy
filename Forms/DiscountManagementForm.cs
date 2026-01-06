using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class DiscountManagementForm : Form
    {
        private readonly DiscountService _discountService;
        private int _selectedId = -1;

        public DiscountManagementForm()
        {
            InitializeComponent();
            _discountService = new DiscountService();
        }

        private void DiscountManagementForm_Load(object sender, EventArgs e)
        {
            LoadRules();
            ClearInput();
        }

        private void LoadRules()
        {
            var rules = _discountService.GetAllRules();
            dgvRules.DataSource = rules;
            
            // Hide ID column usually
            if (dgvRules.Columns["Id"] != null) dgvRules.Columns["Id"].Visible = false;
        }

        private void ClearInput()
        {
            _selectedId = -1;
            txtName.Text = "";
            numDiscount.Value = 0;
            numPriority.Value = 0;
            cboTier.SelectedIndex = 0; // "All"
            dtpStartDate.Checked = false;
            dtpEndDate.Checked = false;
            chkIsActive.Checked = true;
            
            btnAdd.Enabled = true;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private DiscountRule GetModelFromInput()
        {
            return new DiscountRule
            {
                Id = _selectedId,
                Name = txtName.Text.Trim(),
                DiscountPercent = numDiscount.Value,
                ApplicableTier = cboTier.SelectedItem?.ToString() ?? "All",
                StartDate = dtpStartDate.Checked ? dtpStartDate.Value.Date : (DateTime?)null,
                EndDate = dtpEndDate.Checked ? dtpEndDate.Value.Date : (DateTime?)null,
                Priority = (int)numPriority.Value,
                IsActive = chkIsActive.Checked
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var rule = GetModelFromInput();
            if (_discountService.AddRule(rule, out string message))
            {
                MessageBox.Show(message, "Thông báo");
                LoadRules();
                ClearInput();
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedId == -1) return;
            
            var rule = GetModelFromInput();
            if (_discountService.UpdateRule(rule, out string message))
            {
                MessageBox.Show(message, "Thông báo");
                LoadRules();
                ClearInput();
            }
            else
            {
                MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId == -1) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa chương trình này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (_discountService.DeleteRule(_selectedId, out string message))
                {
                    MessageBox.Show(message, "Thông báo");
                    LoadRules();
                    ClearInput();
                }
                else
                {
                    MessageBox.Show(message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        private void dgvRules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvRules.Rows[e.RowIndex];
                if (row.DataBoundItem is DiscountRule rule)
                {
                    _selectedId = rule.Id;
                    txtName.Text = rule.Name;
                    numDiscount.Value = rule.DiscountPercent;
                    numPriority.Value = rule.Priority;
                    
                    // Select Tier
                    if (cboTier.Items.Contains(rule.ApplicableTier)) 
                        cboTier.SelectedItem = rule.ApplicableTier;
                    else
                        cboTier.Text = rule.ApplicableTier;

                    // Dates
                    dtpStartDate.Checked = rule.StartDate.HasValue;
                    if (rule.StartDate.HasValue) dtpStartDate.Value = rule.StartDate.Value;
                    
                    dtpEndDate.Checked = rule.EndDate.HasValue;
                    if (rule.EndDate.HasValue) dtpEndDate.Value = rule.EndDate.Value;
                    
                    chkIsActive.Checked = rule.IsActive;

                    btnAdd.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
            }
        }
    }
}
