using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Explorers.Web.Areas.Admin.Models.UserModel
{
    public class ChangePasswordUserViewModel
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }

        public string UserName { get; set; }
    }
}