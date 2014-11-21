using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class DrinksViewModel : ProductsViewModelBase
    {
        private List<Product> products;
        private string picturesBaseDirectory;
        private string chosenPicturePath;

        public DrinksViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Drinks/";
            this.chosenPicturePath = "";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            this.products.Add(new Product("Zagorka", 2.00, 500, 1, this.picturesBaseDirectory + "zagorka.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Heineken", 2.50, 500, 1, this.picturesBaseDirectory + "heineken.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Jack Daniels", 3.80, 50, 1, this.picturesBaseDirectory + "jack-daniels.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Flirt", 2.90, 50, 1, this.picturesBaseDirectory + "flirt.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Devin", 1.50, 1500, 1, this.picturesBaseDirectory + "devin.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Cappy Orange", 1.30, 250, 1, this.picturesBaseDirectory + "cappy-orange.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Coca-Cola Can", 1.30, 330, 1, this.picturesBaseDirectory + "coca-cola-can.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Coca-Cola Bottle", 1.80, 500, 1, this.picturesBaseDirectory + "coca-cola-0.5l.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Fanta Can", 1.30, 330, 1, this.picturesBaseDirectory + "fanta-can.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Fanta Bottle", 1.80, 500, 1, this.picturesBaseDirectory + "fanta-0.5l.png", false, this.chosenPicturePath));
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
