using System;
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
    public class ShowTraineesViewModel : ViewModelBase
    {
      //  [QueryProperty("SelectedTraining", "SelectedTraining")]
        private readonly TrainingHelperWebAPIProxy proxy;
        public ShowTraineesViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            selectedTrainee = new Trainee();
        }
        private Trainee selectedTrainee;
        public Trainee SelectedTrainee
        {
            get => selectedTrainee;
            set
            {
                selectedTrainee = value;
                OnPropertyChanged();
            }
        }

        public async Task InitializeAsync(Trainee trainee)
        {
            selectedTrainee = trainee;
     
        }
        private ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ??= new Command(async () => await taskDeleteCommand());

        private async Task taskDeleteCommand()
        {
            try
            {
                if (SelectedTrainee == null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No trainee selected!", "OK");
                    return;
                }
                bool success = await proxy.DeleteTrainee(SelectedTrainee.Id);
                if (success)
                {
                    await App.Current.MainPage.DisplayAlert("Success", "Trainee deleted!", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Failed to delete trainee!", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Failed to delete trainee: {ex.Message}", "OK");
            }
        }


    }
}
