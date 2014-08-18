using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopTrumps.Helpers;

namespace TopTrumps.Controllers
{
    public class HomeController : Controller
    {
        private PackHelper packHelper = new PackHelper();
        
        public ActionResult Index()
        {
            ViewBag.Message = "Top Trumps is the card game that brings your favourite things to life, whether it's fast cars, football or Star Wars.";

            packHelper.CreatePack();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Top Trumps is the card game that brings your favourite things to life, whether it's fast cars, football or Star Wars.";

            return View();
        }

        public ActionResult DropDb()
        {
            packHelper.ClearDb();
            
            return View("Index");
        }
    }
}
