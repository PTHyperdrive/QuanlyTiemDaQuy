using System;
using System.Collections.Generic;

namespace QuanLyTiemDaQuy.Models
{
    /// <summary>
    /// Trạng thái hoá đơn
    /// </summary>
    public static class InvoiceStatus
    {
        public const string Pending = "Đang chờ thanh toán";
        public const string Completed = "Đã xuất";
        public const string Cancelled = "Đã huỷ";

        public static readonly string[] AllStatuses = { Pending, Completed, Cancelled };
    }

    /// <summary>
    /// Hóa đơn bán hàng
    /// </summary>
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; } = string.Empty;
        public int CustomerId { get; set; } // 0 = Khách lẻ
        public string CustomerName { get; set; } // Để hiển thị
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } // Để hiển thị
        public DateTime InvoiceDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal VAT { get; set; } = 10; // Mặc định 10%
        public decimal VATAmount { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; } = "Tiền mặt";
        public string Status { get; set; } = InvoiceStatus.Pending;
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CancelledAt { get; set; }
        public string CancelReason { get; set; }

        // Thuộc tính điều hướng
        public List<InvoiceDetail> Details { get; set; } = new List<InvoiceDetail>();

        // Thuộc tính tính toán
        public bool IsPending { get { return Status == InvoiceStatus.Pending; } }
        public bool IsCompleted { get { return Status == InvoiceStatus.Completed; } }
        public bool IsCancelled { get { return Status == InvoiceStatus.Cancelled; } }
        public bool CanCancel { get { return !IsCancelled; } }

        /// <summary>
        /// Tính toán tổng tiền hóa đơn
        /// </summary>
        public void CalculateTotals()
        {
            Subtotal = 0;
            foreach (var detail in Details)
            {
                Subtotal += detail.LineTotal;
            }

            DiscountAmount = Subtotal * (DiscountPercent / 100);
            decimal afterDiscount = Subtotal - DiscountAmount;
            VATAmount = afterDiscount * (VAT / 100);
            Total = afterDiscount + VATAmount;
        }
    }

    /// <summary>
    /// Chi tiết hóa đơn bán hàng
    /// </summary>
    public class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; } // Để hiển thị
        public string ProductName { get; set; } // Để hiển thị
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }

        // Tính nếu chưa được đặt
        public void CalculateLineTotal()
        {
            LineTotal = Qty * UnitPrice;
        }
    }

    /// <summary>
    /// Hằng số phương thức thanh toán
    /// </summary>
    public static class PaymentMethods
    {
        public const string Cash = "Tiền mặt";
        public const string Card = "Thẻ";
        public const string Transfer = "Chuyển khoản";

        public static readonly string[] AllMethods = { Cash, Card, Transfer };
    }
}
