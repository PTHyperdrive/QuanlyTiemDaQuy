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

        // Cờ để xác định quay về màn hình đăng nhập hay thoát ứng dụng
        public bool ReturnToLogin { get; private set; } = false;

        public MainForm()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            _reportService = new ReportService();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin người dùng
            var employee = EmployeeService.CurrentEmployee;
            if (employee != null)
            {
                lblUserName.Text = employee.Name;
                lblUserRole.Text = employee.Role;
            }

            // Áp dụng phân quyền menu theo vai trò
            ApplyRolePermissions();

            // Tải thống kê dashboard
            LoadDashboardStats();

            // Hiển thị ngày hiện tại
            lblCurrentDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy");
        }

        private void ApplyRolePermissions()
        {
            var employee = EmployeeService.CurrentEmployee;
            if (employee == null) return;

            // Admin và Manager có quyền truy cập Quản lý người dùng
            btnUsers.Visible = employee.IsAdmin || employee.IsManager;

            // Chỉ Manager trở lên mới xem được Báo cáo
            btnReport.Visible = employee.IsManager;

            // Chỉ Manager trở lên mới xem được Nhà cung cấp
            btnSuppliers.Visible = employee.IsManager;

            // Chỉ Admin hoặc Director mới được vào trang Giảm giá (Manager và Sales không được)
            btnDiscounts.Visible = employee.Role == "Admin" || employee.Role == "Director";
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
            catch (Exception)
            {
                // Chưa kết nối được database
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
            OpenChildForm(new SystemManagementForm());
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

        private void btnMarketPrice_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new MarketPriceForm());
        }

        private void btnDiscounts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlDashboard.Visible = false;
            OpenChildForm(new DiscountManagementForm());
        }



        #endregion

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _employeeService.Logout();
                ReturnToLogin = true; // Báo hiệu quay về màn hình đăng nhập
                this.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đăng xuất khi đóng form
            _employeeService.Logout();
            // Nếu không đăng xuất thủ công, không quay về login (người dùng đóng cửa sổ)
            // ReturnToLogin giữ nguyên false trừ khi được đặt bởi nút đăng xuất
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
