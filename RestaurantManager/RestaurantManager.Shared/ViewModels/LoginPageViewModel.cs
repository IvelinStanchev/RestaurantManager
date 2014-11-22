using Parse;
using RestaurantManager.Commands;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace RestaurantManager.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel()
        {
            this.User = new UserViewModel();
            //this.User = new UserViewModel()
            //    {
            //        Username = "Pesho",
            //        Password = "123456q",
            //        TelephoneNumber = "Missing"
            //    };
        }

        public UserViewModel User { get; set; }

        public async Task SignUp()
        {
            string mode = "signUp";

            bool hasInternetConnection = await this.CheckForInternetConnection();
            bool areFieldsValid = await this.CheckForValidFields(mode);
            if (hasInternetConnection && areFieldsValid)
            {
                var user = new ParseUser()
                {
                    Username = this.User.Username,
                    Password = this.User.Password
                };

                user["phone"] = this.User.TelephoneNumber;

                await user.SignUpAsync();
            }
        }

        public async Task<bool> Login()
        {
            string mode = "login";

            try
            {
                bool areFieldsValid = await this.CheckForValidFields(mode);

                if (areFieldsValid)
                {
                    bool hasInternetConnection = await this.CheckForInternetConnection();

                    if (hasInternetConnection)
                    {
                        await ParseUser.LogInAsync(this.User.Username, this.User.Password);
                        var userPhone = ParseUser.CurrentUser["phone"];
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        private async Task<bool> CheckForValidFields(string mode)
        {
            string dialogMessage = string.Empty;

            if (this.User.Username == "" || string.IsNullOrEmpty(this.User.Username))
            {
                dialogMessage = "The username should not be empty!";
            }
            else if (this.User.Password == "" || string.IsNullOrEmpty(this.User.Password))
            {
                dialogMessage = "The password should not be empty!";
            }
            else if (this.User.TelephoneNumber == "" || string.IsNullOrEmpty(this.User.TelephoneNumber))
            {
                if (mode == "signUp")
                {
                    dialogMessage = "The thelephone number should not be empty!";
                }
            }

            if (!string.IsNullOrEmpty(dialogMessage))
            {
                MessageDialog dialog = new MessageDialog(dialogMessage);
                await dialog.ShowAsync();

                return false;
            }

            return true;
        }

        private async Task<bool> CheckForInternetConnection()
        {
            //Get the Internet connection profile
            //string connectionProfileInfo = string.Empty;
            //try
            //{
            //    ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

            //    if (InternetConnectionProfile == null)
            //    {
            //        int b = 5;
            //        return false;
            //        //NotifyUser("Not connected to Internet\n");
            //    }
            //    else
            //    {
            //        int b = 5;
            //        return true;
            //        //connectionProfileInfo = GetConnectionProfile(InternetConnectionProfile);
            //        //NotifyUser("Internet connection profile = " + connectionProfileInfo);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //NotifyUser("Unexpected exception occurred: " + ex.ToString());
            //}
            //return false;
            //ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            ////bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            //bool internet = NetworkInterface.GetIsNetworkAvailable();
            //if (!internet)
            //{
            //    MessageDialog dialog = new MessageDialog("No Internet Connection!");
            //    await dialog.ShowAsync();

            //    return false;
            //}

            bool isConnected = NetworkInterface.GetIsNetworkAvailable();
            if (!isConnected)
            {
                await new MessageDialog("No internet connection is avaliable. The full functionality of the app isn't avaliable.").ShowAsync();
            }
            else
            {
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                NetworkConnectivityLevel connection = InternetConnectionProfile.GetNetworkConnectivityLevel();
                if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess)
                {
                    isConnected = false;
                }
            }        

            return isConnected;
        }
    }
}
