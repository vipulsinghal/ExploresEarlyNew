using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExplorersEarlyLearning.Models
{
    public class UserModel
    {
        public User user { get; set; }
        [Required]
        public string Password { get; set; }
        
        public UserModel() {
            user = new User();
        }
        public string message { get; set; }

        public IList<RoleModel> AvailableRoles { get; set; }
        public IList<RoleModel> SelectedRoles { get; set; }
        [Required]
        public string[] RoleNames { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        public string UserName { get; set; }
        public string message { get; set; }
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class UserChangePasswordModel
    {
        public string message { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NewPassword { get; set; }

        public string UserName { get; set; }
    }

    public class RoleModel
    {
        public RoleModel(string name, bool isSelected)
        {
            this.Name = name;
            this.IsSelected = isSelected;
        }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}