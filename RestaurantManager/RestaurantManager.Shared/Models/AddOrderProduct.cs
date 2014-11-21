using RestaurantManager.Models.Interfaces;
using RestaurantManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Models
{
    public class AddOrderProduct
    {
        private string name;
        private string imagePath;
        private ProductsViewModelBase childDataContext;

        public AddOrderProduct(string name, string imagePath, ProductsViewModelBase childDataContext)
        {
            this.name = name;
            this.imagePath = imagePath;
            this.childDataContext = childDataContext;
        }

        public string Name 
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("The name should not be empty or null!");
                }
                this.name = value;
            }
        }

        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }
            set
            {
                this.imagePath = value;
            }
        }

        public ProductsViewModelBase ChildDataContext
        {
            get
            {
                return this.childDataContext;
            }
            set
            {
                this.childDataContext = value;
            }
        }
    }
}
