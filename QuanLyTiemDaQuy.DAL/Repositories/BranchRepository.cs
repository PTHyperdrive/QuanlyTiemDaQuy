using System;
using System.Collections.Generic;
using System.Data;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý chi nhánh
    /// </summary>
    public class BranchRepository
    {
        public List<Branch> GetAll()
        {
            string query = @"SELECT BranchId, BranchCode, Name, Address, Phone, IsActive, CreatedAt 
                             FROM Branches ORDER BY BranchCode";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public Branch GetById(int branchId)
        {
            string query = @"SELECT BranchId, BranchCode, Name, Address, Phone, IsActive, CreatedAt 
                             FROM Branches WHERE BranchId = @BranchId";
            var dt = DatabaseHelper.ExecuteQuery(query,
                DatabaseHelper.CreateParameter("@BranchId", branchId));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public List<Branch> GetActive()
        {
            string query = @"SELECT BranchId, BranchCode, Name, Address, Phone, IsActive, CreatedAt 
                             FROM Branches WHERE IsActive = 1 ORDER BY BranchCode";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        /// <summary>
        /// Thêm chi nhánh mới
        /// </summary>
        public int Insert(Branch branch)
        {
            string query = @"INSERT INTO Branches (BranchCode, Name, Address, Phone, IsActive, CreatedAt)
                             VALUES (@BranchCode, @Name, @Address, @Phone, @IsActive, GETDATE());
                             SELECT SCOPE_IDENTITY();";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@BranchCode", branch.BranchCode),
                DatabaseHelper.CreateParameter("@Name", branch.Name),
                DatabaseHelper.CreateParameter("@Address", branch.Address),
                DatabaseHelper.CreateParameter("@Phone", branch.Phone),
                DatabaseHelper.CreateParameter("@IsActive", branch.IsActive));
            
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Cập nhật thông tin chi nhánh
        /// </summary>
        public bool Update(Branch branch)
        {
            string query = @"UPDATE Branches 
                             SET BranchCode = @BranchCode, Name = @Name, Address = @Address, 
                                 Phone = @Phone, IsActive = @IsActive
                             WHERE BranchId = @BranchId";
            
            int rows = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@BranchId", branch.BranchId),
                DatabaseHelper.CreateParameter("@BranchCode", branch.BranchCode),
                DatabaseHelper.CreateParameter("@Name", branch.Name),
                DatabaseHelper.CreateParameter("@Address", branch.Address),
                DatabaseHelper.CreateParameter("@Phone", branch.Phone),
                DatabaseHelper.CreateParameter("@IsActive", branch.IsActive));
            
            return rows > 0;
        }

        /// <summary>
        /// Vô hiệu hóa chi nhánh (soft delete)
        /// </summary>
        public bool Deactivate(int branchId)
        {
            string query = "UPDATE Branches SET IsActive = 0 WHERE BranchId = @BranchId";
            int rows = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@BranchId", branchId));
            return rows > 0;
        }

        /// <summary>
        /// Kích hoạt lại chi nhánh
        /// </summary>
        public bool Activate(int branchId)
        {
            string query = "UPDATE Branches SET IsActive = 1 WHERE BranchId = @BranchId";
            int rows = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@BranchId", branchId));
            return rows > 0;
        }

        /// <summary>
        /// Kiểm tra mã chi nhánh đã tồn tại chưa
        /// </summary>
        public bool IsBranchCodeExists(string branchCode, int excludeId = 0)
        {
            string query = "SELECT COUNT(*) FROM Branches WHERE BranchCode = @BranchCode AND BranchId != @ExcludeId";
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@BranchCode", branchCode),
                DatabaseHelper.CreateParameter("@ExcludeId", excludeId));
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Sinh mã chi nhánh tự động
        /// </summary>
        public string GenerateBranchCode()
        {
            string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(BranchCode, 3, 10) AS INT)), 0) + 1 FROM Branches WHERE BranchCode LIKE 'CN%'";
            var result = DatabaseHelper.ExecuteScalar(query);
            int nextId = Convert.ToInt32(result);
            return $"CN{nextId:D3}"; // CN001, CN002, ...
        }

        /// <summary>
        /// Lấy danh sách nhân viên theo chi nhánh
        /// </summary>
        public List<Employee> GetEmployeesByBranch(int branchId)
        {
            string query = @"SELECT e.EmployeeId, e.Name, e.Username, e.PasswordHash, e.Role,
                                    e.Phone, e.Email, e.IsActive, e.MustChangePassword, 
                                    e.BranchId, b.Name as BranchName, e.CreatedAt
                             FROM Employees e
                             LEFT JOIN Branches b ON e.BranchId = b.BranchId
                             WHERE e.BranchId = @BranchId
                             ORDER BY e.Name";
            var dt = DatabaseHelper.ExecuteQuery(query,
                DatabaseHelper.CreateParameter("@BranchId", branchId));
            
            var list = new List<Employee>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Employee
                {
                    EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                    Name = row["Name"].ToString() ?? "",
                    Username = row["Username"].ToString() ?? "",
                    PasswordHash = row["PasswordHash"].ToString() ?? "",
                    Role = row["Role"].ToString() ?? "Sales",
                    Phone = DatabaseHelper.GetString(row, "Phone"),
                    Email = DatabaseHelper.GetString(row, "Email"),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    MustChangePassword = Convert.ToBoolean(row["MustChangePassword"]),
                    BranchId = Convert.ToInt32(row["BranchId"]),
                    BranchName = DatabaseHelper.GetString(row, "BranchName"),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }

        private List<Branch> MapDataTableToList(DataTable dt)
        {
            var list = new List<Branch>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Branch
                {
                    BranchId = Convert.ToInt32(row["BranchId"]),
                    BranchCode = row["BranchCode"].ToString() ?? "",
                    Name = row["Name"].ToString() ?? "",
                    Address = DatabaseHelper.GetString(row, "Address"),
                    Phone = DatabaseHelper.GetString(row, "Phone"),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }
    }
}
