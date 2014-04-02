using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExplorersEarlyLearning.Controllers
{
    public class CentersController : Controller
    {
        //
        // GET: /Centers/

        public ActionResult Index()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 4 });
        }

        public ActionResult AbbotsfordRichmond()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 4, NavSubCurrentIndex = 6});
        }

        public ActionResult MaidstoneMaribyrnong()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 4, NavSubCurrentIndex = 7});
        }
    }
}
