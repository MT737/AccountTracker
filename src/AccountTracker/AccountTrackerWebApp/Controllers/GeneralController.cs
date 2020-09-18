using System.Web.Mvc;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "About" and "Contact" sections of the website.
    /// </summary>
    public class GeneralController : Controller
    {        
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "This web application is intended for demonstration purposes only.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "See Git repo to contact the creator.";

            return View();
        }
    }
}