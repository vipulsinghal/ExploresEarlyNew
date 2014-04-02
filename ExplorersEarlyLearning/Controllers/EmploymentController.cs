using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExplorersEarlyLearning.Controllers
{
    public class EmploymentController : Controller
    {
        //
        // GET: /Employment/

        public ActionResult Index()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 6 });
        }

    }
}
