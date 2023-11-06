using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using PayPals.UI.Interfaces;
using PayPals.UI.Services;
using PayPals.UI.ViewModels;
using PayPals.UI.Views;

namespace PayPals.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMopups()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<IRestService, RestService>();
            builder.Services.AddSingleton<ILoginService, LoginService>();
            builder.Services.AddSingleton<IStorageService, StorageService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IGroupService, GroupService>();
            builder.Services.AddSingleton<LoadingPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<RegisterPage>();
            builder.Services.AddSingleton<HomePage>();
            builder.Services.AddSingleton<GroupsPage>();
            builder.Services.AddSingleton<ExpensesPage>();
            builder.Services.AddSingleton<PalsPage>();
            builder.Services.AddSingleton<ProfilePage>();
            builder.Services.AddSingleton<GroupDetailsPage>();
            builder.Services.AddSingleton<GroupViewModel>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}