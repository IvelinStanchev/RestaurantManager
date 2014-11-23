using RestaurantManager.Models;
using RestaurantManager.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Parse;
using System.Net.Http;
using System.Xml.Linq;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.ViewManagement;
using SQLite;

namespace RestaurantManager.ViewModels
{
    public class AddOrderViewModel : ViewModelBase
    {
        private const string dbName = "MyOrders.db";

        private List<AddOrderProduct> addOrderProducts;
        private List<Product> chosenProducts;
        private string tableNumber;
        private string picturesBaseDirectory;
        private ICommand saveOrder;
        private ICommand backToAnotherView;
        private Popup popup = new Popup();
        private TextBlock textBlockContent = new TextBlock();

        public AddOrderViewModel()
        {
            this.InitilizePopup();
            this.saveOrder = new RelayCommand(this.SaveOrderAction);
            this.backToAnotherView = new RelayCommand(this.BackToAnotherViewAction);
            this.chosenProducts = new List<Product>();
            this.tableNumber = "";
            this.picturesBaseDirectory = "/Images/Products/";
            this.addOrderProducts = new List<AddOrderProduct>();
            this.PopulateProducts();
        }

        private void BackToAnotherViewAction()
        {
            var frame = ((Frame)Window.Current.Content);
            frame.Navigate(typeof(MainPage), 0);
        }

        private async void SaveOrderAction()
        {
            if (this.tableNumber == "" || string.IsNullOrEmpty(tableNumber))
            {
                MessageDialog dialog = new MessageDialog("Please, provide table number or name!");
                await dialog.ShowAsync();
            }
            else
            {
                if (this.chosenProducts.Count == 0)
                {
                    MessageDialog dialog = new MessageDialog("There are no chosen products!");
                    await dialog.ShowAsync();
                }
                else
                {
                    //Check for internet connection !

                    string baseUri = "http://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false";
                    string location = string.Empty;

                    this.textBlockContent.Text = "Getting Coordinates!";
                    this.popup.IsOpen = true;

                    List<string> coordinates = await this.GetCurrentLocation();
                    if (coordinates.Count > 0)
                    {
                        //Has Location
                        string latitude = coordinates[0];
                        string longitute = coordinates[1];

                        this.textBlockContent.Text = "Getting Address!";

                        string address = await this.RetrieveFormatedAddress(baseUri, latitude, longitute);

                        this.textBlockContent.Text = "Saving Order!";

                        AllOrdersModel orderObject = new AllOrdersModel();
                        orderObject.TableNumber = this.tableNumber;
                        orderObject.PhoneNumber = ParseUser.CurrentUser["phone"].ToString();
                        orderObject.Username = ParseUser.CurrentUser.Username;
                        orderObject.Value = this.CalculateOrderValue().ToString();
                        orderObject.Location = address;
                        await orderObject.SaveAsync();

                        //Save to SQLite database
                        await this.AddFinishedOrderAsync();

                        this.popup.IsOpen = false;
                    }
                }
            }
        }

        private async Task AddFinishedOrderAsync()
        {
            MyOrderModel finishedOrder = new MyOrderModel();
            finishedOrder.TableNumber = this.tableNumber;
            finishedOrder.Date = DateTime.Now;
            finishedOrder.OrderValue = this.CalculateOrderValue().ToString();

            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(dbName);
            await conn.InsertAsync(finishedOrder);
        }

        private void InitilizePopup()
        {
            Border border = new Border();
            border.BorderBrush = new SolidColorBrush(Colors.Green);
            border.BorderThickness = new Thickness(2);
            string currentViewState = ApplicationView.GetForCurrentView().Orientation.ToString();
            if (currentViewState == "Landscape")
            {
                border.Width = Window.Current.Bounds.Width;
            }
            else if (currentViewState == "Portrait")
            {
                border.Width = Window.Current.Bounds.Height;
            }
            border.Width = Window.Current.Bounds.Width;
            border.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel stackPanelOuter = new StackPanel();
            stackPanelOuter.HorizontalAlignment = HorizontalAlignment.Stretch;
            stackPanelOuter.Background = new SolidColorBrush(Colors.LightBlue);
            stackPanelOuter.Orientation = Windows.UI.Xaml.Controls.Orientation.Vertical;

            textBlockContent.TextAlignment = TextAlignment.Center;
            textBlockContent.FontSize = 40;
            textBlockContent.Margin = new Thickness(10, 0, 10, 0);
            textBlockContent.Foreground = new SolidColorBrush(Colors.Red);

            stackPanelOuter.Children.Add(this.textBlockContent);

            border.Child = stackPanelOuter;

            this.popup.HorizontalAlignment = HorizontalAlignment.Stretch;

            this.popup.Child = border;

            this.popup.VerticalOffset = 30;
            this.popup.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private async Task<List<string>> GetCurrentLocation()
        {
            var loc = new Geolocator();
            List<string> coordinates = new List<string>();
            try
            {
                loc.DesiredAccuracy = PositionAccuracy.High;
                Geoposition pos = await loc.GetGeopositionAsync();
                coordinates.Add(pos.Coordinate.Latitude.ToString());
                coordinates.Add(pos.Coordinate.Longitude.ToString());
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageDialog msg = new MessageDialog("Your location can't be access");
                msg.ShowAsync();
            }

            return coordinates;
        }

        private async Task<string> RetrieveFormatedAddress(string baseUri, string lat, string lng)
        {
            string requestUri = string.Format(baseUri, lat, lng);
            string address = string.Empty;
            var hc = new HttpClient();
            using (hc)
            {
                string result = await hc.GetStringAsync(requestUri);
                var xmlElm = XElement.Parse(result);
                var status = (from elm in xmlElm.Descendants()
                              where
                                  elm.Name == "status"
                              select elm).FirstOrDefault();
                if (status.Value.ToLower() == "ok")
                {
                    var res = (from elm in xmlElm.Descendants()
                               where
                                   elm.Name == "formatted_address"
                               select elm).FirstOrDefault();
                    address = res.Value;
                }
            }

            return address;
        }

        private double CalculateOrderValue()
        {
            double orderValue = 0.0;

            for (int i = 0; i < this.chosenProducts.Count; i++)
            {
                orderValue += this.chosenProducts[i].Quantity * this.chosenProducts[i].Price;
            }

            return orderValue;
        }

        private void PopulateProducts()
        {
            this.addOrderProducts.Add(new AddOrderProduct("Salads", this.picturesBaseDirectory + "Salads/shopska.png", new SaladsViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Soups", this.picturesBaseDirectory + "Soups/ball.png", new SoupsViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Barbecue", this.picturesBaseDirectory + "Barbecue/kebapche.png", new BarbecueViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("LunchFood", this.picturesBaseDirectory + "LunchFood/moussaka.png", new LunchFoodViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Specialties", this.picturesBaseDirectory + "Specialties/chirpan-onions.png", new SpecialtiesViewModel()));
            this.addOrderProducts.Add(new AddOrderProduct("Drinks", this.picturesBaseDirectory + "Drinks/zagorka.png", new DrinksViewModel()));
        }

        public ICommand SaveOrder 
        {
            get
            {
                return this.saveOrder;
            }
        }

        public ICommand BackToAnotherView
        {
            get
            {
                return this.backToAnotherView;
            }
        }

        public List<AddOrderProduct> AddOrderProducts 
        {
            get
            {
                return this.addOrderProducts;
            }
            set
            {
                this.addOrderProducts = value;
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
                OnPropertyChanged("ChosenProducts");
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
                OnPropertyChanged("TableNumber");
            }
        }
    }
}
