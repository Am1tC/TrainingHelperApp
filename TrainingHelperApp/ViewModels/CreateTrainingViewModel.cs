using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TrainingHelper.Services;
using TrainingHelperApp.Models;
using Microsoft.Maui.Controls;

namespace TrainingHelperApp.ViewModels
{
    public class CreateTrainingViewModel : ViewModelBase
    {
        private readonly TrainingHelperWebAPIProxy proxy;
        public ObservableCollection<Trainer> Trainers { get; set; }
        public Command CreateTrainingCommand { get; }
        public Command CancelCommand { get; }

        public CreateTrainingViewModel(TrainingHelperWebAPIProxy proxy)
        {
            this.proxy = proxy;
            Trainers = new ObservableCollection<Trainer>();
            CreateTrainingCommand = new Command(OnCreateTraining);
            CancelCommand = new Command(OnCancel);
            LoadTrainers();
            LoadPlaces();
            LoadDurations();
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
        public ObservableCollection<string> Durations { get; set; }

        private string selectedDuration;
        public string SelectedDuration
        {
            get => selectedDuration;
            set
            {
                if (selectedDuration != value)
                {
                    selectedDuration = value;
                    OnPropertyChanged(nameof(SelectedDuration)); // Notify the UI
                }
            }
        }

        private void LoadDurations()
        {
            Durations = new ObservableCollection<string>
    {
        "45m",   // 45 minutes
        "1h",    // 1 hour
        "1.5h",  // 1.5 hours
        "2h",    // 2 hours
        "2.5h",  // 2.5 hours
        "3h"     // 3 hours
    };

            // Set the default selected duration to "45m"
            SelectedDuration = Durations.FirstOrDefault();

            OnPropertyChanged(nameof(Durations));  // Notify the UI
        }

        public ObservableCollection<string> Places { get; set; }

        private string selectedPlace;
        public string SelectedPlace
        {
            get => selectedPlace;
            set
            {
                if (selectedPlace != value)
                {
                    selectedPlace = value;
                    OnPropertyChanged(nameof(SelectedPlace)); // Notify the UI
                }
            }
        }

        private void LoadPlaces()
        {
            Places = new ObservableCollection<string>
    {
        "Studio A", "Studio B", "Studio C", "Studio D", "Studio E"
    };
            OnPropertyChanged(nameof(Places));  // Notify the UI
        }


        private Trainer selectedTrainer;
        public Trainer SelectedTrainer
        {
            get => selectedTrainer;
            set
            {
                if (selectedTrainer != value)
                {
                    selectedTrainer = value;
                    OnPropertyChanged(nameof(SelectedTrainer));
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
                OnPropertyChanged(nameof(ShowTrainerError));
            }
        }

        private string trainerError;
        public string TrainerError
        {
            get => trainerError;
            set
            {
                trainerError = value;
                OnPropertyChanged(nameof(TrainerError));
            }
        }

        private int maxParticipants;
        public int MaxParticipants
        {
            get => maxParticipants;
            set
            {
                if (maxParticipants != value)
                {
                    maxParticipants = value;
                    OnPropertyChanged(nameof(MaxParticipants));
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
                OnPropertyChanged(nameof(ShowMaxParticipantsError));
            }
        }

        private string maxParticipantsError;
        public string MaxParticipantsError
        {
            get => maxParticipantsError;
            set
            {
                maxParticipantsError = value;
                OnPropertyChanged(nameof(MaxParticipantsError));
            }
        }

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
                OnPropertyChanged(nameof(ShowPlaceError));
            }
        }

        private string placeError;
        public string PlaceError
        {
            get => placeError;
            set
            {
                placeError = value;
                OnPropertyChanged(nameof(PlaceError));
            }
        }

        private DateTime date = DateTime.Now;
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
                OnPropertyChanged(nameof(DateError));
            }
        }

        private bool showDateError;
        public bool ShowDateError
        {
            get => showDateError;
            set
            {
                showDateError = value;
                OnPropertyChanged(nameof(ShowDateError));
            }
        }

        private TimeSpan time = DateTime.Now.TimeOfDay;
        public TimeSpan Time
        {
            get => time;
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }
        private string timeError;
        public string TimeError
        {
            get => timeError;
            set
            {
                durationError = value;
                OnPropertyChanged(nameof(TimeError));
            }
        }

