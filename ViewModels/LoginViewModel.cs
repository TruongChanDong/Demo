﻿using Demo.Models;
using Demo.Views;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;


namespace Demo.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _userName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _password;

        [ObservableProperty]
        private string? _message;

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:8080/"),
        };

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    phone = $"{UserName}",
                    password = $"{Password}"
                }),
                Encoding.UTF8,"application/json");

            var jsonResponse = new User();
            try
            {
                using HttpResponseMessage response = await httpClient.PostAsync("login",jsonContent);
                response.EnsureSuccessStatusCode();
                jsonResponse = await response.Content.ReadFromJsonAsync<User>();
            }
            catch (Exception ex)
            {
                Message = "Invalid Username or Password!";
                jsonResponse = null;
            }
            if (jsonResponse != null) {
                Message = "Login Success!";
                Messenger.Send(new LoginEvent(1));
            }
        }

        [RelayCommand]
        private void Register() {
            Messenger.Send(new LoginEvent(2));
        }

        private bool CanLogin() => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);

    }
}
