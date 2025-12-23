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
    }
}
