using System.Net.Http.Json;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;
using QuanLyTiemDaQuy.Core.DAL;

namespace QuanLyTiemDaQuy.Core.BLL.Services;

/// <summary>
/// Service cho báo cáo
/// </summary>
public class ReportService : IReportService
{
    public DashboardStats GetDashboardStats()
    {
        var stats = new DashboardStats();
        
        try
        {
            // Total products
            var result = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Products");
            stats.TotalProducts = Convert.ToInt32(result);

            // Low stock products
            result = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Products WHERE StockQty > 0 AND StockQty <= 5");
            stats.LowStockProducts = Convert.ToInt32(result);

            // Total customers
            result = DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Customers");
            stats.TotalCustomers = Convert.ToInt32(result);

            // Today's revenue
            result = DatabaseHelper.ExecuteScalar(@"
                SELECT ISNULL(SUM(Total), 0) FROM Invoices 
                WHERE CAST(InvoiceDate AS DATE) = CAST(GETDATE() AS DATE) AND Status = N'Đã xuất'");
            stats.TodayRevenue = Convert.ToDecimal(result);

            // Today's invoices
            result = DatabaseHelper.ExecuteScalar(@"
                SELECT COUNT(*) FROM Invoices 
                WHERE CAST(InvoiceDate AS DATE) = CAST(GETDATE() AS DATE)");
            stats.TodayInvoices = Convert.ToInt32(result);

            // Month revenue
            result = DatabaseHelper.ExecuteScalar(@"
                SELECT ISNULL(SUM(Total), 0) FROM Invoices 
                WHERE MONTH(InvoiceDate) = MONTH(GETDATE()) AND YEAR(InvoiceDate) = YEAR(GETDATE()) 
                AND Status = N'Đã xuất'");
            stats.MonthRevenue = Convert.ToDecimal(result);

            // Month invoices
            result = DatabaseHelper.ExecuteScalar(@"
                SELECT COUNT(*) FROM Invoices 
                WHERE MONTH(InvoiceDate) = MONTH(GETDATE()) AND YEAR(InvoiceDate) = YEAR(GETDATE())");
            stats.MonthInvoices = Convert.ToInt32(result);

            // Today's new customers
            result = DatabaseHelper.ExecuteScalar(@"
                SELECT COUNT(*) FROM Customers 
                WHERE CAST(CreatedAt AS DATE) = CAST(GETDATE() AS DATE)");
            stats.TodayNewCustomers = Convert.ToInt32(result);
        }
        catch
        {
            // Return default stats on error
        }
        
        return stats;
    }

    public List<DailySalesReport> GetDailySalesReport(DateTime fromDate, DateTime toDate)
    {
        string query = @"
            SELECT 
                CAST(InvoiceDate AS DATE) AS Date,
                COUNT(*) AS TotalInvoices,
                ISNULL(SUM(Total), 0) AS TotalRevenue,
                ISNULL(SUM(DiscountAmount), 0) AS TotalDiscount,
                ISNULL(SUM(VATAmount), 0) AS TotalVAT,
                ISNULL(SUM(Total - VATAmount), 0) AS NetRevenue
            FROM Invoices
            WHERE CAST(InvoiceDate AS DATE) BETWEEN @FromDate AND @ToDate
                AND Status = N'Đã xuất'
            GROUP BY CAST(InvoiceDate AS DATE)
            ORDER BY Date";

        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
            DatabaseHelper.CreateParameter("@ToDate", toDate.Date));

        var list = new List<DailySalesReport>();
        foreach (System.Data.DataRow row in dt.Rows)
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

    public List<MonthlySalesReport> GetMonthlySalesReport(int year)
    {
        string query = @"
            SELECT 
                YEAR(InvoiceDate) AS Year,
                MONTH(InvoiceDate) AS Month,
                COUNT(*) AS TotalInvoices,
                ISNULL(SUM(Total), 0) AS TotalRevenue,
                ISNULL(SUM(Subtotal - Total + VATAmount), 0) AS TotalCost
            FROM Invoices
            WHERE YEAR(InvoiceDate) = @Year AND Status = N'Đã xuất'
            GROUP BY YEAR(InvoiceDate), MONTH(InvoiceDate)
            ORDER BY Month";

        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@Year", year));

        var list = new List<MonthlySalesReport>();
        foreach (System.Data.DataRow row in dt.Rows)
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

    public List<TopProductReport> GetTopProducts(DateTime fromDate, DateTime toDate, int limit = 10)
    {
        string query = @"
            SELECT TOP (@Limit)
                p.ProductId,
                p.ProductCode,
                p.Name AS ProductName,
                st.Name AS StoneTypeName,
                SUM(d.Qty) AS TotalQtySold,
                SUM(d.LineTotal) AS TotalRevenue
            FROM InvoiceDetails d
            INNER JOIN Invoices i ON d.InvoiceId = i.InvoiceId
            INNER JOIN Products p ON d.ProductId = p.ProductId
            LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
            WHERE CAST(i.InvoiceDate AS DATE) BETWEEN @FromDate AND @ToDate
                AND i.Status = N'Đã xuất'
            GROUP BY p.ProductId, p.ProductCode, p.Name, st.Name
            ORDER BY TotalQtySold DESC";

        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@Limit", limit),
            DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
            DatabaseHelper.CreateParameter("@ToDate", toDate.Date));

        var list = new List<TopProductReport>();
        foreach (System.Data.DataRow row in dt.Rows)
        {
            list.Add(new TopProductReport
            {
                ProductId = Convert.ToInt32(row["ProductId"]),
                ProductCode = row["ProductCode"].ToString() ?? "",
                ProductName = row["ProductName"].ToString() ?? "",
                StoneTypeName = DatabaseHelper.GetString(row, "StoneTypeName") ?? "",
                TotalQtySold = Convert.ToInt32(row["TotalQtySold"]),
                TotalRevenue = Convert.ToDecimal(row["TotalRevenue"])
            });
        }
        return list;
    }

    public List<InventoryReport> GetInventoryReport()
    {
        string query = @"
            SELECT 
                p.ProductId,
                p.ProductCode,
                p.Name AS ProductName,
                st.Name AS StoneTypeName,
                p.Carat,
                p.StockQty,
                p.CostPrice,
                p.SellPrice,
                CASE 
                    WHEN p.StockQty <= 0 THEN N'Hết hàng'
                    WHEN p.StockQty <= 5 THEN N'Sắp hết'
                    ELSE N'Còn hàng'
                END AS StockStatus
            FROM Products p
            LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
            ORDER BY p.StockQty, p.ProductCode";

        var dt = DatabaseHelper.ExecuteQuery(query);

        var list = new List<InventoryReport>();
        foreach (System.Data.DataRow row in dt.Rows)
        {
            list.Add(new InventoryReport
            {
                ProductId = Convert.ToInt32(row["ProductId"]),
                ProductCode = row["ProductCode"].ToString() ?? "",
                ProductName = row["ProductName"].ToString() ?? "",
                StoneTypeName = DatabaseHelper.GetString(row, "StoneTypeName") ?? "",
                Carat = Convert.ToDecimal(row["Carat"]),
                StockQty = Convert.ToInt32(row["StockQty"]),
                CostPrice = Convert.ToDecimal(row["CostPrice"]),
                SellPrice = Convert.ToDecimal(row["SellPrice"]),
                StockStatus = row["StockStatus"].ToString() ?? ""
            });
        }
        return list;
    }
}

/// <summary>
/// Service cho tính giá
/// </summary>
public class PricingService : IPricingService
{
    private readonly IMarketPriceRepository _marketPriceRepository;

    public PricingService(IMarketPriceRepository marketPriceRepository)
    {
        _marketPriceRepository = marketPriceRepository;
    }

    public List<GemstoneMarketPrice> GetAllMarketPrices() => _marketPriceRepository.GetAll();

    public PurchasePriceResult CalculatePurchasePrice(int stoneTypeId, decimal carat, string color, string clarity, string cut)
    {
        var marketPrice = _marketPriceRepository.GetByStoneType(stoneTypeId);
        decimal basePrice = marketPrice?.BasePricePerCarat ?? 0;
        
        // Apply multipliers (simplified)
        decimal colorMultiplier = GetColorMultiplier(color);
        decimal clarityMultiplier = GetClarityMultiplier(clarity);
        decimal cutMultiplier = GetCutMultiplier(cut);
        
        decimal suggestedPrice = basePrice * carat * colorMultiplier * clarityMultiplier * cutMultiplier;
        
        return new PurchasePriceResult
        {
            BasePrice = basePrice,
            SuggestedPrice = suggestedPrice,
            MinPrice = suggestedPrice * 0.7m,
            MaxPrice = suggestedPrice * 1.3m,
            PriceBreakdown = $"Base: {basePrice:N0} × Carat: {carat} × Color: {colorMultiplier} × Clarity: {clarityMultiplier} × Cut: {cutMultiplier}"
        };
    }

    public decimal CalculateSellPrice(decimal purchasePrice, decimal profitMargin = 30)
    {
        return purchasePrice * (1 + profitMargin / 100);
    }

    private static decimal GetColorMultiplier(string color)
    {
        return color?.ToUpper() switch
        {
            "D" => 1.5m,
            "E" => 1.4m,
            "F" => 1.3m,
            "G" => 1.2m,
            "H" => 1.1m,
            "I" or "J" => 1.0m,
            _ => 0.9m
        };
    }

    private static decimal GetClarityMultiplier(string clarity)
    {
        return clarity?.ToUpper() switch
        {
            "FL" or "IF" => 1.5m,
            "VVS1" or "VVS2" => 1.3m,
            "VS1" or "VS2" => 1.15m,
            "SI1" or "SI2" => 1.0m,
            _ => 0.85m
        };
    }

    private static decimal GetCutMultiplier(string cut)
    {
        return cut?.ToLower() switch
        {
            "excellent" or "ideal" => 1.2m,
            "very good" => 1.1m,
            "good" => 1.0m,
            _ => 0.9m
        };
    }
}

/// <summary>
/// Service cho Market Price API
/// </summary>
public class MarketPriceApiService : IMarketPriceApiService
{
    private readonly HttpClient _httpClient;

    public MarketPriceApiService()
    {
        _httpClient = new HttpClient();
    }

    public MarketPriceApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MarketPriceApiResult> FetchMarketPricesAsync()
    {
        try
        {
            decimal exchangeRate = await GetExchangeRateUsdVndAsync();
            
            // Use reference prices since there's no free public gemstone API
            var prices = new List<GemstoneMarketPriceData>();
            
            foreach (var kvp in GemstoneReferencePrices.ReferencePrices)
            {
                prices.Add(new GemstoneMarketPriceData
                {
                    StoneTypeName = kvp.Key,
                    PricePerCaratUsd = kvp.Value.AvgPrice,
                    PricePerCaratVnd = kvp.Value.AvgPrice * exchangeRate,
                    Source = "Reference",
                    Notes = kvp.Value.Notes,
                    LastUpdated = DateTime.Now
                });
            }

            return new MarketPriceApiResult
            {
                Success = true,
                Message = "Lấy giá tham khảo thành công",
                Prices = prices,
                ExchangeRateUsdVnd = exchangeRate,
                FetchedAt = DateTime.Now,
                Source = "Reference"
            };
        }
        catch (Exception ex)
        {
            return MarketPriceApiResult.Error($"Lỗi: {ex.Message}");
        }
    }

    public async Task<decimal> GetExchangeRateUsdVndAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ExchangeRateApiResponse>(
                "https://open.er-api.com/v6/latest/USD");
            
            if (response?.Rates?.TryGetValue("VND", out decimal vndRate) == true)
            {
                return vndRate;
            }
        }
        catch
        {
            // Fallback
        }
        
        // Default fallback rate
        return 24500m;
    }
}
