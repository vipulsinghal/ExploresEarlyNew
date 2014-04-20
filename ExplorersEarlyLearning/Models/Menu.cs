using ExplorersEarlyLearning.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExplorersEarlyLearning.Models
{
    public class Menu
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }
        [Required]
        public string Name { get; set; }
        public string PageTitle { get; set; }
        public int? ParentMenuId { get; set; }

        public Int16 OrderIndex { get; set; }
        public bool IsActive { get; set; }
        public PageType? PageType { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

}