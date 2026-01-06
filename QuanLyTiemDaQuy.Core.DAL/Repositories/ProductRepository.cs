using System.Data;
using Microsoft.Data.SqlClient;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.DAL.Repositories;

/// <summary>
/// Repository cho quản lý sản phẩm
/// </summary>
public class ProductRepository : IProductRepository
{
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

    public List<Product> Search(string keyword)
    {
        string query = @"
            SELECT p.*, st.Name AS StoneTypeName, c.CertCode
            FROM Products p
            LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
            LEFT JOIN Certificates c ON p.CertId = c.CertId
            WHERE p.ProductCode LIKE @Keyword OR p.Name LIKE @Keyword
            ORDER BY p.ProductCode";
        
        var dt = DatabaseHelper.ExecuteQuery(query,
            DatabaseHelper.CreateParameter("@Keyword", $"%{keyword}%"));
        return MapDataTableToList(dt);
    }

    public List<Product> GetLowStock()
    {
        string query = @"
            SELECT p.*, st.Name AS StoneTypeName, c.CertCode
            FROM Products p
            LEFT JOIN StoneTypes st ON p.StoneTypeId = st.StoneTypeId
            LEFT JOIN Certificates c ON p.CertId = c.CertId
            WHERE p.StockQty > 0 AND p.StockQty <= 5
            ORDER BY p.StockQty";
        
        var dt = DatabaseHelper.ExecuteQuery(query);
        return MapDataTableToList(dt);
    }

    public int Add(Product product)
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

    public bool Delete(int productId)
    {
        string query = "DELETE FROM Products WHERE ProductId = @ProductId";
        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@ProductId", productId));
        return affected > 0;
    }

    public bool UpdateStock(int productId, int qty)
    {
        string query = @"
            UPDATE Products SET 
                StockQty = @StockQty,
                Status = CASE WHEN @StockQty <= 0 THEN N'Hết hàng' ELSE Status END,
                UpdatedAt = GETDATE()
            WHERE ProductId = @ProductId";

        int affected = DatabaseHelper.ExecuteNonQuery(query,
            DatabaseHelper.CreateParameter("@ProductId", productId),
            DatabaseHelper.CreateParameter("@StockQty", qty));

        return affected > 0;
    }

    public string GenerateNextCode(string prefix)
    {
        string query = @"
            SELECT MAX(ProductCode) FROM Products 
            WHERE ProductCode LIKE @Pattern";
        
        var result = DatabaseHelper.ExecuteScalar(query,
            DatabaseHelper.CreateParameter("@Pattern", prefix + "-%"));
        
        int nextNumber = 1;
        
        if (result != null && result != DBNull.Value)
        {
            string? lastCode = result.ToString();
            if (lastCode != null)
            {
                int dashIndex = lastCode.LastIndexOf('-');
                if (dashIndex >= 0 && dashIndex < lastCode.Length - 1)
                {
                    string numPart = lastCode[(dashIndex + 1)..];
                    if (int.TryParse(numPart, out int lastNum))
                    {
                        nextNumber = lastNum + 1;
                    }
                }
            }
        }
        
        return $"{prefix}-{nextNumber:D3}";
    }

    private static List<Product> MapDataTableToList(DataTable dt)
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
