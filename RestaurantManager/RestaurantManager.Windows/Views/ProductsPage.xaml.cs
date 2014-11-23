using RestaurantManager.Common;
using RestaurantManager.Models;
using RestaurantManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace RestaurantManager.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductsPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private int holdingCalledCount;

        public ProductsPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            holdingCalledCount = 0;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            var parameters = e.Parameter as List<object>;
            var clickedItem = parameters[0] as AddOrderProduct;
            List<Product> chosenProducts = parameters[1] as List<Product>;
            string tableNumber = parameters[2].ToString();
            ProductsViewModelBase dataContext = clickedItem.ChildDataContext;
            this.DataContext = dataContext;

            ((ProductsViewModelBase)this.DataContext).ChosenProducts = chosenProducts;
            ((ProductsViewModelBase)this.DataContext).TableNumber = tableNumber;

            string clickedItemName = clickedItem.Name;
            this.Title.Text = clickedItemName;
            this.GoToAddOrderPage.Content = string.Format("Finish {0} Order", clickedItemName);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //var clickedItem = ((Product)e.h);
            //if (clickedItem.IsChosenImagePath == "")
            //{
            //    clickedItem.IsChosenImagePath = "/Images/ChosenProduct.png";
            //}
            //else
            //{
            //    clickedItem.IsChosenImagePath = "";
            //}
        }

        private void ListView_Holding(object sender, HoldingRoutedEventArgs e)
        {
            holdingCalledCount++;
            var frameworkElement = (FrameworkElement)e.OriginalSource;
            var holdedItem = ((Product)frameworkElement.DataContext);
            if (holdedItem != null)
            {
                if (holdedItem.IsChosenImagePath == "" && !holdedItem.IsChosen && holdingCalledCount % 2 != 0)
                {
                    holdedItem.IsChosenImagePath = "/Images/ChosenProduct.png";
                    holdedItem.IsChosen = true;
                }
                else if (holdedItem.IsChosenImagePath != "" && holdingCalledCount % 2 != 0)
                {
                    holdedItem.IsChosenImagePath = "";
                    holdedItem.Quantity = 1;
                    holdedItem.IsChosen = false;
                }
            }
            
        }

        private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var frameworkElement = (FrameworkElement)e.OriginalSource;
            var doubleTappedProduct = ((Product)frameworkElement.DataContext);
            doubleTappedProduct.Quantity++;
        }
    }
}
