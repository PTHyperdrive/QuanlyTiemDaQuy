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
        public string SupplierName { get; set; } = string.Empty; // For display
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty; // For display
        public DateTime ImportDate { get; set; } = DateTime.Now;
        public decimal TotalCost { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public List<ImportDetail> Details { get; set; } = new List<ImportDetail>();

        /// <summary>
        /// Tính tổng tiền phiếu nhập
        /// </summary>
        public void CalculateTotal()
        {
            TotalCost = 0;
            foreach (var detail in Details)
            {
                TotalCost += detail.LineTotal;
            }
        }
    }

    /// <summary>
    /// Chi tiết phiếu nhập
    /// </summary>
    public class ImportDetail
    {
        public int ImportDetailId { get; set; }
        public int ImportId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty; // For display
        public string ProductName { get; set; } = string.Empty; // For display
        public int Qty { get; set; }
        public decimal UnitCost { get; set; }
        public decimal LineTotal { get; set; }

        public void CalculateLineTotal()
        {
            LineTotal = Qty * UnitCost;
        }
    }
}
