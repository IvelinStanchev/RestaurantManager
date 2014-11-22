using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManager.Models
{
    [Table("FinishedOrders")]
    public class MyOrderModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public List<Product> ProductsToAdd { get; set; }

        public string TableNumber { get; set; }
    }
}
