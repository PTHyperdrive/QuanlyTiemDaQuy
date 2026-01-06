using Microsoft.Extensions.Logging;
using QuanLyTiemDaQuy.Core.Interfaces;
using QuanLyTiemDaQuy.Core.DAL.Repositories;
using QuanLyTiemDaQuy.Core.BLL.Services;
using QuanLyTiemDaQuy.Maui.ViewModels;
using QuanLyTiemDaQuy.Maui.Views;

namespace QuanLyTiemDaQuy.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        // Register Repositories
        builder.Services.AddSingleton<IProductRepository, ProductRepository>();
        builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
        builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddSingleton<IInvoiceRepository, InvoiceRepository>();
        builder.Services.AddSingleton<IBranchRepository, BranchRepository>();
        builder.Services.AddSingleton<ISupplierRepository, SupplierRepository>();
        builder.Services.AddSingleton<IStoneTypeRepository, StoneTypeRepository>();
        builder.Services.AddSingleton<ICertificateRepository, CertificateRepository>();
        builder.Services.AddSingleton<IImportRepository, ImportRepository>();
        builder.Services.AddSingleton<IMarketPriceRepository, MarketPriceRepository>();

        // Register Services
        builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
        builder.Services.AddSingleton<ICustomerService, CustomerService>();
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<ISalesService, SalesService>();
        builder.Services.AddSingleton<IImportService, ImportService>();
        builder.Services.AddSingleton<IReportService, ReportService>();
        builder.Services.AddSingleton<IBranchService, BranchService>();
        builder.Services.AddSingleton<ISupplierService, SupplierService>();
        builder.Services.AddSingleton<IPricingService, PricingService>();
        builder.Services.AddSingleton<IMarketPriceApiService, MarketPriceApiService>();

        // Register ViewModels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<ProductsViewModel>();
        builder.Services.AddTransient<CustomersViewModel>();
        builder.Services.AddTransient<SalesViewModel>();
        builder.Services.AddTransient<ReportsViewModel>();

        // Register Pages
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<ProductsPage>();
        builder.Services.AddTransient<CustomersPage>();
        builder.Services.AddTransient<SalesPage>();
        builder.Services.AddTransient<ReportsPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

