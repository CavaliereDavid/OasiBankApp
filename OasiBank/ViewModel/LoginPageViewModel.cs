using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OasiBank.Classes;
using OasiBank.Model;
using OasiBank.View;
using System.Text.RegularExpressions;

namespace OasiBank.ViewModel
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginPageViewModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        bool isLoginSuccessful;

        [RelayCommand]
        Task NavigateToHomePage() => Shell.Current.GoToAsync($"{nameof(HomePage)}");

        [RelayCommand]
        Task NavigateToRegistrationPage() => Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "One or more fields are empty", "OK");
                return;
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Insert a valid email address", "OK");
                return;
            }

            LoginModel loginModel = new()
            {
                email = Email,
                password = Password
            };

            bool loginResult = await MakeLogin(loginModel);

            if (loginResult)
            {
                IsLoginSuccessful = true;
                await NavigateToHomePage();
            }
        }

        private async Task<bool> MakeLogin(LoginModel loginModel)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();

                string apiURL = "https://project-work.azurewebsites.net/api/login";

                string jsonData = JsonConvert.SerializeObject(loginModel);


                StringContent content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(apiURL, content);

                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var responseObject = System.Text.Json.JsonSerializer.Deserialize<UserToken>(responseContent);
                    TokenProvider.Token = responseObject.token;

                    await Application.Current.MainPage.DisplayAlert("Success", "The login was succesfull.", "OK");

                    return true; 
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Wrong credentials.", "OK");
                    return false; 
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "There was an error: " + ex.Message, "OK");
                return false; 
            }
        }
    }
}
