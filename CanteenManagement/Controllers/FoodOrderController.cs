using CanteenManagement.Models;
using CanteenManagement.Repository;
using CanteenManagementwithAdoDotNet.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanteenManagement.Controllers
{
    public class FoodOrderController : Controller
    {
        // GET: FoodOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMenuList()
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                MenuRep mMenuRep = new MenuRep();
                ModelState.Clear();
                return View(mMenuRep.GetMenuList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult GetMenuList(MenuModel mMenuModel)
        {
            return View();
        }

        public ActionResult AddMenuList(int ID)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                if (ModelState.IsValid)
                {
                    FoodOrderRep mFoodOrderRep = new FoodOrderRep();
                    if (mFoodOrderRep.AddToCart(ID, Session["UserName"].ToString(), 1))
                    {
                        ViewBag.Message = "Food added in Card";
                    }
                    return RedirectToAction("ViewCart");
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ViewCart()
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                FoodOrderRep mFoodOrderRep = new FoodOrderRep();
                return View(mFoodOrderRep.GetOrderByStatus(Session["UserName"].ToString(), "Cart", "Student"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ViewPlacedCart()
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                FoodOrderRep mFoodOrderRep = new FoodOrderRep();
                return View(mFoodOrderRep.GetOrderByStatus(Session["UserName"].ToString(), "Placed", "Student"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult DeleteFromCart(int id)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                FoodOrderRep mFoodOrderRep = new FoodOrderRep();
                if (mFoodOrderRep.DeleteFromCart(id))
                {
                    ViewBag.AlertMsg = "Food Item deleted successfully";
                }
                return RedirectToAction("ViewCart");
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult PlaceOrder()
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                FoodOrderRep mFoodOrderRep = new FoodOrderRep();
                if (mFoodOrderRep.PlaceOrder(Session["UserName"].ToString()))
                {
                    ViewBag.AlertMsg = "Order Placed successfully";
                }
                return RedirectToAction("ViewCart");
            }
            catch
            {
                return RedirectToAction("Login", "Login");
            }
        }
    }
}