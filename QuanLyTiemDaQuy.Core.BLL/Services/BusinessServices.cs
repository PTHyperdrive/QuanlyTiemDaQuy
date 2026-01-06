using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.BLL.Services;

/// <summary>
/// Service cho quản lý sản phẩm
/// </summary>
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IStoneTypeRepository _stoneTypeRepository;
    private readonly ICertificateRepository _certificateRepository;

    public ProductService(
        IProductRepository productRepository, 
        IStoneTypeRepository stoneTypeRepository,
        ICertificateRepository certificateRepository)
    {
        _productRepository = productRepository;
        _stoneTypeRepository = stoneTypeRepository;
        _certificateRepository = certificateRepository;
    }

    public List<Product> GetAllProducts() => _productRepository.GetAll();

    public Product? GetProductById(int productId) => _productRepository.GetById(productId);

    public Product? GetProductByCode(string productCode) => _productRepository.GetByCode(productCode);

    public List<Product> SearchProducts(string keyword) => _productRepository.Search(keyword);

    public List<Product> GetLowStockProducts() => _productRepository.GetLowStock();

    public string GenerateProductCode(int stoneTypeId)
    {
        var stoneType = _stoneTypeRepository.GetById(stoneTypeId);
        string prefix = GetStoneTypePrefix(stoneType?.Name ?? "");
        return _productRepository.GenerateNextCode(prefix);
    }

    public (bool Success, string Message, int ProductId) AddProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            return (false, "Tên sản phẩm không được để trống", 0);

        if (product.StoneTypeId <= 0)
            return (false, "Vui lòng chọn loại đá", 0);

        if (product.Carat <= 0)
            return (false, "Trọng lượng carat phải lớn hơn 0", 0);

        if (product.SellPrice <= 0)
            return (false, "Giá bán phải lớn hơn 0", 0);

        if (string.IsNullOrWhiteSpace(product.ProductCode))
            product.ProductCode = GenerateProductCode(product.StoneTypeId);

        int productId = _productRepository.Add(product);
        return productId > 0 
            ? (true, "Thêm sản phẩm thành công", productId) 
            : (false, "Lỗi khi thêm sản phẩm", 0);
    }

    public (bool Success, string Message) UpdateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            return (false, "Tên sản phẩm không được để trống");

        if (product.SellPrice <= 0)
            return (false, "Giá bán phải lớn hơn 0");

        bool success = _productRepository.Update(product);
        return success ? (true, "Cập nhật thành công") : (false, "Lỗi khi cập nhật");
    }

    public (bool Success, string Message) DeleteProduct(int productId)
    {
        var product = _productRepository.GetById(productId);
        if (product == null)
            return (false, "Không tìm thấy sản phẩm");

        // Check if product has been sold (would need invoice check - simplified here)
        bool success = _productRepository.Delete(productId);
        return success ? (true, "Xóa thành công") : (false, "Lỗi khi xóa sản phẩm");
    }

    public List<StoneType> GetAllStoneTypes() => _stoneTypeRepository.GetAll();

    public (bool Success, string Message, int StoneTypeId) AddStoneType(StoneType stoneType)
    {
        if (string.IsNullOrWhiteSpace(stoneType.Name))
            return (false, "Tên loại đá không được để trống", 0);

        int stoneTypeId = _stoneTypeRepository.Add(stoneType);
        return stoneTypeId > 0 
            ? (true, "Thêm loại đá thành công", stoneTypeId) 
            : (false, "Lỗi khi thêm loại đá", 0);
    }

    public List<Certificate> GetAllCertificates() => _certificateRepository.GetAll();

    public (bool Success, string Message, int CertId) AddCertificate(Certificate certificate)
    {
        if (string.IsNullOrWhiteSpace(certificate.CertCode))
            return (false, "Mã chứng nhận không được để trống", 0);

        int certId = _certificateRepository.Add(certificate);
        return certId > 0 
            ? (true, "Thêm chứng nhận thành công", certId) 
            : (false, "Lỗi khi thêm chứng nhận", 0);
    }

    public (bool Success, string Message) CheckStock(int productId, int requestedQty)
    {
        var product = _productRepository.GetById(productId);
        if (product == null)
            return (false, "Không tìm thấy sản phẩm");

        if (product.StockQty < requestedQty)
            return (false, $"Không đủ hàng. Tồn kho: {product.StockQty}");

        return (true, "Đủ hàng");
    }

    public (bool Success, string Message) AdjustStock(int productId, int adjustment, string reason)
    {
        var product = _productRepository.GetById(productId);
        if (product == null)
            return (false, "Không tìm thấy sản phẩm");

        int newQty = product.StockQty + adjustment;
        if (newQty < 0)
            return (false, "Số lượng tồn kho không thể âm");

        bool success = _productRepository.UpdateStock(productId, newQty);
        return success 
            ? (true, $"Đã điều chỉnh tồn kho. Số lượng mới: {newQty}") 
            : (false, "Lỗi khi điều chỉnh tồn kho");
    }

    private static string GetStoneTypePrefix(string stoneTypeName)
    {
        if (string.IsNullOrEmpty(stoneTypeName))
            return "SP";

        string name = stoneTypeName.Trim().ToLower();
        
        if (name.Contains("kim cương") || name.Contains("diamond")) return "KC";
        if (name.Contains("ruby") || name.Contains("hồng ngọc")) return "RB";
        if (name.Contains("sapphire") || name.Contains("bích ngọc")) return "SP";
        if (name.Contains("emerald") || name.Contains("ngọc lục bảo")) return "EM";
        if (name.Contains("opal") || name.Contains("mắt mèo")) return "OP";
        if (name.Contains("pearl") || name.Contains("ngọc trai")) return "PR";
        if (name.Contains("topaz")) return "TP";
        if (name.Contains("amethyst") || name.Contains("thạch anh tím")) return "AM";
        if (name.Contains("aquamarine")) return "AQ";
        if (name.Contains("jade") || name.Contains("ngọc bích") || name.Contains("cẩm thạch")) return "JD";
        if (name.Contains("tanzanite")) return "TZ";
        if (name.Contains("tourmaline")) return "TM";
        if (name.Contains("garnet")) return "GR";
        if (name.Contains("peridot")) return "PD";
        if (name.Contains("citrine")) return "CT";
        if (name.Contains("alexandrite")) return "AL";
        
        if (stoneTypeName.Length >= 2)
            return stoneTypeName[..2].ToUpper();
        
        return "XX";
    }
}

