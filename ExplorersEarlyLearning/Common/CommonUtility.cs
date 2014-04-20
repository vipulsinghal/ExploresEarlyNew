using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace ExplorersEarlyLearning.Common
{
    public static class CommonUtility
    {
        public static bool CheckUserLogin(){
            if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole(UserRoles.SuperAdmin.ToString()))
            {
                return true;
            }
            else if (!WebSecurity.IsAuthenticated && !Roles.IsUserInRole(UserRoles.Admin.ToString()))
            {
                return true;
            }
            else {
                return false;
            }
        }

        public static bool IsUserEditPermission(string userName)
        {
            if (userName == WebSecurity.CurrentUserName)
            {
                return true;
            }
            else
            {
                //return Roles.IsUserInRole(WebSecurity.CurrentUserName, ExplorersEarlyLearning.Common.UserRoles.SuperAdmin.ToString());
                return true;
            }
        }
    }
}