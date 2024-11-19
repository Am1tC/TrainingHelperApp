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
            Routing.RegisterRoute("updateProfileView", typeof(ProfileView));
            Routing.RegisterRoute("updateProfileView", typeof(AddTrainerView));



        }
    }
}
