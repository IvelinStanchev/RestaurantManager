using Parse;
using RestaurantManager.Commands;
using RestaurantManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RestaurantManager.Models
{
    public class AllOrdersViewModel : ViewModelBase
    {
        private List<AllOrdersModel> allOrders;
        private ICommand backToAnotherView;

        public AllOrdersViewModel()
        {
            this.backToAnotherView = new RelayCommand(this.BackToAnotherViewAction);
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

        public ICommand BackToAnotherView
        {
            get
            {
                return this.backToAnotherView;
            }
        }

        public async Task<IEnumerable<AllOrdersModel>> GetData()
        {
            var allOrders = await new ParseQuery<AllOrdersModel>().FindAsync();

            return allOrders;
        }

        private void BackToAnotherViewAction()
        {
            var frame = ((Frame)Window.Current.Content);
            frame.Navigate(typeof(MainPage), 0);
        }
    }
}
