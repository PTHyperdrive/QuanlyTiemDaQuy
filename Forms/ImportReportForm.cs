using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class ImportReportForm : Form
    {
        private readonly ImportService _importService;

        // UI Controls
        private Panel pnlTop;
        private Label lblTitle;
        private Panel pnlFilter;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private Button btnFilter;
        private Button btnViewDetails;
        private Button btnRefresh;
        private FlowLayoutPanel pnlSummary;
        private Panel pnlStat1, pnlStat2, pnlStat3;
        private Label lblTotalImports, lblTotalProducts, lblTotalCost;
        private DataGridView dgvImports;

        public ImportReportForm()
        {
            InitializeComponent();
            _importService = new ImportService();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form settings
            this.BackColor = Color.FromArgb(32, 32, 48);
            this.ClientSize = new Size(980, 600);
            this.Font = new Font("Segoe UI", 10F);
            this.Name = "ImportReportForm";
            this.Text = "Báo cáo Nhập kho";
            this.Load += ImportReportForm_Load;

            // Top panel with title
            pnlTop = new Panel
            {
                BackColor = Color.FromArgb(0, 150, 136),
                Dock = DockStyle.Top,
                Height = 50
            };
            lblTitle = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                Text = "📦 BÁO CÁO NHẬP KHO",
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlTop.Controls.Add(lblTitle);

            // Filter panel
            pnlFilter = new Panel
            {
                BackColor = Color.FromArgb(45, 45, 68),
                Dock = DockStyle.Top,
                Height = 55,
                Padding = new Padding(10)
            };

            var lblFrom = new Label { Text = "Từ ngày", ForeColor = Color.White, AutoSize = true, Location = new Point(15, 18) };
            dtpFromDate = new DateTimePicker { Format = DateTimePickerFormat.Short, Location = new Point(75, 15), Width = 100 };
            var lblTo = new Label { Text = "đến", ForeColor = Color.White, AutoSize = true, Location = new Point(185, 18) };
            dtpToDate = new DateTimePicker { Format = DateTimePickerFormat.Short, Location = new Point(220, 15), Width = 100 };

            btnFilter = new Button
            {
                Text = "🔍 Lọc",
                BackColor = Color.FromArgb(40, 167, 69),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Location = new Point(340, 12),
                Size = new Size(100, 32)
            };
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.Click += btnFilter_Click;

            btnViewDetails = new Button
            {
                Text = "👁 Chi tiết",
                BackColor = Color.FromArgb(0, 123, 255),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Location = new Point(450, 12),
                Size = new Size(100, 32)
            };
            btnViewDetails.FlatAppearance.BorderSize = 0;
            btnViewDetails.Click += btnViewDetails_Click;

            btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                BackColor = Color.FromArgb(23, 162, 184),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Location = new Point(560, 12),
                Size = new Size(100, 32)
            };
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.Click += btnRefresh_Click;

            pnlFilter.Controls.AddRange(new Control[] { lblFrom, dtpFromDate, lblTo, dtpToDate, btnFilter, btnViewDetails, btnRefresh });

            // Summary panel
            pnlSummary = new FlowLayoutPanel
            {
                BackColor = Color.FromArgb(32, 32, 48),
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(5)
            };

            // Stat 1 - Total Imports
            pnlStat1 = CreateStatPanel(Color.FromArgb(0, 123, 255), "Tổng phiếu nhập", out lblTotalImports);
            // Stat 2 - Total Products
            pnlStat2 = CreateStatPanel(Color.FromArgb(40, 167, 69), "Tổng sản phẩm", out lblTotalProducts);
            // Stat 3 - Total Cost
            pnlStat3 = CreateStatPanel(Color.FromArgb(111, 66, 193), "💰 Tổng chi phí", out lblTotalCost);

            pnlSummary.Controls.AddRange(new Control[] { pnlStat1, pnlStat2, pnlStat3 });

            // Data grid
            dgvImports = new DataGridView
            {
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.FromArgb(32, 32, 48),
                BorderStyle = BorderStyle.None,
                Dock = DockStyle.Fill,
                GridColor = Color.FromArgb(50, 50, 70),
                MultiSelect = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvImports.EnableHeadersVisualStyles = false;
            dgvImports.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 70);
            dgvImports.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvImports.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvImports.ColumnHeadersHeight = 40;
            dgvImports.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgvImports.DefaultCellStyle.ForeColor = Color.White;
            dgvImports.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 150, 136);
            dgvImports.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvImports.RowTemplate.Height = 35;
            dgvImports.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 75);

            // Add controls in correct order
            this.Controls.Add(dgvImports);
            this.Controls.Add(pnlSummary);
            this.Controls.Add(pnlFilter);
            this.Controls.Add(pnlTop);

            this.ResumeLayout(false);
        }

        private Panel CreateStatPanel(Color bgColor, string title, out Label valueLabel)
        {
            var panel = new Panel
            {
                BackColor = bgColor,
                Size = new Size(200, 60),
                Margin = new Padding(5)
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.White,
                Location = new Point(5, 5),
                Size = new Size(190, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            valueLabel = new Label
            {
                Text = "0",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(5, 25),
                Size = new Size(190, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            panel.Controls.Add(titleLabel);
            panel.Controls.Add(valueLabel);
            return panel;
        }

        private void ImportReportForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            dtpToDate.Value = DateTime.Today;
            LoadImportReport();
        }

        private void LoadImportReport()
        {
            try
            {
                var fromDate = dtpFromDate.Value.Date;
                var toDate = dtpToDate.Value.Date;

                var imports = _importService.GetImportsByDateRange(fromDate, toDate);

                dgvImports.DataSource = null;
                dgvImports.DataSource = imports;
                FormatImportGrid();
                CalculateSummary(imports);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải báo cáo: {ex.Message}", "Lỗi");
            }
        }

        private void FormatImportGrid()
        {
            if (dgvImports.Columns.Count == 0) return;

            // Hide unnecessary columns
            string[] hideColumns = { "Details", "SupplierId", "EmployeeId" };
            foreach (var col in hideColumns)
            {
                if (dgvImports.Columns.Contains(col))
                    dgvImports.Columns[col].Visible = false;
            }

            // Set headers
            if (dgvImports.Columns.Contains("ImportId"))
                dgvImports.Columns["ImportId"].Visible = false;
            if (dgvImports.Columns.Contains("ImportCode"))
                dgvImports.Columns["ImportCode"].HeaderText = "Mã phiếu";
            if (dgvImports.Columns.Contains("SupplierName"))
                dgvImports.Columns["SupplierName"].HeaderText = "Nhà cung cấp";
            if (dgvImports.Columns.Contains("EmployeeName"))
                dgvImports.Columns["EmployeeName"].HeaderText = "Nhân viên";
            if (dgvImports.Columns.Contains("ImportDate"))
                dgvImports.Columns["ImportDate"].HeaderText = "Ngày nhập";
            if (dgvImports.Columns.Contains("TotalCost"))
                dgvImports.Columns["TotalCost"].HeaderText = "Tổng tiền";
            if (dgvImports.Columns.Contains("Note"))
                dgvImports.Columns["Note"].HeaderText = "Ghi chú";
            if (dgvImports.Columns.Contains("CreatedAt"))
                dgvImports.Columns["CreatedAt"].HeaderText = "Ngày tạo";
        }

        private void CalculateSummary(List<ImportReceipt> imports)
        {
            int totalImports = imports.Count;
            int totalProducts = 0;
            decimal totalCost = 0;

            foreach (var imp in imports)
            {
                totalProducts += imp.Details?.Count ?? 0;
                totalCost += imp.TotalCost;
            }

            lblTotalImports.Text = totalImports.ToString("N0");
            lblTotalProducts.Text = totalProducts.ToString("N0");
            lblTotalCost.Text = totalCost.ToString("N0") + " VNĐ";
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadImportReport();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadImportReport();
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dgvImports.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập để xem chi tiết!", "Thông báo");
                return;
            }

            var import = dgvImports.SelectedRows[0].DataBoundItem as ImportReceipt;
            if (import == null) return;

            var fullImport = _importService.GetImportById(import.ImportId);
            if (fullImport == null)
            {
                MessageBox.Show("Không tìm thấy phiếu nhập!", "Lỗi");
                return;
            }

            string details = $"MÃ PHIẾU NHẬP: {fullImport.ImportCode}\n";
            details += $"Ngày: {fullImport.ImportDate:dd/MM/yyyy HH:mm}\n";
            details += $"Nhà cung cấp: {fullImport.SupplierName}\n";
            details += $"Nhân viên: {fullImport.EmployeeName}\n";
            details += $"Ghi chú: {fullImport.Note}\n\n";
            details += "CHI TIẾT:\n";
            details += "─────────────────────────────\n";

            if (fullImport.Details != null)
            {
                foreach (var item in fullImport.Details)
                {
                    details += $"{item.ProductCode} - {item.ProductName}\n";
                    details += $"  SL: {item.Qty} x {item.UnitCost:N0} = {item.LineTotal:N0} VNĐ\n";
                }
            }

            details += "─────────────────────────────\n";
            details += $"TỔNG CHI PHÍ: {fullImport.TotalCost:N0} VNĐ\n";

            MessageBox.Show(details, "Chi tiết phiếu nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
