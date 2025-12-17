using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho quản lý bán hàng với các business rules
    /// </summary>
    public class SalesService
    {
        private readonly InvoiceRepository _invoiceRepository;
        private readonly ProductRepository _productRepository;
        private readonly CustomerRepository _customerRepository;

        public SalesService()
        {
            _invoiceRepository = new InvoiceRepository();
            _productRepository = new ProductRepository();
            _customerRepository = new CustomerRepository();
        }

        #region Invoice Operations

        public List<Invoice> GetAllInvoices()
        {
            return _invoiceRepository.GetAll();
        }

        public Invoice GetInvoiceById(int invoiceId)
        {
            return _invoiceRepository.GetById(invoiceId);
        }

        public List<Invoice> GetInvoicesByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _invoiceRepository.GetByDateRange(fromDate, toDate);
        }

        public List<Invoice> GetInvoicesByCustomer(int customerId)
        {
            return _invoiceRepository.GetByCustomer(customerId);
        }

        public List<Invoice> GetInvoicesByStatus(string status)
        {
            return _invoiceRepository.GetByStatus(status);
        }

        /// <summary>
        /// Sinh mã hóa đơn mới
        /// </summary>
        public string GenerateInvoiceCode()
        {
            return _invoiceRepository.GenerateInvoiceCode();
        }

        #endregion

        #region Invoice Status Management

        /// <summary>
        /// Hoàn thành hoá đơn (xuất hoá đơn)
        /// </summary>
        public (bool Success, string Message) CompleteInvoice(int invoiceId)
        {
            var invoice = _invoiceRepository.GetById(invoiceId);
            if (invoice == null)
                return (false, "Không tìm thấy hoá đơn");

            if (invoice.IsCancelled)
                return (false, "Không thể hoàn thành hoá đơn đã huỷ");

            if (invoice.IsCompleted)
                return (false, "Hoá đơn đã được hoàn thành trước đó");

            bool success = _invoiceRepository.CompleteInvoice(invoiceId);
            if (success)
            {
                // Update customer total purchase
                if (invoice.CustomerId > 0)
                {
                    _customerRepository.UpdateTotalPurchase(invoice.CustomerId, invoice.Total);
                }
                return (true, $"Đã xuất hoá đơn {invoice.InvoiceCode}");
            }
            return (false, "Không thể cập nhật trạng thái hoá đơn");
        }

        /// <summary>
        /// Huỷ hoá đơn với lý do (trigger sẽ hoàn trả tồn kho)
        /// </summary>
        public (bool Success, string Message) CancelInvoice(int invoiceId, string reason)
        {
            var invoice = _invoiceRepository.GetById(invoiceId);
            if (invoice == null)
                return (false, "Không tìm thấy hoá đơn");

            if (invoice.IsCancelled)
                return (false, "Hoá đơn đã được huỷ trước đó");

            if (string.IsNullOrWhiteSpace(reason))
                return (false, "Vui lòng nhập lý do huỷ hoá đơn");

            bool success = _invoiceRepository.CancelInvoice(invoiceId, reason);
            if (success)
                return (true, $"Đã huỷ hoá đơn {invoice.InvoiceCode}. Tồn kho đã được hoàn trả.");
            return (false, "Không thể huỷ hoá đơn");
        }

        #endregion

        #region Create Invoice (Sale)

        /// <summary>
        /// Tạo hóa đơn bán hàng với đầy đủ kiểm tra tồn kho
        /// Mặc định trạng thái là "Đang chờ thanh toán"
        /// </summary>
        public (bool Success, string Message, int InvoiceId) CreateInvoice(Invoice invoice, bool autoComplete = true)
        {
            // Validate invoice
            if (invoice.Details == null || invoice.Details.Count == 0)
                return (false, "Hóa đơn phải có ít nhất 1 sản phẩm", 0);

            if (invoice.EmployeeId <= 0)
                return (false, "Không xác định được nhân viên bán hàng", 0);

            // Check stock availability for all items
            var stockErrors = new List<string>();
            foreach (var detail in invoice.Details)
            {
                var product = _productRepository.GetById(detail.ProductId);
                if (product == null)
                {
                    stockErrors.Add($"Sản phẩm ID {detail.ProductId} không tồn tại");
                    continue;
                }

                if (product.StockQty < detail.Qty)
                {
                    stockErrors.Add($"{product.Name}: Yêu cầu {detail.Qty}, chỉ còn {product.StockQty}");
                }

                // Set product info for display
                detail.ProductCode = product.ProductCode;
                detail.ProductName = product.Name;
                detail.UnitPrice = product.SellPrice;
                detail.CalculateLineTotal();
            }

            if (stockErrors.Count > 0)
            {
                return (false, "Không đủ tồn kho:\n" + string.Join("\n", stockErrors), 0);
            }

            // Calculate totals
            invoice.CalculateTotals();

            // Generate invoice code
            if (string.IsNullOrEmpty(invoice.InvoiceCode))
            {
                invoice.InvoiceCode = GenerateInvoiceCode();
            }

            invoice.InvoiceDate = DateTime.Now;
            invoice.Status = autoComplete ? InvoiceStatus.Completed : InvoiceStatus.Pending;

            try
            {
                // Insert invoice (trigger in DB will update stock)
                int invoiceId = _invoiceRepository.Insert(invoice);

                // Update customer total purchase if completed and has customer
                if (autoComplete && invoice.CustomerId > 0)
                {
                    _customerRepository.UpdateTotalPurchase(invoice.CustomerId, invoice.Total);
                }

                string statusText = autoComplete ? "đã xuất" : "đang chờ thanh toán";
                return (true, $"Tạo hóa đơn {invoice.InvoiceCode} thành công ({statusText}). Tổng tiền: {invoice.Total:N0} VNĐ", invoiceId);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi tạo hóa đơn: {ex.Message}", 0);
            }
        }

        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng (kiểm tra tồn kho)
        /// </summary>
        public (bool Success, string Message, InvoiceDetail Detail) AddToCart(string productCode, int qty, List<InvoiceDetail> currentCart)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                return (false, "Vui lòng nhập mã sản phẩm", null);

            if (qty <= 0)
                return (false, "Số lượng phải lớn hơn 0", null);

            var product = _productRepository.GetByCode(productCode);
            if (product == null)
                return (false, $"Không tìm thấy sản phẩm với mã '{productCode}'", null);

            // Check current qty in cart
            int currentCartQty = 0;
            foreach (var item in currentCart)
            {
                if (item.ProductId == product.ProductId)
                    currentCartQty += item.Qty;
            }

            int totalRequestedQty = currentCartQty + qty;
            if (totalRequestedQty > product.StockQty)
            {
                return (false, $"Không đủ tồn kho. Hiện có: {product.StockQty}, Trong giỏ: {currentCartQty}, Yêu cầu thêm: {qty}", null);
            }

            var detail = new InvoiceDetail
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ProductName = product.Name,
                Qty = qty,
                UnitPrice = product.SellPrice,
                LineTotal = qty * product.SellPrice
            };

            return (true, $"Đã thêm {product.Name} x {qty}", detail);
        }

        /// <summary>
        /// Tính toán lại tổng tiền hóa đơn
        /// </summary>
        public Invoice CalculateInvoiceTotals(List<InvoiceDetail> details, decimal discountPercent = 0, decimal vatPercent = 10)
        {
            var invoice = new Invoice
            {
                Details = details,
                DiscountPercent = discountPercent,
                VAT = vatPercent
            };
            invoice.CalculateTotals();
            return invoice;
        }

        #endregion

        #region Statistics

        /// <summary>
        /// Lấy doanh thu hôm nay (chỉ tính hoá đơn đã xuất)
        /// </summary>
        public decimal GetTodayRevenue()
        {
            var today = DateTime.Today;
            var invoices = GetInvoicesByDateRange(today, today);
            
            decimal total = 0;
            foreach (var inv in invoices)
            {
                if (inv.IsCompleted)
                    total += inv.Total;
            }
            return total;
        }

        /// <summary>
        /// Lấy số hóa đơn hôm nay
        /// </summary>
        public int GetTodayInvoiceCount()
        {
            var today = DateTime.Today;
            var invoices = GetInvoicesByDateRange(today, today);
            return invoices.Count;
        }

        #endregion
    }
}

