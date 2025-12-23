using System;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// Sản phẩm đá quý với đầy đủ thông tin 4C
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int StoneTypeId { get; set; }
        public string StoneTypeName { get; set; } // For display
        public decimal Carat { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellPrice { get; set; }
        public int StockQty { get; set; }
        public string Status { get; set; } = "Còn hàng";
        public string ImagePath { get; set; }
        public int CertId { get; set; }
        public string CertCode { get; set; } // For display
        public string DisplayLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Computed properties
        public decimal Profit { get { return SellPrice - CostPrice; } }
        public decimal ProfitMargin { get { return CostPrice > 0 ? (Profit / CostPrice) * 100 : 0; } }
        public bool IsLowStock { get { return StockQty <= 5 && StockQty > 0; } }
        public bool IsOutOfStock { get { return StockQty <= 0; } }
    }
}
