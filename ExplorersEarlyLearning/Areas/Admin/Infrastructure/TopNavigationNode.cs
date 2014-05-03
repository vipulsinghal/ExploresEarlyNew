using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Explorers.Web.Areas.Admin.Infrastructure
{
    public class TopNavigationNode
    {
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string NavigateUrl { get; set; }
        public TopNavigationNode Parent { get; set; }
        public TopNavigationNodeType Type { get; set; }
        public IList<TopNavigationNode> Children { get; set; } 
    }

    public enum TopNavigationNodeType
    {
        Settings
    }
}