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
    public class AddOrderViewModel
    {
        private List<AddOrderProduct> addOrderProducts;
        private string picturesBaseDirectory;

        public AddOrderViewModel()
        {
            this.picturesBaseDirectory = "/Images/Products/Salads/";
            this.addOrderProducts = new List<AddOrderProduct>();
            this.PopulateProducts();
        }

        private void PopulateProducts()
        {
            this.addOrderProducts.Add(new AddOrderProduct("Salads", this.picturesBaseDirectory + "shopska.png", new SaladsViewModel()));
            for (int i = 1; i < 20; i++)
            {
                this.addOrderProducts.Add(new AddOrderProduct(string.Format("{0} - {1}", "Pesho", i), this.picturesBaseDirectory + "shopska.png", new SoupsViewModel()));
            }
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
    }
}
