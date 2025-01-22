using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class OwnerLoginView : ContentPage
{
	public OwnerLoginView(OwnerLoginViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}