using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý khách hàng
    /// </summary>
    public class CustomerRepository
    {
        public List<Customer> GetAll()
        {
            string query = "SELECT * FROM Customers ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public Customer? GetById(int customerId)
        {
            string query = "SELECT * FROM Customers WHERE CustomerId = @CustomerId";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@CustomerId", customerId));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public List<Customer> Search(string keyword)
        {
            string query = "SELECT * FROM Customers WHERE Name LIKE @Keyword OR Phone LIKE @Keyword ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Keyword", $"%{keyword}%"));
            return MapDataTableToList(dt);
        }

        public List<Customer> GetByTier(string tier)
        {
            string query = "SELECT * FROM Customers WHERE Tier = @Tier ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Tier", tier));
            return MapDataTableToList(dt);
        }

        public int Insert(Customer customer)
        {
            string query = @"
                INSERT INTO Customers (Name, Phone, Email, Address, Tier, TotalPurchase) 
                VALUES (@Name, @Phone, @Email, @Address, @Tier, @TotalPurchase);
                SELECT SCOPE_IDENTITY();";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Name", customer.Name),
                DatabaseHelper.CreateParameter("@Phone", customer.Phone),
                DatabaseHelper.CreateParameter("@Email", customer.Email),
                DatabaseHelper.CreateParameter("@Address", customer.Address),
                DatabaseHelper.CreateParameter("@Tier", customer.Tier),
                DatabaseHelper.CreateParameter("@TotalPurchase", customer.TotalPurchase));
            
            return Convert.ToInt32(result);
        }

        public bool Update(Customer customer)
        {
            string query = @"
                UPDATE Customers SET 
                    Name = @Name, 
                    Phone = @Phone, 
                    Email = @Email, 
                    Address = @Address, 
                    Tier = @Tier,
                    TotalPurchase = @TotalPurchase
                WHERE CustomerId = @CustomerId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@CustomerId", customer.CustomerId),
                DatabaseHelper.CreateParameter("@Name", customer.Name),
                DatabaseHelper.CreateParameter("@Phone", customer.Phone),
                DatabaseHelper.CreateParameter("@Email", customer.Email),
                DatabaseHelper.CreateParameter("@Address", customer.Address),
                DatabaseHelper.CreateParameter("@Tier", customer.Tier),
                DatabaseHelper.CreateParameter("@TotalPurchase", customer.TotalPurchase));
            
            return affected > 0;
        }

        public bool UpdateTotalPurchase(int customerId, decimal amount)
        {
            string query = @"
                UPDATE Customers SET TotalPurchase = TotalPurchase + @Amount WHERE CustomerId = @CustomerId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@CustomerId", customerId),
                DatabaseHelper.CreateParameter("@Amount", amount));
            
            return affected > 0;
        }

        public bool Delete(int customerId)
        {
            string query = "DELETE FROM Customers WHERE CustomerId = @CustomerId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@CustomerId", customerId));
            return affected > 0;
        }

        private List<Customer> MapDataTableToList(DataTable dt)
        {
            var list = new List<Customer>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Customer
                {
                    CustomerId = Convert.ToInt32(row["CustomerId"]),
                    Name = row["Name"].ToString() ?? "",
                    Phone = DatabaseHelper.GetString(row, "Phone"),
                    Email = DatabaseHelper.GetString(row, "Email"),
                    Address = DatabaseHelper.GetString(row, "Address"),
                    Tier = row["Tier"].ToString() ?? "Thường",
                    TotalPurchase = Convert.ToDecimal(row["TotalPurchase"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }
    }
}
