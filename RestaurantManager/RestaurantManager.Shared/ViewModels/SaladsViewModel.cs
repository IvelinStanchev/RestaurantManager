using RestaurantManager.Models;
using RestaurantManager.Models.Attributes;
using RestaurantManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class SaladsViewModel : ViewModelBase
    {
        private List<Product> products;
        private string picturesBaseDirectory;
        private string chosenPicturePath;

        public SaladsViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Salads/";
            this.chosenPicturePath = "";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            for (int i = 0; i < 20; i++)
            {
                this.products.Add(new Product("Shopska", 4.5, 350, 1, this.picturesBaseDirectory + "shopska.png", false, this.chosenPicturePath));
            }
            
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
    }
}
