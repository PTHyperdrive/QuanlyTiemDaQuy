using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý chứng nhận
    /// </summary>
    public class CertificateRepository
    {
        public List<Certificate> GetAll()
        {
            string query = "SELECT * FROM Certificates ORDER BY CertCode";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public Certificate? GetById(int certId)
        {
            string query = "SELECT * FROM Certificates WHERE CertId = @CertId";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@CertId", certId));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public Certificate? GetByCode(string certCode)
        {
            string query = "SELECT * FROM Certificates WHERE CertCode = @CertCode";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@CertCode", certCode));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public int Insert(Certificate cert)
        {
            string query = @"
                INSERT INTO Certificates (CertCode, Issuer, IssueDate) 
                VALUES (@CertCode, @Issuer, @IssueDate);
                SELECT SCOPE_IDENTITY();";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@CertCode", cert.CertCode),
                DatabaseHelper.CreateParameter("@Issuer", cert.Issuer),
                DatabaseHelper.CreateParameter("@IssueDate", cert.IssueDate));
            
            return Convert.ToInt32(result);
        }

        public bool Update(Certificate cert)
        {
            string query = @"
                UPDATE Certificates SET 
                    CertCode = @CertCode, 
                    Issuer = @Issuer, 
                    IssueDate = @IssueDate 
                WHERE CertId = @CertId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@CertId", cert.CertId),
                DatabaseHelper.CreateParameter("@CertCode", cert.CertCode),
                DatabaseHelper.CreateParameter("@Issuer", cert.Issuer),
                DatabaseHelper.CreateParameter("@IssueDate", cert.IssueDate));
            
            return affected > 0;
        }

        public bool Delete(int certId)
        {
            string query = "DELETE FROM Certificates WHERE CertId = @CertId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@CertId", certId));
            return affected > 0;
        }

        public bool IsCodeExists(string certCode, int? excludeId = null)
        {
            string query = "SELECT COUNT(*) FROM Certificates WHERE CertCode = @CertCode";
            var parameters = new List<SqlParameter> { DatabaseHelper.CreateParameter("@CertCode", certCode) };

            if (excludeId.HasValue)
            {
                query += " AND CertId != @ExcludeId";
                parameters.Add(DatabaseHelper.CreateParameter("@ExcludeId", excludeId.Value));
            }

            var result = DatabaseHelper.ExecuteScalar(query, parameters.ToArray());
            return Convert.ToInt32(result) > 0;
        }

        private List<Certificate> MapDataTableToList(DataTable dt)
        {
            var list = new List<Certificate>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Certificate
                {
                    CertId = Convert.ToInt32(row["CertId"]),
                    CertCode = row["CertCode"].ToString() ?? "",
                    Issuer = row["Issuer"].ToString() ?? "",
                    IssueDate = Convert.ToDateTime(row["IssueDate"]),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"])
                });
            }
            return list;
        }
    }
}
