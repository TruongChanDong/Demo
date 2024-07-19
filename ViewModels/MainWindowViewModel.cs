using CommunityToolkit.Mvvm.Messaging;
using Demo.Models;
using Demo.Services;
using Demo.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase, IRecipient<LoginEvent>, IRecipient<GetData>
    {
        [ObservableProperty]
        public ViewModelBase _currentViewModel;
        private TestDataService _testDataService ;
        private bool _lock;
        

        public MainWindowViewModel()
        {
            CurrentViewModel = new ListViewModel();
            _lock = false;
            Messenger.RegisterAll(this);

        }

        public void Receive(LoginEvent message)
        {
            CurrentViewModel = message.model;
        }

        public async void Receive(GetData message)
        {
            if (message.IsReading && !_lock)
            {
                _lock = true;
                _testDataService = new TestDataService();
                await _testDataService.getDataAsync();
            }
            if (!message.IsReading)
            {
                _testDataService.stopDataAsync();
                _lock = false;
            }       
        }
    }
}

public record class LoginEvent(ViewModelBase model, User user);

public record class GetData(TestData data,bool IsReading = false);


