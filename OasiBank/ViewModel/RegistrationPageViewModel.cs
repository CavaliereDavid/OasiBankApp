using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using OasiBank.Classes;
using OasiBank.Model;
using OasiBank.View;
using System.Text;
using System.Text.RegularExpressions;

namespace OasiBank.ViewModel
{
    public partial class RegistrationPageViewModel : ObservableObject
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegistrationPageViewModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string surname;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string confirmPassword;

        [RelayCommand]
        Task Back() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        async Task NavigateToLoginPage() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Surname) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "All fields are required.", "OK");
                return;
            }

            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Enter a valid email address.", "OK");
                return;
            }

            string passwordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$";
            if (!Regex.IsMatch(Password, passwordPattern))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "The password must contain at least 8 characters, at least one capital letter, at least one number, and at least one symbol (@#$%^&+=).", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Password mismatch", "OK");
                return;
            }

            await MakeRegistration();
        }

        private async Task MakeRegistration()
        {
            try
            {
                // Create a new HttpClient instance using the factory.
                HttpClient httpClient = _httpClientFactory.CreateClient();

                // Define your API URL for registration.
                string apiURL = "https://project-work.azurewebsites.net/api/register";

                RegistrationModel registrationRequest = new()
                {
                    name = Name,
                    surname = Surname,
                    email = Email,
                    password = Password,
                };

                string jsonData = JsonConvert.SerializeObject(registrationRequest);

                // Create the HTTP content with the JSON data.
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Send a POST request to the API.
                HttpResponseMessage response = await httpClient.PostAsync(apiURL, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Parse the registration response (assuming it's similar to the Postman response).
                    var responseObject = JsonConvert.DeserializeObject<RegistrationResponse>(responseContent);

                    // Access the necessary data from responseObject if needed
                    // e.g., var user = responseObject.user;

                    await Application.Current.MainPage.DisplayAlert("Success", "Registration successful!", "OK");

                    // Redirect to the home page after successful registration.
                    await NavigateToLoginPage();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Email already exists.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "An error occurred: " + ex.Message, "OK");
            }
        }
    }
}
