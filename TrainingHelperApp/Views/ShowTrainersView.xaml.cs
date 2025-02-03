using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class ShowTrainersView : ContentPage
{
	public ShowTrainersView(ShowTrainersViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}