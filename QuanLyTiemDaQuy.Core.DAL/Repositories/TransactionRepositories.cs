using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.DAL.Repositories;

/// <summary>
/// Repository cho quản lý hóa đơn
/// </summary>
public class InvoiceRepository : IInvoiceRepository
{
    public List<Invoice> GetAll()
    {
        string query = @"
            SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName, b.BranchCode, b.Name AS BranchName
            FROM Invoices i
            LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            LEFT JOIN Branches b ON i.BranchId = b.BranchId
            ORDER BY i.InvoiceDate DESC";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public Invoice? GetById(int invoiceId)
    {
        string query = @"
            SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName, b.BranchCode, b.Name AS BranchName
            FROM Invoices i
            LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            LEFT JOIN Branches b ON i.BranchId = b.BranchId
            WHERE i.InvoiceId = @InvoiceId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@InvoiceId", invoiceId));
        var list = MapDataTableToList(dt);
        
        if (list.Count > 0)
        {
            list[0].Details = GetInvoiceDetails(invoiceId);
            return list[0];
        }
        return null;
    }

    public Invoice? GetByCode(string invoiceCode)
    {
        string query = @"
            SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName, b.BranchCode, b.Name AS BranchName
            FROM Invoices i
            LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            LEFT JOIN Branches b ON i.BranchId = b.BranchId
            WHERE i.InvoiceCode = @InvoiceCode";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@InvoiceCode", invoiceCode));
        var list = MapDataTableToList(dt);
        
