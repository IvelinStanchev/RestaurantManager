using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class BarbecueViewModel : ProductsViewModelBase
    {
        private List<Product> products;
        private string picturesBaseDirectory;
        private string chosenPicturePath;

        public BarbecueViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Barbecue/";
            this.chosenPicturePath = "";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            this.products.Add(new Product("Kebapche", 0.80, 80, 1, this.picturesBaseDirectory + "kebapche.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Meatball", 0.80, 80, 1, this.picturesBaseDirectory + "meatball.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Chicken stack", 2.80, 200, 1, this.picturesBaseDirectory + "chicken-stack.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Pork stack", 2.10, 200, 1, this.picturesBaseDirectory + "pork-stack.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Skewer", 2.50, 150, 1, this.picturesBaseDirectory + "skewer.png", false, this.chosenPicturePath));
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
