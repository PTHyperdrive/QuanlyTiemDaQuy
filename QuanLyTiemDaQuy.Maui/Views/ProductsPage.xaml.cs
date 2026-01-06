using QuanLyTiemDaQuy.Maui.ViewModels;

namespace QuanLyTiemDaQuy.Maui.Views;

public partial class ProductsPage : ContentPage
{
    private readonly ProductsViewModel _viewModel;

    public ProductsPage()
    {
        InitializeComponent();
        _viewModel = App.Current?.Handler?.MauiContext?.Services.GetService<ProductsViewModel>()
            ?? new ProductsViewModel(null!);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadProductsCommand.ExecuteAsync(null);
    }
}
