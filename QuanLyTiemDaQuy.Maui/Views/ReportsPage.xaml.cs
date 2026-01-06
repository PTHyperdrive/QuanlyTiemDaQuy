using QuanLyTiemDaQuy.Maui.ViewModels;

namespace QuanLyTiemDaQuy.Maui.Views;

public partial class ReportsPage : ContentPage
{
    private readonly ReportsViewModel _viewModel;

    public ReportsPage()
    {
        InitializeComponent();
        _viewModel = App.Current?.Handler?.MauiContext?.Services.GetService<ReportsViewModel>()
            ?? new ReportsViewModel(null!);
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadReportsCommand.ExecuteAsync(null);
    }
}
