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
    public class OwnerLoginViewModel : ViewModelBase
    {

        private TrainingHelperWebAPIProxy proxy;
        private IServiceProvider serviceProvider;
        public OwnerLoginViewModel(TrainingHelperWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            LoginCommand = new Command(OnLogin);

            id = "";
            password = "";
            InServerCall = false;
            errorMsg = "";
        }
        #region stuff
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


        #endregion
        public ICommand LoginCommand { get; }
        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall = true;
            ErrorMsg = "";
            //Call the server to login
            LoginInfo loginInfo = new LoginInfo { Id = Id, Password = Password };
            Owner? u = await this.proxy.OwnerLoginAsync(loginInfo);
            ((App)Application.Current).OwnerIn = true;//set owner user
            ((App)Application.Current).TraineeIn = false;//set trainee user
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInOwner= u;
            if (u == null)
            {
                ErrorMsg = "Invalid Id or Password";
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
    }
}
