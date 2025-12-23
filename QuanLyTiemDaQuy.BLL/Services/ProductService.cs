using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho quản lý sản phẩm với các business rules
    /// </summary>
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly StoneTypeRepository _stoneTypeRepository;
        private readonly CertificateRepository _certificateRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
            _stoneTypeRepository = new StoneTypeRepository();
            _certificateRepository = new CertificateRepository();
        }

        #region Product CRUD

        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product? GetProductById(int productId)
        {
            return _productRepository.GetById(productId);
        }

        public Product? GetProductByCode(string productCode)
        {
            return _productRepository.GetByCode(productCode);
        }

        public List<Product> SearchProducts(string? keyword = null, int? stoneTypeId = null,
            decimal? minCarat = null, decimal? maxCarat = null,
            decimal? minPrice = null, decimal? maxPrice = null,
            string? status = null)
        {
            return _productRepository.Search(keyword, stoneTypeId, minCarat, maxCarat, minPrice, maxPrice, status);
        }

        public List<Product> GetLowStockProducts(int threshold = 5)
        {
            return _productRepository.GetLowStock(threshold);
        }

        /// <summary>
        /// Sinh mã sản phẩm tự động theo loại đá
        /// Format: [PREFIX]-[XXX] (ví dụ: KC-001 cho Kim cương)
        /// </summary>
        public string GenerateProductCode(string stoneTypeName)
        {
            return _productRepository.GetNextProductCode(stoneTypeName);
        }

        /// <summary>
        /// Thêm sản phẩm mới với validation
        /// </summary>
        public (bool Success, string Message, int ProductId) AddProduct(Product product)
        {
            // Validate mã sản phẩm
            if (string.IsNullOrWhiteSpace(product.ProductCode))
                return (false, "Mã sản phẩm không được để trống", 0);

            if (_productRepository.IsCodeExists(product.ProductCode))
                return (false, "Mã sản phẩm đã tồn tại", 0);

            // Validate tên sản phẩm
            if (string.IsNullOrWhiteSpace(product.Name))
                return (false, "Tên sản phẩm không được để trống", 0);

            // Validate giá
            if (product.CostPrice < 0)
                return (false, "Giá vốn không được âm", 0);

            if (product.SellPrice < 0)
                return (false, "Giá bán không được âm", 0);

            // Cảnh báo nếu giá bán < giá vốn (nhưng vẫn cho phép)
            string warningMessage = "";
            if (product.SellPrice < product.CostPrice)
                warningMessage = " (Cảnh báo: Giá bán thấp hơn giá vốn)";

            // Validate carat
            if (product.Carat <= 0)
                return (false, "Carat phải lớn hơn 0", 0);

            try
            {
                int productId = _productRepository.Insert(product);
                return (true, $"Thêm sản phẩm thành công{warningMessage}", productId);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi thêm sản phẩm: {ex.Message}", 0);
            }
        }

        /// <summary>
        /// Cập nhật sản phẩm với validation
        /// </summary>
        public (bool Success, string Message) UpdateProduct(Product product)
        {
            // Validate mã sản phẩm
            if (string.IsNullOrWhiteSpace(product.ProductCode))
                return (false, "Mã sản phẩm không được để trống");

            if (_productRepository.IsCodeExists(product.ProductCode, product.ProductId))
                return (false, "Mã sản phẩm đã tồn tại");

            // Validate tên sản phẩm
            if (string.IsNullOrWhiteSpace(product.Name))
                return (false, "Tên sản phẩm không được để trống");

            // Validate giá
            if (product.CostPrice < 0)
                return (false, "Giá vốn không được âm");

            if (product.SellPrice < 0)
                return (false, "Giá bán không được âm");

            string warningMessage = "";
            if (product.SellPrice < product.CostPrice)
                warningMessage = " (Cảnh báo: Giá bán thấp hơn giá vốn)";

            try
            {
                bool success = _productRepository.Update(product);
                if (success)
                    return (true, $"Cập nhật sản phẩm thành công{warningMessage}");
                else
                    return (false, "Không tìm thấy sản phẩm để cập nhật");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi cập nhật sản phẩm: {ex.Message}");
            }
        }

        /// <summary>
        /// Xóa sản phẩm
        /// </summary>
        public (bool Success, string Message) DeleteProduct(int productId)
        {
            try
            {
                bool success = _productRepository.Delete(productId);
                if (success)
                    return (true, "Xóa sản phẩm thành công");
                else
                    return (false, "Không tìm thấy sản phẩm để xóa");
            }
            catch (Exception ex)
            {
                // Có thể fail do FK constraint
                if (ex.Message.Contains("REFERENCE"))
                    return (false, "Không thể xóa sản phẩm đã có phiếu nhập hoặc hóa đơn");
                return (false, $"Lỗi khi xóa sản phẩm: {ex.Message}");
            }
        }

        #endregion

        #region Stone Types

        public List<StoneType> GetAllStoneTypes()
        {
            return _stoneTypeRepository.GetAll();
        }

        public (bool Success, string Message, int StoneTypeId) AddStoneType(StoneType stoneType)
        {
            if (string.IsNullOrWhiteSpace(stoneType.Name))
                return (false, "Tên loại đá không được để trống", 0);

            try
            {
                int id = _stoneTypeRepository.Insert(stoneType);
                return (true, "Thêm loại đá thành công", id);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        #endregion

        #region Certificates

        public List<Certificate> GetAllCertificates()
        {
            return _certificateRepository.GetAll();
        }

        public (bool Success, string Message, int CertId) AddCertificate(Certificate cert)
        {
            if (string.IsNullOrWhiteSpace(cert.CertCode))
                return (false, "Mã chứng nhận không được để trống", 0);

            if (_certificateRepository.IsCodeExists(cert.CertCode))
                return (false, "Mã chứng nhận đã tồn tại", 0);

            try
            {
                int id = _certificateRepository.Insert(cert);
                return (true, "Thêm chứng nhận thành công", id);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}", 0);
            }
        }

        #endregion

        #region Inventory

        /// <summary>
        /// Kiểm tra tồn kho có đủ để bán không
        /// </summary>
        public (bool Available, int CurrentStock) CheckStock(int productId, int requiredQty)
        {
            var product = _productRepository.GetById(productId);
            if (product == null)
                return (false, 0);

            return (product.StockQty >= requiredQty, product.StockQty);
        }

        /// <summary>
        /// Cập nhật tồn kho thủ công (điều chỉnh kho)
        /// </summary>
        public (bool Success, string Message) AdjustStock(int productId, int newQty, string reason)
        {
            if (newQty < 0)
                return (false, "Số lượng tồn kho không được âm");

            try
            {
                bool success = _productRepository.UpdateStock(productId, newQty);
                if (success)
                    return (true, $"Điều chỉnh tồn kho thành công. Lý do: {reason}");
                else
                    return (false, "Không tìm thấy sản phẩm");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi: {ex.Message}");
            }
        }

        #endregion
    }
}
