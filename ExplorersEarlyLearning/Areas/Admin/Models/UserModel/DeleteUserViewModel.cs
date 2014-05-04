using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Explorers.Infrastructure.Entities;

namespace Explorers.Web.Areas.Admin.Models.UserModel
{
    public class DeleteUserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}