        if (list.Count > 0)
        {
            list[0].Details = GetInvoiceDetails(list[0].InvoiceId);
            return list[0];
        }
        return null;
    }

    public List<Invoice> GetByDateRange(DateTime fromDate, DateTime toDate)
    {
        string query = @"
            SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName, b.BranchCode, b.Name AS BranchName
            FROM Invoices i
            LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            LEFT JOIN Branches b ON i.BranchId = b.BranchId
            WHERE CAST(i.InvoiceDate AS DATE) BETWEEN @FromDate AND @ToDate
            ORDER BY i.InvoiceDate DESC";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
            DatabaseHelper.CreateParameter("@ToDate", toDate.Date));
        return MapDataTableToList(dt);
    }

    public List<Invoice> GetByCustomerId(int customerId)
    {
        string query = @"
            SELECT i.*, c.Name AS CustomerName, e.Name AS EmployeeName, b.BranchCode, b.Name AS BranchName
            FROM Invoices i
            LEFT JOIN Customers c ON i.CustomerId = c.CustomerId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            LEFT JOIN Branches b ON i.BranchId = b.BranchId
            WHERE i.CustomerId = @CustomerId
            ORDER BY i.InvoiceDate DESC";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@CustomerId", customerId));
        return MapDataTableToList(dt);
    }

    public int Add(Invoice invoice)
    {
        int invoiceId = 0;
        
        DatabaseHelper.ExecuteTransaction((conn, trans) =>
        {
            // Insert invoice header
            string query = @"
                INSERT INTO Invoices (InvoiceCode, CustomerId, EmployeeId, BranchId, InvoiceDate, 
                    Subtotal, DiscountPercent, DiscountAmount, VAT, VATAmount, Total, PaymentMethod, Status, Note)
                VALUES (@InvoiceCode, @CustomerId, @EmployeeId, @BranchId, @InvoiceDate,
                    @Subtotal, @DiscountPercent, @DiscountAmount, @VAT, @VATAmount, @Total, @PaymentMethod, @Status, @Note);
                SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(query, conn, trans);
            cmd.Parameters.AddRange([
                DatabaseHelper.CreateParameter("@InvoiceCode", invoice.InvoiceCode),
                DatabaseHelper.CreateParameter("@CustomerId", invoice.CustomerId),
                DatabaseHelper.CreateParameter("@EmployeeId", invoice.EmployeeId),
                DatabaseHelper.CreateParameter("@BranchId", invoice.BranchId > 0 ? invoice.BranchId : DBNull.Value),
                DatabaseHelper.CreateParameter("@InvoiceDate", invoice.InvoiceDate),
                DatabaseHelper.CreateParameter("@Subtotal", invoice.Subtotal),
                DatabaseHelper.CreateParameter("@DiscountPercent", invoice.DiscountPercent),
                DatabaseHelper.CreateParameter("@DiscountAmount", invoice.DiscountAmount),
                DatabaseHelper.CreateParameter("@VAT", invoice.VAT),
                DatabaseHelper.CreateParameter("@VATAmount", invoice.VATAmount),
                DatabaseHelper.CreateParameter("@Total", invoice.Total),
                DatabaseHelper.CreateParameter("@PaymentMethod", invoice.PaymentMethod),
                DatabaseHelper.CreateParameter("@Status", invoice.Status),
                DatabaseHelper.CreateParameter("@Note", invoice.Note)
            ]);
            
            var result = cmd.ExecuteScalar();
            invoiceId = Convert.ToInt32(result);

            // Insert invoice details
            foreach (var detail in invoice.Details)
            {
                string detailQuery = @"
                    INSERT INTO InvoiceDetails (InvoiceId, ProductId, Qty, UnitPrice, LineTotal)
                    VALUES (@InvoiceId, @ProductId, @Qty, @UnitPrice, @LineTotal)";

                using var detailCmd = new SqlCommand(detailQuery, conn, trans);
                detailCmd.Parameters.AddRange([
                    DatabaseHelper.CreateParameter("@InvoiceId", invoiceId),
                    DatabaseHelper.CreateParameter("@ProductId", detail.ProductId),
                    DatabaseHelper.CreateParameter("@Qty", detail.Qty),
                    DatabaseHelper.CreateParameter("@UnitPrice", detail.UnitPrice),
                    DatabaseHelper.CreateParameter("@LineTotal", detail.LineTotal)
                ]);
                detailCmd.ExecuteNonQuery();
            }
        });

        return invoiceId;
    }

    public bool UpdateStatus(int invoiceId, string status)
    {
        string query = "UPDATE Invoices SET Status = @Status WHERE InvoiceId = @InvoiceId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@InvoiceId", invoiceId),
            DatabaseHelper.CreateParameter("@Status", status));
        return affected > 0;
    }

    public bool Cancel(int invoiceId, string reason)
    {
        string query = @"UPDATE Invoices SET 
            Status = @Status, CancelledAt = GETDATE(), CancelReason = @Reason 
            WHERE InvoiceId = @InvoiceId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@InvoiceId", invoiceId),
            DatabaseHelper.CreateParameter("@Status", InvoiceStatus.Cancelled),
            DatabaseHelper.CreateParameter("@Reason", reason));
        return affected > 0;
    }

    public string GenerateNextCode()
    {
        string prefix = $"HD-{DateTime.Now:yyMMdd}";
        string query = @"SELECT MAX(InvoiceCode) FROM Invoices WHERE InvoiceCode LIKE @Pattern";
        
        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@Pattern", prefix + "-%"));
        
        int nextNumber = 1;
        if (result != null && result != DBNull.Value)
        {
            string? lastCode = result.ToString();
            if (lastCode != null)
            {
                int dashIndex = lastCode.LastIndexOf('-');
                if (dashIndex >= 0 && dashIndex < lastCode.Length - 1)
                {
                    string numPart = lastCode[(dashIndex + 1)..];
                    if (int.TryParse(numPart, out int lastNum))
                    {
                        nextNumber = lastNum + 1;
                    }
                }
            }
        }
        
        return $"{prefix}-{nextNumber:D3}";
    }

    private List<InvoiceDetail> GetInvoiceDetails(int invoiceId)
    {
        string query = @"
            SELECT d.*, p.ProductCode, p.Name AS ProductName
            FROM InvoiceDetails d
            LEFT JOIN Products p ON d.ProductId = p.ProductId
            WHERE d.InvoiceId = @InvoiceId";
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
                ProductCode = DatabaseHelper.GetString(row, "ProductCode"),
                ProductName = DatabaseHelper.GetString(row, "ProductName"),
                Qty = Convert.ToInt32(row["Qty"]),
                UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                LineTotal = Convert.ToDecimal(row["LineTotal"])
            });
        }
        return list;
    }

    private static List<Invoice> MapDataTableToList(DataTable dt)
    {
        var list = new List<Invoice>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new Invoice
            {
                InvoiceId = Convert.ToInt32(row["InvoiceId"]),
                InvoiceCode = row["InvoiceCode"].ToString() ?? "",
                CustomerId = Convert.ToInt32(row["CustomerId"]),
                CustomerName = DatabaseHelper.GetString(row, "CustomerName"),
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                EmployeeName = DatabaseHelper.GetString(row, "EmployeeName"),
                BranchId = DatabaseHelper.GetValue<int>(row, "BranchId") ?? 0,
                BranchName = DatabaseHelper.GetString(row, "BranchName") ?? "",
                InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]),
                Subtotal = Convert.ToDecimal(row["Subtotal"]),
                DiscountPercent = Convert.ToDecimal(row["DiscountPercent"]),
                DiscountAmount = Convert.ToDecimal(row["DiscountAmount"]),
                VAT = Convert.ToDecimal(row["VAT"]),
                VATAmount = Convert.ToDecimal(row["VATAmount"]),
                Total = Convert.ToDecimal(row["Total"]),
                PaymentMethod = row["PaymentMethod"].ToString() ?? "Tiền mặt",
                Status = row["Status"].ToString() ?? InvoiceStatus.Pending,
                Note = DatabaseHelper.GetString(row, "Note"),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }
        return list;
    }
}

