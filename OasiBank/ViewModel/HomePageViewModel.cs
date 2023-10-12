using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OasiBank.Classes;
using OasiBank.View;

namespace OasiBank.ViewModel;

public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    User user;

    [ObservableProperty]
    string iban;

    [ObservableProperty]
    List<Transaction> transactions;

    [RelayCommand]
    Task Back() => Shell.Current.GoToAsync("..");

    [RelayCommand]
    Task NavigateToBankTransferPage() => Shell.Current.GoToAsync($"{nameof(BankTransferPage)}");

    [RelayCommand]
    Task NavigateToPhoneCreditRechargePage() => Shell.Current.GoToAsync($"{nameof(PhoneCreditRechargePage)}");
}
