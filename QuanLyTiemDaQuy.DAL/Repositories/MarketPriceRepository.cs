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
    }
}
