using AccountTrackerLibrary.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AccountTrackerWebApp.Controllers
{
    public abstract class BaseController : Controller 
    {
        //Tracking if the dispose method has already been called.
        private bool _disposed = false;

        //Property
        public Context Context { get; set; }
        
        //Base constructor
        public BaseController()
        {
            Context = new Context();
        }

        //The bool parameter of this method determines if managed resources should be removed as well.
        protected override void Dispose(bool disposing)
        {
            //TODO: Review this: If _disposed, then short circuit the method by returning. Guarding against the dispose method being called more than once.

            if (disposing)
            {
                Context.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}