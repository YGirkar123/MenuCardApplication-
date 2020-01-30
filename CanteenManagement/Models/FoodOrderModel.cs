using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CanteenManagement.Models
{
    public class FoodOrderModel
    {
        public int ID { get; set; }

        public string FoodName { get; set; }

        public string OrderStatus { get; set; }

        public string OtherDetails { get; set; }

        public string Feedback { get; set; }

        public int Quantity { get; set; }

        public int TotalAmount { get; set; }

        public int Amount { get; set; }

    }
}