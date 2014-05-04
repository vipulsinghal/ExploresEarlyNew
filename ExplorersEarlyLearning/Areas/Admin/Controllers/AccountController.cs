using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;
using System.Web.Security;
using Explorers.Web.Infrastructure;
using WebMatrix.WebData;
using Explorers.Web.Areas.Admin.Infrastructure;
using Explorers.Web.Areas.Admin.Controllers.BaseController;
using DataAccess.RepoServices;
using Explorers.Web.Areas.Admin.Models.UserModel;
using DataAccess;
using System.Web;
using Infrastructure;

namespace Explorers.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = ExplorerUserRoles.SuperAdmin + "," + ExplorerUserRoles.Admin)]
    public class AccountController : ExplorersControllerBase
    {
        //
        // GET: /Account/
        private RepoUsers _repoUsers;

        public AccountController()
        {
            _repoUsers = new RepoUsers(new ExDatabaseContext());
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public void SignOut()
        {
            WebSecurity.Logout();
            RedirectToAction("Login", new{Area = ExplorerWebAreas.Admin.ToString()});
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            bool success = WebSecurity.Login(form["username"], form["password"], false);
            var user= HttpContext.User;
            if (success)
            {
                string returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null)
                {
                   return RedirectToAction("Index", "Home", new{Area = ExplorerWebAreas.Admin});
                }
                else
                {
                    Response.Redirect(returnUrl);
                }
            }
            return View();
        }

        public ActionResult Index()
        {
            var lstUserModel = GetListingModel();
            return View(lstUserModel);
        }

        private ListUsersViewModel GetListingModel()
        {
            var lstUsers = _repoUsers.GetAllUsers();
            var model = new ListPager(lstUsers.Count);
            //var pagerString = new HtmlString(RenderPartialViewToString("_Pager", model));
            var lstUserModel = new ListUsersViewModel
            {
                CurrentUsers = lstUsers
                //PagerString = pagerString
            };
            return lstUserModel;
        }

        [HttpGet]
        public JsonResult Register()
        {
           var viewModel = new RegisterUserViewModel();
           var viewAdd = RenderPartialViewToString("Register", viewModel);
           return Json(new JsonResponse((object)viewAdd), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Edit(int userId=0)
        {
            var getUser = _repoUsers.DbContext.Users.FirstOrDefault(usr => usr.UserId == userId);
            if (getUser == null)
                return Json(new JsonResponse("Invalid User!", "Invalid user id."), JsonRequestBehavior.AllowGet);

            var viewModel = new EditUserViewModel
            {
                UserId = getUser.UserId,
                UserName = getUser.UserName,
                FirstName = getUser.FirstName,
                LastName = getUser.LastName,
                IsActive = getUser.IsActive
            };

            var viewEdit = RenderPartialViewToString("Edit", viewModel);
            return Json(new JsonResponse((object)viewEdit), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(EditUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "Please provide correct details for page"));


            var getUser = _repoUsers.DbContext.Users.FirstOrDefault(usr => usr.UserId == viewModel.UserId);
            if (getUser == null)
                return Json(new JsonResponse("Invalid User!", "Invalid user id."));

            if (_repoUsers.DbContext.Users.FirstOrDefault(usr => usr.UserId != viewModel.UserId && usr.UserName == viewModel.UserName) != null)
                return Json(new JsonResponse("User exists Already!", "A user with same username already exists"));
            
            getUser.UserName =viewModel.UserName;
            getUser.FirstName =viewModel.FirstName;
            getUser.LastName=viewModel.LastName;
            getUser.IsActive=viewModel.IsActive;
            getUser.UpdateDate =DateTime.Now;
            
            _repoUsers.DbContext.SaveChanges();
           
                //usermodel.user.UpdateDate = DateTime.Now;
                //db.Entry(usermodel.user).State = EntityState.Modified;
                //db.SaveChanges();

                //string[] existingRoles = Roles.GetRolesForUser(usermodel.user.UserName);

                //var deletedRoles = (from existRole in existingRoles
                //                    where !(usermodel.RoleNames != null && usermodel.RoleNames.Contains(existRole))
                //                    select existRole).ToList();

                //foreach (string deleteRole in deletedRoles)
                //{
                //    Roles.RemoveUserFromRole(usermodel.user.UserName, deleteRole);
                //}

                //if (usermodel.RoleNames != null && usermodel.RoleNames.Length > 0)
                //{
                //    foreach (string rolename in usermodel.RoleNames)
                //    {
                //        if (!Roles.IsUserInRole(usermodel.user.UserName, rolename))
                //        {
                //            Roles.AddUserToRole(usermodel.user.UserName, rolename);
                //        }
                //    }
                //}

            var lstUserModel = GetListingModel();

            return Json(new JsonResponse((object)null)
            {
                Message = "User updated successfully!",
                Description = "User details have been updated successfully!",
                Content =
                new
                {
                    UserList = RenderPartialViewToString("List", lstUserModel)
                }
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Register(RegisterUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "Please provide correct details for page"));

            if (WebSecurity.UserExists(viewModel.UserName))
            {
                return Json(new JsonResponse("User exists Already!", "A user with same username already exists"));
            }


            WebSecurity.CreateUserAndAccount(viewModel.UserName, viewModel.Password,
            propertyValues: new
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                IsActive = viewModel.IsActive,
                CreateDate = DateTime.Now
            }
            );

            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }
            
            Roles.AddUserToRole(viewModel.UserName, "Admin");

            var lstUserModel = GetListingModel();

            return Json(new JsonResponse(
                new
                {
                    UserList = RenderPartialViewToString("List", lstUserModel)
                })
                {
                    Message = "User Added Successfully!",
                    Description = "New User has been added succesfully."
                });
        }

        public JsonResult ChangePassword(int userId = 0)
        {
            var getUser = _repoUsers.DbContext.Users.FirstOrDefault(usr => usr.UserId == userId);
            if (getUser == null)
                return Json(new JsonResponse("Invalid User!", "Invalid user id."), JsonRequestBehavior.AllowGet);

            var viewModel = new ChangePasswordUserViewModel
            {
                UserName = getUser.UserName
            };

            var viewChangePassword = RenderPartialViewToString("ChangePassword", viewModel);
            return Json(new JsonResponse((object)viewChangePassword), JsonRequestBehavior.AllowGet);
           
            //return View(userChangeModel);
        }

        [HttpPost]
        public JsonResult ChangePassword(UserChangePasswordModel userchangemodel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "Please provide correct details for page"));

            var getUser = _repoUsers.DbContext.Users.FirstOrDefault(usr => usr.UserName == userchangemodel.UserName);
            if (getUser == null)
                return Json(new JsonResponse("Invalid User!", "Invalid user name."));

            var lstUserModel = GetListingModel();

            if (WebSecurity.ChangePassword(userchangemodel.UserName, userchangemodel.Password, userchangemodel.NewPassword))
            {
                return Json(new JsonResponse(
             new
             {
                 UserList = RenderPartialViewToString("List", lstUserModel)
             })
                {
                    Message = "Change Password!",
                    Description = "Change Password Changed Successfully."
                });
            }
            else {
                return Json(new JsonResponse("Invalid Password!", "Your password is not correct."));
            }

        }

        [HttpGet]
        public JsonResult Delete(int userId = 0)
        {
            var getUser = _repoUsers.DbContext.Users.FirstOrDefault(usr => usr.UserId == userId);
            if (getUser == null)
                return Json(new JsonResponse("Invalid User!", "Invalid user id."), JsonRequestBehavior.AllowGet);

            var viewModel = new DeleteUserViewModel
            {
                UserId = getUser.UserId,
                UserName = getUser.UserName
            };

            var viewDelete = RenderPartialViewToString("Delete", viewModel);
            return Json(new JsonResponse((object)viewDelete), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Delete(DeleteUserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResponse("Invalid Data", "This is an invalid page"));

            var getUser = _repoUsers.DbContext.Users.FirstOrDefault(usr => usr.UserId == viewModel.UserId);
            if (getUser == null)
                return Json(new JsonResponse("Invalid User!", "Invalid user id."));

            //_repoWebPage.DbContext.WebPages.Remove(getPage);
            //_repoWebPage.DbContext.SaveChanges();
            try
            {
                if (Roles.GetRolesForUser(getUser.UserName).Count() > 0)
                {
                    Roles.RemoveUserFromRoles(getUser.UserName, Roles.GetRolesForUser(getUser.UserName));
                }

                ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(getUser.UserName); // deletes record from webpages_Membership table
                ((SimpleMembershipProvider)Membership.Provider).DeleteUser(getUser.UserName, true); // deletes record from UserProfile table

                var lstUserModel = GetListingModel();

                return Json(new JsonResponse(
                new
                {
                    UserList = RenderPartialViewToString("List", lstUserModel)
                })
                {
                    Message = "User deleted successfully!",
                    Description = "User has been deleted successfully!",
                });
            }
            catch (Exception ex) {
                return Json(new JsonResponse("Error!", "Error in delete user."));
            }
        }
    }
}
