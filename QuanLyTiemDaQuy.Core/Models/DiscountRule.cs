using System;

namespace QuanLyTiemDaQuy.Core.Models
{
    public class DiscountRule
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal DiscountPercent { get; set; }
        public string ApplicableTier { get; set; } // "All", "VIP", "VVIP", "Normal"
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int Priority { get; set; } = 0; // Higher = Higher priority
        
        // Helper to check validity
        public bool IsValid(DateTime date, string customerTier)
        {
            if (!IsActive) return false;
            
            // Date check
            if (StartDate.HasValue && date < StartDate.Value) return false;
            if (EndDate.HasValue && date > EndDate.Value) return false;
            
            // Tier check
            if (string.IsNullOrEmpty(ApplicableTier) || ApplicableTier == "All") return true;
            
            // Flexible match
            return ApplicableTier.Contains(customerTier, StringComparison.OrdinalIgnoreCase);
        }
    }
}
