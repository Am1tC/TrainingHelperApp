using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class AddTrainerView : ContentPage
{
	public AddTrainerView(AddTrainerViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}