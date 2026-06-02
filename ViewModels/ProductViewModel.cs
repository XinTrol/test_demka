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
using Tmds.DBus.Protocol;

namespace demka_podg1.ViewModels
{
    public partial class ProductViewModel : ViewModelBase
    {
        [ObservableProperty] private User _currentUser;

        [ObservableProperty] private bool _isGuest = false;
        [ObservableProperty] private bool _isManager = false;
        [ObservableProperty] private bool _isAdmin = false;

        public List<string> PriceFilter { get; } = new() { "Нет", "По возростанию цены", "По убыванию цены" };
        public List<string> CountInStockFilter { get; } = new() { "Нет", "По возростанию количества", "По убыванию количества" };
        public List<string> DiscountFilter { get; } = new() { "Нет", "0-11%", "11-15%", "15+%" };

        [ObservableProperty] private string _selectedPriceFilter = "Нет";
        [ObservableProperty] private string _selectedCountInStockFilter = "Нет";
        [ObservableProperty] private string _selectedDiscountFilter = "Нет";
        [ObservableProperty] private string _searchText = "";

        public ObservableCollection<Product> Products { get; } = new();

        private void LoadProducts()
        {
            var products = db.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.ProductCategory)
                .Include(x => x.GenderTypeNavigation)
                .Include(x => x.Units)
                .ToList();

            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private void ApplyFilter()
        {
            var result = db.Products
                .Include(x => x.Manufacturer)
                .Include(x => x.Supplier)
                .Include(x => x.ProductCategory)
                .Include(x => x.GenderTypeNavigation)
                .Include(x => x.Units)
                .AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var q = SearchText.ToLower();
                result = result.Where(p => (p.CardName.ToLower().Contains(q))
                                || (p.Description.ToLower().Contains(q))
                                || (p.Manufacturer.Manufacurer1.ToLower().Contains(q))
                                || (p.Supplier.Supplier1.ToLower().Contains(q))
                                || (p.ProductCategory.CategoeyName.ToLower().Contains(q)));
            }

            result = SelectedPriceFilter switch
            {
                "По возростанию цены" => result.OrderBy(x => x.Price),
                "По убыванию цены" => result.OrderByDescending(x => x.Price),
                _ => result
            };

            result = SelectedCountInStockFilter switch
            {
                "По возростанию количества" => result.OrderBy(x => x.UnitInStock),
                "По убыванию количества" => result.OrderByDescending(x => x.UnitInStock),
                _ => result
            };

            result = SelectedDiscountFilter switch
            {
                "0-11%" => result.Where(x => x.Discount >= 0 && x.Discount < 11),
                "11-15%" => result.Where(x => x.Discount >= 11 && x.Discount < 15),
                "15+%" => result.Where(x => x.Discount >= 15),
                _ => result
            };

            Products.Clear();
            foreach (var q in result)
            {
                Products.Add(q);
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFilter();
        }
        partial void OnSelectedPriceFilterChanged(string value)
        {
            ApplyFilter();
        }
        partial void OnSelectedDiscountFilterChanged(string value)
        {
            ApplyFilter();
        }

        partial void OnSelectedCountInStockFilterChanged(string value)
        {
            ApplyFilter();
        }

        public ProductViewModel(User user)
        {
            CurrentUser = user;
            if (CurrentUser.IdRoleNavigation.Id == 1)
            {
                IsAdmin = true;
            }
            if (CurrentUser.IdRoleNavigation.Id == 2)
            {
                IsManager = true;
            }
            LoadProducts();
        }

        public ProductViewModel()
        {
            IsGuest = true;
            LoadProducts();
        }

        [RelayCommand]
        private void Logout()
        {
            CurrentUser = null;
            MainWindowViewModel.Instance.CurrentViewModel = new AuthViewModel();
        }

        [RelayCommand]
        public void Edit(string article)
        {
            MainWindowViewModel.Instance.CurrentViewModel = new AddOrEditProductsViewModel(article, CurrentUser);
        }

        [RelayCommand]
        public void Create(string article)
        {
            MainWindowViewModel.Instance.CurrentViewModel = new AddOrEditProductsViewModel(CurrentUser);
        }

        [RelayCommand]
        private void Delete(string article)
        {
            bool inOrders = db.OrderProducts.Any(op => op.ProductId == article);
            if (inOrders)
            {
                return;
            }

            var pr = db.Products.FirstOrDefault(x => x.Id == article);
            if (pr == null)
                return;

            db.Products.Remove(pr);
            db.SaveChanges();

            LoadProducts();
        }
    }
}

