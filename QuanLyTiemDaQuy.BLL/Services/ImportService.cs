using System;
using System.Collections.Generic;
using QuanLyTiemDaQuy.DAL.Repositories;
using QuanLyTiemDaQuy.Models;

namespace QuanLyTiemDaQuy.BLL.Services
{
    /// <summary>
    /// Service cho quản lý nhập hàng
    /// </summary>
    public class ImportService
    {
        private readonly ImportRepository _importRepository;
        private readonly ProductRepository _productRepository;
        private readonly SupplierRepository _supplierRepository;

        public ImportService()
        {
            _importRepository = new ImportRepository();
            _productRepository = new ProductRepository();
            _supplierRepository = new SupplierRepository();
        }

        #region Import Receipt Operations

        public List<ImportReceipt> GetAllImports()
        {
            return _importRepository.GetAll();
        }

        public ImportReceipt? GetImportById(int importId)
        {
            return _importRepository.GetById(importId);
        }

        public List<ImportReceipt> GetImportsByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _importRepository.GetByDateRange(fromDate, toDate);
        }

        public string GenerateImportCode()
        {
            return _importRepository.GenerateImportCode();
        }

        #endregion

        #region Create Import Receipt

        /// <summary>
        /// Tạo phiếu nhập hàng
        /// Trigger trong DB sẽ tự động cập nhật tồn kho
        /// </summary>
        public (bool Success, string Message, int ImportId) CreateImportReceipt(ImportReceipt receipt)
        {
            // Validate
            if (receipt.SupplierId <= 0)
                return (false, "Vui lòng chọn nhà cung cấp", 0);

            if (receipt.Details == null || receipt.Details.Count == 0)
                return (false, "Phiếu nhập phải có ít nhất 1 sản phẩm", 0);

            if (receipt.EmployeeId <= 0)
                return (false, "Không xác định được nhân viên tạo phiếu", 0);

            // Validate each detail and calculate totals
            decimal totalCost = 0;
            foreach (var detail in receipt.Details)
            {
                var product = _productRepository.GetById(detail.ProductId);
                if (product == null)
                    return (false, $"Sản phẩm ID {detail.ProductId} không tồn tại", 0);

                if (detail.Qty <= 0)
                    return (false, $"Số lượng nhập của {product.Name} phải lớn hơn 0", 0);

                if (detail.UnitCost < 0)
                    return (false, $"Giá nhập của {product.Name} không hợp lệ", 0);

                detail.ProductCode = product.ProductCode;
                detail.ProductName = product.Name;
                detail.CalculateLineTotal();
                totalCost += detail.LineTotal;
            }

            receipt.TotalCost = totalCost;
            receipt.ImportDate = DateTime.Now;

            // Generate code
            if (string.IsNullOrEmpty(receipt.ImportCode))
            {
                receipt.ImportCode = GenerateImportCode();
            }

            try
            {
                int importId = _importRepository.Insert(receipt);

                // Optionally update cost price if import price is different
                foreach (var detail in receipt.Details)
                {
                    var product = _productRepository.GetById(detail.ProductId);
                    if (product != null && detail.UnitCost != product.CostPrice)
                    {
                        // Could update cost price here if needed
                        // For now, we keep the original cost price
                    }
                }

                return (true, $"Tạo phiếu nhập {receipt.ImportCode} thành công. Tổng tiền: {receipt.TotalCost:N0} VNĐ", importId);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi khi tạo phiếu nhập: {ex.Message}", 0);
            }
        }

        /// <summary>
        /// Thêm sản phẩm vào phiếu nhập
        /// </summary>
        public (bool Success, string Message, ImportDetail? Detail) AddToImport(string productCode, int qty, decimal unitCost)
        {
            if (string.IsNullOrWhiteSpace(productCode))
                return (false, "Vui lòng nhập mã sản phẩm", null);

            if (qty <= 0)
                return (false, "Số lượng phải lớn hơn 0", null);

            if (unitCost < 0)
                return (false, "Giá nhập không hợp lệ", null);

            var product = _productRepository.GetByCode(productCode);
            if (product == null)
                return (false, $"Không tìm thấy sản phẩm với mã '{productCode}'", null);

            var detail = new ImportDetail
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ProductName = product.Name,
                Qty = qty,
                UnitCost = unitCost,
                LineTotal = qty * unitCost
            };

            return (true, $"Đã thêm {product.Name} x {qty}", detail);
        }

        /// <summary>
        /// Cập nhật giá vốn sản phẩm sau khi nhập
        /// </summary>
        public (bool Success, string Message) UpdateCostPriceFromImport(int productId, decimal newCostPrice)
        {
            var product = _productRepository.GetById(productId);
            if (product == null)
                return (false, "Không tìm thấy sản phẩm");

            product.CostPrice = newCostPrice;
            bool success = _productRepository.Update(product);

            if (success)
                return (true, $"Đã cập nhật giá vốn {product.Name} thành {newCostPrice:N0} VNĐ");
            else
                return (false, "Không thể cập nhật giá vốn");
        }

        #endregion

        #region Statistics

        /// <summary>
        /// Lấy tổng chi phí nhập hàng trong khoảng thời gian
        /// </summary>
        public decimal GetTotalImportCost(DateTime fromDate, DateTime toDate)
        {
            var imports = GetImportsByDateRange(fromDate, toDate);
            
            decimal total = 0;
            foreach (var imp in imports)
            {
                total += imp.TotalCost;
            }
            return total;
        }

        #endregion
    }
}
