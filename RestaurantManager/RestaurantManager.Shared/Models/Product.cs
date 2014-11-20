using RestaurantManager.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.Models
{
    public class Product : IProduct
    {
        private string name;
        private double price;
        private double weight;
        private int quantity;
        //private Image image;
        private string imagePath;

        public Product()
        {
        }

        public Product(string name)
            : this(name, 0, 0, 0, null)
        {
        }

        public Product(string name, string imagePath)
            : this(name, 0, 0, 0, imagePath)
        {
        }

        public Product(string name, double price, double weight, int quantity, string imagePath)
        {
            this.name = name;
            this.price = price;
            this.weight = weight;
            this.quantity = quantity;
            this.imagePath = imagePath;
        }

        //public Product(string name, Image image)
        //    : this(name, 0, 0, 0, image)
        //{
        //}

        //public Product(string name, double price, double weight, int quantity, Image image)
        //{
        //    this.name = name;
        //    this.price = price;
        //    this.weight = weight;
        //    this.quantity = quantity;
        //    this.image = image;
        //}

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

        //public Image Image
        //{
        //    get
        //    {
        //        return this.image;
        //    }
        //    set
        //    {
        //        this.image = value;
        //    }
        //}

        public double CalculateValue()
        {
            double sum = this.quantity * this.price;

            return sum;
        }
    }
}
