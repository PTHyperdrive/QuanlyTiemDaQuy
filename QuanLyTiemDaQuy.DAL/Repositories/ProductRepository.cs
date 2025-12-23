using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.DAL.Repositories
{
    /// <summary>
    /// Repository cho quản lý sản phẩm
    /// </summary>
    public class ProductRepository
    {
        /// <summary>
        /// Lấy tất cả sản phẩm
        /// </summary>
        public List<Product> GetAll()
        {
            string query = @"
                SELECT p.*, st.Name AS StoneTypeName, c.CertCode
                FROM Products p
                LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
                LEFT JOIN Certificates c ON p.CertId = c.CertId
                ORDER BY p.ProductCode";
            
            var dt = DatabaseHelper.ExecuteQuery(query);
            return MapDataTableToList(dt);
        }

        /// <summary>
        /// Lấy sản phẩm theo ID
        /// </summary>
        public Product? GetById(int productId)
        {
            string query = @"
                SELECT p.*, st.Name AS StoneTypeName, c.CertCode
                FROM Products p
                LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
                LEFT JOIN Certificates c ON p.CertId = c.CertId
                WHERE p.ProductId = @ProductId";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@ProductId", productId));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// Lấy sản phẩm theo mã
        /// </summary>
        public Product? GetByCode(string productCode)
        {
            string query = @"
                SELECT p.*, st.Name AS StoneTypeName, c.CertCode
                FROM Products p
                LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
                LEFT JOIN Certificates c ON p.CertId = c.CertId
                WHERE p.ProductCode = @ProductCode";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@ProductCode", productCode));
            
            var list = MapDataTableToList(dt);
            return list.Count > 0 ? list[0] : null;
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo nhiều tiêu chí
        /// </summary>
        public List<Product> Search(string? keyword = null, int? stoneTypeId = null, 
            decimal? minCarat = null, decimal? maxCarat = null,
            decimal? minPrice = null, decimal? maxPrice = null,
            string? status = null)
        {
            string query = @"
                SELECT p.*, st.Name AS StoneTypeName, c.CertCode
                FROM Products p
                LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
                LEFT JOIN Certificates c ON p.CertId = c.CertId
                WHERE 1=1";
            
            var parameters = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(keyword))
            {
                query += " AND (p.ProductCode LIKE @Keyword OR p.Name LIKE @Keyword)";
                parameters.Add(DatabaseHelper.CreateParameter("@Keyword", $"%{keyword}%"));
            }

            if (stoneTypeId.HasValue)
            {
                query += " AND p.StoneTypeId = @StoneTypeId";
                parameters.Add(DatabaseHelper.CreateParameter("@StoneTypeId", stoneTypeId.Value));
            }

            if (minCarat.HasValue)
            {
                query += " AND p.Carat >= @MinCarat";
                parameters.Add(DatabaseHelper.CreateParameter("@MinCarat", minCarat.Value));
            }

            if (maxCarat.HasValue)
            {
                query += " AND p.Carat <= @MaxCarat";
                parameters.Add(DatabaseHelper.CreateParameter("@MaxCarat", maxCarat.Value));
            }

            if (minPrice.HasValue)
            {
                query += " AND p.SellPrice >= @MinPrice";
                parameters.Add(DatabaseHelper.CreateParameter("@MinPrice", minPrice.Value));
            }

            if (maxPrice.HasValue)
            {
                query += " AND p.SellPrice <= @MaxPrice";
                parameters.Add(DatabaseHelper.CreateParameter("@MaxPrice", maxPrice.Value));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query += " AND p.Status = @Status";
                parameters.Add(DatabaseHelper.CreateParameter("@Status", status));
            }

            query += " ORDER BY p.ProductCode";

            var dt = DatabaseHelper.ExecuteQuery(query, parameters.ToArray());
            return MapDataTableToList(dt);
        }

        /// <summary>
        /// Lấy sản phẩm tồn kho thấp
        /// </summary>
        public List<Product> GetLowStock(int threshold = 5)
        {
            string query = @"
                SELECT p.*, st.Name AS StoneTypeName, c.CertCode
                FROM Products p
                LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
                LEFT JOIN Certificates c ON p.CertId = c.CertId
                WHERE p.StockQty > 0 AND p.StockQty <= @Threshold
                ORDER BY p.StockQty";
            
            var dt = DatabaseHelper.ExecuteQuery(query, 
                DatabaseHelper.CreateParameter("@Threshold", threshold));
            return MapDataTableToList(dt);
        }

        /// <summary>
        /// Thêm sản phẩm mới
        /// </summary>
        public int Insert(Product product)
        {
            string query = @"
                INSERT INTO Products (ProductCode, Name, StoneTypeId, Carat, Color, Clarity, Cut, 
                    CostPrice, SellPrice, StockQty, Status, ImagePath, CertId, DisplayLocation)
                VALUES (@ProductCode, @Name, @StoneTypeId, @Carat, @Color, @Clarity, @Cut,
                    @CostPrice, @SellPrice, @StockQty, @Status, @ImagePath, @CertId, @DisplayLocation);
                SELECT SCOPE_IDENTITY();";

            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@ProductCode", product.ProductCode),
                DatabaseHelper.CreateParameter("@Name", product.Name),
                DatabaseHelper.CreateParameter("@StoneTypeId", product.StoneTypeId),
                DatabaseHelper.CreateParameter("@Carat", product.Carat),
                DatabaseHelper.CreateParameter("@Color", product.Color),
                DatabaseHelper.CreateParameter("@Clarity", product.Clarity),
                DatabaseHelper.CreateParameter("@Cut", product.Cut),
                DatabaseHelper.CreateParameter("@CostPrice", product.CostPrice),
                DatabaseHelper.CreateParameter("@SellPrice", product.SellPrice),
                DatabaseHelper.CreateParameter("@StockQty", product.StockQty),
                DatabaseHelper.CreateParameter("@Status", product.Status),
                DatabaseHelper.CreateParameter("@ImagePath", product.ImagePath),
                DatabaseHelper.CreateParameter("@CertId", product.CertId),
                DatabaseHelper.CreateParameter("@DisplayLocation", product.DisplayLocation));

            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Cập nhật sản phẩm
        /// </summary>
        public bool Update(Product product)
        {
            string query = @"
                UPDATE Products SET 
                    ProductCode = @ProductCode,
                    Name = @Name,
                    StoneTypeId = @StoneTypeId,
                    Carat = @Carat,
                    Color = @Color,
                    Clarity = @Clarity,
                    Cut = @Cut,
                    CostPrice = @CostPrice,
                    SellPrice = @SellPrice,
                    StockQty = @StockQty,
                    Status = @Status,
                    ImagePath = @ImagePath,
                    CertId = @CertId,
                    DisplayLocation = @DisplayLocation,
                    UpdatedAt = GETDATE()
                WHERE ProductId = @ProductId";

            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@ProductId", product.ProductId),
                DatabaseHelper.CreateParameter("@ProductCode", product.ProductCode),
                DatabaseHelper.CreateParameter("@Name", product.Name),
                DatabaseHelper.CreateParameter("@StoneTypeId", product.StoneTypeId),
                DatabaseHelper.CreateParameter("@Carat", product.Carat),
                DatabaseHelper.CreateParameter("@Color", product.Color),
                DatabaseHelper.CreateParameter("@Clarity", product.Clarity),
                DatabaseHelper.CreateParameter("@Cut", product.Cut),
                DatabaseHelper.CreateParameter("@CostPrice", product.CostPrice),
                DatabaseHelper.CreateParameter("@SellPrice", product.SellPrice),
                DatabaseHelper.CreateParameter("@StockQty", product.StockQty),
                DatabaseHelper.CreateParameter("@Status", product.Status),
                DatabaseHelper.CreateParameter("@ImagePath", product.ImagePath),
                DatabaseHelper.CreateParameter("@CertId", product.CertId),
                DatabaseHelper.CreateParameter("@DisplayLocation", product.DisplayLocation));

            return affected > 0;
        }

        /// <summary>
        /// Cập nhật số lượng tồn kho
        /// </summary>
        public bool UpdateStock(int productId, int newQty)
        {
            string query = @"
                UPDATE Products SET 
                    StockQty = @StockQty,
                    Status = CASE WHEN @StockQty <= 0 THEN N'Hết hàng' ELSE Status END,
                    UpdatedAt = GETDATE()
                WHERE ProductId = @ProductId";

            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@ProductId", productId),
                DatabaseHelper.CreateParameter("@StockQty", newQty));

            return affected > 0;
        }

        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        public bool Delete(int productId)
        {
            string query = "DELETE FROM Products WHERE ProductId = @ProductId";
            int affected = DatabaseHelper.ExecuteNonQuery(query,
                DatabaseHelper.CreateParameter("@ProductId", productId));
            return affected > 0;
        }

        /// <summary>
        /// Lấy mã sản phẩm tiếp theo theo loại đá
        /// Format: [PREFIX]-[XXX] (ví dụ: KC-001 cho Kim cương)
        /// </summary>
        public string GetNextProductCode(string stoneTypeName)
        {
            // Mapping loại đá sang prefix
            string prefix = GetStoneTypePrefix(stoneTypeName);
            
            // Tìm mã cao nhất với prefix này
            string query = @"
                SELECT MAX(ProductCode) FROM Products 
                WHERE ProductCode LIKE @Pattern";
            
            var result = DatabaseHelper.ExecuteScalar(query,
                DatabaseHelper.CreateParameter("@Pattern", prefix + "-%"));
            
            int nextNumber = 1;
            
            if (result != null && result != DBNull.Value)
            {
                string lastCode = result.ToString();
                // Parse số từ mã cuối (ví dụ: KC-015 -> 15)
                int dashIndex = lastCode.LastIndexOf('-');
                if (dashIndex >= 0 && dashIndex < lastCode.Length - 1)
                {
                    string numPart = lastCode.Substring(dashIndex + 1);
                    if (int.TryParse(numPart, out int lastNum))
                    {
                        nextNumber = lastNum + 1;
                    }
                }
            }
            
            // Format: PREFIX-XXX (3 chữ số)
            return $"{prefix}-{nextNumber:D3}";
        }

        /// <summary>
        /// Lấy prefix code theo tên loại đá
        /// </summary>
        private string GetStoneTypePrefix(string stoneTypeName)
        {
            if (string.IsNullOrEmpty(stoneTypeName))
                return "SP";

            string name = stoneTypeName.Trim().ToLower();
            
            // Mapping các loại đá phổ biến
            if (name.Contains("kim cương") || name.Contains("diamond"))
                return "KC";
            if (name.Contains("ruby") || name.Contains("hồng ngọc"))
                return "RB";
            if (name.Contains("sapphire") || name.Contains("bích ngọc"))
                return "SP";
            if (name.Contains("emerald") || name.Contains("ngọc lục bảo"))
                return "EM";
            if (name.Contains("opal") || name.Contains("mắt mèo"))
                return "OP";
            if (name.Contains("pearl") || name.Contains("ngọc trai"))
                return "PR";
            if (name.Contains("topaz"))
                return "TP";
            if (name.Contains("amethyst") || name.Contains("thạch anh tím"))
                return "AM";
            if (name.Contains("aquamarine"))
                return "AQ";
            if (name.Contains("jade") || name.Contains("ngọc bích") || name.Contains("cẩm thạch"))
                return "JD";
            if (name.Contains("tanzanite"))
                return "TZ";
            if (name.Contains("tourmaline"))
                return "TM";
            if (name.Contains("garnet"))
                return "GR";
            if (name.Contains("peridot"))
                return "PD";
            if (name.Contains("citrine"))
                return "CT";
            if (name.Contains("alexandrite"))
                return "AL";
            
            // Mặc định: lấy 2 ký tự đầu viết hoa
            if (stoneTypeName.Length >= 2)
                return stoneTypeName.Substring(0, 2).ToUpper();
            
            return "XX";
        }

        /// <summary>
        /// Kiểm tra mã sản phẩm đã tồn tại chưa
        /// </summary>
        public bool IsCodeExists(string productCode, int? excludeId = null)
        {
            string query = "SELECT COUNT(*) FROM Products WHERE ProductCode = @ProductCode";
            var parameters = new List<SqlParameter> { DatabaseHelper.CreateParameter("@ProductCode", productCode) };

            if (excludeId.HasValue)
            {
                query += " AND ProductId != @ExcludeId";
                parameters.Add(DatabaseHelper.CreateParameter("@ExcludeId", excludeId.Value));
            }

            var result = DatabaseHelper.ExecuteScalar(query, parameters.ToArray());
            return Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// Map DataTable sang List<Product>
        /// </summary>
        private List<Product> MapDataTableToList(DataTable dt)
        {
            var list = new List<Product>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Product
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    ProductCode = row["ProductCode"].ToString() ?? "",
                    Name = row["Name"].ToString() ?? "",
                    StoneTypeId = Convert.ToInt32(row["StoneTypeId"]),
                    StoneTypeName = DatabaseHelper.GetString(row, "StoneTypeName"),
                    Carat = Convert.ToDecimal(row["Carat"]),
                    Color = DatabaseHelper.GetString(row, "Color"),
                    Clarity = DatabaseHelper.GetString(row, "Clarity"),
                    Cut = DatabaseHelper.GetString(row, "Cut"),
                    CostPrice = Convert.ToDecimal(row["CostPrice"]),
                    SellPrice = Convert.ToDecimal(row["SellPrice"]),
                    StockQty = Convert.ToInt32(row["StockQty"]),
                    Status = row["Status"].ToString() ?? "Còn hàng",
                    ImagePath = DatabaseHelper.GetString(row, "ImagePath"),
                    CertId = DatabaseHelper.GetValue<int>(row, "CertId") ?? 0,
                    CertCode = DatabaseHelper.GetString(row, "CertCode"),
                    DisplayLocation = DatabaseHelper.GetString(row, "DisplayLocation"),
                    CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                    UpdatedAt = Convert.ToDateTime(row["UpdatedAt"])
                });
            }
            return list;
        }
    }
}
