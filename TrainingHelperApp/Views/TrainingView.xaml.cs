using TrainingHelperApp.ViewModels;

namespace TrainingHelperApp.Views;

public partial class TrainingView : ContentPage
{


    public TrainingView(TrainingViewModel vm)
    {
        BindingContext = vm;

        InitializeComponent();
    }
}