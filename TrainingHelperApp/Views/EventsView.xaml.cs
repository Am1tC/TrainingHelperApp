using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class EventsView : ContentPage
{
	public EventsView(EventsViewModel  vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}