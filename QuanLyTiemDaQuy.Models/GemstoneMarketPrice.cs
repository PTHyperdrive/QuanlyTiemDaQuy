using System;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// Giá thị trường cơ sở cho từng loại đá quý
    /// </summary>
    public class GemstoneMarketPrice
    {
        public int Id { get; set; }
        public int StoneTypeId { get; set; }
        public string StoneTypeName { get; set; } = string.Empty;
        public decimal BasePricePerCarat { get; set; } // VNĐ
        public DateTime LastUpdated { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// Bảng hệ số màu sắc
    /// </summary>
    public class ColorGrade
    {
        public string Grade { get; set; } = string.Empty;
        public decimal Multiplier { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Bảng hệ số độ tinh khiết
    /// </summary>
    public class ClarityGrade
    {
        public string Grade { get; set; } = string.Empty;
        public decimal Multiplier { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Bảng hệ số cắt
    /// </summary>
    public class CutGrade
    {
        public string Grade { get; set; } = string.Empty;
        public decimal Multiplier { get; set; }
    }

    /// <summary>
    /// Kết quả tính giá thu mua
    /// </summary>
    public class PurchasePriceResult
    {
        public decimal BasePrice { get; set; }
        public decimal SuggestedPrice { get; set; }
        public decimal MinPrice { get; set; } // -30%
        public decimal MaxPrice { get; set; } // +30%
        public string PriceBreakdown { get; set; } = string.Empty;
    }
}
