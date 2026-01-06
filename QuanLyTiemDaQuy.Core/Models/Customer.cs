namespace QuanLyTiemDaQuy.Core.Models;

/// <summary>
/// Khách hàng với phân loại tier (Thường, VIP, VVIP)
/// </summary>
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string Tier { get; set; } = "Thường"; // Thường, VIP, VVIP
    public decimal TotalPurchase { get; set; }
    public DateTime CreatedAt { get; set; }

    // Display property
    public string DisplayText => $"{Name} - {Phone} ({Tier})";
    
    /// <summary>
    /// Lấy % chiết khấu dựa trên hạng khách hàng
    /// VVIP: 25%, VIP: 10%, Thường: 0%
    /// </summary>
    public decimal DiscountPercent => GetDiscountByTier(Tier);

    /// <summary>
    /// Lấy % chiết khấu theo tier
    /// </summary>
    public static decimal GetDiscountByTier(string tier)
    {
        return tier switch
        {
            "VVIP" => 25m,
            "VIP" => 10m,
            _ => 0m
        };
    }

    /// <summary>
    /// Lấy ngưỡng tổng mua để đạt tier
    /// VVIP: ≥ 1 tỷ, VIP: ≥ 500 triệu
    /// </summary>
    public static decimal GetTierThreshold(string tier)
    {
        return tier switch
        {
            "VVIP" => 1000000000m, // 1 tỷ
            "VIP" => 500000000m,   // 500 triệu
            _ => 0m
        };
    }

    /// <summary>
    /// Xác định tier dựa trên tổng mua hàng
    /// </summary>
    public static string DetermineTier(decimal totalPurchase)
    {
        if (totalPurchase >= 1000000000m) // 1 tỷ
            return "VVIP";
        else if (totalPurchase >= 500000000m) // 500 triệu
            return "VIP";
        else
            return "Thường";
    }
}
