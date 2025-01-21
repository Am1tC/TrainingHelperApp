using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp
{
    public partial class App : Application
    {
        public  Trainee? LoggedInUser { get; set; }
        public Owner? LoggedInOwner { get; set; }

        private TrainingHelperWebAPIProxy proxy;
        public App(IServiceProvider serviceProvider, TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;          
            LoggedInUser = null;
            InitializeComponent();
            //Start with the Login View
            MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }
    }
}
