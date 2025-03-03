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

            if (ShowTrainerError || ShowMaxParticipantsError || ShowPlaceError || ShowDateError || ShowDurationError)
            {
                await Application.Current.MainPage.DisplayAlert("Create Training", "Please fix the errors before proceeding.", "OK");
                return;
            }

            Training training = new Training
            {
                TrainerId = int.Parse(SelectedTrainer.Id),
                MaxParticipants = MaxParticipants,
                Place = SelectedPlace,
                Date = Date,
                Duration = SelectedDuration,
                Trainer = SelectedTrainer

            };

            var createdTraining = await proxy.CreateTrainingAsync(training);
            if (createdTraining != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Training created successfully!", "OK");
                await Shell.Current.GoToAsync("EventsView");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Could not create training.", "OK");
            }
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
