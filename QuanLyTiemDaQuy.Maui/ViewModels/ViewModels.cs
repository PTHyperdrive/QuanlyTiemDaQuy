using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.Models;
using QuanLyTiemDaQuy.Maui.Messages;
using QuanLyTiemDaQuy.Maui.Views;

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

    [RelayCommand]
    private async Task AddCustomerAsync()
    {
        string name = await Shell.Current.DisplayPromptAsync("Thêm khách hàng", "Nhập tên khách hàng:");
        if (string.IsNullOrWhiteSpace(name)) return;
        
        string phone = await Shell.Current.DisplayPromptAsync("Thêm khách hàng", "Nhập số điện thoại:");
        if (string.IsNullOrWhiteSpace(phone)) return;

        IsLoading = true;
        try
        {
            var customer = new Customer
            {
                Name = name,
                Phone = phone,
                Tier = "Thường",
                Address = "",
                Email = "",
                CreatedAt = DateTime.Now
            };

            await Task.Run(() => 
            {
                var result = _customerService.AddCustomer(customer);
                if (result.Success)
                {
                    customer.CustomerId = result.CustomerId;
                }
            });

            await LoadCustomersAsync();
            await Shell.Current.DisplayAlert("Thành công", "Thêm khách hàng thành công", "OK");
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Lỗi", ex.Message, "OK");
        }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    private async Task SelectCustomerAsync(Customer customer)
    {
        if (customer == null) return;
        
        // Navigate to SalesPage and pass customer
        // We can communicate via Messenger or query parameters. 
        // Using Messenger for decoupled communication is cleaner in MVVM Toolkit.
        WeakReferenceMessenger.Default.Send(new CustomerSelectedMessage(customer));
        
        // Find the Sales tab via route or index. Assuming Sales is usually the 4th tab (Dashboard, Products, Customers, Sales...)
        // Or route navigation: //SalesPage
        await Shell.Current.GoToAsync($"//{nameof(SalesPage)}");
        
        // Reset selection
        SelectedCustomer = null;
    }
}


public partial class SalesViewModel : ObservableObject, IRecipient<CustomerSelectedMessage>
{
    private readonly ISalesService _salesService;
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;
    private readonly IDiscountService _discountService;
    
    private List<Invoice> _invoices = [];
    private List<Product> _availableProducts = [];
    private List<Product> _allProducts = [];
    private List<Customer> _customerList = [];
    private List<InvoiceDetail> _cartItems = [];
    private Customer? _selectedCustomer;
    private string _searchText = string.Empty;
    private decimal _subtotal;
    private decimal _discount;
    private decimal _vatAmount;
    private decimal _total;
    private bool _isLoading;
    private bool _isRefreshing;
    private string _discountReason = "";

    public List<Invoice> Invoices { get => _invoices; set => SetProperty(ref _invoices, value); }
    public List<Product> AvailableProducts { get => _availableProducts; set => SetProperty(ref _availableProducts, value); }
    public List<Customer> CustomerList { get => _customerList; set => SetProperty(ref _customerList, value); }
    public List<InvoiceDetail> CartItems { get => _cartItems; set => SetProperty(ref _cartItems, value); }
    public Customer? SelectedCustomer 
    { 
        get => _selectedCustomer; 
        set 
        {
            if (SetProperty(ref _selectedCustomer, value))
            {
                CalculateTotals();
            }
        }
    }
    public string SearchText { get => _searchText; set => SetProperty(ref _searchText, value); }
    public decimal Subtotal { get => _subtotal; set => SetProperty(ref _subtotal, value); }
    public decimal Discount { get => _discount; set => SetProperty(ref _discount, value); }
    public decimal VATAmount { get => _vatAmount; set => SetProperty(ref _vatAmount, value); }
    public decimal Total { get => _total; set => SetProperty(ref _total, value); }
    public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
    public bool IsRefreshing { get => _isRefreshing; set => SetProperty(ref _isRefreshing, value); }
    public string DiscountReason { get => _discountReason; set => SetProperty(ref _discountReason, value); }

    public SalesViewModel(ISalesService salesService, 
        IProductService productService, 
        ICustomerService customerService,
        IDiscountService discountService)
    {
        _salesService = salesService;
        _productService = productService;
        _customerService = customerService;
        _discountService = discountService;

        WeakReferenceMessenger.Default.Register<CustomerSelectedMessage>(this);
    }

    public void Receive(CustomerSelectedMessage message)
    {
        SelectedCustomer = message.Value;
        // CalculateTotals() is called in setter
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        IsLoading = true;
        try
        {
            await Task.Run(() =>
            {
                Invoices = _salesService.GetTodayInvoices();
                _allProducts = _productService.GetAllProducts();
                AvailableProducts = _allProducts.Where(p => p.StockQty > 0).ToList();
                CustomerList = _customerService.GetAllCustomers();
            });
        }
        finally { IsLoading = false; IsRefreshing = false; }
    }

