﻿using System.Windows.Input;
using TrainingHelperApp.ViewModels;
using TrainingHelperApp.Views;

namespace TrainingHelperApp
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {        
            Routing.RegisterRoute("Profile", typeof(ProfileView));              // disabled when owner comes up
            Routing.RegisterRoute("ContactPage", typeof(ContactPageView));
            Routing.RegisterRoute("TrainingView", typeof(TrainingView));
            Routing.RegisterRoute("Events", typeof(EventsView));

            Routing.RegisterRoute("Owner", typeof(OwnerView));                   //
            Routing.RegisterRoute("AddTrainer", typeof(AddTrainerView));         // these are only owner visible
            Routing.RegisterRoute("CreateTraining", typeof(CreateTrainingView));//
            Routing.RegisterRoute("ShowTrainees", typeof(ShowTraineesView));
            Routing.RegisterRoute("ShowTrainers", typeof(ShowTrainersView));


        }
        public ICommand LogOutCommand { get; set; }
        private void OnLogOut()
        {
            DisplayAlert("Log out", "Are you sure you want to log out", "Ok");
            
        }

        public T? GetTabPage<T>(string route) where T : Page
        {
            
            ShellContent? item = this.Items.OfType<ShellItem>().SelectMany(item => item.Items)
                                                 .OfType<ShellSection>()
                                                 .SelectMany(section => section.Items)
                                                 .FirstOrDefault(content => content.Route == route);

            return item?.Content as T;
        }

        public event Action<Type> DataChanged;
        public void Refresh(Type type)
        {
            if (DataChanged != null)
            {
                DataChanged(type);
            }
        }
    }
}
