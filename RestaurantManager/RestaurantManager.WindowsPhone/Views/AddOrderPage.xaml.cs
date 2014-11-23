using RestaurantManager.Common;
using RestaurantManager.Models;
using RestaurantManager.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
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
    public sealed partial class AddOrderPage : Page
    {
        private const string dbName = "MyOrders.db";

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private Accelerometer accelerometer;

        public AddOrderPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            this.DataContext = new AddOrderViewModel();

            this.Accelerometer = Accelerometer.GetDefault();

            this.Accelerometer.ReadingChanged += async (snd, args) =>
            {
                await this.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => {
                    if (args.Reading.AccelerationX < -0.98)
                    {
                        var frame = ((Frame)Window.Current.Content);
                        frame.Navigate(typeof(MainPage), 0);
                    }
                });
            };
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
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);

            // Create Db if not exist
            bool dbExists = await CheckDbAsync(dbName);
            if (!dbExists)
            {
                await CreateDatabaseAsync();
            }

            //Save current table number state
            var state = SuspensionManager.SessionStateForFrame(this.Frame);
            if (state != null && state.ContainsKey("tableNumber"))
            {
                object value = null;

                if (state.TryGetValue("tableNumber", out value))
                {
                    ((AddOrderViewModel)this.DataContext).TableNumber = state["tableNumber"].ToString();
                }
            }

            var parameters = e.Parameter as List<object>;
            if (parameters != null)
            {
                var chosenProducts = parameters[0] as List<Product>;
                string tableNumber = parameters[1].ToString();
                if (chosenProducts != null)
                {
                    ((AddOrderViewModel)this.DataContext).ChosenProducts = chosenProducts;
                }

                if (tableNumber != null && tableNumber != "")
                {
                    ((AddOrderViewModel)this.DataContext).TableNumber = tableNumber;
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);

            var state = SuspensionManager.SessionStateForFrame(this.Frame);
            var tableNumber = this.TableNumber.Text;
            ((AddOrderViewModel)this.DataContext).TableNumber = tableNumber;
            state["tableNumber"] = tableNumber;
        }

        private async Task<bool> CheckDbAsync(string dbName)
        {
            bool dbExist = true;

            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(dbName);
            }
            catch (Exception)
            {
                dbExist = false;
            }

            return dbExist;
        }

        private async Task CreateDatabaseAsync()
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.CreateTableAsync<MyOrderModel>();
        }

        #endregion

        private void GoToOtherView_Click(object sender, RoutedEventArgs e)
        {
            //var frame = ((Frame)Window.Current.Content);
            //string pesho = "just testing";
            //frame.Navigate(typeof(MainPage), pesho);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = ((AddOrderProduct)e.ClickedItem);
            var chosenProducts = ((AddOrderViewModel)this.DataContext).ChosenProducts;
            string tableNumber = this.TableNumber.Text;

            List<object> parameters = new List<object>() { clickedItem, chosenProducts, tableNumber };

            if (!Frame.Navigate(typeof(ProductsPage), parameters))
            {
                var resourceLoader = ResourceLoader.GetForCurrentView("Resources");
                throw new Exception(resourceLoader.GetString("NavigationFailedExceptionMessage"));
            }
        }

        private void TableNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            ((AddOrderViewModel)this.DataContext).TableNumber = this.TableNumber.Text;
        }

        public Accelerometer Accelerometer
        {
            get
            {
                return this.accelerometer;
            }
            set
            {
                this.accelerometer = value;
            }
        }

        //private void ListView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        //{
        //    var item = (FrameworkElement)e.OriginalSource;
        //    var itema = ((AddOrderProduct)item.DataContext);
        //    int b = 5;
        //    //var clickedItem = ((AddOrderProduct)e.ClickedItem);
        //    //if (!Frame.Navigate(typeof(ProductsPage), clickedItem))
        //    //{
        //    //    var resourceLoader = ResourceLoader.GetForCurrentView("Resources");
        //    //    throw new Exception(resourceLoader.GetString("NavigationFailedExceptionMessage"));
        //    //}
        //}
    }
}
