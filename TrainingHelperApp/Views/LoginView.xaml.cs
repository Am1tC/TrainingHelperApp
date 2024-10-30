using TrainingHelperApp.Views;

namespace TrainingHelperApp.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginView vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}