using RestaurantManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class AddOrderViewModel
    {
        private ObservableCollection<Product> products;

        public AddOrderViewModel()
        {
            this.products = new ObservableCollection<Product>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            for (int i = 0; i < 20; i++)
            {
                this.products.Add(new Product(string.Format("{0} - {1}", "Pesho", i), "/Images/Products/salad.png"));
            }
        }

        public ObservableCollection<Product> Products 
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
