using RestaurantManager.Models;
using RestaurantManager.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;

namespace RestaurantManager.ViewModels
{
    public class AddOrderViewModel : ViewModelBase
    {
        private List<AddOrderProduct> addOrderProducts;
        private List<Product> chosenProducts;
        private string tableNumber;
        private string picturesBaseDirectory;
        private ICommand saveOrder;

        public AddOrderViewModel()
        {
            this.saveOrder = new RelayCommand(this.SaveOrderAction);
            this.chosenProducts = new List<Product>();
            this.tableNumber = "";
            this.picturesBaseDirectory = "/Images/Products/";
            this.addOrderProducts = new List<AddOrderProduct>();
            this.PopulateProducts();
        }

        private async void SaveOrderAction()
        {
            if (this.tableNumber == "" || string.IsNullOrEmpty(tableNumber))
            {
                MessageDialog dialog = new MessageDialog("Please, provide table number or name!");
                await dialog.ShowAsync();
            }
        }

        private void PopulateProducts()
        {
            this.addOrderProducts.Add(new AddOrderProduct("Salads", this.picturesBaseDirectory + "Salads/shopska.png", new SaladsViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Soups", this.picturesBaseDirectory + "Soups/ball.png", new SoupsViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Barbecue", this.picturesBaseDirectory + "Barbecue/kebapche.png", new BarbecueViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("LunchFood", this.picturesBaseDirectory + "LunchFood/moussaka.png", new LunchFoodViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Specialties", this.picturesBaseDirectory + "Specialties/chirpan-onions.png", new SpecialtiesViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Drinks", this.picturesBaseDirectory + "Drinks/zagorka.png", new DrinksViewModel()));
        }

        public ICommand SaveOrder 
        {
            get
            {
                return this.saveOrder;
            }
        }

        public List<AddOrderProduct> AddOrderProducts 
        {
            get
            {
                return this.addOrderProducts;
            }
            set
            {
                this.addOrderProducts = value;
            }
        }

        public List<Product> ChosenProducts
        {
            get
            {
                return this.chosenProducts;
            }
            set
            {
                this.chosenProducts = value;
                OnPropertyChanged("ChosenProducts");
            }
        }

        public string TableNumber 
        {
            get
            {
                return this.tableNumber;
            }
            set
            {
                this.tableNumber = value;
                OnPropertyChanged("TableNumber");
            }
        }
    }
}
