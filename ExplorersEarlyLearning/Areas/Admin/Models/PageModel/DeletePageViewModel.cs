using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Explorers.Infrastructure.Entities;

namespace Explorers.Web.Areas.Admin.Models.PageModel
{
    public class DeletePageViewModel
    {
        [Required]
        public int PageId { get; set; }

        public string Title { get; set; }

        public bool IsPublished { get; set; }
    }
}