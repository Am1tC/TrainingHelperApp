using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp.ViewModels
{
    public class AddTrainerViewModel : ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;
        public AddTrainerViewModel()
        {
            this.proxy = proxy;
            AddTrainerCommand = new Command(OnAddTrainer);
            ClearCommand = new Command(OnClear);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            

            IdError = "Invalid Id";
            BirthDateError = "must be older than 10 years";
            NameError = "Invalid Name";
            LastNameError = "Invalid Last name ";
            EmailError = "Email must be in the correct format";
            PasswordError = "Password must contain letters and numbers";
            PhoneError = "Phone must starts with 05 and have 10 digits";
            //GenderError = "Please select a gender.";
        }

        #region properties

        #region Name
        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");

            }
        }

        private string nameError;
        public string NameError
        {
            get
            {
                return this.nameError;
            }
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");

            }
        }

        private bool showNameError;
        public bool ShowNameError
        {
            get
            {
                return this.showNameError;
            }
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");

            }
        }

        private void ValidateName()
        {
            this.ShowNameError = name.Any(char.IsDigit);
        }
        #endregion

        #region LastName 
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastNameError;

        public string LastNameError
        {
            get => lastNameError;
            set
            {
                lastNameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowLastNameError = lastName.Any(char.IsDigit);
        }
        #endregion

        #region Email

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");

            }
        }

        private string emailError;
        public string EmailError
        {
            get
            {
                return this.emailError;
            }
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");

            }
        }

        private bool showEmailError;
        public bool ShowEmailError
        {
            get
            {
                return this.showEmailError;
            }
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");

            }
        }

        private void ValidateEmail()
        {

            if (!ShowEmailError)
            {
                // check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    EmailError = "Email is not valid";
                    ShowEmailError = true;
                }
                else
                {
                    EmailError = "";
                    ShowEmailError = false;
                }
                ShowEmailError = false;
            }
            else
            {
                EmailError = "Email is required";
            }
        }
        #endregion

        #region Password

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                password = value;
                ValidatePassword();
                OnPropertyChanged("Password");

            }
        }

        private string passwordError;
        public string PasswordError
        {
            get
            {
                return this.passwordError;
            }
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");

            }
        }

        private bool showPasswordError;
        public bool ShowPasswordError
        {
            get
            {
                return this.showPasswordError;
            }
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");

            }
        }

        private void ValidatePassword()
        {
            //Password must include characters and numbers 
            if (string.IsNullOrEmpty(password) ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsLetter))
            {
                this.ShowPasswordError = true;
            }
            else
                this.ShowPasswordError = false;
        }

        private bool isPassword = true;
        public bool IsPassword
        {
            get
            {
                return this.isPassword;
            }
            set
            {
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }

        //This command will trigger on pressing the password eye icon
        //public Command ShowPasswordCommand { get; }
        ////This method will be called when the password eye icon is pressed
        //public void OnShowPassword()
        //{
        //    //Toggle the password visibility
        //    IsPassword = !IsPassword;
        //}


        #endregion

        #region Phone

        private string phone;

        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                ValidatePhone();
                OnPropertyChanged("Phone");
            }
        }

        private bool showPhoneError;

        public bool ShowPhoneError
        {
            get => showPhoneError;
            set
            {
                showPhoneError = value;
                OnPropertyChanged("ShowPhoneError");
            }
        }



        private string phoneError;

        public string PhoneError
        {
            get => phoneError;
            set
            {
                phoneError = value;
                OnPropertyChanged("PhoneError");
            }
        }

        private void ValidatePhone()
        {

            if (phone.StartsWith("05") && phone.Length == 10 && phone.All(char.IsDigit))
            {
                ShowPhoneError = false;
            }
            else
            {
                ShowPhoneError = true;
            }

        }
        #endregion

        #region Id
        private string id;
        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                id = value;
                ValidateId();
                OnPropertyChanged("Id");

            }
        }

        private string idError;
        public string IdError
        {
            get
            {
                return this.idError;
            }
            set
            {
                idError = value;
                OnPropertyChanged("IdError");

            }
        }

        private bool showIdError;
        public bool ShowIdError
        {
            get
            {
                return this.showIdError;
            }
            set
            {
                showIdError = value;
                OnPropertyChanged("ShowIdError");

            }
        }

        private void ValidateId()
        {
            if (string.IsNullOrEmpty(id) ||
                  id.Length != 9 ||
                  !id.All(char.IsDigit))
            {
                this.ShowIdError = true;
            }
            else
            {
                this.ShowIdError = false;
            }

        }
        #endregion

        #region Gender

        private bool isMale;
        public bool IsMale
        {
            get => isMale;
            set
            {
                isMale = value;
                if (isMale)
                    Gender = "Male"; // Update Gender property
                OnPropertyChanged("IsMale");
            }
        }

        private bool isFemale;
        public bool IsFemale
        {
            get => isFemale;
            set
            {
                isFemale = value;
                if (isFemale)
                    Gender = "Female"; // Update Gender property
                OnPropertyChanged("IsFemale");
            }
        }

        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }


        #endregion

        #region BirthDate
        private bool showBirthDateError;

        public bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private DateTime birthDate;

        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                ValidateBirthDate();
                OnPropertyChanged("BirthDate");
            }
        }

        private string birthDateError;

        public string BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private void ValidateBirthDate()
        {
            DateTime currentDate = DateTime.Now;
            DateTime YearsAgo = currentDate.AddYears(-18);
            if (!(YearsAgo >= birthDate))
                this.ShowBirthDateError = true;
        }
        #endregion

        #region Photo

        private string photoURL;

        public string PhotoURL
        {
            get => photoURL;
            set
            {
                photoURL = value;
                OnPropertyChanged("PhotoURL");
            }
        }

        private string localPhotoPath;

        public string LocalPhotoPath
        {
            get => localPhotoPath;
            set
            {
                localPhotoPath = value;
                OnPropertyChanged("LocalPhotoPath");
            }
        }

        public Command UploadPhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    // The user picked a file
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = proxy.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }

        #endregion  //needs review /
        //needs review 
        #endregion

        #region logic
        public Command AddTrainerCommand { get; }
        public Command ClearCommand { get; }

        public async void OnAddTrainer()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidatePhone();
            ValidateId();
            ValidateBirthDate();

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowPhoneError && !ShowIdError && !ShowBirthDateError)
            {
                Trainer trainer = new Trainer
                {
                    FirstName = Name,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    PhoneNum = Phone,
                    Id = Id,
                };

                InServerCall = true;
                trainer = await proxy.RegisterTrainerAsync(trainer);
                InServerCall = false;

                if (trainer != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Add Trainer", "Trainer added successfully", "ok");
                    InServerCall = false;
                    await Shell.Current.GoToAsync("TrainersView");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Trainer", "Trainer not added", "ok");
                    InServerCall = false;
                }

            }
            else
            {
                string errorMsg = "Creation failed. Please try again and make sure all fields are valid.";
                await Application.Current.MainPage.DisplayAlert("Add Trainer", errorMsg, "ok");
            }
        }
        public async void OnClear()
        {
            Name = "";
            LastName = "";
            Email = "";
            Password = "";
            Phone = "";
            Id = "";

        }
#endregion
    }
}