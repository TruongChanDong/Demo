﻿using CommunityToolkit.Mvvm.Messaging;
using Demo.Models;
using Demo.ViewModels;

namespace Demo.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IRecipient<LoginEvent>
    {
        [ObservableProperty]
        public ViewModelBase _currentViewModel;

        public MainWindowViewModel()
        {
            CurrentViewModel = new ListViewModel();
            Messenger.RegisterAll(this);
        }

        public void Receive(LoginEvent message)
        {
            CurrentViewModel = null;
            CurrentViewModel = message.model;
        }
    }
}

public record class LoginEvent(ViewModelBase model, User user);

