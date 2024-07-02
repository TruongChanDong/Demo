namespace Demo.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainWindowViewModel()
        {
            CurrentViewModel = new LoginViewModel();
        }
    }
}
