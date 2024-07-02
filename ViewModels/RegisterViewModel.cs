using CommunityToolkit.Mvvm.Messaging;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _userName;

        [ObservableProperty]
        private string? _password;

        [ObservableProperty]
        private string? _confirmPassword;

        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _phone;

        [ObservableProperty]
        private string? _address;

        [ObservableProperty]
        private string? _message;

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:8080/"),
        };

        [RelayCommand]
        private async Task CreateUser()
        {
            using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    name = $"{UserName}",
                    phone = $"{Phone}",
                    password = $"{Password}",
                    email = $"{Email}",
                    roleId = 0,
                    address = $"{Address}"
                }),
                Encoding.UTF8, "application/json");

            string jsonResponse = "";
            try
            {
                using HttpResponseMessage response = await httpClient.PostAsync("user", jsonContent);
                response.EnsureSuccessStatusCode();
                jsonResponse = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Message = "Sign Up Failed!";
            }
            Message = jsonResponse.ToString();
            if (jsonResponse.Contains("Created account"))
            {
                Message = "Sign Up Success!";
                Messenger.Send(new LoginEvent(0));
            }
        }

        [RelayCommand]
        private void GoToLogin()
        {
            Messenger.Send(new LoginEvent(0));
        }
    }
}
