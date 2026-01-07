using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.DAL.Repositories
{
    public class DiscountRepository
    {
        public void EnsureTableExists()
        {
            try
            {
                string checkQuery = "SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'DiscountRules') AND type in (N'U')";
                var scalar = DatabaseHelper.ExecuteScalar(checkQuery);
                
                if (scalar == null)
                {
                    string createQuery = @"
                        CREATE TABLE DiscountRules (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            Name NVARCHAR(100) NOT NULL,
                            DiscountPercent DECIMAL(5,2) NOT NULL DEFAULT 0,
                            ApplicableTier NVARCHAR(50) NULL,
                            StartDate DATETIME NULL,
                            EndDate DATETIME NULL,
                            IsActive BIT NOT NULL DEFAULT 1,
                            Priority INT NOT NULL DEFAULT 0
                        );
                        
                        INSERT INTO DiscountRules (Name, DiscountPercent, ApplicableTier, IsActive, Priority)
                        VALUES (N'Giảm giá VIP', 10, 'VIP', 1, 10);

                        INSERT INTO DiscountRules (Name, DiscountPercent, ApplicableTier, IsActive, Priority)
                        VALUES (N'Giảm giá VVIP', 25, 'VVIP', 1, 20);
                    ";
                    DatabaseHelper.ExecuteNonQuery(createQuery);
                }
            }
            catch { /* Ignore */ }
        }

        public List<DiscountRule> GetAll()
        {
            string query = "SELECT * FROM DiscountRules ORDER BY Priority DESC, Id DESC";
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        public List<DiscountRule> GetActiveRules(DateTime date)
        {
            string query = @"
                SELECT * FROM DiscountRules 
                WHERE IsActive = 1 
                AND (StartDate IS NULL OR StartDate <= @Date)
                AND (EndDate IS NULL OR EndDate >= @Date)
                ORDER BY Priority DESC";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Date", date));
            return MapDataTableToList(dt);
        }

        public DiscountRule GetById(int id)
        {
            string query = "SELECT * FROM DiscountRules WHERE Id = @Id";
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Id", id));
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        public int Add(DiscountRule rule)
        {
            string query = @"
                INSERT INTO DiscountRules (Name, DiscountPercent, ApplicableTier, StartDate, EndDate, IsActive, Priority)
                VALUES (@Name, @DiscountPercent, @ApplicableTier, @StartDate, @EndDate, @IsActive, @Priority);
                SELECT SCOPE_IDENTITY();";
            
            using (var conn = new SqlConnection(DatabaseHelper.ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", rule.Name);
                    cmd.Parameters.AddWithValue("@DiscountPercent", rule.DiscountPercent);
                    cmd.Parameters.AddWithValue("@ApplicableTier", (object)rule.ApplicableTier ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartDate", (object)rule.StartDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", (object)rule.EndDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", rule.IsActive);
                    cmd.Parameters.AddWithValue("@Priority", rule.Priority);
                    
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public bool Update(DiscountRule rule)
        {
            string query = @"
                UPDATE DiscountRules 
                SET Name = @Name, 
                    DiscountPercent = @DiscountPercent, 
                    ApplicableTier = @ApplicableTier, 
                    StartDate = @StartDate, 
                    EndDate = @EndDate, 
                    IsActive = @IsActive, 
                    Priority = @Priority
                WHERE Id = @Id";
            
            int rows = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@Name", rule.Name),
                DatabaseHelper.CreateParameter("@DiscountPercent", rule.DiscountPercent),
                DatabaseHelper.CreateParameter("@ApplicableTier", (object)rule.ApplicableTier ?? DBNull.Value),
                DatabaseHelper.CreateParameter("@StartDate", (object)rule.StartDate ?? DBNull.Value),
                DatabaseHelper.CreateParameter("@EndDate", (object)rule.EndDate ?? DBNull.Value),
                DatabaseHelper.CreateParameter("@IsActive", rule.IsActive),
                DatabaseHelper.CreateParameter("@Priority", rule.Priority),
                DatabaseHelper.CreateParameter("@Id", rule.Id));
            
            return rows > 0;
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM DiscountRules WHERE Id = @Id";
            return DatabaseHelper.ExecuteNonQuery(query, DatabaseHelper.CreateParameter("@Id", id)) > 0;
        }

        private List<DiscountRule> MapDataTableToList(DataTable dt)
        {
            var list = new List<DiscountRule>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new DiscountRule
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    DiscountPercent = Convert.ToDecimal(row["DiscountPercent"]),
                    ApplicableTier = row["ApplicableTier"] == DBNull.Value ? null : row["ApplicableTier"].ToString(),
                    StartDate = row["StartDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["StartDate"]),
                    EndDate = row["EndDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["EndDate"]),
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    Priority = Convert.ToInt32(row["Priority"])
                });
            }
            return list;
        }
    }
}
