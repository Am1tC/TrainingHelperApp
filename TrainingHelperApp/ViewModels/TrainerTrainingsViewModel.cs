using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingHelper.Services;
using TrainingHelperApp.Models;

namespace TrainingHelperApp.ViewModels
{
    public class TrainerTrainingsViewModel:ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;

        public TrainerTrainingsViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Trainings = new ObservableCollection<Training>();
            unfilteredTrainings = new List<Training>();
            ReadTrainerTrainings();
        }

        private List<Training> unfilteredTrainings;

        private ObservableCollection<Training> trainings;
        public ObservableCollection<Training> Trainings
        {
            get { return this.trainings; }
            set
            {
                this.trainings = value;
                OnPropertyChanged();
            }
        }

        // Fetch the trainings for the trainer using the GetTrainerEvents task
        private async void ReadTrainerTrainings()
        {
            List<Training> list = await proxy.GetTrainerEvents(); // Using the GetTrainerEvents task
            if (list != null)
                this.unfilteredTrainings = list;
            SortTrainings(); // Order the trainings by date once fetched
        }

        // Sort trainings by date
        private void SortTrainings()
        {
            // Sort trainings by date
            var orderedTrainings = unfilteredTrainings.OrderBy(t => t.Date).ToList();

            Trainings.Clear();
            foreach (var training in orderedTrainings)
            {
                Trainings.Add(training);
            }
        }
    }
}
