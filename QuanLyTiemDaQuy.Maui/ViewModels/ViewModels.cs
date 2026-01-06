using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;

namespace QuanLyTiemDaQuy.Maui.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IEmployeeService _employeeService;
    private readonly IBranchService _branchService;
    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isLoading;
    private bool _hasError;
    private List<Branch> _branches = [];
    private int _selectedBranchIndex = 0;

    public string Username { get => _username; set => SetProperty(ref _username, value); }
    public string Password { get => _password; set => SetProperty(ref _password, value); }
    public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
    public bool HasError { get => _hasError; set => SetProperty(ref _hasError, value); }
    public List<Branch> Branches { get => _branches; set => SetProperty(ref _branches, value); }
    public int SelectedBranchIndex { get => _selectedBranchIndex; set => SetProperty(ref _selectedBranchIndex, value); }
    
    // Computed property to get selected branch
    public Branch? SelectedBranch => Branches.Count > 0 && SelectedBranchIndex >= 0 && SelectedBranchIndex < Branches.Count 
        ? Branches[SelectedBranchIndex] 
        : null;

    public LoginViewModel(IEmployeeService employeeService, IBranchService branchService)
    {
        _employeeService = employeeService;
        _branchService = branchService;
        LoadBranches();
    }

    private void LoadBranches()
    {
        try
        {
            Branches = _branchService.GetAllBranches();
            if (Branches.Count > 0)
                SelectedBranchIndex = 0;
        }
        catch
        {
            // Fallback if database not available
            Branches = [new Branch { BranchId = 1, Name = "Chi nhánh chính" }];
            SelectedBranchIndex = 0;
        }
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Vui lòng nhập tài khoản và mật khẩu";
            HasError = true;
            return;
        }

        if (SelectedBranch == null)
        {
            ErrorMessage = "Vui lòng chọn chi nhánh";
            HasError = true;
            return;
        }

        IsLoading = true;
        HasError = false;
        ErrorMessage = string.Empty;

        try
        {
            await Task.Delay(500);
            var (success, message, employee) = _employeeService.Login(Username, Password);
            
            if (success && employee != null)
            {
                Application.Current!.MainPage = new AppShell();
            }
            else
            {
                ErrorMessage = message;
                HasError = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Lỗi kết nối: {ex.Message}";
            HasError = true;
        }
        finally
        {
            IsLoading = false;
        }
    }
}

public partial class DashboardViewModel : ObservableObject
{
    private readonly IReportService _reportService;
    private readonly IEmployeeService _employeeService;
    private DashboardStats _stats = new();
    private string _welcomeMessage = "Xin chào!";
    private bool _isLoading;
    private bool _isRefreshing;

    public DashboardStats Stats { get => _stats; set => SetProperty(ref _stats, value); }
    public string WelcomeMessage { get => _welcomeMessage; set => SetProperty(ref _welcomeMessage, value); }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
    public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }

    public DashboardViewModel(IReportService reportService, IEmployeeService employeeService)
    {
        _reportService = reportService;
        _employeeService = employeeService;
        if (_employeeService.CurrentEmployee != null)
            WelcomeMessage = $"Xin chào, {_employeeService.CurrentEmployee.Name}!";
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsLoading = true;
        try { await Task.Run(() => { Stats = _reportService.GetDashboardStats(); }); }
        finally { IsLoading = false; IsRefreshing = false; }
    }

    [RelayCommand]
    private async Task RefreshAsync() { IsRefreshing = true; await LoadDataAsync(); }
}

public partial class ProductsViewModel : ObservableObject
{
    private readonly IProductService _productService;
    private List<Product> _products = [];
    private string _searchText = string.Empty;
    private bool _isLoading;
    private bool _isRefreshing;
    private Product? _selectedProduct;

    public List<Product> Products { get => _products; set => SetProperty(ref _products, value); }
    public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
    public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }
    public Product? SelectedProduct { get => _selectedProduct; set => SetProperty(ref _selectedProduct, value); }

    public ProductsViewModel(IProductService productService) { _productService = productService; }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        IsLoading = true;
        try { await Task.Run(() => { Products = _productService.GetAllProducts(); }); }
        finally { IsLoading = false; IsRefreshing = false; }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        IsLoading = true;
        try
        {
            await Task.Run(() =>
            {
                Products = string.IsNullOrWhiteSpace(SearchText)
                    ? _productService.GetAllProducts()
                    : _productService.SearchProducts(SearchText);
            });
        }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    private async Task RefreshAsync() { IsRefreshing = true; await LoadProductsAsync(); }
}

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;
    private List<Customer> _customers = [];
    private string _searchText = string.Empty;
    private bool _isLoading;
    private bool _isRefreshing;
    private Customer? _selectedCustomer;

    public List<Customer> Customers { get => _customers; set => SetProperty(ref _customers, value); }
    public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
    public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }
    public Customer? SelectedCustomer { get => _selectedCustomer; set => SetProperty(ref _selectedCustomer, value); }

    public CustomersViewModel(ICustomerService customerService) { _customerService = customerService; }

    [RelayCommand]
    private async Task LoadCustomersAsync()
    {
        IsLoading = true;
        try { await Task.Run(() => { Customers = _customerService.GetAllCustomers(); }); }
        finally { IsLoading = false; IsRefreshing = false; }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        IsLoading = true;
        try
        {
            await Task.Run(() =>
            {
                Customers = string.IsNullOrWhiteSpace(SearchText)
                    ? _customerService.GetAllCustomers()
                    : _customerService.SearchCustomers(SearchText);
            });
        }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    private async Task RefreshAsync() { IsRefreshing = true; await LoadCustomersAsync(); }
}

