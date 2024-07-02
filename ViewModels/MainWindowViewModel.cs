using CommunityToolkit.Mvvm.Messaging;

namespace Demo.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IRecipient<LoginEvent>
    {
        [ObservableProperty]
        public ViewModelBase _currentViewModel;

        public MainWindowViewModel()
        {
            CurrentViewModel = new LoginViewModel();
            Messenger.RegisterAll(this);
        }

        public void Receive(LoginEvent message)
        {
            if (message.EventIndex == 0)
            {
                CurrentViewModel = new LoginViewModel();
            }
            if(message.EventIndex == 1)
            {
                CurrentViewModel = new ListViewModel();
            }
            if (message.EventIndex == 2)
            {
                CurrentViewModel = new RegisterViewModel();
            }
        }
    }
}

public record class LoginEvent(int? EventIndex);

