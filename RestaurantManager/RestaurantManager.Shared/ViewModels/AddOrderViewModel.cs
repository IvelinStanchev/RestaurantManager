using RestaurantManager.Models;
using RestaurantManager.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using RestaurantManager.Models.Interfaces;

namespace RestaurantManager.ViewModels
{
    public class AddOrderViewModel : ViewModelBase
    {
        private List<AddOrderProduct> addOrderProducts;
        private List<Product> chosenProducts;
        private string picturesBaseDirectory;

        public AddOrderViewModel()
        {
            this.chosenProducts = new List<Product>();
            this.picturesBaseDirectory = "/Images/Products/";
            this.addOrderProducts = new List<AddOrderProduct>();
            this.PopulateProducts();
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
    }
}
