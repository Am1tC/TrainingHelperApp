using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class TrainerTrainingsView : ContentPage
{
	public TrainerTrainingsView(TrainerTrainingsViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}