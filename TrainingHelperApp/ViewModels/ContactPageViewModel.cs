using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Services;

namespace TrainingHelperApp.ViewModels
{
    public class ContactPageViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;
        private readonly SendEmailService sendEmailService;

        public ContactPageViewModel(TrainingHelperWebAPIProxy proxy, SendEmailService sendEmailService)
        {
            this.proxy = proxy;
            this.sendEmailService = sendEmailService;

            OwnerIn = ((App)Application.Current).OwnerIn;
            TrainerIn = ((App)Application.Current).TrainerIn;

            if (OwnerIn)
                From = "Owner";
            else if (!TrainerIn)
                From = $"{((App)Application.Current).LoggedInUser.FirstName} {((App)Application.Current).LoggedInUser.LastName}";
            else
                From = $"{((App)Application.Current).LoggedInTrainer.FirstName} {((App)Application.Current).LoggedInTrainer.LastName}";

            InServerCall = false;
        }

        #region Fields and Properties

        private bool ownerIn;
        public bool OwnerIn
        {
            get => ownerIn;
            set
            {
                ownerIn = value;
                OnPropertyChanged(nameof(OwnerIn));
                OnPropertyChanged(nameof(IsToFieldVisible));
            }
        }

        private bool trainerIn;
        public bool TrainerIn
        {
            get => trainerIn;
            set
            {
                trainerIn = value;
                OnPropertyChanged(nameof(TrainerIn));
            }
        }

        public bool IsToFieldVisible => OwnerIn;

        private string from;
        public string From
        {
            get => from;
            set
            {
                from = value;
                OnPropertyChanged(nameof(From));
            }
        }

        private string to;
        public string To
        {
            get => to;
            set
            {
                to = value;
                OnPropertyChanged(nameof(To));
            }
        }

        private string subject;
        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }

        private string body;
        public string Body
        {
            get => body;
            set
            {
                body = value;
                OnPropertyChanged(nameof(Body));
            }
        }

        private string statusMessage;
        public string StatusMessage
        {
            get => statusMessage;
            set
            {
                statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        #endregion

        #region Command

        private ICommand sendEmailCommand;
        public ICommand SendEmailCommand => sendEmailCommand ??= new Command(async () => await SendEmailAsync());

        #endregion

        #region Methods

        public async Task SendEmailAsync()
        {
            if (string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(Body))
            {
                StatusMessage = "All fields are required to send an email.";
                return;
            }

            string targetEmail = OwnerIn ? To : "theaceofhearts52@gmail.com";

            var emailData = new EmailData
            {
                From = From,
                To = targetEmail,
                Subject = Subject,
                Body = Body
            };

            try
            {
                InServerCall = true;
                bool isSent = await sendEmailService.Send(emailData);
                InServerCall = false;

                if (isSent)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "You have successfully sent the message.", "OK");
                    Subject = string.Empty;
                    Body = string.Empty;
                    StatusMessage = string.Empty;
                }
                else
                {
                    StatusMessage = "Failed to send email.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        #endregion
    }
}
