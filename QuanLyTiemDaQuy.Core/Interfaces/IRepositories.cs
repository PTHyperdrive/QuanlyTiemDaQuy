using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.Interfaces;

/// <summary>
/// Interface cho Product Repository
/// </summary>
public interface IProductRepository
{
    List<Product> GetAll();
    Product? GetById(int productId);
    Product? GetByCode(string productCode);
    List<Product> Search(string keyword);
    List<Product> GetLowStock();
    int Add(Product product);
    bool Update(Product product);
    bool Delete(int productId);
    bool UpdateStock(int productId, int qty);
    string GenerateNextCode(string prefix);
}

/// <summary>
/// Interface cho Customer Repository
/// </summary>
public interface ICustomerRepository
{
    List<Customer> GetAll();
    Customer? GetById(int customerId);
    List<Customer> Search(string keyword);
    int Add(Customer customer);
    bool Update(Customer customer);
    bool Delete(int customerId);
    bool UpdateTotalPurchase(int customerId, decimal amount);
}

/// <summary>
/// Interface cho Employee Repository
/// </summary>
public interface IEmployeeRepository
{
    List<Employee> GetAll();
    Employee? GetById(int employeeId);
    Employee? GetByUsername(string username);
    int Add(Employee employee);
    bool Update(Employee employee);
    bool Delete(int employeeId);
    bool UpdatePassword(int employeeId, string passwordHash);
    bool SetMustChangePassword(int employeeId, bool value);
}

/// <summary>
/// Interface cho Invoice Repository
/// </summary>
public interface IInvoiceRepository
{
    List<Invoice> GetAll();
    Invoice? GetById(int invoiceId);
    Invoice? GetByCode(string invoiceCode);
    List<Invoice> GetByDateRange(DateTime fromDate, DateTime toDate);
    List<Invoice> GetByCustomerId(int customerId);
    int Add(Invoice invoice);
    bool UpdateStatus(int invoiceId, string status);
    bool Cancel(int invoiceId, string reason);
    string GenerateNextCode();
}

/// <summary>
/// Interface cho Branch Repository
/// </summary>
public interface IBranchRepository
{
    List<Branch> GetAll();
    Branch? GetById(int branchId);
    int Add(Branch branch);
    bool Update(Branch branch);
    bool Delete(int branchId);
}

/// <summary>
/// Interface cho Supplier Repository
/// </summary>
public interface ISupplierRepository
{
    List<Supplier> GetAll();
    Supplier? GetById(int supplierId);
    List<Supplier> Search(string keyword);
    int Add(Supplier supplier);
    bool Update(Supplier supplier);
    bool Delete(int supplierId);
}

/// <summary>
/// Interface cho StoneType Repository
/// </summary>
public interface IStoneTypeRepository
{
    List<StoneType> GetAll();
    StoneType? GetById(int stoneTypeId);
    int Add(StoneType stoneType);
    bool Update(StoneType stoneType);
}

/// <summary>
/// Interface cho Certificate Repository
/// </summary>
public interface ICertificateRepository
{
    List<Certificate> GetAll();
    Certificate? GetById(int certId);
    int Add(Certificate certificate);
}

/// <summary>
/// Interface cho Import Repository
/// </summary>
public interface IImportRepository
{
    List<ImportReceipt> GetAll();
    ImportReceipt? GetById(int importId);
    List<ImportReceipt> GetByDateRange(DateTime fromDate, DateTime toDate);
    int Add(ImportReceipt importReceipt);
    string GenerateNextCode();
}

/// <summary>
/// Interface cho MarketPrice Repository
/// </summary>
public interface IMarketPriceRepository
{
    List<GemstoneMarketPrice> GetAll();
    GemstoneMarketPrice? GetByStoneType(int stoneTypeId);
    bool UpdatePrice(int stoneTypeId, decimal pricePerCarat);
    List<MarketPriceHistory> GetPriceHistory(int stoneTypeId, int limit = 30);
    bool SavePriceHistory(MarketPriceHistory history);
    ExchangeRate? GetLatestExchangeRate();
    bool SaveExchangeRate(ExchangeRate rate);
}
