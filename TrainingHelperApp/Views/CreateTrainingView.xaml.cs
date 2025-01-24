using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class CreateTrainingView : ContentPage
{
	public CreateTrainingView(CreateTrainingViewModel vm)
	{
        this.BindingContext = vm;
        InitializeComponent();
	}
}