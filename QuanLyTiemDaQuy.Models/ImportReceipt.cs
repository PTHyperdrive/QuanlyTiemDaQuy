using System;
using System.Collections.Generic;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// Phiếu nhập hàng
    /// </summary>
    public class ImportReceipt
    {
        public int ImportId { get; set; }
        public string ImportCode { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } // For display
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } // For display
        public DateTime ImportDate { get; set; }
        public decimal TotalCost { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public List<ImportDetail> Details { get; set; } = new List<ImportDetail>();
    }

    /// <summary>
    /// Chi tiết phiếu nhập hàng
    /// </summary>
    public class ImportDetail
    {
        public int ImportDetailId { get; set; }
        public int ImportId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; } // For display
        public string ProductName { get; set; } // For display
        public int Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LineTotal { get; set; }

        // Computed if not set
        public void CalculateLineTotal()
        {
            LineTotal = Qty * UnitCost;
        }
    }
}
