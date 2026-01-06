namespace QuanLyTiemDaQuy.Core.Models;

#region Exchange Rate API Models

/// <summary>
/// Response từ ExchangeRate-API (https://open.er-api.com/v6/latest/USD)
/// </summary>
public class ExchangeRateApiResponse
{
    public string? Result { get; set; }
    public string? Provider { get; set; }
    public string? Documentation { get; set; }
    public string? TermsOfUse { get; set; }
    public long TimeLastUpdateUnix { get; set; }
    public string? TimeLastUpdateUtc { get; set; }
    public long TimeNextUpdateUnix { get; set; }
    public string? TimeNextUpdateUtc { get; set; }
    public int TimeEolUnix { get; set; }
    public string? BaseCode { get; set; }
    public Dictionary<string, decimal>? Rates { get; set; }
}

#endregion

#region Market Price History

/// <summary>
/// Lịch sử giá thị trường
/// </summary>
public class MarketPriceHistory
{
    public int Id { get; set; }
    public int StoneTypeId { get; set; }
    public string StoneTypeName { get; set; } = string.Empty;
    public decimal PricePerCarat { get; set; }
    public DateTime RecordedAt { get; set; }
    public string Source { get; set; } = "Manual";
}

/// <summary>
/// Tỷ giá đã lưu
/// </summary>
public class ExchangeRate
{
    public int Id { get; set; }
    public string BaseCurrency { get; set; } = "USD";
    public string TargetCurrency { get; set; } = "VND";
    public decimal Rate { get; set; }
    public DateTime FetchedAt { get; set; }
}

#endregion

#region API Result Models

/// <summary>
/// Kết quả tổng hợp từ các API
/// </summary>
public class MarketPriceApiResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<GemstoneMarketPriceData> Prices { get; set; } = [];
    public decimal ExchangeRateUsdVnd { get; set; }
    public DateTime FetchedAt { get; set; }
    public string Source { get; set; } = "API";

    public static MarketPriceApiResult Error(string message)
    {
        return new MarketPriceApiResult
        {
            Success = false,
            Message = message,
            FetchedAt = DateTime.Now
        };
    }
}

/// <summary>
/// Dữ liệu giá từng loại đá
/// </summary>
public class GemstoneMarketPriceData
{
    public int StoneTypeId { get; set; }
    public string StoneTypeName { get; set; } = string.Empty;
    public decimal PricePerCaratUsd { get; set; }
    public decimal PricePerCaratVnd { get; set; }
    public string Source { get; set; } = "API";
    public string Notes { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; }
}

#endregion

#region Reference Price Data

