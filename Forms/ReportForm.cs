using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class ReportForm : Form
    {
        private readonly SalesService _salesService;
        private readonly ReportService _reportService;

        public ReportForm()
        {
            InitializeComponent();
            _salesService = new SalesService();
            _reportService = new ReportService();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            // Set default dates
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;

            // Load status filter
            LoadStatusFilter();
            
            // Load initial data
            LoadInvoiceReport();
        }

        private void LoadStatusFilter()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("Tất cả");
            cboStatus.Items.Add(InvoiceStatus.Completed);
            cboStatus.Items.Add(InvoiceStatus.Pending);
            cboStatus.Items.Add(InvoiceStatus.Cancelled);
            cboStatus.SelectedIndex = 0;
        }

        private void LoadInvoiceReport()
        {
            try
            {
                var fromDate = dtpFromDate.Value.Date;
                var toDate = dtpToDate.Value.Date;
                string statusFilter = cboStatus.SelectedIndex == 0 ? null : cboStatus.SelectedItem.ToString();

                var invoices = _salesService.GetInvoicesByDateRange(fromDate, toDate);
                
                // Filter by status if selected
                if (!string.IsNullOrEmpty(statusFilter))
                {
                    invoices = invoices.FindAll(i => i.Status == statusFilter);
                }

                // Display invoices
                dgvInvoices.DataSource = null;
                dgvInvoices.DataSource = invoices;
                FormatInvoiceGrid();

                // Calculate summary
                CalculateSummary(invoices);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Lỗi");
            }
        }

        private void FormatInvoiceGrid()
        {
            if (dgvInvoices.Columns.Count == 0) return;

            // Hide unnecessary columns
            string[] hideColumns = { "CustomerId", "EmployeeId", "Details", "IsPending", "IsCompleted", "IsCancelled", "CanCancel", "CancelledAt", "CancelReason", "DiscountPercent", "VAT" };
            foreach (var col in hideColumns)
            {
                if (dgvInvoices.Columns.Contains(col))
                    dgvInvoices.Columns[col].Visible = false;
            }

            // Set headers
            if (dgvInvoices.Columns.Contains("InvoiceId"))
                dgvInvoices.Columns["InvoiceId"].Visible = false;
            if (dgvInvoices.Columns.Contains("InvoiceCode"))
                dgvInvoices.Columns["InvoiceCode"].HeaderText = "Mã HĐ";
            if (dgvInvoices.Columns.Contains("CustomerName"))
                dgvInvoices.Columns["CustomerName"].HeaderText = "Khách hàng";
            if (dgvInvoices.Columns.Contains("EmployeeName"))
                dgvInvoices.Columns["EmployeeName"].HeaderText = "Nhân viên";
            if (dgvInvoices.Columns.Contains("InvoiceDate"))
                dgvInvoices.Columns["InvoiceDate"].HeaderText = "Ngày";
            if (dgvInvoices.Columns.Contains("Subtotal"))
                dgvInvoices.Columns["Subtotal"].HeaderText = "Tạm tính";
            if (dgvInvoices.Columns.Contains("DiscountAmount"))
                dgvInvoices.Columns["DiscountAmount"].HeaderText = "Giảm giá";
            if (dgvInvoices.Columns.Contains("VATAmount"))
                dgvInvoices.Columns["VATAmount"].HeaderText = "VAT";
            if (dgvInvoices.Columns.Contains("Total"))
                dgvInvoices.Columns["Total"].HeaderText = "Tổng tiền";
            if (dgvInvoices.Columns.Contains("Status"))
                dgvInvoices.Columns["Status"].HeaderText = "Trạng thái";
            if (dgvInvoices.Columns.Contains("PaymentMethod"))
                dgvInvoices.Columns["PaymentMethod"].HeaderText = "Thanh toán";

            // Color by status
            foreach (DataGridViewRow row in dgvInvoices.Rows)
            {
                var invoice = row.DataBoundItem as Invoice;
                if (invoice != null)
                {
                    if (invoice.IsCancelled)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Red;
                        row.DefaultCellStyle.Font = new Font(dgvInvoices.Font, FontStyle.Strikeout);
                    }
                    else if (invoice.IsPending)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Orange;
                    }
                    else if (invoice.IsCompleted)
                    {
                        row.DefaultCellStyle.ForeColor = Color.LightGreen;
                    }
                }
            }
        }

        private void CalculateSummary(List<Invoice> invoices)
        {
            int totalInvoices = invoices.Count;
            int completedCount = 0;
            int pendingCount = 0;
            int cancelledCount = 0;
            decimal totalRevenue = 0;
            decimal totalCancelled = 0;

            foreach (var inv in invoices)
            {
                if (inv.IsCompleted)
                {
                    completedCount++;
                    totalRevenue += inv.Total;
                }
                else if (inv.IsPending)
                {
                    pendingCount++;
                }
                else if (inv.IsCancelled)
                {
                    cancelledCount++;
                    totalCancelled += inv.Total;
                }
            }

            lblTotalInvoices.Text = totalInvoices.ToString("N0");
            lblCompletedCount.Text = completedCount.ToString("N0");
            lblPendingCount.Text = pendingCount.ToString("N0");
            lblCancelledCount.Text = cancelledCount.ToString("N0");
            lblTotalRevenue.Text = totalRevenue.ToString("N0") + " VNĐ";
            lblCancelledAmount.Text = totalCancelled.ToString("N0") + " VNĐ";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadInvoiceReport();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để xem chi tiết!", "Thông báo");
                return;
            }

            var invoice = dgvInvoices.SelectedRows[0].DataBoundItem as Invoice;
            if (invoice == null) return;

            // Load full invoice with details
            var fullInvoice = _salesService.GetInvoiceById(invoice.InvoiceId);
            if (fullInvoice == null)
            {
                MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi");
                return;
            }

            // Show details in message box (can be improved to a separate form)
            string details = $"MÃ HÓA ĐƠN: {fullInvoice.InvoiceCode}\n";
            details += $"Ngày: {fullInvoice.InvoiceDate:dd/MM/yyyy HH:mm}\n";
            details += $"Khách hàng: {fullInvoice.CustomerName}\n";
            details += $"Nhân viên: {fullInvoice.EmployeeName}\n";
            details += $"Trạng thái: {fullInvoice.Status}\n\n";
            details += "CHI TIẾT:\n";
            details += "─────────────────────────────\n";
            
            foreach (var item in fullInvoice.Details)
            {
                details += $"{item.ProductCode} - {item.ProductName}\n";
                details += $"  SL: {item.Qty} x {item.UnitPrice:N0} = {item.LineTotal:N0} VNĐ\n";
            }
            
            details += "─────────────────────────────\n";
            details += $"Tạm tính: {fullInvoice.Subtotal:N0} VNĐ\n";
            details += $"Giảm giá: {fullInvoice.DiscountAmount:N0} VNĐ\n";
            details += $"VAT ({fullInvoice.VAT}%): {fullInvoice.VATAmount:N0} VNĐ\n";
            details += $"TỔNG: {fullInvoice.Total:N0} VNĐ\n";

            if (fullInvoice.IsCancelled)
            {
                details += $"\n⚠️ ĐÃ HỦY: {fullInvoice.CancelReason}";
            }

            MessageBox.Show(details, "Chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để hủy!", "Thông báo");
                return;
            }

            var invoice = dgvInvoices.SelectedRows[0].DataBoundItem as Invoice;
            if (invoice == null) return;

            if (invoice.IsCancelled)
            {
                MessageBox.Show("Hóa đơn này đã bị hủy trước đó!", "Thông báo");
                return;
            }

            string reason = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập lý do hủy hóa đơn:", "Hủy hóa đơn", "");

            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Vui lòng nhập lý do hủy!", "Thông báo");
                return;
            }

            var result = _salesService.CancelInvoice(invoice.InvoiceId, reason);
            MessageBox.Show(result.Message, result.Success ? "Thành công" : "Lỗi",
                MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);

            if (result.Success)
            {
                LoadInvoiceReport();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInvoiceReport();
        }
    }
}
