﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingHelper.Services;
using TrainingHelperApp.Services;
using TrainingHelperApp.ViewModels;
using System.Collections.ObjectModel;
using TrainingHelperApp.Models;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;


namespace TrainingHelperApp.ViewModels
{
    public class ContactPageViewModel : ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;
        private SendEmailService sendEmailService;
        public bool OwnerIn = ((App)Application.Current).OwnerIn;
        public bool TrainerIn = ((App)Application.Current).TrainerIn;

        public ContactPageViewModel (TrainingHelperWebAPIProxy proxy, SendEmailService sendEmailService)
        {
            this.proxy = proxy;
            this.sendEmailService = sendEmailService;
            if(!OwnerIn)
             To = ((App)Application.Current).LoggedInUser.Email;
            else if(TrainerIn)
                To = ((App)Application.Current).LoggedInTrainer.Email;

            sentEmails = new ObservableCollection<EmailData>();
        }

        private string from;
        private string to;
        private string subject;
        private string body;
        private string statusMessage;
        private ObservableCollection<EmailData> sentEmails; //if i want to see history

        #region properties
        public string From
        {
            get => from;
            set
            {
                if (OwnerIn)
                    from = "Owner";
                else
                    from = ((App)Application.Current).LoggedInUser.FirstName + " " +((App)Application.Current).LoggedInUser.LastName;

                //OnPropertyChanged(nameof(From));
            }
        }

        public string To
        {
            get => to;
            set
            {
                to = value;
                //OnPropertyChanged(nameof(To));
            }
        }

        public string Subject
        {
            get => subject;
            set
            {
                subject = value;
                OnPropertyChanged(nameof(Subject));
            }
        }

        public string Body
        {
            get => body;
            set
            {
                body = value;
                OnPropertyChanged(nameof(Body));
            }
        }

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
        public ObservableCollection<EmailData> SentEmails => sentEmails;

        // Commands or Actions
        private ICommand sendEmailCommand;
        public ICommand SendEmailCommand => sendEmailCommand ??= new Command(async () => await SendEmailAsync());

        public async Task SendEmailAsync()
        {

            if ( string.IsNullOrWhiteSpace(Subject) || string.IsNullOrWhiteSpace(Body))
            {
                StatusMessage = "All fields are required to send an email.";
                return;
            }

            var emailData = new EmailData
            {
                From = From,
                To = To,
                Subject = Subject,
                Body = Body
            };

            try
            {
                bool isSent = await sendEmailService.Send(emailData);
                if (isSent)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "You have successfully sent the message.", "OK");
                    StatusMessage = "";
                    sentEmails.Add(emailData);
                    From = "";
                    Subject = "";
                    Body = "";
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

        //public async Task LoadEmailsAsync()
        //{
        //    try
        //    {
        //        // Assuming `proxy` has a method to fetch email data.
        //        var emails = await proxy.GetEmailsAsync();
        //        foreach (var email in emails)
        //        {
        //            SentEmails.Add(email);
        //        }

        //        StatusMessage = "Emails loaded successfully.";
        //    }
        //    catch (Exception ex)
        //    {
        //        StatusMessage = $"Error loading emails: {ex.Message}";
        //    }
        //}
    }
}

