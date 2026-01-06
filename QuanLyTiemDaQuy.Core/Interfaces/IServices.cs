using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Core.Interfaces;

/// <summary>
/// Interface cho Product Service
/// </summary>
public interface IProductService
{
    List<Product> GetAllProducts();
    Product? GetProductById(int productId);
    Product? GetProductByCode(string productCode);
    List<Product> SearchProducts(string keyword);
    List<Product> GetLowStockProducts();
    string GenerateProductCode(int stoneTypeId);
    (bool Success, string Message, int ProductId) AddProduct(Product product);
    (bool Success, string Message) UpdateProduct(Product product);
    (bool Success, string Message) DeleteProduct(int productId);
    List<StoneType> GetAllStoneTypes();
    (bool Success, string Message, int StoneTypeId) AddStoneType(StoneType stoneType);
    List<Certificate> GetAllCertificates();
    (bool Success, string Message, int CertId) AddCertificate(Certificate certificate);
    (bool Success, string Message) CheckStock(int productId, int requestedQty);
    (bool Success, string Message) AdjustStock(int productId, int adjustment, string reason);
}

/// <summary>
/// Interface cho Customer Service
/// </summary>
public interface ICustomerService
{
    List<Customer> GetAllCustomers();
    Customer? GetCustomerById(int customerId);
    List<Customer> SearchCustomers(string keyword);
    (bool Success, string Message, int CustomerId) AddCustomer(Customer customer);
    (bool Success, string Message) UpdateCustomer(Customer customer);
    (bool Success, string Message) DeleteCustomer(int customerId);
    (bool TierChanged, string OldTier, string NewTier) UpdateTotalPurchaseAndCheckTier(int customerId, decimal purchaseAmount);
}

/// <summary>
/// Interface cho Employee Service
/// </summary>
public interface IEmployeeService
{
    Employee? CurrentEmployee { get; set; }
    List<Employee> GetAllEmployees();
    Employee? GetEmployeeById(int employeeId);
    (bool Success, string Message, Employee? Employee) Login(string username, string password);
    (bool Success, string Message) ChangePassword(int employeeId, string currentPassword, string newPassword);
    (bool Success, string Message) SetPassword(int employeeId, string newPassword);
    (bool Success, string Message, int EmployeeId) AddEmployee(Employee employee);
    (bool Success, string Message) UpdateEmployee(Employee employee);
    (bool Success, string Message) DeleteEmployee(int employeeId);
    (bool Success, string Message) ToggleActiveStatus(int employeeId);
}

/// <summary>
/// Interface cho Sales Service
/// </summary>
public interface ISalesService
{
    List<Invoice> GetAllInvoices();
    Invoice? GetInvoiceById(int invoiceId);
    List<Invoice> GetInvoicesByDateRange(DateTime fromDate, DateTime toDate);
    List<Invoice> GetInvoicesByCustomer(int customerId);
    string GenerateInvoiceCode();
    (bool Success, string Message, int InvoiceId) CreateInvoice(Invoice invoice);
    (bool Success, string Message) CompleteInvoice(int invoiceId);
    (bool Success, string Message) CancelInvoice(int invoiceId, string reason);
}

/// <summary>
/// Interface cho Import Service
/// </summary>
public interface IImportService
{
    List<ImportReceipt> GetAllImports();
    ImportReceipt? GetImportById(int importId);
    List<ImportReceipt> GetImportsByDateRange(DateTime fromDate, DateTime toDate);
    string GenerateImportCode();
    (bool Success, string Message, int ImportId) CreateImport(ImportReceipt importReceipt);
}

/// <summary>
/// Interface cho Report Service
/// </summary>
public interface IReportService
{
    DashboardStats GetDashboardStats();
    List<DailySalesReport> GetDailySalesReport(DateTime fromDate, DateTime toDate);
    List<MonthlySalesReport> GetMonthlySalesReport(int year);
    List<TopProductReport> GetTopProducts(DateTime fromDate, DateTime toDate, int limit = 10);
    List<InventoryReport> GetInventoryReport();
}

/// <summary>
/// Interface cho Branch Service
/// </summary>
public interface IBranchService
{
    List<Branch> GetAllBranches();
    Branch? GetBranchById(int branchId);
    (bool Success, string Message, int BranchId) AddBranch(Branch branch);
    (bool Success, string Message) UpdateBranch(Branch branch);
    (bool Success, string Message) DeleteBranch(int branchId);
}

/// <summary>
/// Interface cho Supplier Service
/// </summary>
public interface ISupplierService
{
    List<Supplier> GetAllSuppliers();
    Supplier? GetSupplierById(int supplierId);
    List<Supplier> SearchSuppliers(string keyword);
    (bool Success, string Message, int SupplierId) AddSupplier(Supplier supplier);
    (bool Success, string Message) UpdateSupplier(Supplier supplier);
    (bool Success, string Message) DeleteSupplier(int supplierId);
}

/// <summary>
/// Interface cho Pricing Service
/// </summary>
public interface IPricingService
{
    List<GemstoneMarketPrice> GetAllMarketPrices();
    PurchasePriceResult CalculatePurchasePrice(int stoneTypeId, decimal carat, string color, string clarity, string cut);
    decimal CalculateSellPrice(decimal purchasePrice, decimal profitMargin = 30);
}

/// <summary>
/// Interface cho Market Price API Service
/// </summary>
public interface IMarketPriceApiService
{
    Task<MarketPriceApiResult> FetchMarketPricesAsync();
    Task<decimal> GetExchangeRateUsdVndAsync();
}