        private bool showTimeError;
        public bool ShowTimeError
        {
            get => showTimeError;
            set
            {
                showTimeError = value;
                OnPropertyChanged(nameof(ShowTimeError));
            }
        }

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
                OnPropertyChanged(nameof(DurationError));
            }
        }

        private bool showDurationError;
        public bool ShowDurationError
        {
            get => showDurationError;
            set
            {
                showDurationError = value;
                OnPropertyChanged(nameof(ShowDurationError));
            }
        }

        private bool repeatWeekly;
        public bool RepeatWeekly
        {
            get => repeatWeekly;
            set
            {
                if (repeatWeekly != value)
                {
                    repeatWeekly = value;
                    OnPropertyChanged(nameof(RepeatWeekly));
                }
            }
        }

        private int repetitionCount = 1;
        public int RepetitionCount
        {
            get => repetitionCount;
            set
            {
                if (repetitionCount != value)
                {
                    repetitionCount = value;
                    OnPropertyChanged(nameof(RepetitionCount));
                }
            }
        }

        private string repetitionCountError;
        public string RepetitionCountError
        {
            get => repetitionCountError;
            set
            {
                repetitionCountError = value;
                OnPropertyChanged(nameof(RepetitionCountError));
            }
        }

        private bool showRepetitionCountError;
        public bool ShowRepetitionCountError
        {
            get => showRepetitionCountError;
            set
            {
                showRepetitionCountError = value;
                OnPropertyChanged(nameof(ShowRepetitionCountError));
            }
        }

        private void ValidateRepetitionCount()
        {
            if (RepeatWeekly && (RepetitionCount < 1 || RepetitionCount > 52))
            {
                ShowRepetitionCountError = true;
                RepetitionCountError = "Please enter a value between 1 and 52.";
            }
            else
            {
                ShowRepetitionCountError = false;
                RepetitionCountError = "";
            }
        }


        private void ValidateTrainer()
        {
            if (SelectedTrainer == null)
            {
                ShowTrainerError = true;
                TrainerError = "Please select a trainer.";
            }
            else
            {
                ShowTrainerError = false;
                TrainerError = "";
            }
        }

        private void ValidateMaxParticipants()
        {
            if (MaxParticipants < 1)
            {
                ShowMaxParticipantsError = true;
                MaxParticipantsError = "Participants must be at least 1.";
            }
            else
            {
                ShowMaxParticipantsError = false;
                MaxParticipantsError = "";
            }
        }

        private void ValidatePlace()
        {
            if (string.IsNullOrEmpty(SelectedPlace) || SelectedPlace == "Select Studio")
            {
                ShowPlaceError = true;
                PlaceError = "Please select a valid studio.";
            }
            else
            {
                ShowPlaceError = false;
                PlaceError = "";
            }
        }


        private void ValidateDate()
        {
            if (Date < DateTime.Now)
            {
                ShowDateError = true;
                DateError = "Date must be in the future.";
            }
            else
            {
                ShowDateError = false;
                DateError = "";
            }
        }

        private void ValidateDuration()
        {
            if (string.IsNullOrEmpty(SelectedDuration) || SelectedDuration == "Select Duration")
            {
                ShowDurationError = true;
                DurationError = "Please select a valid duration.";
            }
            else
            {
                ShowDurationError = false;
                DurationError = "";
            }
        }


        public async void OnCreateTraining()
        {
            ValidateTrainer();
            ValidateMaxParticipants();
            ValidatePlace();
            ValidateDate();
            ValidateDuration();
            ValidateRepetitionCount();
            DateTime startDateTime = Date.Date + Time;
            if (ShowTrainerError || ShowMaxParticipantsError || ShowPlaceError || ShowDateError || ShowDurationError ||ShowRepetitionCountError)
            {
                await Application.Current.MainPage.DisplayAlert("Create Training", "Please fix the errors before proceeding.", "OK");
                return;
            }

            //Training training = new Training
            //{
            //    TrainerId = int.Parse(SelectedTrainer.Id),
            //    MaxParticipants = MaxParticipants,
            //    Place = SelectedPlace,
            //    Date = Date+Time,
            //    Duration = SelectedDuration,
            //    Trainer = SelectedTrainer,
            //    Picture =""

            //};
            int repeat = RepeatWeekly ? RepetitionCount : 1;
            bool anyFailed = false;

            for (int i = 0; i < repeat; i++)
            {
                var training = new Training
                {
                    TrainerId = int.Parse(SelectedTrainer.Id),
                    MaxParticipants = MaxParticipants,
                    Place = SelectedPlace,
                    Date = startDateTime.AddDays(i * 7),
                    Duration = SelectedDuration,
                    Trainer = SelectedTrainer,
                    Picture = ""
                };

                var createdTraining = await proxy.CreateTrainingAsync(training);
                if (createdTraining == null)
                {
                    anyFailed = true;
                }
            }

            if (!anyFailed)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Trainings created successfully!", "OK");
                await Shell.Current.GoToAsync("Events");
                OnCancel();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Some trainings could not be created.", "OK");
            }



            ////var createdTraining = await proxy.CreateTrainingAsync(training);
            //if (createdTraining != null)
            //{
            //    await Application.Current.MainPage.DisplayAlert("Success", "Training created successfully!", "OK");
            //    await Shell.Current.GoToAsync("Events");
            //    OnCancel();
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "Could not create training.", "OK");
            //    anyFailed = true;
            //}
        }

        public void OnCancel()
        {
            SelectedTrainer = null;
            MaxParticipants = 0;
            Place = null;
            Date = DateTime.Now;
            Duration = string.Empty;
        }
    }
}
