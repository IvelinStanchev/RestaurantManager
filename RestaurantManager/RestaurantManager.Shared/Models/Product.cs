using RestaurantManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.Models
{
    public class Product : ModelBase
    {
        private string name;
        private double price;
        private double weight;
        private int quantity;
        private string imagePath;
        private bool isChosen;
        private string isChosenImagePath;

        public Product(string name, double price, double weight, int quantity, string imagePath, bool isChosen, string isChosenImagePath)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
            this.quantity = quantity;
            this.imagePath = imagePath;
            this.isChosen = false;
            this.isChosenImagePath = isChosenImagePath;
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
        public double Price 
        {
            get
            {
                return this.price;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The price should be bigger or equal to zero!");
                }
                this.price = value;
            }
        }
        public double Weight 
        {
            get
            {
                return this.weight;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The weight should be bigger or equal to zero!");
                }
                this.weight = value;
            }
        }
        public int Quantity 
        {
            get
            {
                return this.quantity;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("The quantity should be bigger or equal to zero!");
                }
                this.quantity = value;
                OnPropertyChanged("Quantity");
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
        public bool IsChosen
        {
            get
            {
                return this.isChosen;
            }
            set
            {
                this.isChosen = value;
            }
        }
        public string IsChosenImagePath
        {
            get
            {
                return this.isChosenImagePath;
            }
            set
            {
                this.isChosenImagePath = value;
                OnPropertyChanged("IsChosenImagePath");
            }
        }
    }
}
