using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class ContactPageView : ContentPage
{
	public ContactPageView(ContactPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}