using System;
using System.Collections.Generic;
using System.Data;
using QuanLyTiemDaQuy.DAL;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho báo cáo và thống kê
    /// </summary>
    public class ReportService
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly ProductRepository _productRepository;
        private readonly CustomerRepository _customerRepository;

        public ReportService()
        {
            _invoiceRepository = new InvoiceRepository();
            _productRepository = new ProductRepository();
            _customerRepository = new CustomerRepository();
        }

        #region Sales Reports

        /// <summary>
        /// Báo cáo doanh thu theo ngày
        /// </summary>
        public List<DailySalesReport> GetDailySalesReport(DateTime fromDate, DateTime toDate)
        {
            string query = @"
                SELECT 
                    CAST(InvoiceDate AS DATE) AS Date,
                    COUNT(*) AS TotalInvoices,
                    SUM(Subtotal) AS TotalRevenue,
                    SUM(DiscountAmount) AS TotalDiscount,
                    SUM(VATAmount) AS TotalVAT,
                    SUM(Total) AS NetRevenue
                FROM Invoices
                WHERE CAST(InvoiceDate AS DATE) BETWEEN @FromDate AND @ToDate
                GROUP BY CAST(InvoiceDate AS DATE)
                ORDER BY Date";

            var dt = DatabaseHelper.ExecuteQuery(query,
                DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
                DatabaseHelper.CreateParameter("@ToDate", toDate.Date));

            var list = new List<DailySalesReport>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DailySalesReport
                {
                    Date = Convert.ToDateTime(row["Date"]),
                    TotalInvoices = Convert.ToInt32(row["TotalInvoices"]),
                    TotalRevenue = Convert.ToDecimal(row["TotalRevenue"]),
                    TotalDiscount = Convert.ToDecimal(row["TotalDiscount"]),
                    TotalVAT = Convert.ToDecimal(row["TotalVAT"]),
                    NetRevenue = Convert.ToDecimal(row["NetRevenue"])
                });
            }
            return list;
        }

        /// <summary>
        /// Báo cáo doanh thu theo tháng
        /// </summary>
        public List<MonthlySalesReport> GetMonthlySalesReport(int year)
        {
            string query = @"
                SELECT 
                    YEAR(InvoiceDate) AS Year,
                    MONTH(InvoiceDate) AS Month,
                    COUNT(*) AS TotalInvoices,
                    SUM(Total) AS TotalRevenue,
                    ISNULL((
                        SELECT SUM(id.Qty * p.CostPrice) 
                        FROM InvoiceDetails id 
                        INNER JOIN Products p ON id.ProductId = p.ProductId
                        INNER JOIN Invoices i2 ON id.InvoiceId = i2.InvoiceId
                        WHERE YEAR(i2.InvoiceDate) = YEAR(i.InvoiceDate) 
                          AND MONTH(i2.InvoiceDate) = MONTH(i.InvoiceDate)
                    ), 0) AS TotalCost
                FROM Invoices i
                WHERE YEAR(InvoiceDate) = @Year
                GROUP BY YEAR(InvoiceDate), MONTH(InvoiceDate)
                ORDER BY Month";

            var dt = DatabaseHelper.ExecuteQuery(query,
                DatabaseHelper.CreateParameter("@Year", year));

            var list = new List<MonthlySalesReport>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new MonthlySalesReport
                {
                    Year = Convert.ToInt32(row["Year"]),
                    Month = Convert.ToInt32(row["Month"]),
                    TotalInvoices = Convert.ToInt32(row["TotalInvoices"]),
                    TotalRevenue = Convert.ToDecimal(row["TotalRevenue"]),
                    TotalCost = Convert.ToDecimal(row["TotalCost"])
                });
            }
            return list;
        }

        /// <summary>
        /// Top sản phẩm bán chạy
        /// </summary>
        public List<TopProductReport> GetTopSellingProducts(DateTime fromDate, DateTime toDate, int top = 10)
        {
            string query = $@"
                SELECT TOP {top}
                    p.ProductId,
                    p.ProductCode,
                    p.Name AS ProductName,
                    st.Name AS StoneTypeName,
                    SUM(id.Qty) AS TotalQtySold,
                    SUM(id.LineTotal) AS TotalRevenue
                FROM InvoiceDetails id
                INNER JOIN Invoices i ON id.InvoiceId = i.InvoiceId
                INNER JOIN Products p ON id.ProductId = p.ProductId
                INNER JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
                WHERE CAST(i.InvoiceDate AS DATE) BETWEEN @FromDate AND @ToDate
                GROUP BY p.ProductId, p.ProductCode, p.Name, st.Name
                ORDER BY TotalQtySold DESC";

            var dt = DatabaseHelper.ExecuteQuery(query,
                DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
                DatabaseHelper.CreateParameter("@ToDate", toDate.Date));

            var list = new List<TopProductReport>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new TopProductReport
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductCode = row["ProductCode"].ToString() ?? "",
                    ProductName = row["ProductName"].ToString() ?? "",
                    StoneTypeName = row["StoneTypeName"].ToString() ?? "",
                    TotalQtySold = Convert.ToInt32(row["TotalQtySold"]),
                    TotalRevenue = Convert.ToDecimal(row["TotalRevenue"])
                });
            }
            return list;
        }

        #endregion

        #region Inventory Reports

        /// <summary>
        /// Báo cáo tồn kho
        /// </summary>
        public List<InventoryReport> GetInventoryReport(string? stockStatus = null)
        {
            string query = @"
                SELECT * FROM vw_Inventory 
                WHERE 1=1";

            var parameters = new List<System.Data.SqlClient.SqlParameter>();

            if (!string.IsNullOrEmpty(stockStatus))
            {
                query += " AND StockStatus = @StockStatus";
                parameters.Add(DatabaseHelper.CreateParameter("@StockStatus", stockStatus));
            }

            query += " ORDER BY StockQty";

            var dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());

            var list = new List<InventoryReport>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new InventoryReport
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductCode = row["ProductCode"].ToString() ?? "",
                    ProductName = row["Name"].ToString() ?? "",
                    StoneTypeName = row["StoneType"].ToString() ?? "",
                    Carat = Convert.ToDecimal(row["Carat"]),
                    StockQty = Convert.ToInt32(row["StockQty"]),
                    CostPrice = Convert.ToDecimal(row["CostPrice"]),
                    SellPrice = Convert.ToDecimal(row["SellPrice"]),
                    StockStatus = row["StockStatus"].ToString() ?? ""
                });
            }
            return list;
        }

        /// <summary>
        /// Tổng giá trị tồn kho
        /// </summary>
        public decimal GetTotalInventoryValue()
        {
            string query = "SELECT ISNULL(SUM(StockQty * CostPrice), 0) FROM Products";
            var result = DatabaseHelper.ExecuteScalar(query);
            return Convert.ToDecimal(result);
        }

        #endregion

        #region Dashboard

        /// <summary>
        /// Thống kê tổng quan cho dashboard
        /// </summary>
        public DashboardStats GetDashboardStats()
        {
            var stats = new DashboardStats();

            // Total products
            var result = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Products WHERE StockQty > 0");
            stats.TotalProducts = Convert.ToInt32(result);

            // Low stock products
            result = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Products WHERE StockQty > 0 AND StockQty <= 5");
            stats.LowStockProducts = Convert.ToInt32(result);

            // Total customers
            result = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Customers");
            stats.TotalCustomers = Convert.ToInt32(result);

            // Today's revenue
            result = DatabaseHelper.ExecuteScalar(
                "SELECT ISNULL(SUM(Total), 0) FROM Invoices WHERE CAST(InvoiceDate AS DATE) = @Today",
                DatabaseHelper.CreateParameter("@Today", DateTime.Today));
            stats.TodayRevenue = Convert.ToDecimal(result);

            // Today's invoices
            result = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Invoices WHERE CAST(InvoiceDate AS DATE) = @Today",
                DatabaseHelper.CreateParameter("@Today", DateTime.Today));
            stats.TodayInvoices = Convert.ToInt32(result);

            // This month's revenue
            var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            result = DatabaseHelper.ExecuteScalar(
                "SELECT ISNULL(SUM(Total), 0) FROM Invoices WHERE InvoiceDate >= @FirstDay AND InvoiceDate < @NextMonth",
                DatabaseHelper.CreateParameter("@FirstDay", firstDayOfMonth),
                DatabaseHelper.CreateParameter("@NextMonth", firstDayOfMonth.AddMonths(1)));
            stats.MonthRevenue = Convert.ToDecimal(result);

            // This month's invoices
            result = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Invoices WHERE InvoiceDate >= @FirstDay AND InvoiceDate < @NextMonth",
                DatabaseHelper.CreateParameter("@FirstDay", firstDayOfMonth),
                DatabaseHelper.CreateParameter("@NextMonth", firstDayOfMonth.AddMonths(1)));
            stats.MonthInvoices = Convert.ToInt32(result);

            return stats;
        }

        #endregion
    }
}