/// <summary>
/// Repository cho quản lý nhập hàng
/// </summary>
public class ImportRepository : IImportRepository
{
    public List<ImportReceipt> GetAll()
    {
        string query = @"
            SELECT i.*, s.Name AS SupplierName, e.Name AS EmployeeName
            FROM ImportReceipts i
            LEFT JOIN Suppliers s ON i.SupplierId = s.SupplierId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            ORDER BY i.ImportDate DESC";
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public ImportReceipt? GetById(int importId)
    {
        string query = @"
            SELECT i.*, s.Name AS SupplierName, e.Name AS EmployeeName
            FROM ImportReceipts i
            LEFT JOIN Suppliers s ON i.SupplierId = s.SupplierId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            WHERE i.ImportId = @ImportId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@ImportId", importId));
        var list = MapDataTableToList(dt);
        
        if (list.Count > 0)
        {
            list[0].Details = GetImportDetails(importId);
            return list[0];
        }
        return null;
    }

    public List<ImportReceipt> GetByDateRange(DateTime fromDate, DateTime toDate)
    {
        string query = @"
            SELECT i.*, s.Name AS SupplierName, e.Name AS EmployeeName
            FROM ImportReceipts i
            LEFT JOIN Suppliers s ON i.SupplierId = s.SupplierId
            LEFT JOIN Employees e ON i.EmployeeId = e.EmployeeId
            WHERE CAST(i.ImportDate AS DATE) BETWEEN @FromDate AND @ToDate
            ORDER BY i.ImportDate DESC";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
            DatabaseHelper.CreateParameter("@ToDate", toDate.Date));
        return MapDataTableToList(dt);
    }

    public int Add(ImportReceipt importReceipt)
    {
        int importId = 0;
        
        DatabaseHelper.ExecuteTransaction((conn, trans) =>
        {
            string query = @"
                INSERT INTO ImportReceipts (ImportCode, SupplierId, EmployeeId, ImportDate, TotalCost, Note)
                VALUES (@ImportCode, @SupplierId, @EmployeeId, @ImportDate, @TotalCost, @Note);
                SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(query, conn, trans);
            cmd.Parameters.AddRange([
                DatabaseHelper.CreateParameter("@ImportCode", importReceipt.ImportCode),
                DatabaseHelper.CreateParameter("@SupplierId", importReceipt.SupplierId),
                DatabaseHelper.CreateParameter("@EmployeeId", importReceipt.EmployeeId),
                DatabaseHelper.CreateParameter("@ImportDate", importReceipt.ImportDate),
                DatabaseHelper.CreateParameter("@TotalCost", importReceipt.TotalCost),
                DatabaseHelper.CreateParameter("@Note", importReceipt.Note)
            ]);
            
            var result = cmd.ExecuteScalar();
            importId = Convert.ToInt32(result);

            foreach (var detail in importReceipt.Details)
            {
                string detailQuery = @"
                    INSERT INTO ImportDetails (ImportId, ProductId, Qty, UnitCost, LineTotal)
                    VALUES (@ImportId, @ProductId, @Qty, @UnitCost, @LineTotal)";

                using var detailCmd = new SqlCommand(detailQuery, conn, trans);
                detailCmd.Parameters.AddRange([
                    DatabaseHelper.CreateParameter("@ImportId", importId),
                    DatabaseHelper.CreateParameter("@ProductId", detail.ProductId),
                    DatabaseHelper.CreateParameter("@Qty", detail.Qty),
                    DatabaseHelper.CreateParameter("@UnitCost", detail.UnitCost),
                    DatabaseHelper.CreateParameter("@LineTotal", detail.LineTotal)
                ]);
                detailCmd.ExecuteNonQuery();
            }
        });

        return importId;
    }

