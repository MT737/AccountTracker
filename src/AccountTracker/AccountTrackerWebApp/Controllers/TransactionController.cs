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
    /// Controller for the "Transction" Portion of the website.
    /// </summary>
    public class TransactionController : BaseController
    {
        //Private field
        private TransactionRepository _transactionRepository = null;

        //Base constructor
        public TransactionController()
        {
            _transactionRepository = new TransactionRepository(Context);
        }

        public ActionResult Index()
        {
            //Instantiate viewmodel
            var transactions = new ViewModel();

            //Complete viewmodel property required for transaction view
            transactions.Transactions = GetTransactionsWithDetails();

            return View(transactions);
        }
    }
}