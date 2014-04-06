using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExplorersEarlyLearning.Controllers
{
    public class ProgramsController : Controller
    {
        //
        // GET: /Programs/

        public ActionResult Index()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 3 });
        }

        public ActionResult AnEmergent()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 3, NavSubCurrentIndex = 1 });
        }

        public ActionResult EarlyYearsFramework()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 3, NavSubCurrentIndex = 2 });
        }

        public ActionResult OnlinePortfoliosMethod()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 3, NavSubCurrentIndex = 4 });
        }

        public ActionResult EnvironmentalFocus()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 3, NavSubCurrentIndex = 3 });
        }

        public ActionResult KindergartenProgram()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 3, NavSubCurrentIndex = 4 });
        }

        public ActionResult ReadyProgram()
        {
            return View(new ViewModelBase() { NavCurrentIndex = 3, NavSubCurrentIndex = 5 });
        }
    }
}