    [RelayCommand]
    private void SearchProduct()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
            AvailableProducts = _allProducts.Where(p => p.StockQty > 0).ToList();
        else
            AvailableProducts = _allProducts
                .Where(p => p.StockQty > 0 && 
                    (p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                     p.ProductCode.Contains(SearchText, StringComparison.OrdinalIgnoreCase)))
                .ToList();
    }

    [RelayCommand]
    private void AddToCart(Product product)
    {
        if (product == null || product.StockQty <= 0) return;
        
        var existingItem = CartItems.FirstOrDefault(c => c.ProductId == product.ProductId);
        if (existingItem != null)
        {
            existingItem.Qty++;
            existingItem.CalculateLineTotal();
        }
        else
        {
            CartItems.Add(new InvoiceDetail
            {
                ProductId = product.ProductId,
                ProductCode = product.ProductCode,
                ProductName = product.Name,
                UnitPrice = product.SellPrice,
                Qty = 1,
                LineTotal = product.SellPrice
            });
        }
        CartItems = [.. CartItems]; // Trigger UI update
        CalculateTotals();
    }

    [RelayCommand]
    private void RemoveFromCart(InvoiceDetail item)
    {
        if (item == null) return;
        CartItems.Remove(item);
        CartItems = [.. CartItems];
        CalculateTotals();
    }

    [RelayCommand]
    private void IncreaseQty(InvoiceDetail item)
    {
        if (item == null) return;
        var product = _allProducts.FirstOrDefault(p => p.ProductId == item.ProductId);
        if (product != null && item.Qty < product.StockQty)
        {
            item.Qty++;
            item.CalculateLineTotal();
            CartItems = [.. CartItems];
            CalculateTotals();
        }
    }

    [RelayCommand]
    private void DecreaseQty(InvoiceDetail item)
    {
        if (item == null) return;
        if (item.Qty > 1)
        {
            item.Qty--;
            item.CalculateLineTotal();
            CartItems = [.. CartItems];
            CalculateTotals();
        }
        else
        {
            RemoveFromCart(item);
        }
    }

    [RelayCommand]
    private void ClearCart()
    {
        CartItems = [];
        SelectedCustomer = null;
        _currentPendingInvoiceId = 0;
        CalculateTotals();
    }

    private int _currentPendingInvoiceId = 0;

    [RelayCommand]
    private async Task SavePendingAsync()
    {
        if (!CartItems.Any())
        {
            await Shell.Current.DisplayAlert("Thông báo", "Giỏ hàng trống!", "OK");
            return;
        }

        IsLoading = true;
        try
        {
            var invoice = new Invoice
            {
                CustomerId = SelectedCustomer?.CustomerId ?? 0,
                EmployeeId = 1, // TODO: Get from logged in user
                BranchId = 1, // TODO: Get from logged in user's branch
                InvoiceDate = DateTime.Now,
                Details = CartItems.ToList(),
                PaymentMethod = PaymentMethods.Cash,
                Status = InvoiceStatus.Pending,
                DiscountPercent = 0,
                DiscountAmount = Discount,
                VAT = 10,
                Note = "Hóa đơn lưu chờ"
            };
            invoice.CalculateTotals();
            invoice.DiscountAmount = Discount;
            invoice.Total = Total;
            invoice.VATAmount = VATAmount;

            await Task.Run(() => 
            {
                // If we are editing a pending invoice, cancel the old one first (Strategy: Clone & Replace)
                if (_currentPendingInvoiceId > 0)
                {
                    _salesService.CancelInvoice(_currentPendingInvoiceId, "Cập nhật hóa đơn chờ");
                }
                _salesService.CreateInvoice(invoice);
            });

            await Shell.Current.DisplayAlert("Thành công", "Đã lưu hóa đơn chờ", "OK");
            ClearCart();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Lỗi", ex.Message, "OK");
        }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    private async Task RetrievePendingAsync()
    {
        IsLoading = true;
        try
        {
            var pendingInvoices = await Task.Run(() => _salesService.GetPendingInvoices());
            
            if (pendingInvoices.Count == 0)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không có hóa đơn đang chờ", "OK");
                return;
            }

            var options = pendingInvoices.Select(i => $"{i.InvoiceCode} - {i.CustomerName} - {i.Total:N0}đ").ToArray();
            string action = await Shell.Current.DisplayActionSheet("Chọn hóa đơn để lấy lại", "Hủy", null, options);
            
            if (!string.IsNullOrEmpty(action) && action != "Hủy")
            {
                var selectedIndex = Array.IndexOf(options, action);
                if (selectedIndex >= 0)
                {
                    var selectedInvoice = pendingInvoices[selectedIndex];
                    LoadInvoiceToCart(selectedInvoice);
                }
            }
        }
        finally { IsLoading = false; }
    }

    [RelayCommand]
    private async Task CancelPendingAsync()
    {
        IsLoading = true;
        try
        {
            var pendingInvoices = await Task.Run(() => _salesService.GetPendingInvoices());
            
            if (pendingInvoices.Count == 0)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không có hóa đơn đang chờ", "OK");
                return;
            }

            var options = pendingInvoices.Select(i => $"{i.InvoiceCode} - {i.CustomerName} - {i.Total:N0}đ").ToArray();
            string action = await Shell.Current.DisplayActionSheet("Chọn hóa đơn để hủy", "Thoát", null, options);
            
            if (!string.IsNullOrEmpty(action) && action != "Thoát")
            {
                var selectedIndex = Array.IndexOf(options, action);
                if (selectedIndex >= 0)
                {
                    var selectedInvoice = pendingInvoices[selectedIndex];
                    bool confirm = await Shell.Current.DisplayAlert("Xác nhận", $"Bạn có chắc muốn hủy hóa đơn {selectedInvoice.InvoiceCode}?", "Có", "Không");
                    if (confirm)
                    {
                        await Task.Run(() => _salesService.CancelInvoice(selectedInvoice.InvoiceId, "Người dùng hủy thủ công"));
                        await Shell.Current.DisplayAlert("Thành công", "Đã hủy hóa đơn chờ", "OK");
                    }
                }
            }
        }
        finally { IsLoading = false; }
    }

    private void LoadInvoiceToCart(Invoice invoice)
    {
        // Load Details
        // We need to fetch details full info (though GetPendingInvoices usually eagerly loads details? Check Repo)
        // Repo: GetPendingInvoices -> MapDataTableToList.
        // Wait, MapDataTableToList does NOT load details automatically! 
        // GetById DOES load details.
        // So I should fetch the full invoice by ID to be sure.
        
        var fullInvoice = _salesService.GetInvoiceById(invoice.InvoiceId);
        if (fullInvoice == null) return;

        CartItems = [.. fullInvoice.Details];
        _cartItems = fullInvoice.Details; // Sync backing field

        // Load Customer
        if (fullInvoice.CustomerId > 0)
        {
            SelectedCustomer = CustomerList.FirstOrDefault(c => c.CustomerId == fullInvoice.CustomerId);
        }
        else
        {
            SelectedCustomer = null;
        }

        _currentPendingInvoiceId = fullInvoice.InvoiceId;
        CalculateTotals();
    }

    // Override Checkout to clear pending flag
    // Re-implementing CheckoutAsync to handle _currentPendingInvoiceId
    [RelayCommand]
    private async Task CheckoutAsync()
    {
        if (!CartItems.Any())
        {
            await Shell.Current.DisplayAlert("Thông báo", "Giỏ hàng trống!", "OK");
            return;
        }

        bool confirm = await Shell.Current.DisplayAlert(
            "Thanh toán", "Hủy");

        if (!confirm) return;

        // Prompt for payment method
        string selectedMethod = await Shell.Current.DisplayActionSheet(
            "Chọn phương thức thanh toán", 
            "Hủy", 
            null, 
            PaymentMethods.Transfer, 
            PaymentMethods.Card, 
            PaymentMethods.Cash, 
            PaymentMethods.EWallet);

        if (string.IsNullOrEmpty(selectedMethod) || selectedMethod == "Hủy") return;

        IsLoading = true;
        try
        {
            var invoice = new Invoice
            {
                CustomerId = SelectedCustomer?.CustomerId ?? 0,
                EmployeeId = 1, 
                BranchId = 1, 
                InvoiceDate = DateTime.Now,
                Details = CartItems.ToList(),
                PaymentMethod = selectedMethod,
                Status = InvoiceStatus.Completed,
                DiscountPercent = 0,
                DiscountAmount = Discount,
                VAT = 10
            };
            invoice.CalculateTotals();
            invoice.DiscountAmount = Discount;
            invoice.Total = Total;
            invoice.VATAmount = VATAmount;

            int newInvoiceId = 0;
            await Task.Run(() => 
            {
                // If this was a pending invoice, cancel/delete the old pending one
                if (_currentPendingInvoiceId > 0)
                {
                   _salesService.CancelInvoice(_currentPendingInvoiceId, "Hoàn tất thanh toán (Chuyển sang HĐ mới)");
                }

                var result = _salesService.CreateInvoice(invoice);
                if (result.Success) newInvoiceId = result.InvoiceId; // Capture ID
            });
            
            if (newInvoiceId > 0)
            {
                await Shell.Current.DisplayAlert("Thành công", 
                    $"Đã tạo hóa đơn", "OK");
                ClearCart();
                await LoadDataAsync();
            }
        }

        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Lỗi", ex.Message, "OK");
        }
        finally { IsLoading = false; }
    }

    private void CalculateTotals()
    {
        Subtotal = CartItems.Sum(c => c.LineTotal);
        
        decimal discountPercent = 0;
        if (SelectedCustomer != null)
        {
            var result = _discountService.CalculateBestDiscount(SelectedCustomer, DateTime.Now);
            discountPercent = result.DiscountPercent;
            DiscountReason = result.Reason;
        }
        else
        {
            DiscountReason = "";
        }

        Discount = Subtotal * (discountPercent / 100);
        
        var afterDiscount = Subtotal - Discount;
        VATAmount = afterDiscount * 0.10m; // 10% VAT
        Total = afterDiscount + VATAmount;
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
