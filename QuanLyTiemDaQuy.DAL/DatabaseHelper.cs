using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTiemDaQuy.DAL
{
    /// <summary>
    /// Helper class để kết nối và thao tác với SQL Server Express
    /// </summary>
    public static class DatabaseHelper
    {
        // ====================================================================================================
        // CẤU HÌNH KẾT NỐI DATABASE
        // ====================================================================================================
        // 
        // Connection string HIỆN TẠI: SQL Server Express trên máy local
        // Thay đổi server name nếu cần (.\SQLEXPRESS hoặc localhost\SQLEXPRESS)
        private static string _connectionString = @"Data Source=tcp:stormtrooper.notrespond.com,1433;Initial Catalog=QuanLyTiemDaQuy;User Id=HUTECH;Password=NRSP3dhouse@;Encrypt=True;TrustServerCertificate=True";

        // ----------------------------------------------------------------------------------------------------
        // ĐỂ KẾT NỐI TỚI SQL SERVER EXTERNAL (Remote Server), COMMENT dòng trên và UNCOMMENT một trong các 
        // connection string bên dưới tùy theo loại xác thực:
        // ----------------------------------------------------------------------------------------------------
        //
        // OPTION 1: Windows Authentication (cho server trong cùng domain)
        // private static string _connectionString = @"Data Source=YOUR_SERVER_IP\INSTANCE_NAME;Initial Catalog=QuanLyTiemDaQuy;Integrated Security=True;TrustServerCertificate=True";
        //
        // OPTION 2: SQL Server Authentication (username/password)
        // private static string _connectionString = @"Data Source=YOUR_SERVER_IP\INSTANCE_NAME;Initial Catalog=QuanLyTiemDaQuy;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True";
        //
        // OPTION 3: Azure SQL Database  
        // private static string _connectionString = @"Server=tcp:YOUR_SERVER.database.windows.net,1433;Initial Catalog=QuanLyTiemDaQuy;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;Encrypt=True;TrustServerCertificate=False";
        //
        // LƯU Ý KHI KẾT NỐI EXTERNAL:
        // - Đảm bảo SQL Server cho phép remote connections (cấu hình trong SQL Server Configuration Manager)
        // - Mở port 1433 (hoặc port tùy chọn) trên firewall của server
        // - Nếu dùng named instance, đảm bảo SQL Server Browser service đang chạy
        // ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// Lấy hoặc set connection string
        /// </summary>
        public static string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        /// <summary>
        /// Tạo và mở một SqlConnection mới
        /// </summary>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Execute query và trả về DataTable
        /// </summary>
        public static DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        /// <summary>
        /// Execute INSERT, UPDATE, DELETE và trả về số dòng bị ảnh hưởng
        /// </summary>
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Execute query và trả về giá trị đơn (ví dụ: COUNT, MAX, Identity)
        /// </summary>
        public static object? ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    return command.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Execute query với transaction (cho các thao tác phức tạp)
        /// </summary>
        public static bool ExecuteTransaction(Action<SqlConnection, SqlTransaction> action)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action(connection, transaction);
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Test kết nối database
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tạo SqlParameter helper
        /// </summary>
        public static SqlParameter CreateParameter(string name, object? value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// Helper để đọc giá trị nullable từ DataRow
        /// </summary>
        public static T? GetValue<T>(DataRow row, string columnName) where T : struct
        {
            if (row.IsNull(columnName))
                return null;
            return (T)Convert.ChangeType(row[columnName], typeof(T));
        }

        /// <summary>
        /// Helper để đọc string từ DataRow (có thể null)
        /// </summary>
        public static string? GetString(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
                return null;
            return row[columnName].ToString();
        }
    }
}
