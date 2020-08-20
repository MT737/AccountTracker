using AccountTrackerLibrary.Models;
using AccountTrackerLibrary.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static AccountTrackerWebApp.ViewModels.ViewModel;

namespace AccountTrackerWebApp.Controllers
{
    public abstract class BaseController : Controller 
    {
        //Tracking if the dispose method has already been called.
        private bool _disposed = false;
        protected TransactionRepository _transactionRepository = null;
        protected TransactionTypeRepository _transactionTypeRepository = null;
        protected AccountRepository _accountRepository = null;
        protected CategoryRepository _categoryRepository = null;
        protected VendorRepository _vendorRepository = null;

        //Property
        public Context Context { get; set; }
        
        //Base constructor
        public BaseController()
        {
            Context = new Context();
            _transactionRepository = new TransactionRepository(Context);
            _transactionTypeRepository = new TransactionTypeRepository(Context);
            _accountRepository = new AccountRepository(Context);
            _categoryRepository = new CategoryRepository(Context);
            _vendorRepository = new VendorRepository(Context);
        }

        public IList<Transaction> GetTransactionsWithDetails()
        {
            //Get get a list of transactions to gain access to transaction ids
            IList<Transaction> transactions = new List<Transaction>();
            foreach (var transaction in _transactionRepository.GetList())
            {
                //Get the detailed data for each transaction and add it to the IList of transactions
                transactions.Add(_transactionRepository.Get(transaction.TransactionID, true));
            }

            return transactions;
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

        public IList<AccountWithBalance> GetAccountWithBalances()
        {
            //Get list of accounts
            IList<AccountWithBalance> accountsWithBalances = new List<AccountWithBalance>();
            foreach (var account in _accountRepository.GetList())
            {
                //Set detailed values and get amount
                AccountWithBalance accountWithBalanceHolder = new AccountWithBalance();
                accountWithBalanceHolder.AccountID = account.AccountID;
                accountWithBalanceHolder.Name = account.Name;
                accountWithBalanceHolder.IsAsset = account.IsAsset;
                accountWithBalanceHolder.IsActive = account.IsActive;
                accountWithBalanceHolder.Balance = _accountRepository.GetBalance(account.AccountID, account.IsAsset);
                accountsWithBalances.Add(accountWithBalanceHolder);
            }

            return accountsWithBalances;
        }
    }
}