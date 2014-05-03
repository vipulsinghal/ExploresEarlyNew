using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Explorers.Infrastructure.Entities;

namespace Explorers.Web.Areas.Admin.Models.PageModel
{
    public class PagesTreeView
    {
        public IList<WebPage> CurrentPages { get; set; } 
    }
}