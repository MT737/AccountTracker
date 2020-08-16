using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "About" and "Contact" sections of the website.
    /// </summary>
    public class GeneralController : Controller
    {        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}