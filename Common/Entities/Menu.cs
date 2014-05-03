using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Explorers.Infrastructure.Entities
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string PageTitle { get; set; }
        public int? ParentMenuId { get; set; }

        public Int16 OrderIndex { get; set; }
        public bool IsActive { get; set; }
        public PageType? PageType { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public enum PageType {
        Home, 
        Philosophy, 
        Program, 
        Center, 
        Portfolio, 
        Employment, 
        ContactUs
    }
}