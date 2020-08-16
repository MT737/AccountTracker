using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using AccountTrackerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "Dashboard" section of the website.
    /// </summary>
    public class DashboardController : BaseController
    {

        //Private field
        private TransactionRepository _transactionRepository = null;

        public DashboardController()
        {
            _transactionRepository = new TransactionRepository(Context);
        }



        public ActionResult Index()
        {   
            //Instantiate dashboardviewmodel
            var dashboardItems = new DashboardViewModel();
            IList<Transaction> transactions = new List<Transaction>();

            //Get get a list of transactions to gain access to transaction ids
            foreach (var transaction in _transactionRepository.GetList())
            {
                //Get the detailed data for each transaction and add it to the DashboardViewModel
                transactions.Add(_transactionRepository.Get(transaction.TransactionID, true));
            }

            dashboardItems.Transactions = transactions;
            
            return View(dashboardItems);
        }
    }
}