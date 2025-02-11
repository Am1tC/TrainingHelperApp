
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TrainingHelper.Services;
using TrainingHelperApp.Models;

namespace TrainingHelperApp.ViewModels
{
    public class OrderedTrainingViewModel:ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;

        public OrderedTrainingViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Trainings = new ObservableCollection<Training>();
            unfilteredTrainings = new List<Training>();
            ReadTrainings();
        }

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

        private async void ReadTrainings()
        {
            List<Training> list = await proxy.GetTrainings();
            if (list != null)
                this.unfilteredTrainings = list;
            SortTrainings(); // Order the trainings as soon as they are fetched
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


    }
}
