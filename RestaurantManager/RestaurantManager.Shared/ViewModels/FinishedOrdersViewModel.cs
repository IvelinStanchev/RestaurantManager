using RestaurantManager.Commands;
using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.ViewModels
{
    public class FinishedOrdersViewModel : ViewModelBase
    {
        private List<MyOrderModel> finishedOrders;
        private ICommand backToAnotherView;

        public FinishedOrdersViewModel()
        {
            this.backToAnotherView = new RelayCommand(this.BackToAnotherViewAction);
        }

        public List<MyOrderModel> FinishedOrders 
        {
            get
            {
                return this.finishedOrders;
            }
            set
            {
                this.finishedOrders = value;
                OnPropertyChanged("FinishedOrders");
            }
        }

        public ICommand BackToAnotherView
        {
            get
            {
                return this.backToAnotherView;
            }
        }

        private void BackToAnotherViewAction()
        {
            var frame = ((Frame)Window.Current.Content);
            frame.Navigate(typeof(MainPage), 0);
        }
    }
}
