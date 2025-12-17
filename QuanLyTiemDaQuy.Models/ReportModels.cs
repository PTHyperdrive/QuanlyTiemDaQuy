using System;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// DTO cho báo cáo doanh thu theo ngày
    /// </summary>
    public class DailySalesReport
    {
        public DateTime Date { get; set; }
        public int TotalInvoices { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalVAT { get; set; }
        public decimal NetRevenue { get; set; }
    }

    /// <summary>
    /// DTO cho báo cáo doanh thu theo tháng
    /// </summary>
    public class MonthlySalesReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName { get { return "Tháng " + Month + "/" + Year; } }
        public int TotalInvoices { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalCost { get; set; }
        public decimal GrossProfit { get { return TotalRevenue - TotalCost; } }
        public decimal ProfitMargin { get { return TotalRevenue > 0 ? (GrossProfit / TotalRevenue) * 100 : 0; } }
    }

    /// <summary>
    /// DTO cho báo cáo sản phẩm bán chạy
    /// </summary>
    public class TopProductReport
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string StoneTypeName { get; set; } = string.Empty;
        public int TotalQtySold { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    /// <summary>
    /// DTO cho báo cáo tồn kho
    /// </summary>
    public class InventoryReport
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string StoneTypeName { get; set; } = string.Empty;
        public decimal Carat { get; set; }
        public int StockQty { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal TotalStockValue { get { return StockQty * CostPrice; } }
        public string StockStatus { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO cho thống kê tổng quan dashboard
    /// </summary>
    public class DashboardStats
    {
        public int TotalProducts { get; set; }
        public int LowStockProducts { get; set; }
        public int TotalCustomers { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal MonthRevenue { get; set; }
        public int TodayInvoices { get; set; }
        public int MonthInvoices { get; set; }
    }
}
