using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;
using AccountTrackerWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static AccountTrackerWebApp.ViewModels.ViewModel;


namespace AccountTrackerWebApp.Controllers
{
    /// <summary>
    /// Controller for the "Dashboard" section of the website.
    /// </summary>
    public class DashboardController : Controller
    {
        private AccountRepository _accountRepository = null;
        private CategoryRepository _categoryRepository = null;
        private TransactionRepository _transactionRepository = null;
        private VendorRepository _vendorRepository = null;

        public DashboardController(AccountRepository accountRepository, CategoryRepository categoryRepository, TransactionRepository transactionRepository,
            VendorRepository vendorRepository)
        {
            _accountRepository = accountRepository;
            _categoryRepository = categoryRepository;
            _transactionRepository = transactionRepository;
            _vendorRepository = vendorRepository;
        }

        public ActionResult Index()
        {
            //Instantiate viewmodel
            //TODO Create a dashbaord viewmodel.
            var vm = new ViewModel();

            //Complete viewmodel's properties required for dashboard view
            vm.Transactions = GetTransactionsWithDetails();            
            vm.AccountsWithBalances = GetAccountWithBalances();
            vm.ByCategorySpending = GetCategorySpending();
            vm.ByVendorSpending = GetVendorSpending();
            return View(vm);
        }

        private IList<VendorSpending> GetVendorSpending()
        {
            IList<VendorSpending> vendorSpending = new List<VendorSpending>();
            foreach (var vendor in _vendorRepository.GetList())
            {
                if (vendor.IsDisplayed)
                {
                    VendorSpending vendorSpendingHolder = new VendorSpending();
                    vendorSpendingHolder.Name = vendor.Name;
                    vendorSpendingHolder.Amount = _vendorRepository.GetAmount(vendor.VendorID);
                    vendorSpending.Add(vendorSpendingHolder);
                }
            }
            return vendorSpending;
        }

        private IList<CategorySpending> GetCategorySpending()
        {
            IList<CategorySpending> categorySpending = new List<CategorySpending>();
            foreach (var category in _categoryRepository.GetList())
            {
                if (category.IsDisplayed)
                {
                    CategorySpending categorySpendingHolder = new CategorySpending();
                    categorySpendingHolder.Name = category.Name;
                    categorySpendingHolder.Amount = _categoryRepository.GetCategorySpending(category.CategoryID);
                    categorySpending.Add(categorySpendingHolder); 
                }
            }
            return categorySpending;
        }

        //TODO: This exact method is used in the account controller as well. Put in a central location (DRY).
        private IList<AccountWithBalance> GetAccountWithBalances()
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

        //TODO: This exact method is used in the transaction controller as well. Put in a central location (DRY).
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
    }
}