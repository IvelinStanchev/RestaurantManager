using RestaurantManager.Commands;
using RestaurantManager.Models;
using RestaurantManager.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.ViewModels
{
    public class ProductsViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected ICommand finishOrderCommand;
        protected List<Product> chosenProducts;
        protected string tableNumber;

        public ProductsViewModelBase()
        {
            this.finishOrderCommand = new RelayCommandWithOneParameter(this.FinishOrder);
        }

        public ICommand FinishOrderCommand
        {
            get
            {
                return this.finishOrderCommand;
            }
        }

        public List<Product> ChosenProducts 
        {
            get
            {
                return this.chosenProducts;
            }
            set
            {
                this.chosenProducts = value;
            }
        }

        public string TableNumber
        {
            get
            {
                return this.tableNumber;
            }
            set
            {
                this.tableNumber = value;
            }
        }

        protected void FinishOrder(object parameter)
        {
            List<Product> allProducts = parameter as List<Product>;
            if (chosenProducts == null)
            {
                this.chosenProducts = new List<Product>();
            }

            for (int i = 0; i < allProducts.Count; i++)
            {
                if (allProducts[i].IsChosen)
                {
                    int indexOfItem = IndexOfDuplicateChosenItem(this.chosenProducts, allProducts[i]);
                    if (indexOfItem < 0)
                    {
                        this.chosenProducts.Add(allProducts[i]);
                    }
                    else
                    {
                        this.chosenProducts[indexOfItem].Quantity += allProducts[i].Quantity;
                    }
                }
            }

            List<object> paramametesToSend = new List<object>() { this.chosenProducts, this.tableNumber };

            var frame = ((Frame)Window.Current.Content);

            frame.BackStack.RemoveAt(frame.BackStack.Count - 1);

            frame.Navigate(typeof(AddOrderPage), paramametesToSend);

            frame.BackStack.RemoveAt(frame.BackStack.Count - 1);
        }

        private int IndexOfDuplicateChosenItem(List<Product> listOfChosenProducts, Product productToCheck)
        {
            for (int i = 0; i < listOfChosenProducts.Count; i++)
            {
                if (listOfChosenProducts[i].Name == productToCheck.Name && 
                    listOfChosenProducts[i].Weight == productToCheck.Weight &&
                    listOfChosenProducts[i].Price == productToCheck.Price)
                {
                    return i;
                }
            }

            return -1;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged == null)
            {
                return;
            }
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
