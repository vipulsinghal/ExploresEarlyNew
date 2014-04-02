using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExplorersEarlyLearning.Controllers
{
    public class PortfoliosController : Controller
    {
        //
        // GET: /Portfolio/

        public ActionResult Index()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 5 });
        }

    }
}
