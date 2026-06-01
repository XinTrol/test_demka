using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using demka_podg1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demka_podg1.ViewModels
{
    public partial class AddOrEditProductsViewModel: ViewModelBase
    {
        [ObservableProperty] private User _currentUser;
        [ObservableProperty] private Product _product;

        // Комбо боксы
        [ObservableProperty] private ObservableCollection<ProductCategory> _productCategories;
        [ObservableProperty] private ObservableCollection<Unit> _units;
        [ObservableProperty] private ObservableCollection<Supplier> _suppliers;
        [ObservableProperty] private ObservableCollection<Manufacurer> _manufacurers;
        [ObservableProperty] private ObservableCollection<GenderPosition> _genders;

        private bool _isEdit = false;

        private void LoadData()
        {
            ProductCategories = new ObservableCollection<ProductCategory>(db.ProductCategories.ToList());
            Units = new ObservableCollection<Unit>(db.Units.ToList());
            Suppliers = new ObservableCollection<Supplier> (db.Suppliers.ToList());
            Manufacurers = new ObservableCollection<Manufacurer>(db.Manufacurers.ToList());
            Genders = new ObservableCollection<GenderPosition>(db.GenderPositions.ToList());
        }

        public AddOrEditProductsViewModel(string id, User user) 
        {
            _isEdit = true;
            CurrentUser = user;
            LoadData();
            Product = db.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.ProductCategory)
                .Include(x => x.GenderTypeNavigation)
                .Include(x => x.Units)
                .FirstOrDefault(x => x.Id == id);
        }
        public AddOrEditProductsViewModel(User user)
        {
            CurrentUser = user;
            LoadData();
            Product = new Product();
        }

        [RelayCommand]
        private void Logout()
        {
            CurrentUser = null;
            MainWindowViewModel.Instance.CurrentViewModel = new AuthViewModel();
        }

        [RelayCommand]
        private void Save()
        {
            if(!_isEdit)
                db.Products.Add(Product);
            else
                db.Products.Update(Product);

            db.SaveChanges();
            MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel(CurrentUser);
        }

        [RelayCommand]
        private void Cancel()
        {
            MainWindowViewModel.Instance.CurrentViewModel = new ProductViewModel(CurrentUser);
        }
    }
}
