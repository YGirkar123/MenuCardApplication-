using CanteenManagement.Models;
using CanteenManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CanteenManagement.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult AddMenuList()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddMenuList(MenuModel mMenu)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                if (ModelState.IsValid)
                {
                    MenuRep mMenuRep = new MenuRep();
                    if (mMenuRep.AddNewMenu(mMenu))
                    {
                        ViewBag.Message = "Menu in list added successfully";
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            catch
            {
                return View();
            }
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

        public ActionResult EditMenu(int id)
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            MenuRep mMenuRep = new MenuRep();
            return View(mMenuRep.GetMenuList().Find(Menu => Menu.ID == id));
        }

        [HttpPost]
        public ActionResult EditMenu(int id, MenuModel obj)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                MenuRep mMenuRep = new MenuRep();
                mMenuRep.EditMenu(obj);
                return RedirectToAction("GetMenuList");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteMenu(int id)
        {
            try
            {
                if (Session["UserName"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                MenuRep mMenuRep = new MenuRep();
                if (mMenuRep.DeleteMenu(id))
                {
                    ViewBag.AlertMsg = "Menu deleted successfully";
                }
                return RedirectToAction("GetMenuList");
            }
            catch
            {
                return View();
            }
        }
    }
}