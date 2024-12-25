using System.Windows.Input;
using TrainingHelperApp.ViewModels;
using TrainingHelperApp.Views;

namespace TrainingHelperApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {        
            Routing.RegisterRoute("Profile", typeof(ProfileView));
            Routing.RegisterRoute("addTrainerView", typeof(AddTrainerView));
           // Routing.RegisterRoute("ContactPage", typeof(ContentPageview));
           Routing.RegisterRoute("TrainingView", typeof(TrainingView));
            Routing.RegisterRoute("Events", typeof(EventsView));

        }
        public ICommand LogOutCommand { get; set; }
        private void OnLogOut()
        {
            DisplayAlert("Log out", "Are you sure you want to log out", "Ok");
        }
    }
}
