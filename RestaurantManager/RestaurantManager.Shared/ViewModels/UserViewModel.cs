using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string TelephoneNumber { get; set; }
    }
}
