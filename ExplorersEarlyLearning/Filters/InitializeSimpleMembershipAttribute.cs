using ExplorersEarlyLearning.Common;
using ExplorersEarlyLearning.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace ExplorersEarlyLearning.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<ExploresEarlyLearningContext>(null);

                try
                {
                    using (var context = new ExploresEarlyLearningContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("ExploresEarlyLearningContext", "User", "UserId", "UserName", autoCreateTables: true);
                    

                    if (!Roles.RoleExists(UserRoles.SuperAdmin.ToString()))
                    {
                        Roles.CreateRole(UserRoles.SuperAdmin.ToString());
                    }
                    if (!Roles.RoleExists(UserRoles.Admin.ToString()))
                    {
                        Roles.CreateRole(UserRoles.Admin.ToString());
                    }
                    if (!WebSecurity.UserExists("Admin"))
                    {
                        WebSecurity.CreateUserAndAccount("Admin", "Admin", propertyValues: new
                        {
                            FirstName = "Admin",
                            LastName = "Admin",
                            IsActive = true,
                            CreateDate = DateTime.Now
                        });
                        Roles.AddUserToRole("Admin", UserRoles.SuperAdmin.ToString());
                    }
                    /*
                    Roles.CreateRole("SuperAdmin");
                    Roles.AddUserToRole("Admin", "SuperAdmin");
                     * 
                    */
                    //Roles.GetAllRoles();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}