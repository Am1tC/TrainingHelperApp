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
    public class EventsViewModel:ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;

        public EventsViewModel(TrainingHelperWebAPIProxy proxy) 
        {
            this.proxy = proxy;
            Trainings = new ObservableCollection<Training>();
            unfilterdtrainings = new List<Training>();
            ReadTrainings();
           // DateOptions = new ObservableCollection<DateTime>();//


        }
        private List<Training> unfilterdtrainings;

        //public ObservableCollection<DateTime> DateOptions { get; private set; } //

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

        //private void InitializeDateOptions()
        //{
        //    // Generate a range of dates (e.g., 7 days starting from today)
        //    for (int i = 0; i < 7; i++)
        //    {
        //        DateOptions.Add(DateTime.Now.AddDays(i));
        //    }
        //}


        private async void ReadTrainings()
        {
            List<Training> list = await proxy.GetTrainings();
            //foreach (Training p in list)
            //{
            //    p.PostPicturePath = proxy.GetImagesBaseAddress() + p.PostPicturePath;
            //}
            if (list != null)
                this.unfilterdtrainings = list;
            filterTrainings();
        }


        #region Single Selection


        private Training selectedTraining;
        public Training SelectedTraining
        {
            get
            {
                return this.selectedTraining;
            }
            set
            {
                this.selectedTraining = value;
                OnSingleSelectTraining(selectedTraining);
                OnPropertyChanged("SelectedTraining");
            }
        }



        private async void OnSingleSelectTraining(Training t)
        {
            if (t != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"SelectedTraining",t }
                };
                await Shell.Current.GoToAsync("TrainingView", navParam);
                SelectedTraining = null;

            }
        }

        #endregion

        private DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                filterTrainings(); // Filter trainings when the date changes
                OnPropertyChanged();
            }
        }

        private void filterTrainings()
        {
            DateTime date = SelectedDate;
            trainings.Clear();
            foreach (var training in unfilterdtrainings)
            {
                if (training.Date.Day == date.Date.Day &&
                    training.Date.Month == date.Date.Month &&
                    training.Date.Year == date.Date.Year)
                {
                    trainings.Add(training);
                }
            }

        }
    }
}
