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
using Windows.UI.Xaml;

namespace RestaurantManager.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string message;
        private int messageChangingCount = 0;

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

        public string Message 
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                OnPropertyChanged("Message");
            }
        }

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

                DispatcherTimer timer = new DispatcherTimer();
                timer.Tick += SigningUpMessage;
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Start();
                
                await user.SignUpAsync();

                timer.Stop();
                this.Message = "Done! You can now log in!";
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
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Tick += SigningInMessage;
                        timer.Interval = TimeSpan.FromMilliseconds(1000);
                        timer.Start();

                        await ParseUser.LogInAsync(this.User.Username, this.User.Password);
                        timer.Stop();
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

        void SigningInMessage(object sender, object e)
        {
            this.messageChangingCount++;
            switch (messageChangingCount)
            {
                case 1:
                    this.Message = "Signing in.";
                    break;
                case 2:
                    this.Message = "Signing in..";
                    break;
                case 3:
                    this.messageChangingCount = 0;
                    this.Message = "Signing in...";
                    break;
            }
        }

        void SigningUpMessage(object sender, object e)
        {
            this.messageChangingCount++;
            switch (messageChangingCount)
            {
                case 1:
                    this.Message = "Signing up.";
                    break;
                case 2:
                    this.Message = "Signing up..";
                    break;
                case 3:
                    this.messageChangingCount = 0;
                    this.Message = "Signing up...";
                    break;
            }
        }
    }
}
