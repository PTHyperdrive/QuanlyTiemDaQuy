using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý nhân viên và xác thực
    /// </summary>
    public class EmployeeRepository
    {
        public List<Employee> GetAll()
        {
            string query = @"
                SELECT e.*, b.Name as BranchName 
                FROM Employees e 
                LEFT JOIN Branches b ON e.BranchId = b.BranchId 
                ORDER BY e.Name";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public List<Employee> GetActive()
        {
            string query = @"
                SELECT e.*, b.Name as BranchName 
                FROM Employees e 
                LEFT JOIN Branches b ON e.BranchId = b.BranchId 
                WHERE e.IsActive = 1 
                ORDER BY e.Name";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public Employee? GetById(int employeeId)
        {
            string query = @"
                SELECT e.*, b.Name as BranchName 
                FROM Employees e 
                LEFT JOIN Branches b ON e.BranchId = b.BranchId 
                WHERE e.EmployeeId = @EmployeeId";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public Employee? GetByUsername(string username)
        {
            string query = @"
                SELECT e.*, b.Name as BranchName 
                FROM Employees e 
                LEFT JOIN Branches b ON e.BranchId = b.BranchId 
                WHERE e.Username = @Username";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Username", username));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// Xác thực đăng nhập
        /// </summary>
        public Employee? Authenticate(string username, string password)
        {
            string passwordHash = HashPassword(password);
            string query = @"
                SELECT e.*, b.Name as BranchName 
                FROM Employees e 
                LEFT JOIN Branches b ON e.BranchId = b.BranchId 
                WHERE e.Username = @Username AND e.PasswordHash = @PasswordHash AND e.IsActive = 1";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Username", username),
                DatabaseHelper.CreateParameter("@PasswordHash", passwordHash));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public int Insert(Employee employee)
        {
            string query = @"
                INSERT INTO Employees (Name, Username, PasswordHash, Role, Phone, Email, IsActive, BranchId) 
                VALUES (@Name, @Username, @PasswordHash, @Role, @Phone, @Email, @IsActive, @BranchId);
                SELECT SCOPE_IDENTITY();";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Name", employee.Name),
                DatabaseHelper.CreateParameter("@Username", employee.Username),
                DatabaseHelper.CreateParameter("@PasswordHash", HashPassword(employee.PasswordHash)),
                DatabaseHelper.CreateParameter("@Role", employee.Role),
                DatabaseHelper.CreateParameter("@Phone", employee.Phone),
                DatabaseHelper.CreateParameter("@Email", employee.Email),
                DatabaseHelper.CreateParameter("@IsActive", employee.IsActive),
                DatabaseHelper.CreateParameter("@BranchId", employee.BranchId));
            
            return Convert.ToInt32(result);
        }

        public bool Update(Employee employee)
        {
            string query = @"
                UPDATE Employees SET 
                    Name = @Name, 
                    Username = @Username, 
                    Role = @Role, 
                    Phone = @Phone, 
                    Email = @Email, 
                    IsActive = @IsActive,
                    BranchId = @BranchId
                WHERE EmployeeId = @EmployeeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employee.EmployeeId),
                DatabaseHelper.CreateParameter("@Name", employee.Name),
                DatabaseHelper.CreateParameter("@Username", employee.Username),
                DatabaseHelper.CreateParameter("@Role", employee.Role),
                DatabaseHelper.CreateParameter("@Phone", employee.Phone),
                DatabaseHelper.CreateParameter("@Email", employee.Email),
                DatabaseHelper.CreateParameter("@IsActive", employee.IsActive),
                DatabaseHelper.CreateParameter("@BranchId", employee.BranchId));
            
            return affected > 0;
        }

        public bool UpdatePassword(int employeeId, string newPassword)
        {
            string query = "UPDATE Employees SET PasswordHash = @PasswordHash, MustChangePassword = 0 WHERE EmployeeId = @EmployeeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId),
                DatabaseHelper.CreateParameter("@PasswordHash", HashPassword(newPassword)));
            
            return affected > 0;
        }

        /// <summary>
        /// Admin đặt mật khẩu tùy ý cho nhân viên
        /// </summary>
        public bool SetPassword(int employeeId, string newPassword, bool mustChangeOnLogin = false)
        {
            string query = "UPDATE Employees SET PasswordHash = @PasswordHash, MustChangePassword = @MustChange WHERE EmployeeId = @EmployeeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId),
                DatabaseHelper.CreateParameter("@PasswordHash", HashPassword(newPassword)),
                DatabaseHelper.CreateParameter("@MustChange", mustChangeOnLogin));
            
            return affected > 0;
        }

        /// <summary>
        /// Cập nhật chi nhánh cho nhân viên
        /// </summary>
        public bool UpdateBranch(int employeeId, int branchId)
        {
            string query = "UPDATE Employees SET BranchId = @BranchId WHERE EmployeeId = @EmployeeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId),
                DatabaseHelper.CreateParameter("@BranchId", branchId));
            
            return affected > 0;
        }

        /// <summary>
        /// Đặt cờ yêu cầu đổi mật khẩu khi đăng nhập
        /// </summary>
        public bool SetMustChangePassword(int employeeId, bool mustChange)
        {
            string query = "UPDATE Employees SET MustChangePassword = @MustChange WHERE EmployeeId = @EmployeeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId),
                DatabaseHelper.CreateParameter("@MustChange", mustChange));
            
            return affected > 0;
        }

        public bool Delete(int employeeId)
        {
            // Soft delete - chỉ vô hiệu hóa
            string query = "UPDATE Employees SET IsActive = 0 WHERE EmployeeId = @EmployeeId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId));
            return affected > 0;
        }

        public bool Activate(int employeeId)
        {
            // Kích hoạt lại tài khoản
            string query = "UPDATE Employees SET IsActive = 1 WHERE EmployeeId = @EmployeeId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId));
            return affected > 0;
        }

        public bool IsUsernameExists(string username, int? excludeId = null)
        {
            string query = "SELECT COUNT(*) FROM Employees WHERE Username = @Username";
            var parameters = new List<SqlParameter> { DatabaseHelper.CreateParameter("@Username", username) };

            if (excludeId.HasValue)
            {
                query += " AND EmployeeId != @ExcludeId";
                parameters.Add(DatabaseHelper.CreateParameter("@ExcludeId", excludeId.Value));
            }

            var result = DatabaseHelper.ExecuteScalar(query, parameters.ToArray());
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Hash password với SHA256
        /// </summary>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private List<Employee> MapDataTableToList(DataTable dt)
        {
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
                    MustChangePassword = dt.Columns.Contains("MustChangePassword") && row["MustChangePassword"] != DBNull.Value
                        ? Convert.ToBoolean(row["MustChangePassword"]) : false,
                    BranchId = dt.Columns.Contains("BranchId") && row["BranchId"] != DBNull.Value
                        ? Convert.ToInt32(row["BranchId"]) : 0,
                    BranchName = dt.Columns.Contains("BranchName") && row["BranchName"] != DBNull.Value
                        ? DatabaseHelper.GetString(row, "BranchName") : "",
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }
    }
}
