using ExplorersEarlyLearning.Filters;
using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace ExplorersEarlyLearning.Controllers
{
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
        }

        public void SignOut()
        {
            WebSecurity.Logout();
            Response.Redirect("~/Account/Login");
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            bool success = WebSecurity.Login(form["username"], form["password"], false);
            if (success)
            {
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null)
                {
                    Response.Redirect("~/Admin/Index");
                }
                else
                {
                    Response.Redirect(returnUrl);
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            //if (!WebSecurity.Initialized)
            //{
            //    WebSecurity.InitializeDatabaseConnection("ExploresEarlyLearningContext", "User", "UserId", "UserName", autoCreateTables: true);
            //    WebSecurity.CreateUserAndAccount("Admin", "Admin");
            //    Roles.CreateRole("Administrator");
            //    Roles.AddUserToRole("Admin", "Administrator");
            //}

            if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("Administrator"))
            {
                Response.Redirect("~/Account/Login");
            }
           return View(new UserModel());
            
        }

        [HttpPost]
        public ActionResult Register(UserModel usermodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!WebSecurity.UserExists(usermodel.user.UserName))
                    {
                        WebSecurity.CreateUserAndAccount(usermodel.user.UserName, usermodel.Password);
                        if (!Roles.RoleExists("Administrator"))
                        {
                            Roles.CreateRole("Administrator");
                        }
                        Roles.AddUserToRole(usermodel.user.UserName, "Administrator");
                        usermodel.message = "User have been registered Successfully.";
                    }
                    else {
                        usermodel.message = "User is already exists. Please try to register with different user.";
                    }
                }
                catch (Exception ex)
                {
                    usermodel.message = "User have not been registered Successfully.";
                }
            }
            return View(usermodel);
        }

    }
}