/// <summary>
/// Service cho quản lý bán hàng
/// </summary>
public class SalesService : ISalesService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;

    public SalesService(
        IInvoiceRepository invoiceRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository)
    {
        _invoiceRepository = invoiceRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public List<Invoice> GetAllInvoices() => _invoiceRepository.GetAll();

    public Invoice? GetInvoiceById(int invoiceId) => _invoiceRepository.GetById(invoiceId);

    public List<Invoice> GetInvoicesByDateRange(DateTime fromDate, DateTime toDate)
        => _invoiceRepository.GetByDateRange(fromDate, toDate);

    public List<Invoice> GetInvoicesByCustomer(int customerId)
        => _invoiceRepository.GetByCustomerId(customerId);

    public string GenerateInvoiceCode() => _invoiceRepository.GenerateNextCode();

    public (bool Success, string Message, int InvoiceId) CreateInvoice(Invoice invoice)
    {
        if (invoice.Details.Count == 0)
            return (false, "Hóa đơn phải có ít nhất 1 sản phẩm", 0);

        // Validate and check stock
        foreach (var detail in invoice.Details)
        {
            var product = _productRepository.GetById(detail.ProductId);
            if (product == null)
                return (false, $"Không tìm thấy sản phẩm ID {detail.ProductId}", 0);

            if (product.StockQty < detail.Qty)
                return (false, $"Sản phẩm {product.Name} không đủ hàng. Tồn kho: {product.StockQty}", 0);
        }

        // Generate code if not set
        if (string.IsNullOrWhiteSpace(invoice.InvoiceCode))
            invoice.InvoiceCode = GenerateInvoiceCode();

        // Calculate totals
        invoice.CalculateTotals();

        int invoiceId = _invoiceRepository.Add(invoice);
        
        if (invoiceId > 0)
        {
            // Update stock for each product
            foreach (var detail in invoice.Details)
            {
                var product = _productRepository.GetById(detail.ProductId);
                if (product != null)
                {
                    _productRepository.UpdateStock(detail.ProductId, product.StockQty - detail.Qty);
                }
            }

            return (true, "Tạo hóa đơn thành công", invoiceId);
        }

        return (false, "Lỗi khi tạo hóa đơn", 0);
    }

    public (bool Success, string Message) CompleteInvoice(int invoiceId)
    {
        var invoice = _invoiceRepository.GetById(invoiceId);
        if (invoice == null)
            return (false, "Không tìm thấy hóa đơn");

        if (!invoice.IsPending)
            return (false, "Chỉ có thể hoàn thành hóa đơn đang chờ");

        bool success = _invoiceRepository.UpdateStatus(invoiceId, InvoiceStatus.Completed);
        
        if (success && invoice.CustomerId > 0)
        {
            // Update customer total purchase
            _customerRepository.UpdateTotalPurchase(invoice.CustomerId, invoice.Total);
        }

        return success ? (true, "Đã hoàn thành hóa đơn") : (false, "Lỗi khi cập nhật");
    }

    public (bool Success, string Message) CancelInvoice(int invoiceId, string reason)
    {
        var invoice = _invoiceRepository.GetById(invoiceId);
        if (invoice == null)
            return (false, "Không tìm thấy hóa đơn");

        if (!invoice.CanCancel)
            return (false, "Không thể hủy hóa đơn này");

        if (string.IsNullOrWhiteSpace(reason))
            return (false, "Vui lòng nhập lý do hủy");

        // Restore stock
        foreach (var detail in invoice.Details)
        {
            var product = _productRepository.GetById(detail.ProductId);
            if (product != null)
            {
                _productRepository.UpdateStock(detail.ProductId, product.StockQty + detail.Qty);
            }
        }

        bool success = _invoiceRepository.Cancel(invoiceId, reason);
        return success ? (true, "Đã hủy hóa đơn") : (false, "Lỗi khi hủy hóa đơn");
    }
}