public partial class SalesViewModel : ObservableObject
{
    private readonly ISalesService _salesService;
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;
    
    private List<Invoice> _invoices = [];
    private List<Product> _availableProducts = [];
    private List<Customer> _customerList = [];
    private List<InvoiceDetail> _cartItems = [];
    private Customer? _selectedCustomer;
    private decimal _subtotal;
    private decimal _discount;
    private decimal _total;
    private bool _isLoading;
    private bool _isRefreshing;

    public List<Invoice> Invoices { get => _invoices; set => SetProperty(ref _invoices, value); }
    public List<Product> AvailableProducts { get => _availableProducts; set => SetProperty(ref _availableProducts, value); }
    public List<Customer> CustomerList { get => _customerList; set => SetProperty(ref _customerList, value); }
    public List<InvoiceDetail> CartItems { get => _cartItems; set => SetProperty(ref _cartItems, value); }
    public Customer? SelectedCustomer { get => _selectedCustomer; set => SetProperty(ref _selectedCustomer, value); }
    public decimal Subtotal { get => _subtotal; set => SetProperty(ref _subtotal, value); }
    public decimal Discount { get => _discount; set => SetProperty(ref _discount, value); }
    public decimal Total { get => _total; set => SetProperty(ref _total, value); }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
    public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }

    public SalesViewModel(ISalesService salesService, IProductService productService, ICustomerService customerService)
    {
        _salesService = salesService;
        _productService = productService;
        _customerService = customerService;
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsLoading = true;
        try
        {
            await Task.Run(() =>
            {
                Invoices = _salesService.GetAllInvoices();
                AvailableProducts = _productService.GetAllProducts().Where(p => p.StockQty > 0).ToList();
                CustomerList = _customerService.GetAllCustomers();
            });
        }
        finally { IsLoading = false; IsRefreshing = false; }
    }

    [RelayCommand]
    private async Task RefreshAsync() { IsRefreshing = true; await LoadDataAsync(); }
}

public partial class ReportsViewModel : ObservableObject
{
    private readonly IReportService _reportService;
    private DashboardStats _stats = new();
    private List<DailySalesReport> _dailyReports = [];
    private List<TopProductReport> _topProducts = [];
    private List<InventoryReport> _inventoryReports = [];
    private DateTime _fromDate = DateTime.Today.AddDays(-30);
    private DateTime _toDate = DateTime.Today;
    private bool _isLoading;
    private bool _isRefreshing;

    public DashboardStats Stats { get => _stats; set => SetProperty(ref _stats, value); }
    public List<DailySalesReport> DailyReports { get => _dailyReports; set => SetProperty(ref _dailyReports, value); }
    public List<TopProductReport> TopProducts { get => _topProducts; set => SetProperty(ref _topProducts, value); }
    public List<InventoryReport> InventoryReports { get => _inventoryReports; set => SetProperty(ref _inventoryReports, value); }
    public DateTime FromDate { get => _fromDate; set => SetProperty(ref _fromDate, value); }
    public DateTime ToDate { get => _toDate; set => SetProperty(ref _toDate, value); }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
    public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }

    public ReportsViewModel(IReportService reportService) { _reportService = reportService; }

    [RelayCommand]
    private async Task LoadReportsAsync()
    {
        IsLoading = true;
        try
        {
            await Task.Run(() =>
            {
                Stats = _reportService.GetDashboardStats();
                DailyReports = _reportService.GetDailySalesReport(FromDate, ToDate);
                TopProducts = _reportService.GetTopProducts(FromDate, ToDate);
                InventoryReports = _reportService.GetInventoryReport();
            });
        }
        finally { IsLoading = false; IsRefreshing = false; }
    }

    [RelayCommand]
    private async Task RefreshAsync() { IsRefreshing = true; await LoadReportsAsync(); }
}
