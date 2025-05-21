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
    public class ShowTraineesViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;

        public ShowTraineesViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            allTrainees = new List<Trainee>();
            FilteredTrainees = new ObservableCollection<Trainee>();
            LoadTrainees();
        }

        #region Properties

        private List<Trainee> allTrainees;

        public ObservableCollection<Trainee> FilteredTrainees { get; }

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
                    FilterTrainees(); // Filter on typing
                }
            }
        }

        private Trainee selectedTrainee;
        public Trainee SelectedTrainee
        {
            get => selectedTrainee;
            set
            {
                selectedTrainee = value;
                DeleteTraineeAsync(selectedTrainee);
                OnPropertyChanged(nameof(SelectedTrainee));
            }
        }

        #endregion

        #region Load & Filter

        private async void LoadTrainees()
        {
            var traineesList = await proxy.GetTrainees();
            allTrainees = traineesList.ToList();
            FilterTrainees();
        }

        private void FilterTrainees()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? allTrainees
                : allTrainees.Where(t =>
                    $"{t.FirstName} {t.LastName}".ToLower().Contains(SearchText.ToLower()))
                    .ToList();

            FilteredTrainees.Clear();
            foreach (var trainee in filtered)
            {
                FilteredTrainees.Add(trainee);
            }
        }

        #endregion

        #region Commands

        private ICommand deleteCommand;
        public ICommand DeleteCommand => deleteCommand ??= new Command<Trainee>(async (trainee) => await DeleteTraineeAsync(trainee));

        public ICommand FilterCommand => new Command(FilterTrainees);

        private async Task DeleteTraineeAsync(Trainee trainee)
        {
            if (trainee == null) return;

            bool success = await proxy.DeleteTrainee(trainee.Id);
            if (success)
            {
                allTrainees.Remove(trainee);
                FilterTrainees();
                await App.Current.MainPage.DisplayAlert("Success", "Trainee Deactivated!", "OK");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to deactivate trainee!", "OK");
            }
        }

        #endregion
    }
}
