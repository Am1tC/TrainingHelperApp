using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using TrainingHelper.Services;
using TrainingHelperApp.ViewModels;
using TrainingHelperApp.Views;
//using SpriteKit;

namespace TrainingHelperApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .RegisterDataServices()
                .RegisterPages()
                .RegisterViewModels();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
        {
           
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<AddTrainerView>();
            builder.Services.AddTransient<EventsView>();
            builder.Services.AddTransient<OwnerView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<EventsView>();
            //  builder.Services.AddTransient<conact>();
            builder.Services.AddTransient<TrainingView>();

            return builder;
        }

        public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<TrainingHelperWebAPIProxy>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<AddTrainerViewModel>();
            builder.Services.AddTransient<EventsViewModel>();
            builder.Services.AddTransient<OwnerViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<ContactPageViewModel>();
            builder.Services.AddTransient<TrainingViewModel>();

            return builder;
        }
    }
}
