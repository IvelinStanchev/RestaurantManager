using RestaurantManager.Models;
using RestaurantManager.Models.Attributes;
using RestaurantManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class SoupsViewModel : ProductViewModelAttribute
    {
        private List<Product> products;
        private string picturesBaseDirectory;

        public SoupsViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Soups/";
            this.products = new List<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    this.products.Add(new Product(string.Format("{0} - {1}", "Pesho", i), this.picturesBaseDirectory + "salad.png"));
            //}
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
