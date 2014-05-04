//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Optimization;

namespace Explorers.Web.Areas.Admin.Infrastructure
{
    public class TopNavigation
    {
        public static IList<TopNavigationNode> MenuItems;

        static TopNavigation()
        {
            MenuItems = new List<TopNavigationNode>
            {
                new TopNavigationNode
                {
                    Title = "Manage Content", Type = TopNavigationNodeType.Settings,
                    Children = new List<TopNavigationNode>
                    {
                          new TopNavigationNode{Title = "Pages", Area = "Admin", Controller = "Page", Action = "Index"},
                          new TopNavigationNode{Title = "Gallery", Area = "Admin", Controller = "Page", Action = "Manage"}
                    }
                },
                new TopNavigationNode
                {
                    Title = "Staff Administration", Area = "Admin", Controller = "Staff", Action = "Manage", Type = TopNavigationNodeType.Settings,
                    Children = new List<TopNavigationNode>
                    {
                       new TopNavigationNode{Title = "Manage Users", Area = "Admin", Controller = "Account", Action = "Index", Type = TopNavigationNodeType.Settings}
                       //new TopNavigationNode{Title = "Create New", Area = "Admin", Controller = "Staff", Action = "Create", Type = TopNavigationNodeType.Settings}
                    }
                },
                new TopNavigationNode{Title = "Configuration", Area = "Admin", Controller = "Config", Action = "Manage", Type = TopNavigationNodeType.Settings}
            };
        }
    }
}