using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrainingHelperApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;
        private IServiceProvider serviceProvider;

        public RegisterViewModel(TrainingHelperWebAPIProxy proxy, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.proxy = proxy;
            RegisterCommand = new Command(OnRegister);
            CancelCommand = new Command(OnCancel);
            LogInCommand = new Command(OnLogIn);
            OwnerCommand = new Command(onOwner);
            TrainerCommand = new Command(onTrainer);


            UploadPhotoCommand = new Command(OnUploadPhoto);
            UploadTakePhotoCommand = new Command(OnUploadTakePhoto);
            PhotoURL = proxy.GetDefaultProfilePhotoUrl();
            LocalPhotoPath = "";
            IsPassword = true;
            GenderOptions = new List<string> { "Male", "Female", "Other" };

            IdError = "Invalid Id";
            BirthDateError = "must be older than 10 years";
            NameError = "Invalid Name";
            LastNameError = "Invalid Last name ";
            EmailError = "Email must be in the correct format";
            PasswordError = "Password must contain letters and numbers";
            PhoneError = "Phone must starts with 05 and have 10 digits";
            GenderError = "Please select a gender.";
        }
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
            if (string.IsNullOrEmpty(this.name) || name.Any(char.IsDigit))
            {
                this.ShowNameError = true;
            }
            else
            {
                this.ShowNameError = false;
            }
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
            if (string.IsNullOrEmpty(this.lastName) || lastName.Any(char.IsDigit))
            {
                this.ShowLastNameError = true;
            }
            else
            {
                this.ShowLastNameError = false;
            }
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
           
         
                // Check if email is in the correct format using regular expression
                if (string.IsNullOrEmpty(this.lastName) || !System.Text.RegularExpressions.Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                {

                    ShowEmailError = true;
                }
                else
                {                 
                    ShowEmailError = false;
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

            if ( !string.IsNullOrEmpty(phone) && phone.StartsWith("05") && phone.Length == 10 && phone.All(char.IsDigit))
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
            // Validate the ID number
            if (string.IsNullOrEmpty(id) || !IsIsraeliIdNumberValid(id))
            {
                // If the ID is invalid (empty, wrong length, contains non-digits, or fails checksum validation)
                ShowIdError = true;
                IdError = "Invalid ID number"; // Set the error message
            }
            else
            {
                // If the ID is valid, hide the error
                ShowIdError = false;
                IdError = "";
            }


        }
        private bool IsIsraeliIdNumberValid(string id)
        {
            // Ensure the ID is 9 digits (prepend zeros if necessary)
            id = id.PadLeft(9, '0');

            // Checksum validation logic for Israeli ID number
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                int digit = int.Parse(id[i].ToString());
                if (i % 2 == 1) // Multiply digits at odd positions by 2
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9; // If the result is greater than 9, subtract 9
                }
                sum += digit;
            }

            return sum % 10 == 0; // Valid ID if the sum is divisible by 10
        }


        #endregion

        #region Gender

        public List<string> GenderOptions { get; }
        private string gender;
        public string Gender
        {
            get => gender;
            set
            {
                //ValidateGender();
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        private string genderError;
        public string GenderError
        {
            get => genderError;
            set
            {
                genderError = value;
                OnPropertyChanged("GenderError");
            }
        }
        private bool showGenderError;
        public bool ShowGenderError
        {
            get => showGenderError;
            set
            {
                showGenderError = value;
                OnPropertyChanged("ShowGenderError");
            }
        }

        private void ValidateGender()
        {
           
            if (gender == null)
                this.ShowGenderError = true;
            else
                this.ShowGenderError = false;

            this.showGenderError = false;

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
            if (birthDate == DateTime.Today || !(tenYearsAgo >= birthDate))
                this.ShowBirthDateError = true;
            else
                this.showBirthDateError = false;
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
                var result = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions
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

        public Command UploadTakePhotoCommand { get; }
        //This method open the file picker to select a photo
        private async void OnUploadTakePhoto()
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
        public Command TrainerCommand { get; }
        public Command OwnerCommand { get; }
        public Command LogInCommand { get; }
        public Command RegisterCommand { get; }
        public Command CancelCommand { get; }

        //Define a method that will be called when the register button is clicked
        public async void OnRegister()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail(); 
            ValidatePassword();
            ValidateBirthDate();
            ValidatePhone();
            ValidateId();
            ValidateBirthDate();
            

            if (!ShowNameError && !ShowLastNameError && !ShowEmailError && !ShowPasswordError && !ShowIdError && !ShowBirthDateError && !ShowPhoneError&& !ShowGenderError)
            {
                //Create a new AppUser object with the data from the registration form
                var newUser = new Models.Trainee()
                {
                    FirstName = Name,
                    LastName = LastName,
                    Email = Email,
                    Password = Password,
                    Id = Id,
                    PhoneNum = Phone,
                    SubscriptionStartDate = DateTime.Now,
                    SubscriptionEndDate = DateTime.Now.AddYears(1),
                    BirthDate = DateTime.Now.AddYears(-20),
                    IsActive = true,    
                };

                //Call the Register method on the proxy to register the new user
                InServerCall = true;
                newUser = await proxy.Register(newUser);
                InServerCall = false;

                //If the registration was successful, navigate to the login page
                if (newUser != null)
                {
                    //UPload profile imae if needed
                    if (!string.IsNullOrEmpty(LocalPhotoPath))
                        {
                        Trainee? updatedUser = await proxy.LoginAsync(new Models.LoginInfo { Id = newUser.Id, Password = newUser.Password });
                    await proxy.UploadProfileImage(LocalPhotoPath);
                    if (updatedUser == null)
                    {
                        InServerCall = false;
                        await Application.Current.MainPage.DisplayAlert("Registration", "User Data Was Saved BUT Profile image upload failed", "ok");
                    }
                }
                InServerCall = false;

                ((App)(Application.Current)).MainPage.Navigation.PopAsync();
                }
                else
                {

                    //If the registration failed, display an error message
                    string errorMsg = "Registration failed. Please try again.";
                    await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
                }
            }
            else
            {
                string errorMsg = "Registration failed. Please fill fields appropriatly";
                await Application.Current.MainPage.DisplayAlert("Registration", errorMsg, "ok");
            }
        }
        

        //Define a method that will be called upon pressing the cancel button
        public void OnCancel()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }



        public void OnLogIn()
        {
            //Navigate back to the login page
            ((App)(Application.Current)).MainPage.Navigation.PopAsync();
        }

        public void onOwner()
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider), "Service provider cannot be null.");
            }

        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<OwnerLoginView>());
        }
        public void onTrainer()
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider), "Service provider cannot be null.");
            }

        ((App)Application.Current).MainPage.Navigation.PushAsync(serviceProvider.GetService<TrainerLoginView>());
        }

    }
}
