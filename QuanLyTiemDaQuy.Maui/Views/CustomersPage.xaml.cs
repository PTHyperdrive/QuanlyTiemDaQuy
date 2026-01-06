using QuanLyTiemDaQuy.Maui.ViewModels;

namespace QuanLyTiemDaQuy.Maui.Views;

public partial class CustomersPage : ContentPage
{
    private readonly CustomersViewModel _viewModel;

    public CustomersPage()
    {
        InitializeComponent();
        _viewModel = App.Current?.Handler?.MauiContext?.Services.GetService<CustomersViewModel>()
            ?? new CustomersViewModel(null!);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCustomersCommand.ExecuteAsync(null);
    }
}
