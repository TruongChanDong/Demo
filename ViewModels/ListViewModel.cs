﻿using CommunityToolkit.Mvvm.Messaging;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;

using System.Threading.Tasks;

namespace Demo.ViewModels
{
    public partial class ListViewModel : ViewModelBase, IRecipient<LoginEvent>
    {
        [ObservableProperty]
        public ObservableCollection<User> _userList = new();

        [ObservableProperty]
        public string? _userName;

        [ObservableProperty]
        public string? _email;

        [ObservableProperty]
        public string? _message;

        [ObservableProperty]
        public int? _count;

        [ObservableProperty]
        public RealTimeChartViewModel _currentView;

        private bool _flag;
        
        public ListViewModel() {
            UserName = "Username";
            Email = "Email";
            _flag = false;
            CurrentView = new RealTimeChartViewModel(0);
            Messenger.RegisterAll(this);
        }

        public void Receive(LoginEvent message)
        {
            if (message.user != null)
            {
                UserName = message.user.Name;
                Email = message.user.Email;
            }
        }

        private static HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:8080/"),
        };


        [RelayCommand]
        public async Task GetListItem()
        {
            var users = new List<User>();
            try
            {
                users = await httpClient.GetFromJsonAsync<List<User>>("user");
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                users = null;
            }
            if (users != null)
            {
                UserList = new ObservableCollection<User>(users);
                Count = users.Count;
                Message = $"Total {Count} users !!";
            }
        }

        [RelayCommand]
        public void LogOut()
        {
            Messenger.Send(new LoginEvent(new LoginViewModel(),null));
        }

        [RelayCommand]
        public void BarChart()
        {
            CurrentView = new RealTimeChartViewModel(0);
        }

        [RelayCommand]
        public void ColumnChart()
        {
            CurrentView = new RealTimeChartViewModel(1);
        }

        [RelayCommand]
        public void Button()
        {
            if (_flag) _flag = false;
            else _flag = true;
            Messenger.Send(new GetData(null, _flag));
            
        }
    }
}
