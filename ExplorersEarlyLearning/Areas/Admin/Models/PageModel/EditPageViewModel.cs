using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Explorers.Infrastructure.Entities;

namespace Explorers.Web.Areas.Admin.Models.PageModel
{
    public class EditPageViewModel
    {
        public int PageId { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; } 
    }
}