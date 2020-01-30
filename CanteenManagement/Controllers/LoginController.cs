using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CanteenManagement.Models;
using CanteenManagement.Repository;

namespace CanteenManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginModel objUser)
        {
            if (ModelState.IsValid)
            {
                LoginRep mLoginRep = new LoginRep();
                var obj = mLoginRep.GetMenuList().Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                //var obj = db.UserProfiles.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserName"] = obj.UserName.ToString();
                    Session["UserType"] = obj.UserType.ToString();
                    Session.Timeout = 10;
                    if (obj.UserType.ToString() == "Admin")
                    {
                        return RedirectToAction("UserDashBoard");
                    }
                    else
                    {
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View();
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserName"] != null)
            {
                ViewBag.Message = "Hello "+ Session["UserName"];
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}