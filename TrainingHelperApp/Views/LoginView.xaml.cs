using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}