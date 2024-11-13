namespace TrainingHelperApp.Views;
using TrainingHelperApp.Models;
using TrainingHelperApp.ViewModels;

public partial class RegisterView : ContentPage
{
    public RegisterView(RegisterViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
}