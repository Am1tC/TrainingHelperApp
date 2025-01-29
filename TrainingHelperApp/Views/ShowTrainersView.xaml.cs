namespace TrainingHelperApp.Views;

public partial class ShowTrainersView : ContentPage
{
	public ShowTrainersView(ShowTrainersView vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}
}