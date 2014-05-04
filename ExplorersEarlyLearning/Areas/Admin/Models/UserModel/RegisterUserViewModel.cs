using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Explorers.Web.Areas.Admin.Models.UserModel
{
    public class RegisterUserViewModel
    {
        [StringLength(50, ErrorMessage = "Please enter a shorter User Name")]
        [Required(ErrorMessage = "Please enter User Name")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        [StringLength(50, ErrorMessage = "Please enter a shorter First Name")]
        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; }
    }
}