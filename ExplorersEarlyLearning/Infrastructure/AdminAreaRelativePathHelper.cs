using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Explorers.Web.Infrastructure
{
    public static class AdminAreaRelativePathHelper
    {
        public static string ContentAdmin(this UrlHelper urlHelper, string path)
        {
            if (path.StartsWith("~/"))
                path = "~/Areas/" + ExplorerWebAreas.Admin.ToString() + path.Substring(1);
            return urlHelper.Content(path);
        }
    }
}