/// <summary>
/// Dữ liệu giá tham khảo cho các loại đá quý
/// Nguồn: GIA, Gemval, industry averages
/// </summary>
public static class GemstoneReferencePrices
{
    /// <summary>
    /// Giá tham khảo (USD/carat) - trung bình quality
    /// </summary>
    public static readonly Dictionary<string, GemstoneReferenceData> ReferencePrices = new()
    {
        // Kim cương - giá trung bình 1ct, Good cut, G-H color, VS clarity
        { "Kim cương", new GemstoneReferenceData { MinPrice = 4000, MaxPrice = 8000, AvgPrice = 5500, Notes = "1ct, Good-VG cut, G-H, VS1-VS2" } },
        { "Diamond", new GemstoneReferenceData { MinPrice = 4000, MaxPrice = 8000, AvgPrice = 5500, Notes = "1ct, Good-VG cut, G-H, VS1-VS2" } },
        
        // Ruby - Burma fine quality
        { "Ruby", new GemstoneReferenceData { MinPrice = 1000, MaxPrice = 8000, AvgPrice = 3500, Notes = "Burma/Myanmar origin, fine quality" } },
        { "Hồng ngọc", new GemstoneReferenceData { MinPrice = 1000, MaxPrice = 8000, AvgPrice = 3500, Notes = "Burma origin" } },
        
        // Sapphire - Ceylon/Kashmir
        { "Sapphire", new GemstoneReferenceData { MinPrice = 800, MaxPrice = 5000, AvgPrice = 2200, Notes = "Ceylon/Kashmir, blue" } },
        { "Lam ngọc", new GemstoneReferenceData { MinPrice = 800, MaxPrice = 5000, AvgPrice = 2200, Notes = "Ceylon origin" } },
        
        // Emerald - Colombian
        { "Emerald", new GemstoneReferenceData { MinPrice = 1000, MaxPrice = 6000, AvgPrice = 2800, Notes = "Colombian, minor oil treatment" } },
        { "Ngọc lục bảo", new GemstoneReferenceData { MinPrice = 1000, MaxPrice = 6000, AvgPrice = 2800, Notes = "Colombian origin" } },
        
        // Alexandrite - rare color change
        { "Alexandrite", new GemstoneReferenceData { MinPrice = 5000, MaxPrice = 15000, AvgPrice = 8000, Notes = "Russian/Brazilian, strong color change" } },
        
        // Tanzanite
        { "Tanzanite", new GemstoneReferenceData { MinPrice = 300, MaxPrice = 1200, AvgPrice = 600, Notes = "AAA quality, vivid blue-violet" } },
        
        // Aquamarine
        { "Aquamarine", new GemstoneReferenceData { MinPrice = 100, MaxPrice = 500, AvgPrice = 250, Notes = "Santa Maria blue" } },
        { "Ngọc xanh biển", new GemstoneReferenceData { MinPrice = 100, MaxPrice = 500, AvgPrice = 250, Notes = "Santa Maria" } },
        
        // Opal
        { "Opal", new GemstoneReferenceData { MinPrice = 50, MaxPrice = 3000, AvgPrice = 500, Notes = "Australian black opal" } },
        
        // Amethyst
        { "Amethyst", new GemstoneReferenceData { MinPrice = 10, MaxPrice = 50, AvgPrice = 25, Notes = "Deep purple, eye-clean" } },
        { "Thạch anh tím", new GemstoneReferenceData { MinPrice = 10, MaxPrice = 50, AvgPrice = 25, Notes = "Deep purple" } },
        
        // Citrine
        { "Citrine", new GemstoneReferenceData { MinPrice = 10, MaxPrice = 30, AvgPrice = 18, Notes = "Natural, golden yellow" } },
        
        // Topaz
        { "Topaz", new GemstoneReferenceData { MinPrice = 25, MaxPrice = 500, AvgPrice = 150, Notes = "Imperial topaz, orange-pink" } },
        
        // Garnet
        { "Garnet", new GemstoneReferenceData { MinPrice = 50, MaxPrice = 500, AvgPrice = 200, Notes = "Tsavorite/Demantoid" } },
        
        // Tourmaline
        { "Tourmaline", new GemstoneReferenceData { MinPrice = 100, MaxPrice = 1000, AvgPrice = 400, Notes = "Paraiba, neon blue-green" } },
        
        // Spinel
        { "Spinel", new GemstoneReferenceData { MinPrice = 200, MaxPrice = 3000, AvgPrice = 800, Notes = "Red/Pink, Burma" } },
        
        // Peridot
        { "Peridot", new GemstoneReferenceData { MinPrice = 50, MaxPrice = 200, AvgPrice = 100, Notes = "Vivid green, Pakistan" } },
        
        // Jade
        { "Jade", new GemstoneReferenceData { MinPrice = 100, MaxPrice = 5000, AvgPrice = 1000, Notes = "Imperial jadeite, Myanmar" } },
        { "Ngọc bích", new GemstoneReferenceData { MinPrice = 100, MaxPrice = 5000, AvgPrice = 1000, Notes = "Imperial jadeite" } },
    };

    /// <summary>
    /// Lấy giá tham khảo theo tên loại đá
    /// </summary>
    public static GemstoneReferenceData GetReferencePrice(string stoneTypeName)
    {
        if (ReferencePrices.TryGetValue(stoneTypeName, out var data))
        {
            return data;
        }
        // Default cho loại đá không xác định
        return new GemstoneReferenceData { MinPrice = 100, MaxPrice = 1000, AvgPrice = 500, Notes = "Unknown gemstone type" };
    }
}

/// <summary>
/// Dữ liệu giá tham khảo
/// </summary>
public class GemstoneReferenceData
{
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal AvgPrice { get; set; }
    public string Notes { get; set; } = string.Empty;
}

#endregion
