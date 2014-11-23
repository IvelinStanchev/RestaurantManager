using RestaurantManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class FinishedOrdersViewModel : ViewModelBase
    {
        private List<MyOrderModel> finishedOrders;

        public FinishedOrdersViewModel()
        {

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
    }
}
