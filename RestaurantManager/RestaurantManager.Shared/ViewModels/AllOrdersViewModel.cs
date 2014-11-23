using RestaurantManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManager.Models
{
    public class AllOrdersViewModel : ViewModelBase
    {
        private List<AllOrdersModel> allOrders;

        public AllOrdersViewModel()
        {
        }

        public List<AllOrdersModel> AllOrders 
        {
            get
            {
                return this.allOrders;
            }
            set
            {
                this.allOrders = value;
                OnPropertyChanged("AllOrders");
            }
        }
    }
}
