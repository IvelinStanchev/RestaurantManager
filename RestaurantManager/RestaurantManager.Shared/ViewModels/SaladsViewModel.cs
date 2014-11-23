using RestaurantManager.Commands;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.ViewModels
{
    public class SaladsViewModel : ProductsViewModelBase
    {
        private List<Product> products;
        private string picturesBaseDirectory;
        private string chosenPicturePath;
        private Frame frame;

        public SaladsViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Salads/";
            this.chosenPicturePath = "";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            this.products.Add(new Product("Shopska", 4.50, 350, 1, this.picturesBaseDirectory + "shopska.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Ovcharska", 5.50, 450, 1, this.picturesBaseDirectory + "ovcharska.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Chow", 3.50, 300, 1, this.picturesBaseDirectory + "tomatoes-and-cucumbers.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Tomatoes", 2.50, 300, 1, this.picturesBaseDirectory + "tomatoes.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Cucumbers", 2.50, 300, 1, this.picturesBaseDirectory + "cucumbers.png", false, this.chosenPicturePath));
        }

        public List<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.products = value;
            }
        }

        public Frame Frame 
        { 
            get
            {
                return this.frame;
            }
            set
            {
                this.frame = value;
            }
        }
    }
}
