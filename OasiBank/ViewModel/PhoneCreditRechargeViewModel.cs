using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OasiBank.Classes;

namespace OasiBank.ViewModel
{
    public partial class PhoneCreditRechargeViewModel : ObservableObject
    {
        [ObservableProperty]
        string phoneNumber;

        [ObservableProperty]
        string senderIban;

        [ObservableProperty]
        string amount;

        [RelayCommand]
        async Task RechargeCredit()
        {
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                await ShowErrorAlert("Error", "Please enter a phone number.");
                return;
            }

            if (string.IsNullOrEmpty(SenderIban))
            {
                await ShowErrorAlert("Error", "Please enter an IBAN.");
                return;
            }

            if (
                string.IsNullOrEmpty(Amount)
                || !float.TryParse(Amount, out float rechargeAmount)
                || rechargeAmount <= 0
            )
            {
                await ShowErrorAlert("Error", "Please enter a valid positive amount.");
                return;
            }

            await MakeCreditRecharge();
        }

        private async Task MakeCreditRecharge()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl =
                        "https://project-work.azurewebsites.net/api/transactions/phone-recharge";

                    var rechargeRequest = new
                    {
                        phoneNumber = PhoneNumber,
                        senderIban = SenderIban,
                        amount = Amount
                    };

                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Bearer",
                            TokenProvider.Token
                        );

                    string jsonData = JsonConvert.SerializeObject(rechargeRequest);

                    StringContent content = new StringContent(
                        jsonData,
                        System.Text.Encoding.UTF8,
                        "application/json"
                    );

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var toast = Toast.Make(
                            "Credit recharge successful",
                            ToastDuration.Short,
                            12
                        );
                        await toast.Show();
                        return;
                    }
                    else
                    {
                        await ShowErrorAlert("Error", "Credit recharge failed.");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAlert("Error", ex.Message);
                return;
            }
        }

        private async Task ShowErrorAlert(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "OK");
        }
    }
}
