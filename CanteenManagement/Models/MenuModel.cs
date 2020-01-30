using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanteenManagement.Models
{
    public class MenuModel
    {
        [Display(Name = "Id")]
        public int ID { get; set; }

        public int Quantity { get; set; }

        [Required(ErrorMessage = "Food name is required.")]
        public string FoodName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public int Price { get; set; }

        public int ImageID { get; set; }
        public string Title { get; set; }
        public string ImageDescription { get; set; }
        [AllowHtml]
        public string Contents { get; set; }
        public byte[] Image { get; set; }

    }
}