using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý hóa đơn bán hàng
    /// </summary>
    public class InvoiceRepository
    {
        public List<Invoice> GetAll()
        {
            string query = @"
                SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName
                FROM Invoices i
                LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
                INNER JOIN Employees e ON i.EmployeeId = e.EmployeeId
                ORDER BY i.InvoiceDate DESC";
            
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public Invoice GetById(int invoiceId)
        {
            string query = @"
                SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName
                FROM Invoices i
                LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
                INNER JOIN Employees e ON i.EmployeeId = e.EmployeeId
                WHERE i.InvoiceId = @InvoiceId";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@InvoiceId", invoiceId));
            
            var list = MapDataTableToList(dt);
            if (list.Count == 0) return null;
            
            // Tải chi tiết hoá đơn
            list[0].Details = GetInvoiceDetails(invoiceId);
            return list[0];
        }

        public List<Invoice> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            string query = @"
                SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName
                FROM Invoices i
                LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
                INNER JOIN Employees e ON i.EmployeeId = e.EmployeeId
                WHERE CAST(i.InvoiceDate AS DATE) BETWEEN @FromDate AND @ToDate
                ORDER BY i.InvoiceDate DESC";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
                DatabaseHelper.CreateParameter("@ToDate", toDate.Date));
            return MapDataTableToList(dt);
        }

        public List<Invoice> GetByStatus(string status)
        {
            string query = @"
                SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName
                FROM Invoices i
                LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
                INNER JOIN Employees e ON i.EmployeeId = e.EmployeeId
                WHERE i.Status = @Status
                ORDER BY i.InvoiceDate DESC";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Status", status));
            return MapDataTableToList(dt);
        }

        public List<Invoice> GetByCustomer(int customerId)
        {
            string query = @"
                SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName
                FROM Invoices i
                LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
                INNER JOIN Employees e ON i.EmployeeId = e.EmployeeId
                WHERE i.CustomerId = @CustomerId
                ORDER BY i.InvoiceDate DESC";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@CustomerId", customerId));
            return MapDataTableToList(dt);
        }

        public List<InvoiceDetail> GetInvoiceDetails(int invoiceId)
        {
            string query = @"
                SELECT id.*, p.ProductCode, p.Name AS ProductName
                FROM InvoiceDetails id
                INNER JOIN Products p ON id.ProductId = p.ProductId
                WHERE id.InvoiceId = @InvoiceId";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@InvoiceId", invoiceId));
            
            var list = new List<InvoiceDetail>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new InvoiceDetail
                {
                    InvoiceDetailId = Convert.ToInt32(row["InvoiceDetailId"]),
                    InvoiceId = Convert.ToInt32(row["InvoiceId"]),
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductCode = row["ProductCode"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    Qty = Convert.ToInt32(row["Qty"]),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    LineTotal = Convert.ToDecimal(row["LineTotal"])
                });
            }
            return list;
        }

        /// <summary>
        /// Tạo hóa đơn mới (với chi tiết) trong transaction
        /// Status mặc định là "Đang chờ thanh toán"
        /// </summary>
        public int Insert(Invoice invoice)
        {
            int invoiceId = 0;
            
            DatabaseHelper.ExecuteTransaction((connection, transaction) =>
            {
                // Thêm header hoá đơn
                string insertInvoice = @"
                    INSERT INTO Invoices (InvoiceCode, CustomerId, EmployeeId, InvoiceDate, 
                        Subtotal, DiscountPercent, DiscountAmount, VAT, VATAmount, Total, PaymentMethod, Status, Note)
                    VALUES (@InvoiceCode, @CustomerId, @EmployeeId, @InvoiceDate,
                        @Subtotal, @DiscountPercent, @DiscountAmount, @VAT, @VATAmount, @Total, @PaymentMethod, @Status, @Note);
                    SELECT SCOPE_IDENTITY();";
                
                using (var cmd = new SqlCommand(insertInvoice, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@InvoiceCode", invoice.InvoiceCode);
                    cmd.Parameters.AddWithValue("@CustomerId", invoice.CustomerId > 0 ? (object)invoice.CustomerId : DBNull.Value);
                    cmd.Parameters.AddWithValue("@EmployeeId", invoice.EmployeeId);
                    cmd.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                    cmd.Parameters.AddWithValue("@Subtotal", invoice.Subtotal);
                    cmd.Parameters.AddWithValue("@DiscountPercent", invoice.DiscountPercent);
                    cmd.Parameters.AddWithValue("@DiscountAmount", invoice.DiscountAmount);
                    cmd.Parameters.AddWithValue("@VAT", invoice.VAT);
                    cmd.Parameters.AddWithValue("@VATAmount", invoice.VATAmount);
                    cmd.Parameters.AddWithValue("@Total", invoice.Total);
                    cmd.Parameters.AddWithValue("@PaymentMethod", invoice.PaymentMethod);
                    cmd.Parameters.AddWithValue("@Status", invoice.Status ?? InvoiceStatus.Pending);
                    cmd.Parameters.AddWithValue("@Note", (object)invoice.Note ?? DBNull.Value);
                    
                    invoiceId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Thêm chi tiết hoá đơn
                string insertDetail = @"
                    INSERT INTO InvoiceDetails (InvoiceId, ProductId, Qty, UnitPrice, LineTotal)
                    VALUES (@InvoiceId, @ProductId, @Qty, @UnitPrice, @LineTotal)";
                
                foreach (var detail in invoice.Details)
                {
                    using (var cmd = new SqlCommand(insertDetail, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        cmd.Parameters.AddWithValue("@ProductId", detail.ProductId);
                        cmd.Parameters.AddWithValue("@Qty", detail.Qty);
                        cmd.Parameters.AddWithValue("@UnitPrice", detail.UnitPrice);
                        cmd.Parameters.AddWithValue("@LineTotal", detail.LineTotal);
                        cmd.ExecuteNonQuery();
                    }
                }
            });

            return invoiceId;
        }

        /// <summary>
        /// Cập nhật trạng thái hoá đơn
        /// </summary>
        public bool UpdateStatus(int invoiceId, string newStatus)
        {
            string query = "UPDATE Invoices SET Status = @Status WHERE InvoiceId = @InvoiceId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@InvoiceId", invoiceId),
                DatabaseHelper.CreateParameter("@Status", newStatus));
            return affected > 0;
        }

        /// <summary>
        /// Hoàn thành hoá đơn (xuất hoá đơn)
        /// </summary>
        public bool CompleteInvoice(int invoiceId)
        {
            return UpdateStatus(invoiceId, InvoiceStatus.Completed);
        }

        /// <summary>
        /// Huỷ hoá đơn với lý do
        /// </summary>
        public bool CancelInvoice(int invoiceId, string reason)
        {
            string query = @"
                UPDATE Invoices SET 
                    Status = @Status,
                    CancelledAt = GETDATE(),
                    CancelReason = @CancelReason
                WHERE InvoiceId = @InvoiceId AND Status <> @CancelledStatus";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@InvoiceId", invoiceId),
                DatabaseHelper.CreateParameter("@Status", InvoiceStatus.Cancelled),
                DatabaseHelper.CreateParameter("@CancelReason", reason ?? ""),
                DatabaseHelper.CreateParameter("@CancelledStatus", InvoiceStatus.Cancelled));
            return affected > 0;
        }

        /// <summary>
        /// Sinh mã hóa đơn tự động
        /// </summary>
        public string GenerateInvoiceCode()
        {
            string prefix = "HD" + DateTime.Now.ToString("yyyyMMdd");
            string query = @"
                SELECT TOP 1 InvoiceCode FROM Invoices 
                WHERE InvoiceCode LIKE @Prefix + '%' 
                ORDER BY InvoiceCode DESC";
            
            var result = DatabaseHelper.ExecuteScalar(query, 
                DatabaseHelper.CreateParameter("@Prefix", prefix));
            
            if (result == null)
            {
                return prefix + "001";
            }
            
            string lastCode = result.ToString();
            int lastNumber = int.Parse(lastCode.Substring(prefix.Length));
            return prefix + (lastNumber + 1).ToString("D3");
        }

        // KHÔNG CHO PHÉP XOÁ HOÁ ĐƠN - Chỉ huỷ
        // public bool Delete(int invoiceId) - REMOVED

        private List<Invoice> MapDataTableToList(DataTable dt)
        {
            var list = new List<Invoice>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Invoice
                {
                    InvoiceId = Convert.ToInt32(row["InvoiceId"]),
                    InvoiceCode = row["InvoiceCode"].ToString() ?? "",
                    CustomerId = DatabaseHelper.GetValue<int>(row, "CustomerId") ?? 0,
                    CustomerName = DatabaseHelper.GetString(row, "CustomerName") ?? "Khách lẻ",
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    EmployeeName = DatabaseHelper.GetString(row, "EmployeeName"),
                    InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                    Subtotal = Convert.ToDecimal(row["Subtotal"]),
                    DiscountPercent = Convert.ToDecimal(row["DiscountPercent"]),
                    DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                    VAT = Convert.ToDecimal(row["VAT"]),
                    VATAmount = Convert.ToDecimal(row["VATAmount"]),
                    Total = Convert.ToDecimal(row["Total"]),
                    PaymentMethod = row["PaymentMethod"].ToString() ?? "Tiền mặt",
                    Status = DatabaseHelper.GetString(row, "Status") ?? InvoiceStatus.Pending,
                    Note = DatabaseHelper.GetString(row, "Note"),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    CancelledAt = row["CancelledAt"] != DBNull.Value ? Convert.ToDateTime(row["CancelledAt"]) : DateTime.MinValue,
                    CancelReason = DatabaseHelper.GetString(row, "CancelReason")
                });
            }
            return list;
        }
    }
}
