using CanteenManagementwithAdoDotNet.Models;
using CanteenManagementwithAdoDotNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanteenManagementwithAdoDotNet.Controllers
{
    public class UserRegistrationController : Controller
    {
        // GET: UserRegistration
        public ActionResult UserRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegistration(UserRegistration Emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserRegistrationRep mUserRegistrationRepo = new UserRegistrationRep();

                    if (mUserRegistrationRepo.AddNewUser(Emp))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}