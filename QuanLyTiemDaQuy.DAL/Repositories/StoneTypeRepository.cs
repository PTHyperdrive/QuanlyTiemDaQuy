using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý loại đá
    /// </summary>
    public class StoneTypeRepository
    {
        public List<StoneType> GetAll()
        {
            string query = "SELECT * FROM StoneTypes ORDER BY Name";
            var dt = DatabaseHelper.ExecuteQuery(query);
            
            var list = new List<StoneType>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new StoneType
                {
                    StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                    Name = row["Name"].ToString() ?? "",
                    Description = DatabaseHelper.GetString(row, "Description")
                });
            }
            return list;
        }

        public StoneType? GetById(int stoneTypeId)
        {
            string query = "SELECT * FROM StoneTypes WHERE StoneTypeId = @StoneTypeId";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId));
            
            if (dt.Rows.Count == 0) return null;
            var row = dt.Rows[0];
            return new StoneType
            {
                StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                Name = row["Name"].ToString() ?? "",
                Description = DatabaseHelper.GetString(row, "Description")
            };
        }

        public int Insert(StoneType stoneType)
        {
            string query = @"
                INSERT INTO StoneTypes (Name, Description) VALUES (@Name, @Description);
                SELECT SCOPE_IDENTITY();";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Name", stoneType.Name),
                DatabaseHelper.CreateParameter("@Description", stoneType.Description));
            
            return Convert.ToInt32(result);
        }

        public bool Update(StoneType stoneType)
        {
            string query = "UPDATE StoneTypes SET Name = @Name, Description = @Description WHERE StoneTypeId = @StoneTypeId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@StoneTypeId", stoneType.StoneTypeId),
                DatabaseHelper.CreateParameter("@Name", stoneType.Name),
                DatabaseHelper.CreateParameter("@Description", stoneType.Description));
            return affected > 0;
        }

        public bool Delete(int stoneTypeId)
        {
            string query = "DELETE FROM StoneTypes WHERE StoneTypeId = @StoneTypeId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId));
            return affected > 0;
        }
    }
}
