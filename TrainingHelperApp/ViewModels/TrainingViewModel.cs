using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp.ViewModels
{
    [QueryProperty("SelectedTraining", "SelectedTraining")]
    public class TrainingViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;


        public TrainingViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            selectedTraining = new Training();
        }
        private bool isTrainee = ((App)Application.Current).TraineeIn;

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
        private ObservableCollection<Trainee> trainees;
        public ObservableCollection<Trainee> Trainees
        {
            get
            {
                return this.trainees;
            }
            set
            {
                this.trainees = value;
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

        private ICommand orderCommand;
        public ICommand OrderCommand => orderCommand ??= new Command(async () => await OrderTraining());

        private async Task OrderTraining()
        {
            try
            {
                //Test
                AppShell shell = (AppShell)(Shell.Current);
                //AppShellViewModel shellVM = (AppShellViewModel)(shell.BindingContext);
                //Object? refreshVM = shellVM.GetTabViewModel(typeof(OrderedTrainingViewModel));
                OrderedTrainingView? refreshV = shell.GetTabPage<OrderedTrainingView>("OrderedTraining");
                if (refreshV != null)
                {
                    OrderedTrainingViewModel vm = (OrderedTrainingViewModel)refreshV.BindingContext;
                    //Call Refresh
                }
                

                //****************
                if (SelectedTraining == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No training selected!", "OK");
                    return;
                }

                // Call the proxy to handle sign-up
                bool success = await proxy.OrderTraining(SelectedTraining.TrainingNumber);

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
