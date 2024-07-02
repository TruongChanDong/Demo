using Demo.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _userName;

        [ObservableProperty]
        private string? _password;

        [ObservableProperty]
        private string? _message;

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:8080/"),
        };

        [RelayCommand]
        private async Task Login()
        {
            using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    phone = $"{UserName}",
                    password = $"{Password}"
                }),
                Encoding.UTF8,
                "application/json");

            using HttpResponseMessage response = await httpClient.PostAsync(
                "login",
                jsonContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = new User();
            try
            {
                jsonResponse = await response.Content.ReadFromJsonAsync<User>();
            }
            catch (Exception ex)
            {
                Message = "Invalid Username or Password!";
                jsonResponse = null;
            }
            if (jsonResponse != null) {
                /*Message = $"Welcome back User: {jsonResponse.Name}\n" +
                    $"User ID: {jsonResponse.UserId}";*/
                Message = jsonResponse.ToString();
            }
        }
    }
}
