using Parse;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RestaurantManager.Models
{
    [ParseClassName("Order")]
    public class AllOrdersModel : ParseObject
    {
        [ParseFieldName("username")]
        public string Username 
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("location")]
        public string Location 
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("orderValue")]
        public string Value 
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("phoneNumber")]
        public string PhoneNumber
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("tableNumber")]
        public string TableNumber
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }
    }
}
