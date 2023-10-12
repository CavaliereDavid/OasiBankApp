using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OasiBank.Classes;

namespace OasiBank.ViewModel
{
    public partial class BankTransferPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string iban;

        [ObservableProperty]
        string amount;

        [RelayCommand]
        async Task BankTransfer()
        {
            if (string.IsNullOrEmpty(Iban))
            {
                await ShowErrorAlert("Error", "Please enter an IBAN.");
                return;
            }

            if (string.IsNullOrEmpty(Amount) || !float.TryParse(Amount, out float transferAmount) || transferAmount <= 0)
            {
                await ShowErrorAlert("Error", "Please enter a valid positive amount.");
                return;
            }

            await MakeBankTransfer();
        }

        private async Task MakeBankTransfer()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string apiUrl = "https://project-work.azurewebsites.net/api/transactions/bank-transfer";

                    var transferRequest = new
                    {
                        receiverIBAN = Iban,
                        amount = Amount
                    };

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenProvider.Token);


                    string jsonData = JsonConvert.SerializeObject(transferRequest);

                    StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    string responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var toast = Toast.Make("Bank transfer successful", ToastDuration.Short, 12);
                        await toast.Show();
                        return;
                    }
                    else
                    {
                        await ShowErrorAlert("Error", "Bank transfer failed.");
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
