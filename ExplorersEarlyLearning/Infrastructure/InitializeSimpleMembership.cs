using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Explorers.Web.Infrastructure
{
    public class InitiatizeSimpleMembership : DropCreateDatabaseAlways<ExDatabaseContext>
    {
        protected override void Seed(ExDatabaseContext context)
        {

            WebSecurity.InitializeDatabaseConnection("ExplorersEarlyLearningContext",
               "User", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("SuperAdmin"))
            {
                roles.CreateRole("SuperAdmin");
            }
            if (!WebSecurity.UserExists("SuperAdmin"))
            {
                WebSecurity.CreateUserAndAccount("superadmin", "superadmin");
                roles.AddUsersToRoles(new []{"superadmin"}, new[]{ "superadmin"});
            }

        }
    }
}