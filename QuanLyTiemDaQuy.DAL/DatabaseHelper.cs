using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyTiemDaQuy.DAL
{
    /// <summary>
    /// Helper class để kết nối và thao tác với SQL Server
    /// </summary>
    public static class DatabaseHelper
    {
        // Connection string cho SQL Server
        // Sử dụng Windows Authentication (local):
        private static string _connectionString = @"Data Source=.\SQLEXPRESS2025;Initial Catalog=QuanLyTiemDaQuy;Integrated Security=True;TrustServerCertificate=True";
        
        // Sử dụng SQL Authentication (external server):
        // private static string _connectionString = @"Data Source=<IP>,1433;Initial Catalog=QuanLyTiemDaQuy;User ID=<username>;Password=<password>;TrustServerCertificate=True";

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