/// <summary>
/// Service cho quản lý nhập hàng
/// </summary>
public class ImportService : IImportService
{
    private readonly IImportRepository _importRepository;
    private readonly IProductRepository _productRepository;

    public ImportService(IImportRepository importRepository, IProductRepository productRepository)
    {
        _importRepository = importRepository;
        _productRepository = productRepository;
    }

    public List<ImportReceipt> GetAllImports() => _importRepository.GetAll();

    public ImportReceipt? GetImportById(int importId) => _importRepository.GetById(importId);

    public List<ImportReceipt> GetImportsByDateRange(DateTime fromDate, DateTime toDate)
        => _importRepository.GetByDateRange(fromDate, toDate);

    public string GenerateImportCode() => _importRepository.GenerateNextCode();

    public (bool Success, string Message, int ImportId) CreateImport(ImportReceipt importReceipt)
    {
        if (importReceipt.Details.Count == 0)
            return (false, "Phiếu nhập phải có ít nhất 1 sản phẩm", 0);

        if (importReceipt.SupplierId <= 0)
            return (false, "Vui lòng chọn nhà cung cấp", 0);

        // Generate code if not set
        if (string.IsNullOrWhiteSpace(importReceipt.ImportCode))
            importReceipt.ImportCode = GenerateImportCode();

        // Calculate total
        importReceipt.CalculateTotal();

        int importId = _importRepository.Add(importReceipt);
        
        if (importId > 0)
        {
            // Update stock for each product
            foreach (var detail in importReceipt.Details)
            {
                var product = _productRepository.GetById(detail.ProductId);
                if (product != null)
                {
                    _productRepository.UpdateStock(detail.ProductId, product.StockQty + detail.Qty);
                }
            }

            return (true, "Tạo phiếu nhập thành công", importId);
        }

        return (false, "Lỗi khi tạo phiếu nhập", 0);
    }
}
