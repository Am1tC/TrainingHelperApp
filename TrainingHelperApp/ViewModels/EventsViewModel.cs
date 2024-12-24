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
    public class EventsViewModel:ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;

        public EventsViewModel(TrainingHelperWebAPIProxy proxy) 
        {
            this.proxy = proxy;
        }
        private List<Training> unfilterdtrainings;

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

        //private DatePicker filter;
        //public string Filter
        //{
        //    get => filter;
        //    set
        //    {
        //        filter = value;
        //        filterTrainings();
        //        OnPropertyChanged();
        //    }
        //}


        private async void ReadTrainings()
        {
            List<Training> list = await proxy.GetTrainings();
            //foreach (Training p in list)
            //{
            //    p.PostPicturePath = proxy.GetImagesBaseAddress() + p.PostPicturePath;
            //}
            this.Trainings = new ObservableCollection<Training>(list);
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
                OnPropertyChanged();
            }
        }



        private async void OnSingleSelectTraining(Training t)
        {
            if (t != null)
            {
                var navParam = new Dictionary<string, object>
                {
                    {"selectedTraining",t }
                };
                await Shell.Current.GoToAsync("TrainingView", navParam);
                //SelectedTraining = null;

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
                filterTrainings(selectedDate); // Filter trainings when the date changes
                OnPropertyChanged();
            }
        }

        private void filterTrainings(DateTime date)
        {
            trainings.Clear();
            foreach (var training in unfilterdtrainings)
            {
                if(training.Date == date)
                {
                    trainings.Add(new Training()
                    {
                        TrainerId =training.TrainerId,
                        Duration = training.Duration,
                        MaxParticipants = training.MaxParticipants,
                        Picture = training.Picture,
                        Place = training.Place,
                        Trainer = training.Trainer,
                        TrainingPictures = training.TrainingPictures,


                            
                    });
                }
            }

        }
    }
}
