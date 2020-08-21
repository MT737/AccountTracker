using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "Vendor" section of the website.
    /// </summary>
    public class VendorController : BaseController
    {
        //Index
        public ActionResult Index()
        {
            IList<Vendor> vendors = new List<Vendor>();
            vendors = _vendorRepository.GetList();

            return View(vendors);
        }

        //Add

        //Add post
        //Make sure to validate 

        //Edit
        
        //Edit Post
        //Make sure to validate and don't allow edit of default vendors

        //delete

        //Delete post
        //Use absorption approach and don't allow deletion of default vendors



    }
}