    public string GenerateNextCode()
    {
        string prefix = $"PN-{DateTime.Now:yyMMdd}";
        string query = @"SELECT MAX(ImportCode) FROM ImportReceipts WHERE ImportCode LIKE @Pattern";
        
        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@Pattern", prefix + "-%"));
        
        int nextNumber = 1;
        if (result != null && result != DBNull.Value)
        {
            string? lastCode = result.ToString();
            if (lastCode != null)
            {
                int dashIndex = lastCode.LastIndexOf('-');
                if (dashIndex >= 0 && dashIndex < lastCode.Length - 1)
                {
                    string numPart = lastCode[(dashIndex + 1)..];
                    if (int.TryParse(numPart, out int lastNum))
                    {
                        nextNumber = lastNum + 1;
                    }
                }
            }
        }
        
        return $"{prefix}-{nextNumber:D3}";
    }

    private List<ImportDetail> GetImportDetails(int importId)
    {
        string query = @"
            SELECT d.*, p.ProductCode, p.Name AS ProductName
            FROM ImportDetails d
            LEFT JOIN Products p ON d.ProductId = p.ProductId
            WHERE d.ImportId = @ImportId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@ImportId", importId));
        
        var list = new List<ImportDetail>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new ImportDetail
            {
                ImportDetailId = Convert.ToInt32(row["ImportDetailId"]),
                ImportId = Convert.ToInt32(row["ImportId"]),
                ProductId = Convert.ToInt32(row["ProductId"]),
                ProductCode = DatabaseHelper.GetString(row, "ProductCode") ?? "",
                ProductName = DatabaseHelper.GetString(row, "ProductName") ?? "",
                Qty = Convert.ToInt32(row["Qty"]),
                UnitCost = Convert.ToDecimal(row["UnitCost"]),
                LineTotal = Convert.ToDecimal(row["LineTotal"])
            });
        }
        return list;
    }

    private static List<ImportReceipt> MapDataTableToList(DataTable dt)
    {
        var list = new List<ImportReceipt>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new ImportReceipt
            {
                ImportId = Convert.ToInt32(row["ImportId"]),
                ImportCode = row["ImportCode"].ToString() ?? "",
                SupplierId = Convert.ToInt32(row["SupplierId"]),
                SupplierName = DatabaseHelper.GetString(row, "SupplierName") ?? "",
                EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                EmployeeName = DatabaseHelper.GetString(row, "EmployeeName") ?? "",
                ImportDate = Convert.ToDateTime(row["ImportDate"]),
                TotalCost = Convert.ToDecimal(row["TotalCost"]),
                Note = DatabaseHelper.GetString(row, "Note") ?? "",
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            });
        }
        return list;
    }
}

