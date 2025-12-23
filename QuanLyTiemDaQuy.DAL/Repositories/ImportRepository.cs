using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý phiếu nhập hàng
    /// </summary>
    public class ImportRepository
    {
        public List<ImportReceipt> GetAll()
        {
            string query = @"
                SELECT ir.*, s.Name AS SupplierName, e.Name AS EmployeeName
                FROM ImportReceipts ir
                INNER JOIN Suppliers s ON ir.SupplierId = s.SupplierId
                INNER JOIN Employees e ON ir.EmployeeId = e.EmployeeId
                ORDER BY ir.ImportDate DESC";
            
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public ImportReceipt? GetById(int importId)
        {
            string query = @"
                SELECT ir.*, s.Name AS SupplierName, e.Name AS EmployeeName
                FROM ImportReceipts ir
                INNER JOIN Suppliers s ON ir.SupplierId = s.SupplierId
                INNER JOIN Employees e ON ir.EmployeeId = e.EmployeeId
                WHERE ir.ImportId = @ImportId";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@ImportId", importId));
            
            var list = MapDataTableToList(dt);
            if (list.Count == 0) return null;
            
            list[0].Details = GetImportDetails(importId);
            return list[0];
        }

        public List<ImportReceipt> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            string query = @"
                SELECT ir.*, s.Name AS SupplierName, e.Name AS EmployeeName
                FROM ImportReceipts ir
                INNER JOIN Suppliers s ON ir.SupplierId = s.SupplierId
                INNER JOIN Employees e ON ir.EmployeeId = e.EmployeeId
                WHERE CAST(ir.ImportDate AS DATE) BETWEEN @FromDate AND @ToDate
                ORDER BY ir.ImportDate DESC";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@FromDate", fromDate.Date),
                DatabaseHelper.CreateParameter("@ToDate", toDate.Date));
            return MapDataTableToList(dt);
        }

        public List<ImportDetail> GetImportDetails(int importId)
        {
            string query = @"
                SELECT id.*, p.ProductCode, p.Name AS ProductName
                FROM ImportDetails id
                INNER JOIN Products p ON id.ProductId = p.ProductId
                WHERE id.ImportId = @ImportId";
            
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
                    ProductCode = row["ProductCode"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    Qty = Convert.ToInt32(row["Qty"]),
                    UnitCost = Convert.ToDecimal(row["UnitCost"]),
                    LineTotal = Convert.ToDecimal(row["LineTotal"])
                });
            }
            return list;
        }

        /// <summary>
        /// Tạo phiếu nhập mới (với chi tiết) trong transaction
        /// Trigger trong DB sẽ tự động cập nhật tồn kho
        /// </summary>
        public int Insert(ImportReceipt receipt)
        {
            int importId = 0;
            
            DatabaseHelper.ExecuteTransaction((connection, transaction) =>
            {
                // Thêm header phiếu nhập
                string insertReceipt = @"
                    INSERT INTO ImportReceipts (ImportCode, SupplierId, EmployeeId, ImportDate, TotalCost, Note)
                    VALUES (@ImportCode, @SupplierId, @EmployeeId, @ImportDate, @TotalCost, @Note);
                    SELECT SCOPE_IDENTITY();";
                
                using (var cmd = new SqlCommand(insertReceipt, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@ImportCode", receipt.ImportCode);
                    cmd.Parameters.AddWithValue("@SupplierId", receipt.SupplierId);
                    cmd.Parameters.AddWithValue("@EmployeeId", receipt.EmployeeId);
                    cmd.Parameters.AddWithValue("@ImportDate", receipt.ImportDate);
                    cmd.Parameters.AddWithValue("@TotalCost", receipt.TotalCost);
                    cmd.Parameters.AddWithValue("@Note", (object?)receipt.Note ?? DBNull.Value);
                    
                    importId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insert details (trigger sẽ tự cập nhật tồn kho)
                string insertDetail = @"
                    INSERT INTO ImportDetails (ImportId, ProductId, Qty, UnitCost, LineTotal)
                    VALUES (@ImportId, @ProductId, @Qty, @UnitCost, @LineTotal)";
                
                foreach (var detail in receipt.Details)
                {
                    using (var cmd = new SqlCommand(insertDetail, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ImportId", importId);
                        cmd.Parameters.AddWithValue("@ProductId", detail.ProductId);
                        cmd.Parameters.AddWithValue("@Qty", detail.Qty);
                        cmd.Parameters.AddWithValue("@UnitCost", detail.UnitCost);
                        cmd.Parameters.AddWithValue("@LineTotal", detail.LineTotal);
                        cmd.ExecuteNonQuery();
                    }
                }
            });

            return importId;
        }

        /// <summary>
        /// Sinh mã phiếu nhập tự động
        /// </summary>
        public string GenerateImportCode()
        {
            string prefix = "PN" + DateTime.Now.ToString("yyyyMMdd");
            string query = @"
                SELECT TOP 1 ImportCode FROM ImportReceipts 
                WHERE ImportCode LIKE @Prefix + '%' 
                ORDER BY ImportCode DESC";
            
            var result = DatabaseHelper.ExecuteScalar(query, 
                DatabaseHelper.CreateParameter("@Prefix", prefix));
            
            if (result == null)
            {
                return prefix + "001";
            }
            
            string lastCode = result.ToString()!;
            int lastNumber = int.Parse(lastCode.Substring(prefix.Length));
            return prefix + (lastNumber + 1).ToString("D3");
        }

        public bool Delete(int importId)
        {
            string query = "DELETE FROM ImportReceipts WHERE ImportId = @ImportId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@ImportId", importId));
            return affected > 0;
        }

        private List<ImportReceipt> MapDataTableToList(DataTable dt)
        {
            var list = new List<ImportReceipt>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ImportReceipt
                {
                    ImportId = Convert.ToInt32(row["ImportId"]),
                    ImportCode = row["ImportCode"].ToString() ?? "",
                    SupplierId = Convert.ToInt32(row["SupplierId"]),
                    SupplierName = DatabaseHelper.GetString(row, "SupplierName"),
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    EmployeeName = DatabaseHelper.GetString(row, "EmployeeName"),
                    ImportDate = Convert.ToDateTime(row["ImportDate"]),
                    TotalCost = Convert.ToDecimal(row["TotalCost"]),
                    Note = DatabaseHelper.GetString(row, "Note"),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }
    }
}
