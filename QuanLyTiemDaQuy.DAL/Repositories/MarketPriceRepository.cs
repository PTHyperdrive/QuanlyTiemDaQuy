using System;
using System.Collections.Generic;
using System.Data;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý giá thị trường đá quý
    /// </summary>
    public class MarketPriceRepository
    {
        #region Market Prices

        public List<GemstoneMarketPrice> GetAllMarketPrices()
        {
            string query = @"
                SELECT gmp.*, st.Name AS StoneTypeName
                FROM GemstoneMarketPrices gmp
                INNER JOIN StoneTypes st ON gmp.StoneTypeId = st.StoneTypeId
                ORDER BY st.Name";
            
            var dt = DatabaseHelper.ExecuteQuery(query);
            var list = new List<GemstoneMarketPrice>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new GemstoneMarketPrice
                {
                    Id = Convert.ToInt32(row["Id"]),
                    StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                    StoneTypeName = row["StoneTypeName"].ToString() ?? "",
                    BasePricePerCarat = Convert.ToDecimal(row["BasePricePerCarat"]),
                    LastUpdated = Convert.ToDateTime(row["LastUpdated"])
                });
            }
            return list;
        }

        public GemstoneMarketPrice GetByStoneType(int stoneTypeId)
        {
            string query = @"
                SELECT gmp.*, st.Name AS StoneTypeName
                FROM GemstoneMarketPrices gmp
                INNER JOIN StoneTypes st ON gmp.StoneTypeId = st.StoneTypeId
                WHERE gmp.StoneTypeId = @StoneTypeId";
            
            var dt = DatabaseHelper.ExecuteQuery(query,
                DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId));
            
            if (dt.Rows.Count == 0) return null;
            
            var row = dt.Rows[0];
            return new GemstoneMarketPrice
            {
                Id = Convert.ToInt32(row["Id"]),
                StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                StoneTypeName = row["StoneTypeName"].ToString() ?? "",
                BasePricePerCarat = Convert.ToDecimal(row["BasePricePerCarat"]),
                LastUpdated = Convert.ToDateTime(row["LastUpdated"])
            };
        }

        public bool UpdateMarketPrice(int stoneTypeId, decimal basePricePerCarat)
        {
            string query = @"
                UPDATE GemstoneMarketPrices 
                SET BasePricePerCarat = @Price, LastUpdated = GETDATE()
                WHERE StoneTypeId = @StoneTypeId";
            
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId),
                DatabaseHelper.CreateParameter("@Price", basePricePerCarat));
            
            return affected > 0;
        }

        #endregion

        #region Color Grades

        public List<ColorGrade> GetAllColorGrades()
        {
            string query = "SELECT * FROM ColorGrades ORDER BY Multiplier DESC";
            var dt = DatabaseHelper.ExecuteQuery(query);
            var list = new List<ColorGrade>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ColorGrade
                {
                    Grade = row["Grade"].ToString() ?? "",
                    Multiplier = Convert.ToDecimal(row["Multiplier"]),
                    Description = DatabaseHelper.GetString(row, "Description")
                });
            }
            return list;
        }

        public decimal GetColorMultiplier(string grade)
        {
            string query = "SELECT Multiplier FROM ColorGrades WHERE Grade = @Grade";
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Grade", grade));
            return result != null ? Convert.ToDecimal(result) : 1.0m;
        }

        #endregion

        #region Clarity Grades

        public List<ClarityGrade> GetAllClarityGrades()
        {
            string query = "SELECT * FROM ClarityGrades ORDER BY Multiplier DESC";
            var dt = DatabaseHelper.ExecuteQuery(query);
            var list = new List<ClarityGrade>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new ClarityGrade
                {
                    Grade = row["Grade"].ToString() ?? "",
                    Multiplier = Convert.ToDecimal(row["Multiplier"]),
                    Description = DatabaseHelper.GetString(row, "Description")
                });
            }
            return list;
        }

        public decimal GetClarityMultiplier(string grade)
        {
            string query = "SELECT Multiplier FROM ClarityGrades WHERE Grade = @Grade";
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Grade", grade));
            return result != null ? Convert.ToDecimal(result) : 1.0m;
        }

        #endregion

        #region Cut Grades

        public List<CutGrade> GetAllCutGrades()
        {
            string query = "SELECT * FROM CutGrades ORDER BY Multiplier DESC";
            var dt = DatabaseHelper.ExecuteQuery(query);
            var list = new List<CutGrade>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new CutGrade
                {
                    Grade = row["Grade"].ToString() ?? "",
                    Multiplier = Convert.ToDecimal(row["Multiplier"])
                });
            }
            return list;
        }

        public decimal GetCutMultiplier(string grade)
        {
            string query = "SELECT Multiplier FROM CutGrades WHERE Grade = @Grade";
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Grade", grade));
            return result != null ? Convert.ToDecimal(result) : 1.0m;
        }

        #endregion

        #region Price History

        /// <summary>
        /// Thêm bản ghi lịch sử giá
        /// </summary>
        public void InsertPriceHistory(int stoneTypeId, decimal pricePerCarat, string source)
        {
            try
            {
                // Kiểm tra xem bảng tồn tại không, nếu không thì tạo
                EnsurePriceHistoryTableExists();

                string query = @"
                    INSERT INTO MarketPriceHistory (StoneTypeId, PricePerCarat, RecordedAt, Source)
                    VALUES (@StoneTypeId, @PricePerCarat, GETDATE(), @Source)";
                
                DatabaseHelper.ExecuteNonQuery(query,
                    DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId),
                    DatabaseHelper.CreateParameter("@PricePerCarat", pricePerCarat),
                    DatabaseHelper.CreateParameter("@Source", source ?? "Manual"));
            }
            catch (Exception)
            {
                // Silently fail if table doesn't exist or other issues
            }
        }

        /// <summary>
        /// Lấy lịch sử giá của một loại đá
        /// </summary>
        public List<MarketPriceHistory> GetPriceHistory(int stoneTypeId, int days = 30)
        {
            var list = new List<MarketPriceHistory>();
            
            try
            {
                string query = @"
                    SELECT h.*, st.Name AS StoneTypeName
                    FROM MarketPriceHistory h
                    INNER JOIN StoneTypes st ON h.StoneTypeId = st.StoneTypeId
                    WHERE h.StoneTypeId = @StoneTypeId
                        AND h.RecordedAt >= DATEADD(day, -@Days, GETDATE())
                    ORDER BY h.RecordedAt DESC";
                
                var dt = DatabaseHelper.ExecuteQuery(query,
                    DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId),
                    DatabaseHelper.CreateParameter("@Days", days));
                
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(new MarketPriceHistory
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                        StoneTypeName = row["StoneTypeName"]?.ToString() ?? "",
                        PricePerCarat = Convert.ToDecimal(row["PricePerCarat"]),
                        RecordedAt = Convert.ToDateTime(row["RecordedAt"]),
                        Source = row["Source"]?.ToString() ?? "Manual"
                    });
                }
            }
            catch (Exception)
            {
                // Return empty list if table doesn't exist
            }
            
            return list;
        }

        /// <summary>
        /// Đảm bảo bảng MarketPriceHistory tồn tại
        /// </summary>
        private void EnsurePriceHistoryTableExists()
        {
            try
            {
                string checkQuery = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='MarketPriceHistory' AND xtype='U')
                    BEGIN
                        CREATE TABLE MarketPriceHistory (
                            Id INT IDENTITY(1,1) PRIMARY KEY,
                            StoneTypeId INT NOT NULL,
                            PricePerCarat DECIMAL(18,2) NOT NULL,
                            RecordedAt DATETIME DEFAULT GETDATE(),
                            Source NVARCHAR(100) DEFAULT 'Manual',
                            CONSTRAINT FK_PriceHistory_StoneType FOREIGN KEY (StoneTypeId) 
                                REFERENCES StoneTypes(StoneTypeId) ON DELETE CASCADE
                        )
                    END";
                DatabaseHelper.ExecuteNonQuery(checkQuery);
            }
            catch (Exception)
            {
                // Ignore errors - table might already exist or FK constraint issue
            }
        }

        #endregion
    }
}
