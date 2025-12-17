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
            string query = "SELECT * FROM Employees ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public List<Employee> GetActive()
        {
            string query = "SELECT * FROM Employees WHERE IsActive = 1 ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public Employee? GetById(int employeeId)
        {
            string query = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public Employee? GetByUsername(string username)
        {
            string query = "SELECT * FROM Employees WHERE Username = @Username";
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
                SELECT * FROM Employees 
                WHERE Username = @Username AND PasswordHash = @PasswordHash AND IsActive = 1";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Username", username),
                DatabaseHelper.CreateParameter("@PasswordHash", passwordHash));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public int Insert(Employee employee)
        {
            string query = @"
                INSERT INTO Employees (Name, Username, PasswordHash, Role, Phone, Email, IsActive) 
                VALUES (@Name, @Username, @PasswordHash, @Role, @Phone, @Email, @IsActive);
                SELECT SCOPE_IDENTITY();";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Name", employee.Name),
                DatabaseHelper.CreateParameter("@Username", employee.Username),
                DatabaseHelper.CreateParameter("@PasswordHash", HashPassword(employee.PasswordHash)),
                DatabaseHelper.CreateParameter("@Role", employee.Role),
                DatabaseHelper.CreateParameter("@Phone", employee.Phone),
                DatabaseHelper.CreateParameter("@Email", employee.Email),
                DatabaseHelper.CreateParameter("@IsActive", employee.IsActive));
            
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
                    IsActive = @IsActive
                WHERE EmployeeId = @EmployeeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employee.EmployeeId),
                DatabaseHelper.CreateParameter("@Name", employee.Name),
                DatabaseHelper.CreateParameter("@Username", employee.Username),
                DatabaseHelper.CreateParameter("@Role", employee.Role),
                DatabaseHelper.CreateParameter("@Phone", employee.Phone),
                DatabaseHelper.CreateParameter("@Email", employee.Email),
                DatabaseHelper.CreateParameter("@IsActive", employee.IsActive));
            
            return affected > 0;
        }

        public bool UpdatePassword(int employeeId, string newPassword)
        {
            string query = "UPDATE Employees SET PasswordHash = @PasswordHash WHERE EmployeeId = @EmployeeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@EmployeeId", employeeId),
                DatabaseHelper.CreateParameter("@PasswordHash", HashPassword(newPassword)));
            
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
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }
    }
}
