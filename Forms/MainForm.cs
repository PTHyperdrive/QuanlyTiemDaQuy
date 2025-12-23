using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTiemDaQuy.BLL.Services;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.Forms
{
    public partial class MainForm : Form
    {
        private readonly EmployeeService _employeeService;
        private readonly ReportService _reportService;
        private Button currentButton;
        private Form activeForm;

        // Flag to indicate return to login screen vs exit app
        public bool ReturnToLogin { get; private set; } = false;

        public MainForm()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _reportService = new ReportService();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Set user info
            var employee = EmployeeService.CurrentEmployee;
            if (employee != null)
            {
                lblUserName.Text = employee.Name;
                lblUserRole.Text = employee.Role;
            }

            // Apply role-based menu visibility
            ApplyRolePermissions();

            // Load dashboard stats
            LoadDashboardStats();

            // Set current date
            lblCurrentDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
        }

        private void ApplyRolePermissions()
        {
            var employee = EmployeeService.CurrentEmployee;
            if (employee == null) return;

            // Admin and Manager can access User Management
            btnUsers.Visible = employee.IsAdmin || employee.IsManager;

            // Only Manager+ can access Reports
            btnReport.Visible = employee.IsManager;
        }

        private void LoadDashboardStats()
        {
            try
            {
                var stats = _reportService.GetDashboardStats();
                
                lblTotalProducts.Text = stats.TotalProducts.ToString("N0");
                lblLowStock.Text = stats.LowStockProducts.ToString("N0");
                lblTotalCustomers.Text = stats.TotalCustomers.ToString("N0");
                lblTodayRevenue.Text = stats.TodayRevenue.ToString("N0") + " VNĐ";
                lblTodayInvoices.Text = stats.TodayInvoices.ToString();
                lblMonthRevenue.Text = stats.MonthRevenue.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                // Database might not be connected yet
                lblTodayRevenue.Text = "Lỗi kết nối DB";
            }
        }

        #region Button Styling

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.FromArgb(0, 150, 136);
                    currentButton.ForeColor = Color.White;
                }
            }
        }

        private void DisableButton()
        {
            if (currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(45, 45, 68);
                currentButton.ForeColor = Color.Gainsboro;
            }
        }

        #endregion

        #region Form Navigation

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            
            lblTitle.Text = childForm.Text;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm = null;
            }
            lblTitle.Text = "Dashboard";
            pnlDashboard.Visible = true;
            LoadDashboardStats();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new ProductForm());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new CustomerForm());
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new SalesForm());
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new UserManagementForm());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new ReportForm());
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new ImportForm());
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new SupplierForm());
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            MessageBox.Show("Module Tồn kho sẽ được phát triển!", "Thông báo");
        }

        #endregion

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _employeeService.Logout();
                ReturnToLogin = true; // Signal to return to login screen
                this.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Logout when closing
            _employeeService.Logout();
            // If not explicitly logging out, don't return to login (user closed window)
            // ReturnToLogin remains false unless set by logout button
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblLowStock_Click(object sender, EventArgs e)
        {

        }

        private void pnlStats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
