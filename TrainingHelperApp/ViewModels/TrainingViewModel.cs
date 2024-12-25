using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingHelper.Services;
using TrainingHelperApp.Models;

namespace TrainingHelperApp.ViewModels
{
    public class TrainingViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;

        public TrainingViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
        }

        private Training selectedTraining;
        public Training SelectedTraining
        {
            get => selectedTraining;
            set
            {
                selectedTraining = value;
                OnPropertyChanged();
            }
        }

        public async Task InitializeAsync(Training training)
        {
            SelectedTraining = training;

            // You can fetch additional training details from the server if needed.
            // Example:
            // SelectedTraining = await proxy.GetTrainingById(training.TrainingNumber);
        }

        private ICommand signUpCommand;
        public ICommand SignUpCommand => signUpCommand ??= new Command(async () => await SignUpForTraining());

        private async Task SignUpForTraining()
        {
            try
            {
                if (SelectedTraining == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No training selected!", "OK");
                    return;
                }

                // Call the proxy to handle sign-up
                bool success = await proxy.SignUpForTraining(SelectedTraining.TrainingNumber);

                if (success)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "You have successfully signed up for the training.", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to sign up for the training. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
