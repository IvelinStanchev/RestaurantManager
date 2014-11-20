using RestaurantManager.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class ViewModelBase : ProductViewModelAttribute, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
