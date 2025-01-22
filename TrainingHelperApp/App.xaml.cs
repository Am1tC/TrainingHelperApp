using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp
{
    public partial class App : Application
    {
        public  Trainee? LoggedInUser { get; set; }
        public Owner? LoggedInOwner { get; set; }
        public Trainer? LoggedInTrainer { get; set; }

        public bool OwnerIn = false; //defult, is set true when owner logs in (in vm)
        public bool TrainerIn = false; //defult, is set true when trainer logs in (in vm)
        public bool TraineeIn = false; //defult, is set true when trainee logs in (in vm)

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
