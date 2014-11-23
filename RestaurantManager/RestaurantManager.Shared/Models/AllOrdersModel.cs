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

        //public static Expression<Func<ParseObject, AllOrdersModel>> FromModel
        //{
        //    get
        //    {
        //        return parseObject =>
        //            new AllOrdersModel()
        //            {
        //                Username = parseObject["username"].ToString(),
        //                Location = parseObject["location"].ToString(),
        //                Value = parseObject["orderValue"].ToString()
        //            };
        //    }
        //}

        //public string Username { get; set; }
        //public string Location { get; set; }
        //public string Value { get; set; }
    }
}
