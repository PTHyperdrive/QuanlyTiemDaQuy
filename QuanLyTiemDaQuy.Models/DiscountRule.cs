using System;

namespace QuanLyTiemDaQuy.Models
{
    public class DiscountRule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountPercent { get; set; }
        public string ApplicableTier { get; set; } // "All", "VIP", "VVIP"
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public int Priority { get; set; }

        public bool IsValid(DateTime date, string customerTier)
        {
            if (!IsActive) return false;
            
            if (StartDate.HasValue && date < StartDate.Value) return false;
            if (EndDate.HasValue && date > EndDate.Value) return false;
            
            if (string.IsNullOrEmpty(ApplicableTier) || ApplicableTier == "All") return true;
            
            if (string.IsNullOrEmpty(customerTier)) return false;

            return ApplicableTier.IndexOf(customerTier, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
