using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class ShowTraineesView : ContentPage
{
	public ShowTraineesView(ShowTraineesViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}