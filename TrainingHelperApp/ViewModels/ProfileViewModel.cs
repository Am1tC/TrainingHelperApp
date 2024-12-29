
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
//using static Java.Util.Jar.Attributes;

namespace TrainingHelperApp.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;
        public ProfileViewModel(TrainingHelperWebAPIProxy proxy)
        {
            Trainee theUser = ((App)App.Current).LoggedInUser;

            this.proxy = proxy;

            Name = theUser.FirstName;
            LastName = theUser.LastName;
            Email = theUser.Email;
            Password = theUser.Password;
            Phone = theUser.PhoneNum;
            Id = theUser.Id;            
            BirthDate = theUser.BirthDate.Value;
            subscriptionstart = theUser.SubscriptionStartDate.Value;
            subscriptionend = theUser.SubscriptionEndDate.Value;








            SaveCommand = new Command(OnSave);
            ShowPasswordCommand = new Command(OnShowPassword);
            UploadPhotoCommand = new Command(OnUploadPhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();

            LocalPhotoPath = "";
            IsPassword = true;
            IdError = "Invalid Id";
            BirthDateError = "must be older than 10 years";
            NameError = "Name is required";
            LastNameError = "Last name is required";
            EmailError = "Email is required";
            PasswordError = "Password must contain letters and numbers";
            PhoneError = "Phone must starts with 05 and have 10 digits";
        }

        #region subscription
        private DateTime subscriptionstart;
        public DateTime SubscriptionStart
        {
            get
            {
                return this.subscriptionstart;
            }
            set
            {
                subscriptionstart = value;
                OnPropertyChanged("SubscriptionStart");
            }
        }

        private DateTime subscriptionend;
        public DateTime SubscriptionEnd
        {
            get
            {
                return this.subscriptionend;
            }
            set
            {
                subscriptionend = value;
                OnPropertyChanged("SubscriptionEnd");
            }
        }
        #endregion //only for display


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
            this.ShowNameError = string.IsNullOrEmpty(Name);
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
            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
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
            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!ShowEmailError)
            {
                //check if email is in the correct format using regular expression
                //if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                //{
                //    EmailError = "Email is not valid";
                //    ShowEmailError = true;
                //}
                //else
                //{
                //    EmailError = "";
                //    ShowEmailError = false;
                //}
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
            //Password must include characters and numbers and be longer than 4 characters
            if (string.IsNullOrEmpty(password) ||
                password.Length < 4 ||
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
        public Command ShowPasswordCommand { get; }
        //This method will be called when the password eye icon is pressed
        public void OnShowPassword()
        {
            //Toggle the password visibility
            IsPassword = !IsPassword;
        }


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

            string pattern = @"^05\d{8}$";

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            if (!regex.IsMatch(phone))
            {
                ShowPhoneError = true;
            }
            else
            {
                ShowPhoneError = false;
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
            if (!string.IsNullOrEmpty(id) && id.Length == 9 && id.All(char.IsDigit))
            {
                showIdError = id.Select((c, i) => (c - '0') * (1 + i % 2))
                               .Sum(d => d > 9 ? d - 9 : d) % 10 != 0;
            }
            else
            {
                showIdError = true;
            }
        }
        #endregion

        #region Gender

        private char gender;
        public char Gender
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
            DateTime tenYearsAgo = currentDate.AddYears(-10);
            if (!(tenYearsAgo >= birthDate))
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

        #endregion

        public Command SaveCommand { get; }
        public async void OnSave() //simillar as on register
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidatePassword();
            ValidateBirthDate();
            ValidatePhone();

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowIdError && !ShowBirthDateError && !ShowPhoneError)
            {
                Trainee theUser = ((App)App.Current).LoggedInUser;
                theUser.FirstName = Name;
                theUser.LastName = LastName;
                theUser.Email = Email;
                theUser.Password = Password;
                theUser.BirthDate = birthDate;
                theUser.PhoneNum = Phone;
                theUser.Id = Id;


                InServerCall = true;
                bool success = await proxy.UpdateUser(theUser);

                //If the save was successful, navigate to the login page
                if (success)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                    {
                        Trainee? updatedUser = await proxy.UploadProfileImage(LocalPhotoPath);
                        if (updatedUser == null)
                        {
                            await Shell.Current.DisplayAlert("Save Profile", "User Data Was Saved BUT Profile image upload failed", "ok");
                        }
                        else
                        {
                            //theUser.ProfileImagePath = updatedUser.ProfileImagePath;
                            //UpdatePhotoURL(theUser.ProfileImagePath);
                        }

                    }
                    InServerCall = false;
                    await Shell.Current.DisplayAlert("Save Profile", "Profile saved successfully", "ok");
                }
                else
                {
                    InServerCall = false;
                    //If the registration failed, display an error message
                    string errorMsg = "Save Profile failed. Please try again.";
                    await Shell.Current.DisplayAlert("Save Profile", errorMsg, "ok");
                }


            }
        }
    }
}
