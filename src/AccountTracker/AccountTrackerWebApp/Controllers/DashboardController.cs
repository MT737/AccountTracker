using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
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
    public class DashboardController : BaseController
    {

        //Private field
        private TransactionRepository _transactionRepository = null;
        private AccountRepository _accountRepository = null;
        private CategoryRepository _categoryRepository = null;
        private VendorRepository _vendorRepository = null;
        
        //Base constructor
        public DashboardController()
        {
            _transactionRepository = new TransactionRepository(Context);
            _accountRepository = new AccountRepository(Context);
            _categoryRepository = new CategoryRepository(Context);
            _vendorRepository = new VendorRepository(Context);
        }

        public ActionResult Index()
        {
            //Instantiate viewmodel
            var dashboardItems = new ViewModel();

            //Complete viewmodel's properties required for dashboard view
            dashboardItems.Transactions = GetTransactionsWithDetails();            
            dashboardItems.AccountsWithBalances = GetAccountWithBalances();
            dashboardItems.ByCategorySpending = GetCategorySpending();
            dashboardItems.ByVendorSpending = GetVendorSpending();
            return View(dashboardItems);
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

        public IList<AccountWithBalance> GetAccountWithBalances()
        {
            //Get list of accounts
            IList<AccountWithBalance> accountsWithBalances = new List<AccountWithBalance>();
            foreach (var account in _accountRepository.GetList())
            {
                //Set detailed values and get amount
                AccountWithBalance accountWithBalanceHolder = new AccountWithBalance();
                accountWithBalanceHolder.Name = account.Name;
                accountWithBalanceHolder.Balance = _accountRepository.GetBalance(account.AccountID, account.IsAsset);
                accountsWithBalances.Add(accountWithBalanceHolder);
            }

            return accountsWithBalances;
        }
    }
}