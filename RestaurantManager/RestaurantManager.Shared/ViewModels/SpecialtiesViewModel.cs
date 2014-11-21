using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class SpecialtiesViewModel : ProductsViewModelBase
    {
        private List<Product> products;
        private string picturesBaseDirectory;
        private string chosenPicturePath;

        public SpecialtiesViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Specialties/";
            this.chosenPicturePath = "";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            this.products.Add(new Product("Chirpan Onions", 3.90, 150, 1, this.picturesBaseDirectory + "chirpan-onions.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Potato ball", 1.20, 80, 1, this.picturesBaseDirectory + "potato-ball.png", false, this.chosenPicturePath));
            this.products.Add(new Product("Fried pizza", 7.80, 450, 1, this.picturesBaseDirectory + "fried-pizza.png", false, this.chosenPicturePath));
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
