using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class LunchFoodViewModel : ProductsViewModelBase
    {
        private List<Product> products;
        private string picturesBaseDirectory;
        private string chosenPicturePath;

        public LunchFoodViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/LunchFood/";
            this.chosenPicturePath = "";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            this.products.Add(new Product("Moussaka", 1.80, 150, 1, this.picturesBaseDirectory + "moussaka.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Tarator", 1.90, 200, 1, this.picturesBaseDirectory + "tarator.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Stuffed peppers", 2.80, 200, 1, this.picturesBaseDirectory + "stuffed-peppers.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Spaghetti", 2.90, 250, 1, this.picturesBaseDirectory + "spaghetti.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Chicken", 7.80, 600, 1, this.picturesBaseDirectory + "chicken.png", false, this.chosenPicturePath));
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
