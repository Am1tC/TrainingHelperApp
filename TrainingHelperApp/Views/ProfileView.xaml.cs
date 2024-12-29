using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class ProfileView : ContentPage
{
	public ProfileView(ProfileViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
    //protected override void OnAppearing()
    //{
    //    //THe code below is a workarround for a bug in the Image control in MAUI
    //    //https://github.com/dotnet/maui/issues/18656
    //    base.OnAppearing();
    //    var bc = theImageBug.BindingContext;
    //    theImageBug.BindingContext = null;
    //    theImageBug.BindingContext = bc;

    //}
}