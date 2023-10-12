using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using OasiBank.View;
using OasiBank.ViewModel;

namespace OasiBank;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddHttpClient();

        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<RegistrationPageViewModel>();
        builder.Services.AddSingleton<HomePageViewModel>();
        builder.Services.AddSingleton<BankTransferPageViewModel>();
        builder.Services.AddSingleton<PhoneCreditRechargeViewModel>();

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<RegistrationPage>();
        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddSingleton<BankTransferPage>();
        builder.Services.AddSingleton<PhoneCreditRechargePage>();
        var app = builder.Build();

        return app;
    }
}