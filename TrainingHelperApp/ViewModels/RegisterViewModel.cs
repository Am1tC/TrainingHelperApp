using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingHelperApp.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private TrainingHelperAppWebAPIProxy proxy;
        //public RegisterViewModel(TrainingHelperAppWebAPIProxy proxy)
        //{
        //    this.proxy = proxy;           
        //    RegisterCommand = new Command(OnRegister);
        //    CancelCommand = new Command(OnCancel);
        //    ShowPasswordCommand = new Command(OnShowPassword);
        //    UploadPhotoCommand = new Command(OnUploadPhoto);
        //    PhotoURL = proxy.GetDefaultProfilePhotoUrl();
        //    LocalPhotoPath = "";
        //    IsPassword = true;
        //    NameError = "Name is required";
        //    LastNameError = "Last name is required";
        //    EmailError = "Email is required";
        //    PasswordError = "Password must be at least 4 characters long and contain letters and numbers";
        //}
    }
}
