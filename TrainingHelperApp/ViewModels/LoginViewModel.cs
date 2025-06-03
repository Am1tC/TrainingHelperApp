using System;
using System.Windows.Input;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;
        private readonly IServiceProvider serviceProvider;

        public LoginViewModel(TrainingHelperWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.proxy = proxy;
            this.serviceProvider = serviceProvider;
            LoginCommand = new Command(OnLogin);
            RegisterCommand = new Command(OnRegister);

            Id = "";
            Password = "";
            InServerCall = false;
            ErrorMsg = "";
            IsTrainee = true; // default selected
        }

        #region Properties

        private string id;
        public string Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        private bool inServerCall;
        public bool InServerCall
        {
            get => inServerCall;
            set
            {
                if (inServerCall != value)
                {
                    inServerCall = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(NotInServerCall));
                }
            }
        }

        public bool NotInServerCall => !InServerCall;

        // Radio button logic (no converter)
        private bool isTrainee;
        public bool IsTrainee
        {
            get => isTrainee;
            set
            {
                if (isTrainee != value)
                {
                    isTrainee = value;
                    if (value) SelectedUserType = "trainee";
                    OnPropertyChanged();
                }
            }
        }

        private bool isTrainer;
        public bool IsTrainer
        {
            get => isTrainer;
            set
            {
                if (isTrainer != value)
                {
                    isTrainer = value;
                    if (value) SelectedUserType = "trainer";
                    OnPropertyChanged();
                }
            }
        }

        private bool isOwner;
        public bool IsOwner
        {
            get => isOwner;
            set
            {
                if (isOwner != value)
                {
                    isOwner = value;
                    if (value) SelectedUserType = "owner";
                    OnPropertyChanged();
                }
            }
        }

        private string selectedUserType;
        public string SelectedUserType
        {
            get => selectedUserType;
            set
            {
                if (selectedUserType != value)
                {
                    selectedUserType = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        private async void OnLogin()
        {
            InServerCall = true;
            ErrorMsg = "";

            LoginInfo loginInfo = new LoginInfo { Id = Id, Password = Password };

            ((App)Application.Current).LoggedInUser = null;
            ((App)Application.Current).TraineeIn = false;
            ((App)Application.Current).TrainerIn = false;
            ((App)Application.Current).OwnerIn = false;

            if (SelectedUserType == "trainee")
            {
                var trainee = await proxy.LoginAsync(loginInfo);
                if (trainee != null)
                {
                    ((App)Application.Current).LoggedInUser = trainee;
                    ((App)Application.Current).TraineeIn = true;
                }
            }
            else if (SelectedUserType == "trainer")
            {
                var trainer = await proxy.TrainerLoginAsync(loginInfo);
                if (trainer != null)
                {
                    ((App)Application.Current).LoggedInTrainer = trainer;
                    ((App)Application.Current).TrainerIn = true;
                }
            }
            else if (SelectedUserType == "owner")
            {
                var owner = await proxy.OwnerLoginAsync(loginInfo);
                if (owner != null)
                {
                    ((App)Application.Current).LoggedInOwner = owner;
                    ((App)Application.Current).OwnerIn = true;
                }
            }

            if (((App)Application.Current).LoggedInUser  == null && ((App)Application.Current).LoggedInOwner == null && ((App)Application.Current).LoggedInTrainer == null)
            {
                ErrorMsg = "Invalid ID or password";
                InServerCall = false;
                return;
            }

            ErrorMsg = "";
            InServerCall = false;

            AppShell shell = serviceProvider.GetService<AppShell>();
            ((App)Application.Current).MainPage = shell;
            Shell.Current.FlyoutIsPresented = false;
        }

        private void OnRegister()
        {
            ErrorMsg = "";
            Id = "";
            Password = "";
            ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<RegisterView>());
        }

        #endregion
    }
}
