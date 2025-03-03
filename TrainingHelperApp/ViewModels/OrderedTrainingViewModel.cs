using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TrainingHelper.Services;
using TrainingHelperApp.Models;

namespace TrainingHelperApp.ViewModels
{
    public class OrderedTrainingViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;

        public OrderedTrainingViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Trainings = new ObservableCollection<Training>();
            unfilteredTrainings = new List<Training>();
            ReadTrainingsAsync();
        }
     //   public string Error = "No Trainings";
       
        private List<Training> unfilteredTrainings;

        private ObservableCollection<Training> trainings;
        public ObservableCollection<Training> Trainings
        {
            get
            {
                return this.trainings;
            }
            set
            {
                this.trainings = value;
                OnPropertyChanged();
            }
        }

        private async void ReadTrainingsAsync()
        {
            List<Training> list = await proxy.GetUserEvents();
            if (list != null)
            {
                this.unfilteredTrainings = list;
                SortTrainings(); // Order the trainings as soon as they are fetched
            }
            else
            {
                // Handle the case where no data is returned (optional)
                Console.WriteLine("No trainings were returned.");
            }
        }

        private void SortTrainings()
        {
            // Sort trainings by date (can be modified if needed)
            var orderedTrainings = unfilteredTrainings.OrderBy(t => t.Date).ToList();

            Trainings.Clear();
            foreach (var training in orderedTrainings)
            {
                Trainings.Add(training);
            }
        }

        public override void Refresh()
        {
            ReadTrainingsAsync();
        }
    }
}