/// <summary>
/// Repository cho Market Price
/// </summary>
public class MarketPriceRepository : IMarketPriceRepository
{
    public List<GemstoneMarketPrice> GetAll()
    {
        string query = @"SELECT p.*, st.Name AS StoneTypeName 
            FROM GemstoneMarketPrices p 
            LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId";
        var dt = DatabaseHelper.ExecuteQuery(query);
        
        var list = new List<GemstoneMarketPrice>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new GemstoneMarketPrice
            {
                Id = Convert.ToInt32(row["Id"]),
                StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                StoneTypeName = DatabaseHelper.GetString(row, "StoneTypeName") ?? "",
                BasePricePerCarat = Convert.ToDecimal(row["BasePricePerCarat"]),
                LastUpdated = Convert.ToDateTime(row["LastUpdated"])
            });
        }
        return list;
    }

    public GemstoneMarketPrice? GetByStoneType(int stoneTypeId)
    {
        string query = @"SELECT p.*, st.Name AS StoneTypeName 
            FROM GemstoneMarketPrices p 
            LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
            WHERE p.StoneTypeId = @StoneTypeId";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId));
        
        if (dt.Rows.Count > 0)
        {
            var row = dt.Rows[0];
            return new GemstoneMarketPrice
            {
                Id = Convert.ToInt32(row["Id"]),
                StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                StoneTypeName = DatabaseHelper.GetString(row, "StoneTypeName") ?? "",
                BasePricePerCarat = Convert.ToDecimal(row["BasePricePerCarat"]),
                LastUpdated = Convert.ToDateTime(row["LastUpdated"])
            };
        }
        return null;
    }

    public bool UpdatePrice(int stoneTypeId, decimal pricePerCarat)
    {
        string query = @"
            UPDATE GemstoneMarketPrices SET BasePricePerCarat = @Price, LastUpdated = GETDATE()
            WHERE StoneTypeId = @StoneTypeId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId),
            DatabaseHelper.CreateParameter("@Price", pricePerCarat));
        return affected > 0;
    }

    public List<MarketPriceHistory> GetPriceHistory(int stoneTypeId, int limit = 30)
    {
        string query = @"SELECT TOP (@Limit) h.*, st.Name AS StoneTypeName 
            FROM MarketPriceHistory h 
            LEFT JOIN StoneTypes st ON h.StoneTypeId = st.StoneTypeId
            WHERE h.StoneTypeId = @StoneTypeId
            ORDER BY h.RecordedAt DESC";
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId),
            DatabaseHelper.CreateParameter("@Limit", limit));
        
        var list = new List<MarketPriceHistory>();
        foreach (DataRow row in dt.Rows)
        {
            list.Add(new MarketPriceHistory
            {
                Id = Convert.ToInt32(row["Id"]),
                StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                StoneTypeName = DatabaseHelper.GetString(row, "StoneTypeName") ?? "",
                PricePerCarat = Convert.ToDecimal(row["PricePerCarat"]),
                RecordedAt = Convert.ToDateTime(row["RecordedAt"]),
                Source = DatabaseHelper.GetString(row, "Source") ?? "Manual"
            });
        }
        return list;
    }

    public bool SavePriceHistory(MarketPriceHistory history)
    {
        string query = @"
            INSERT INTO MarketPriceHistory (StoneTypeId, PricePerCarat, RecordedAt, Source)
            VALUES (@StoneTypeId, @PricePerCarat, @RecordedAt, @Source)";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@StoneTypeId", history.StoneTypeId),
            DatabaseHelper.CreateParameter("@PricePerCarat", history.PricePerCarat),
            DatabaseHelper.CreateParameter("@RecordedAt", history.RecordedAt),
            DatabaseHelper.CreateParameter("@Source", history.Source));
        return affected > 0;
    }

    public ExchangeRate? GetLatestExchangeRate()
    {
        string query = "SELECT TOP 1 * FROM ExchangeRates ORDER BY FetchedAt DESC";
        var dt = DatabaseHelper.ExecuteQuery(query);
        
        if (dt.Rows.Count > 0)
        {
            var row = dt.Rows[0];
            return new ExchangeRate
            {
                Id = Convert.ToInt32(row["Id"]),
                BaseCurrency = row["BaseCurrency"].ToString() ?? "USD",
                TargetCurrency = row["TargetCurrency"].ToString() ?? "VND",
                Rate = Convert.ToDecimal(row["Rate"]),
                FetchedAt = Convert.ToDateTime(row["FetchedAt"])
            };
        }
        return null;
    }

    public bool SaveExchangeRate(ExchangeRate rate)
    {
        string query = @"
            INSERT INTO ExchangeRates (BaseCurrency, TargetCurrency, Rate, FetchedAt)
            VALUES (@BaseCurrency, @TargetCurrency, @Rate, @FetchedAt)";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@BaseCurrency", rate.BaseCurrency),
            DatabaseHelper.CreateParameter("@TargetCurrency", rate.TargetCurrency),
            DatabaseHelper.CreateParameter("@Rate", rate.Rate),
            DatabaseHelper.CreateParameter("@FetchedAt", rate.FetchedAt));
        return affected > 0;
    }
}
