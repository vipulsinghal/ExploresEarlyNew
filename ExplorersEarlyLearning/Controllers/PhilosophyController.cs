using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExplorersEarlyLearning.Models;

namespace ExplorersEarlyLearning.Controllers
{
    public class PhilosophyController : Controller
    {
        //
        // GET: /Philosophy/

        public ActionResult Index()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 2 });
        }

    }
}
