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
    }
}