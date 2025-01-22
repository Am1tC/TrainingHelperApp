using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp.ViewModels
{
    public class AppShellViewModel :ViewModelBase
    {
        private Trainee? currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }

        private bool isOwner = ((App)Application.Current).OwnerIn;
        private bool isTrainer = ((App)Application.Current).TrainerIn;
        private bool isTrainee = ((App)Application.Current).TraineeIn;

        public bool IsOwner
        {
            get
            {
                return this.isOwner;
            }
            set { this.isOwner = value; }        
        }

        public bool IsTrainer
        {
            get
            {
                return this.isTrainer;
            }
            set { this.isTrainer = value; }
        }

        public bool IsTrainee
        {
            get
            {
                return this.isTrainee;
            }
            set { this.isTrainee = value; }
        }
      

        //public bool IsTrainer
        //{
        //    get
        //    {
        //        return this.currentUser.IsManager;
        //    }
        //}

        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;
            ((App)Application.Current).LoggedInOwner = null;
            ((App)Application.Current).LoggedInTrainer = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LoginView>());
        }
    }
}
