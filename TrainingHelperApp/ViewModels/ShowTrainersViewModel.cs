using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingHelper.Services;
using TrainingHelperApp.Models;

namespace TrainingHelperApp.ViewModels
{
    public class ShowTrainersViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;
        public ObservableCollection<Trainer> Trainers { get; set; }
        private Trainer selectedTrainer;

        public Trainer SelectedTrainer
        {
            get => selectedTrainer;
            set
            {
                selectedTrainer = value;
                OnPropertyChanged();
            }
        }

        public ShowTrainersViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Trainers = new ObservableCollection<Trainer>();
            LoadTrainers();
        }

        private async void LoadTrainers()
        {
            var trainers = await proxy.GetTrainers();
            Trainers.Clear();
            foreach (var trainer in trainers)
            {
                Trainers.Add(trainer);
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ??= new Command<Trainer>(async (trainer) => await DeleteTrainer(trainer));

        private async Task DeleteTrainer(Trainer trainer)
        {
            if (trainer == null) return;

            bool success = await proxy.DeleteTrainer(trainer.Id); //make sure same id is used in the backend
            if (success)
            {
                Trainers.Remove(trainer);
                await App.Current.MainPage.DisplayAlert("Success", "Trainer deleted!", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to delete trainer!", "OK");
            }
        }
    }
}
