using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class OrderedTrainingView : ContentPage
{
	public OrderedTrainingView(OrderedTrainingViewModel vm)
	{
		this.BindingContext = vm;
        InitializeComponent();
	}
}