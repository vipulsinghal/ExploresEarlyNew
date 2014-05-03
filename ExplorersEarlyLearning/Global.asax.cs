using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DataAccess;
using Explorers.Web.Infrastructure;
using WebMatrix.WebData;
using System.Threading;
using System.Web.Security;

namespace Explorers.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ////Database.SetInitializer(new ExDatabaseContextInitializer());
            ////WebSecurity.InitializeDatabaseConnection("ExplorersEarlyLearningContext", "User", "Id", "Name", true);
            //Database.SetInitializer<ExDatabaseContext>(new InitiatizeSimpleMembership());
            //var context = new ExDatabaseContext();
            //context.Database.Initialize(true);
            //if (!WebSecurity.Initialized)
            //    WebSecurity.InitializeDatabaseConnection("ExplorersEarlyLearningContext",
            //         "User", "UserId", "UserName", autoCreateTables: true);
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }
    }

    public class SimpleMembershipInitializer
    {
        public SimpleMembershipInitializer()
        {
            using (var context = new ExDatabaseContext())
                context.Users.Find(1);

            if (!WebSecurity.Initialized)
                WebSecurity.InitializeDatabaseConnection("ExplorersEarlyLearningContext", "User", "UserId", "UserName", autoCreateTables: true);

            Seed();
        }

        private void Seed()
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
            if (!roles.RoleExists("SuperAdmin"))
            {
                roles.CreateRole("SuperAdmin");
            }
            if (!WebSecurity.UserExists("SuperAdmin"))
            {
                WebSecurity.CreateUserAndAccount("superadmin", "superadmin");
                roles.AddUsersToRoles(new[] { "superadmin" }, new[] { "superadmin" });
            }
        }
    }
}