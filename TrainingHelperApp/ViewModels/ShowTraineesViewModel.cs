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
    public class ShowTraineesViewModel : ViewModelBase
    {
        #region Single Selection


        private Trainee selectedTrainee;
        public Trainee SelectedTrainee
        {
            get
            {
                return this.selectedTrainee;
            }
            set
            {
                this.selectedTrainee = value;
                DeleteTraineeAsync(selectedTrainee);
                OnPropertyChanged("SelectedTrainee");
            }
        }



        //private async void OnSingleSelectTraining(Trainee t)
        //{
        //    if (t != null)
        //    {
        //        var navParam = new Dictionary<string, object>
        //        {
        //            {"SelectedTraining",t }
        //        };
        //        await Shell.Current.GoToAsync("TrainingView", navParam);
        //        SelectedTraining = null;

        //    }
        //}

        #endregion

        private readonly TrainingHelperWebAPIProxy proxy;

        public ShowTraineesViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Trainees = new ObservableCollection<Trainee>();
            LoadTrainees();
        }

        public ObservableCollection<Trainee> Trainees { get; }

        private async void LoadTrainees()
        {
            var traineesList = await proxy.GetTrainees();
            Trainees.Clear();
            foreach (var trainee in traineesList)
            {
                Trainees.Add(trainee);
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ??= new Command<Trainee>(async (trainee) => await DeleteTraineeAsync(trainee));

        private async Task DeleteTraineeAsync(Trainee trainee)
        {
            if (trainee == null) return;

            bool success = await proxy.DeleteTrainee(trainee.Id);
            if (success)
            {
                Trainees.Remove(trainee);
                await App.Current.MainPage.DisplayAlert("Success", "Trainee Deactivated!", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to Deactivated trainee!", "OK");
            }
        }


    }
}
