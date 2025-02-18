using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class TrainerLoginView : ContentPage
{
	public TrainerLoginView(TrainerLoginViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}