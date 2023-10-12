using OasiBank.View;

namespace OasiBank;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(BankTransferPage), typeof(BankTransferPage));  
        Routing.RegisterRoute(nameof(PhoneCreditRechargePage), typeof(PhoneCreditRechargePage));
        
    }
}