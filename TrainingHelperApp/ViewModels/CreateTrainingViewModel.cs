using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using TrainingHelperApp.Views;

namespace TrainingHelperApp.ViewModels
{
    public class CreateTrainingViewModel : ViewModelBase
    {
        private TrainingHelperWebAPIProxy proxy;
        public CreateTrainingViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            CreateTrainingCommand = new Command(OnCreateTraining);
            CancelCommand = new Command(OnCancel);
          //  UploadPhotoCommand = new Command(OnUploadPhoto);


        }

        #region Properties      

        #region Trainer

        private int trainerId;
        public int TrainerId
        {
            get => trainerId;
            set
            {
                if (trainerId != value)
                {
                    trainerId = value;
                    OnPropertyChanged(nameof(TrainerId));
                }
            }
        }

        private bool showTrainerError;

        public bool ShowTrainerError
        {
            get => showTrainerError;
            set
            {
                showTrainerError = value;
                OnPropertyChanged("ShowTrainerError");
            }
        }

        private string trainerError;

        public string TrainerError
        {
            get => trainerError;
            set
            {
                trainerError = value;
                OnPropertyChanged("TrainerError");
            }
        }
        //makes sure the trainer is valid before creating the training and exsists in the database - Is done in controller
        private void validateTrainer()// 
        {
            if (TrainerId < 1000)
            {
                ShowTrainerError = true;
                TrainerError = "Trainer ID is invalid";
            }
            else
            {
                ShowTrainerError = false;
                TrainerError = "";
            }

        }

        #endregion

        #region MaxParticipants


        private int Maxparticipants;
        public int MaxParticipants
        {
            get => Maxparticipants;
            set
            {
                if (Maxparticipants != value)
                {
                    Maxparticipants = value;
                    OnPropertyChanged(nameof(TrainerId));
                }
            }
        }

        private bool showMaxParticipantsError;

        public bool ShowMaxParticipantsError
        {
            get => showMaxParticipantsError;
            set
            {
                showMaxParticipantsError = value;
                OnPropertyChanged("ShowMaxParticipantsError");
            }
        }

        private string MaxparticipantsError;

        public string MaxParticipantsError
        {
            get => MaxparticipantsError;
            set
            {
                MaxparticipantsError = value;
                OnPropertyChanged("MaxParticipantsError");
            }
        }

        private void validateParticipants()// 
        {
            if (MaxParticipants < 1)
            {
                ShowMaxParticipantsError = true;
                MaxParticipantsError = "Participants must be at least 1";
            }
            else
            {
                ShowMaxParticipantsError = false;
                MaxParticipantsError = "";
            }

        }

        #endregion

        #region place
        private string place;
        public string Place
        {
            get => place;
            set
            {
                if (place != value)
                {
                    place = value;
                    OnPropertyChanged(nameof(Place));
                }
            }
        }

        private bool showPlaceError;
        public bool ShowPlaceError
        {
            get => showPlaceError;
            set
            {
                showPlaceError = value;
                OnPropertyChanged("ShowPlaceError");
            }
        }

        private string placeError;
        public string PlaceError
        {
            get => placeError;
            set
            {
                placeError = value;
                OnPropertyChanged("PlaceError");
            }
        }

        private void validatePlace()// 
        {
            if (string.IsNullOrEmpty(Place))
            {
                ShowPlaceError = true;
                PlaceError = "Place must be filled out";
            }
            else
            {
                ShowPlaceError = false;
                PlaceError = "";
            }
        }

        #endregion

        #region Date
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        private string dateError;
        public string DateError
        {
            get => dateError;
            set
            {
                dateError = value;
                OnPropertyChanged("DateError");
            }
        }

        private bool showDateError;
        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged("ShowDateError");
            }
        }

        private void validateDate()// 
        {
            if (Date < DateTime.Now)
            {
                ShowDateError = true;
                DateError = "Date must be in the future";
            }
            else
            {
                ShowDateError = false;
                DateError = "";
            }
        }

        #endregion

        #region Duration
        private string duration;
        public string Duration
        {
            get => duration;
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }
        private string durationError;
        public string DurationError
        {
            get => durationError;
            set
            {
                durationError = value;
                OnPropertyChanged("DurationError");
            }
        }

        private bool showDurationError;
        public bool ShowDurationError
        {
            get => showDurationError;
            set
            {
                showDurationError = value;
                OnPropertyChanged("ShowDurationError");
            }
        }

        public void validateDuration()// 
        {
            if (string.IsNullOrEmpty(Duration))
            {
                showDurationError = true;
                DurationError = "Duration must be filled out";
            }
            else
            {
                showDurationError = false;
                DurationError = "";
            }
        }


        #endregion

        //need to add picture and upload photo

        #endregion

        #region logic
        public Command CreateTrainingCommand { get; }
        public Command CancelCommand { get; }
       // public Command UploadPhotoCommand { get; }

        public async void OnCreateTraining()
        {
            validateTrainer();
            validateParticipants();
            validatePlace();
            validateDate();
            validateDuration();

            if (ShowTrainerError || ShowMaxParticipantsError || ShowPlaceError || ShowDateError || ShowDurationError)
            {
                string errorMsg = "Creation failed. Please try again and make sure all fields are valid.";

                await Application.Current.MainPage.DisplayAlert("Create Training", errorMsg, "ok");
            }
            else
            {
                Training training = new Training
                {
                    TrainerId = TrainerId,
                    MaxParticipants = MaxParticipants,
                    Place = Place,
                    Date = Date,
                    Duration = Duration
                };

                InServerCall = true;
                training = await proxy.CreateTrainingAsync(training); //needs further imp after picture will be added
                if (training != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Create Training", "Training created successfully", "ok");
                    InServerCall = false;
                    await Shell.Current.GoToAsync("EventsView");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Create Training", "No such Trainer", "ok");
                    InServerCall = false;
                }
                InServerCall = false;

            }
          
        }

        public async void OnCancel()
        {
            TrainerId = 0;
            MaxParticipants = 0;
            Place = "";
            Date = DateTime.Now;
            duration = "";
        }
        #endregion





    }
}
