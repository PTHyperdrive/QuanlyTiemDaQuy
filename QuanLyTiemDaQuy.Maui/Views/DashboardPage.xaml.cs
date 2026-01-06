using QuanLyTiemDaQuy.Maui.ViewModels;

namespace QuanLyTiemDaQuy.Maui.Views;

public partial class DashboardPage : ContentPage
{
    private readonly DashboardViewModel _viewModel;

    public DashboardPage()
    {
        InitializeComponent();
        _viewModel = App.Current?.Handler?.MauiContext?.Services.GetService<DashboardViewModel>() 
            ?? new DashboardViewModel(null!, null!);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}
