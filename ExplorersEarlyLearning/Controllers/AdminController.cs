using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExplorersEarlyLearning.Models;
using ExplorersEarlyLearning.DAL;
using WebMatrix.WebData;
using System.Web.Security;
using ExplorersEarlyLearning.Common;

namespace ExplorersEarlyLearning.Controllers
{
    
    public class AdminController : Controller
    {
        private ExploresEarlyLearningContext db = new ExploresEarlyLearningContext();

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            //if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            if (!CommonUtility.CheckUserLogin())
            {
                RedirectToAction("Login", "Account");
            }

            List<Menu> menuList = (from menu in db.Menus
             where menu.IsActive==true
             select menu).ToList();

            return View(menuList);
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id = 0)
        {
            //if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            if (!CommonUtility.CheckUserLogin())
            {
                RedirectToAction("Login", "Account");
            }

            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            //if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            if (!CommonUtility.CheckUserLogin())
            {
                RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult CreateSubPage(int id)
        {
            //if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            if (!CommonUtility.CheckUserLogin())
            {
                RedirectToAction("Login", "Account");
            }
            Menu menu = new Menu();
            menu.ParentMenuId = id;
            return View("Create",menu);
        }
        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            if (!CommonUtility.CheckUserLogin())
            {
                RedirectToAction("Login","Account");
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                menu.UpdateDate = DateTime.Now;
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            if (!CommonUtility.CheckUserLogin())
            {
                Response.Redirect("~/Account/Login");
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}