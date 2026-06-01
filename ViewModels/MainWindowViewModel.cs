using CommunityToolkit.Mvvm.ComponentModel;

namespace demka_podg1.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public static MainWindowViewModel Instance { get; set; }

        public MainWindowViewModel()
        {
            Instance = this;
        }

        [ObservableProperty]
        private ViewModelBase _currentViewModel = new AuthViewModel();
    }
}

