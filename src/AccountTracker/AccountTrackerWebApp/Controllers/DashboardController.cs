using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using AccountTrackerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static AccountTrackerWebApp.ViewModels.DashboardViewModel;

namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "Dashboard" section of the website.
    /// </summary>
    public class DashboardController : BaseController
    {

        //Private field
        private TransactionRepository _transactionRepository = null;
        private AccountRepository _accountRepository = null;

        public DashboardController()
        {
            _transactionRepository = new TransactionRepository(Context);
            _accountRepository = new AccountRepository(Context);
        }


        public ActionResult Index()
        {   
            //Instantiate dashboardviewmodel
            var dashboardItems = new DashboardViewModel();

            //TODO: Create methods within this controller to handle process of filling each of the view model's properties.

            //Instantiate ILists for each of the viewmodel's properties.
            IList<Transaction> transactions = new List<Transaction>();
            IList<AccountWithBalance> accountsWithBalances = new List<AccountWithBalance>();

            //Get get a list of transactions to gain access to transaction ids
            foreach (var transaction in _transactionRepository.GetList())
            {
                //Get the detailed data for each transaction and add it to the IList of transactions
                transactions.Add(_transactionRepository.Get(transaction.TransactionID, true));
            }            
            //Add IList of transactions to the associated Dashboardviewmodel property.
            dashboardItems.Transactions = transactions;

            //Get list of accounts
            foreach (var account in _accountRepository.GetList())
            {
                //Set detailed values and get amount
                var accountWithBalance = new AccountWithBalance();
                accountWithBalance.AccountId = account.AccountID;
                accountWithBalance.Name = account.Name;
                accountWithBalance.IsAsset = account.IsAsset;
                accountWithBalance.IsActive = account.IsActive;
                accountWithBalance.Balance = _accountRepository.GetBalance(account.AccountID, account.IsAsset);
                accountsWithBalances.Add(accountWithBalance);
            }
            //Add IList of accounts with balances to the associated Dashboardviewmodel property.
            dashboardItems.AccountsWithBalances = accountsWithBalances;
            
            
            return View(dashboardItems);
        }
    }
}