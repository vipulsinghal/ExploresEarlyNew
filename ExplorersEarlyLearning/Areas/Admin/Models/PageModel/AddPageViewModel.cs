using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Explorers.Infrastructure.Entities;

namespace Explorers.Web.Areas.Admin.Models.PageModel
{
    public class AddPageViewModel
    {
        [StringLength(50, ErrorMessage = "Please enter a shorter Page TItle")]
        [Required(ErrorMessage = "Please enter Page TItle")]
        [DisplayName("Page Title")]
        public string Title { get; set; }

        [StringLength(5000, ErrorMessage = "Page contents exceed allowed limit.")]
        [DisplayName("Contents")]
        public string Contents { get; set; }

        public int? ParentId { get; set; }

        public bool IsPublished { get; set; }
        public string ParentName { get; set; }
    }
}