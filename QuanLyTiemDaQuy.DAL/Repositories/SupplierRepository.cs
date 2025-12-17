using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý nhà cung cấp
    /// </summary>
    public class SupplierRepository
    {
        public List<Supplier> GetAll()
        {
            string query = "SELECT * FROM Suppliers ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public Supplier? GetById(int supplierId)
        {
            string query = "SELECT * FROM Suppliers WHERE SupplierId = @SupplierId";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@SupplierId", supplierId));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public List<Supplier> Search(string keyword)
        {
            string query = "SELECT * FROM Suppliers WHERE Name LIKE @Keyword OR ContactPerson LIKE @Keyword ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Keyword", $"%{keyword}%"));
            return MapDataTableToList(dt);
        }

        public int Insert(Supplier supplier)
        {
            string query = @"
                INSERT INTO Suppliers (Name, Phone, Email, Address, ContactPerson) 
                VALUES (@Name, @Phone, @Email, @Address, @ContactPerson);
                SELECT SCOPE_IDENTITY();";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Name", supplier.Name),
                DatabaseHelper.CreateParameter("@Phone", supplier.Phone),
                DatabaseHelper.CreateParameter("@Email", supplier.Email),
                DatabaseHelper.CreateParameter("@Address", supplier.Address),
                DatabaseHelper.CreateParameter("@ContactPerson", supplier.ContactPerson));
            
            return Convert.ToInt32(result);
        }

        public bool Update(Supplier supplier)
        {
            string query = @"
                UPDATE Suppliers SET 
                    Name = @Name, 
                    Phone = @Phone, 
                    Email = @Email, 
                    Address = @Address, 
                    ContactPerson = @ContactPerson
                WHERE SupplierId = @SupplierId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@SupplierId", supplier.SupplierId),
                DatabaseHelper.CreateParameter("@Name", supplier.Name),
                DatabaseHelper.CreateParameter("@Phone", supplier.Phone),
                DatabaseHelper.CreateParameter("@Email", supplier.Email),
                DatabaseHelper.CreateParameter("@Address", supplier.Address),
                DatabaseHelper.CreateParameter("@ContactPerson", supplier.ContactPerson));
            
            return affected > 0;
        }

        public bool Delete(int supplierId)
        {
            string query = "DELETE FROM Suppliers WHERE SupplierId = @SupplierId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@SupplierId", supplierId));
            return affected > 0;
        }

        private List<Supplier> MapDataTableToList(DataTable dt)
        {
            var list = new List<Supplier>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Supplier
                {
                    SupplierId = Convert.ToInt32(row["SupplierId"]),
                    Name = row["Name"].ToString() ?? "",
                    Phone = DatabaseHelper.GetString(row, "Phone"),
                    Email = DatabaseHelper.GetString(row, "Email"),
                    Address = DatabaseHelper.GetString(row, "Address"),
                    ContactPerson = DatabaseHelper.GetString(row, "ContactPerson"),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }
    }
}
