using System;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// Khách hàng với phân loại tier (Thường, VIP, VVIP)
    /// </summary>
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Tier { get; set; } = "Thường"; // Thường, VIP, VVIP
        public decimal TotalPurchase { get; set; }
        public DateTime CreatedAt { get; set; }

        // Display property
        public string DisplayText { get { return Name + " - " + Phone + " (" + Tier + ")"; } }
        
        /// <summary>
        /// Lấy % chiết khấu dựa trên hạng khách hàng
        /// VVIP: 25%, VIP: 10%, Thường: 0%
        /// </summary>
        public decimal DiscountPercent
        {
            get { return GetDiscountByTier(Tier); }
        }

        /// <summary>
        /// Lấy % chiết khấu theo tier
        /// </summary>
        public static decimal GetDiscountByTier(string tier)
        {
            switch (tier)
            {
                case "VVIP":
                    return 25m;
                case "VIP":
                    return 10m;
                default:
                    return 0m;
            }
        }

        /// <summary>
        /// Lấy ngưỡng tổng mua để đạt tier
        /// VVIP: ≥ 1 tỷ, VIP: ≥ 500 triệu
        /// </summary>
        public static decimal GetTierThreshold(string tier)
        {
            switch (tier)
            {
                case "VVIP":
                    return 1000000000m; // 1 tỷ
                case "VIP":
                    return 500000000m;  // 500 triệu
                default:
                    return 0m;
            }
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
}
