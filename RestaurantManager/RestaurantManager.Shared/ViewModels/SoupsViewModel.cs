using RestaurantManager.Models;
using RestaurantManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class SoupsViewModel : ProductsViewModelBase
    {
        private List<Product> products;
        private string picturesBaseDirectory;
        private string chosenPicturePath;

        public SoupsViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Soups/";
            this.chosenPicturePath = "";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            this.products.Add(new Product("Ball", 5.20, 400, 1, this.picturesBaseDirectory + "ball.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Chicken", 4.80, 500, 1, this.picturesBaseDirectory + "chicken.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Rice", 4.10, 600, 1, this.picturesBaseDirectory + "rice.png", false, this.chosenPicturePath));
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
