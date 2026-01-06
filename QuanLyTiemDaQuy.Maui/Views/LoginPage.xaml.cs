using QuanLyTiemDaQuy.Maui.ViewModels;

namespace QuanLyTiemDaQuy.Maui.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = App.Current?.Handler?.MauiContext?.Services.GetService<LoginViewModel>();
    }
}
