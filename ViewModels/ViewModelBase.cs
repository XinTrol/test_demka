using CommunityToolkit.Mvvm.ComponentModel;
using demka_podg1.Models;

namespace demka_podg1.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public static DemkaContext db = new DemkaContext();
        public User currentUser;
    }
}