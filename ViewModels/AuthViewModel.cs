using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demka_podg1.ViewModels
{
    public partial class AuthViewModel: ViewModelBase
    {
        [ObservableProperty] private string _login;
        [ObservableProperty] private string _password;
        [ObservableProperty] private string _message;


        [RelayCommand]
        private void Auth()
        {
            currentUser = db.Users
                .Include(x => x.IdRoleNavigation)
                .FirstOrDefault(x => x.Login == Login && x.Password == Password);

            if(currentUser == null)
            {
                Message = "Нет чувака";
                return;
            }

            MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel(currentUser);
        }

        [RelayCommand]
        private void AuthIsGuest()
        {
            MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel();
        }

    }
}
