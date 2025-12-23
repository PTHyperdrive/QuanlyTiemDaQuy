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
