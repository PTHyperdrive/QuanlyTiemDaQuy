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

        public MainForm()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _reportService = new ReportService();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Thiết lập thông tin người dùng
            var employee = EmployeeService.CurrentEmployee;
            if (employee != null)
            {
                lblUserName.Text = employee.Name;
                lblUserRole.Text = employee.Role;
            }

            // Áp dụng phân quyền hiển thị menu theo vai trò
            ApplyRolePermissions();

            // Tải thống kê dashboard
            LoadDashboardStats();

            // Thiết lập ngày hiện tại
            lblCurrentDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
        }

        private void ApplyRolePermissions()
        {
            var employee = EmployeeService.CurrentEmployee;
            if (employee == null) return;

            // Chỉ Admin mới được truy cập Quản lý Tài khoản
            btnUsers.Visible = employee.IsAdmin;

            // Chỉ Manager trở lên mới được truy cập Báo cáo
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
                // Có thể chưa kết nối được database
                lblTodayRevenue.Text = "Lỗi kết nối DB";
            }
        }

        #region Định dạng nút bấm

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

        #region Điều hướng Form

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
            MessageBox.Show("Module Quản lý Tài khoản sẽ được phát triển!", "Thông báo");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            MessageBox.Show("Module Báo cáo sẽ được phát triển!", "Thông báo");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            MessageBox.Show("Module Nhập hàng sẽ được phát triển!", "Thông báo");
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            MessageBox.Show("Module Nhà cung cấp sẽ được phát triển!", "Thông báo");
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
                this.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đăng xuất khi đóng form
            _employeeService.Logout();
        }
    }
}
