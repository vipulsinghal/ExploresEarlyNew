using Explorers.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Explorers.Web.Areas.Admin.Models.UserModel
{
    public class ListUsersViewModel
    {
        public IList<User> CurrentUsers { get; set; }
        public HtmlString PagerString { get; set; }
    }
}