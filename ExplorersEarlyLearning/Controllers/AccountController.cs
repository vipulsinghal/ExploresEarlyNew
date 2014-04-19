using ExplorersEarlyLearning.DAL;
using ExplorersEarlyLearning.Filters;
using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
        private ExploresEarlyLearningContext db = new ExploresEarlyLearningContext();

        public ActionResult Login()
        {
            return View(new UserLoginModel());
        }

        public void SignOut()
        {
            WebSecurity.Logout();
            Response.Redirect("~/Account/Login");
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel userLogin)
        {
            if (ModelState.IsValid)
            {
                bool success = WebSecurity.Login(userLogin.UserName, userLogin.Password, false);
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
                else
                {
                    userLogin.message = "Your username and password is not valid.";
                }
                return View(userLogin);
            }
            else { 
                return View(userLogin);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            {
                Response.Redirect("~/Account/Login");
            }
           UserModel usermodel = new UserModel();

           IntializeRolesInModel(usermodel);
          
           return View(usermodel);
        }

        private static void IntializeRolesInModel(UserModel usermodel)
        {
            IList<RoleModel> lstRoleModel = new List<RoleModel>();
            foreach (string rolename in Roles.GetAllRoles())
            {
                lstRoleModel.Add(new RoleModel(rolename, false));
            }
            usermodel.AvailableRoles = lstRoleModel;
            usermodel.SelectedRoles = new List<RoleModel>();
            usermodel.RoleNames = new string[0];
        }

        [HttpPost]
        public ActionResult Add(UserModel usermodel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!WebSecurity.UserExists(usermodel.user.UserName))
                    {
                        WebSecurity.CreateUserAndAccount(usermodel.user.UserName, usermodel.Password,
                        propertyValues: new
                        {
                            FirstName = usermodel.user.FirstName,
                            LastName = usermodel.user.LastName,
                            MobileNumber = usermodel.user.MobileNumber,
                            IsActive = usermodel.user.IsActive,
                            CreateDate = DateTime.Now
                        }
                        );

                        if (usermodel.RoleNames != null && usermodel.RoleNames.Length > 0)
                        {
                            foreach (string rolename in usermodel.RoleNames)
                            {
                                Roles.AddUserToRole(usermodel.user.UserName, rolename);
                            }
                        }
                        usermodel.message = "User have been created Successfully.";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        usermodel.message = "User is already exists. Please try to create with different user.";
                        return View(usermodel);
                    }
                }
                catch (Exception ex)
                {
                    usermodel.message = "User have not been create Successfully.";
                    return View(usermodel);
                }
                
            }
            else
            {
                return View(usermodel);
            }
        }

        public ActionResult Edit(int id = 0)
        {
            if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            {
                Response.Redirect("~/Account/Login");
            }
            UserModel userModel = new UserModel();
            userModel.Password = "Exist";
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            userModel.user = user;

            IList<RoleModel> lstRoleModel = new List<RoleModel>();
            IList<RoleModel> lstSelRoleModel = new List<RoleModel>();
            foreach (string rolename in Roles.GetAllRoles())
            {
                lstRoleModel.Add(new RoleModel(rolename, false));
            }
            userModel.AvailableRoles = lstRoleModel;
            foreach (string selRoleName in Roles.GetRolesForUser(user.UserName))
            {
                lstSelRoleModel.Add(new RoleModel(selRoleName, true));
            }
            userModel.SelectedRoles = lstSelRoleModel;
            userModel.RoleNames = Roles.GetRolesForUser(user.UserName);
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Edit(UserModel usermodel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var existuser = (User)(from user in db.Users
                                           where
                                               user.UserName == usermodel.user.UserName && user.UserId != usermodel.user.UserId
                                           select user).FirstOrDefault();

                    if (existuser == null)
                    {

                        usermodel.user.UpdateDate = DateTime.Now;
                        db.Entry(usermodel.user).State = EntityState.Modified;
                        db.SaveChanges();

                        string[] existingRoles = Roles.GetRolesForUser(usermodel.user.UserName);

                        var deletedRoles = (from existRole in existingRoles
                                            where !usermodel.RoleNames.Contains(existRole)
                                            select existRole).ToList();

                        foreach (string deleteRole in deletedRoles)
                        {
                            Roles.RemoveUserFromRole(usermodel.user.UserName, deleteRole);
                        }

                        if (usermodel.RoleNames != null && usermodel.RoleNames.Length > 0)
                        {
                            foreach (string rolename in usermodel.RoleNames)
                            {
                                if (!Roles.IsUserInRole(usermodel.user.UserName, rolename))
                                {
                                    Roles.AddUserToRole(usermodel.user.UserName, rolename);
                                }
                            }
                        }
                        
                        usermodel.message = "User have been modified Successfully.";
                        return RedirectToAction("List");
                    }
                    else
                    {
                        usermodel.message = "User is already exists. Please try to update with different user.";
                        return View(usermodel);
                    }
                }
                catch (Exception ex)
                {
                    usermodel.message = "User have not been modified Successfully.";
                    return View(usermodel);
                }
            }
            else
            {
                return View(usermodel);
            }
            
        }

        public ActionResult Delete(int id = 0)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (Roles.GetRolesForUser(user.UserName).Count() > 0)
            {
                Roles.RemoveUserFromRoles(user.UserName, Roles.GetRolesForUser(user.UserName));
            }

            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.UserName); // deletes record from webpages_Membership table
            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(user.UserName, true); // deletes record from UserProfile table

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole("SuperAdmin"))
            {
                Response.Redirect("~/Account/Login");
            }
            List<User> userList = (from user in db.Users
                                   select user).ToList();
            return View(userList);
        }

        //public ActionResult ChangePassword()
        //{
        //    string userName = WebSecurity.CurrentUserName;
        //    UserChangePasswordModel userChangeModel = new UserChangePasswordModel();
        //    userChangeModel.UserName = userName;

        //    return View(userChangeModel);
        //}

        public ActionResult ChangePassword(int id = 0)
        {
            UserChangePasswordModel userChangeModel = new UserChangePasswordModel();
            if (id != 0)
            {
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                userChangeModel.UserName = user.UserName;
            }
            else
            {
                userChangeModel.UserName = WebSecurity.CurrentUserName;

            }
            return View(userChangeModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(UserChangePasswordModel userchangemodel)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurity.ChangePassword(userchangemodel.UserName, userchangemodel.Password, userchangemodel.NewPassword))
                {
                    return RedirectToAction("List");
                }
                else {
                    userchangemodel.message = "Your password is not correct";
                    return View(userchangemodel);
                }
            }
            else {
                return View(userchangemodel);
            }
            
        }
    }
}
