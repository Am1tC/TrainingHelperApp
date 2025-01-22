using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public LoginViewModel(TrainingHelperWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);
            id = "";
            password = "";
            InServerCall = false;
            errorMsg = "";
        }

        #region Validation
        private string id;
        private string password;

        public string Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string errorMsg;
        public string ErrorMsg
        {
            get => errorMsg;
            set
            {
                if (errorMsg != value)
                {
                    errorMsg = value;
                    OnPropertyChanged(nameof(ErrorMsg));
                }
            }
        }


        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        #endregion

        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall = true;
            ErrorMsg = "";
            //Call the server to login
            LoginInfo loginInfo = new LoginInfo {  Id = Id , Password = Password };
            Trainee? u = await this.proxy.LoginAsync(loginInfo);
            ((App)Application.Current).OwnerIn = false;//set owner user false
            ((App)Application.Current).TraineeIn = true;//

            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                ErrorMsg = "Invalid Id or password";
            }
            else
            {
                ErrorMsg = "";
                //Navigate to the main page
               AppShell? shell = serviceProvider.GetService<AppShell>();
                // tasksViewModel = serviceProvider.GetService<TasksViewModel>();
                //tasksViewModel.Refresh(); //Refresh data and user in the tasksview model as it is a singleton
               
                ((App)Application.Current).MainPage = shell;
                Shell.Current.FlyoutIsPresented = false; //close the flyout
                //Shell.Current.GoToAsync("Tasks"); //Navigate to the Tasks tab page
            }     
        }

        private void OnRegister()
        {
            ErrorMsg = "";
            Id = "";
            Password = "";
            // Navigate to the Register View page
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterView>());
        }
    }
}
