using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TrainingHelper.Services;
using TrainingHelperApp.Models;

namespace TrainingHelperApp.ViewModels
{
    public class ShowTrainersViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;

        public ShowTrainersViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            allTrainers = new List<Trainer>();
            FilteredTrainers = new ObservableCollection<Trainer>();
            LoadTrainers();
        }

        #region Properties

        private List<Trainer> allTrainers;

        public ObservableCollection<Trainer> FilteredTrainers { get; }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterTrainers();
                }
            }
        }

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

        #endregion

        #region Load & Filter

        private async void LoadTrainers()
        {
            var trainers = await proxy.GetTrainers();
            allTrainers = trainers.ToList();
            FilterTrainers();
        }

        private void FilterTrainers()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? allTrainers
                : allTrainers.Where(t =>
                    $"{t.FirstName} {t.LastName}".ToLower().Contains(SearchText.ToLower()))
                    .ToList();

            FilteredTrainers.Clear();
            foreach (var trainer in filtered)
            {
                FilteredTrainers.Add(trainer);
            }
        }

        #endregion

        #region Commands

        private ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ??= new Command<Trainer>(async (trainer) => await DeleteTrainer(trainer));

        public ICommand FilterCommand => new Command(FilterTrainers);

        private async Task DeleteTrainer(Trainer trainer)
        {
            if (trainer == null) return;

            bool success = await proxy.DeleteTrainer(trainer.Id);
            if (success)
            {
                allTrainers.Remove(trainer);
                FilterTrainers();
                await App.Current.MainPage.DisplayAlert("Success", "Trainer deactivated!", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to deactivate trainer!", "OK");
            }
        }

        #endregion
    }
}
