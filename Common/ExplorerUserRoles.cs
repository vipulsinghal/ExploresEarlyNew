using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public static class ExplorerUserRoles
    {
        public const string SuperAdmin = "superadmin";
        public const string Admin = "admin";
        public const string Parent = "parent";

        public static string[] GetAll()
        {
            return new[] { SuperAdmin, Admin, Parent };
        }
    }
}
