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
        /// CN01 = chi nhánh đầu tiên (chính), CN02+ = chi nhánh phụ
        /// </summary>
        public string GenerateBranchCode()
        {
            string query = "SELECT COUNT(*) FROM Branches";
            var result = DatabaseHelper.ExecuteScalar(query);
            int count = Convert.ToInt32(result);
            
            // CN01 cho chi nhánh đầu tiên, CN02, CN03... cho các chi nhánh tiếp theo
            int nextNumber = count + 1;
            return $"CN{nextNumber:D2}"; // CN01, CN02, CN03...
        }

        /// <summary>
        /// Kiểm tra tên chi nhánh hợp lệ (phải bắt đầu bằng "Chi nhánh")
        /// </summary>
        public static (bool IsValid, string Message) ValidateBranchName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "Tên chi nhánh không được để trống");
            
            if (!name.StartsWith("Chi nhánh", StringComparison.OrdinalIgnoreCase))
                return (false, "Tên chi nhánh phải bắt đầu bằng 'Chi nhánh' (ví dụ: Chi nhánh Nguyễn Tri Phương)");
            
            if (name.Length < 12) // "Chi nhánh " + ít nhất 1 ký tự
                return (false, "Tên chi nhánh quá ngắn");
            
            return (true, "");
        }

        /// <summary>
        /// Kiểm tra chi nhánh có phải chi nhánh chính không (CN01)
        /// </summary>
        public static bool IsMainBranch(string branchCode)
        {
            return branchCode == "CN01";
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
