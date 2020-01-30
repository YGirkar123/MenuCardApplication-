using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CanteenManagementwithAdoDotNet.Models
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "MobileNumber is required.")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "EmailID is required.")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}