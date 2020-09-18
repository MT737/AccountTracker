using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;
using AccountTrackerWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;
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
            var vm = new ViewModel();
            var userID = User.Identity.GetUserId();

            //Complete viewmodel's properties required for dashboard view
            vm.Transactions = GetTransactionsWithDetails(userID);            
            vm.AccountsWithBalances = GetAccountWithBalances(userID);
            vm.ByCategorySpending = GetCategorySpending(userID);
            vm.ByVendorSpending = GetVendorSpending(userID);
            return View(vm);
        }

        /// <summary>
        /// Gets a list of VendorSpending objects.
        /// </summary>
        /// <param name="userID">String: UserID for which to retrieve VendorSpending objects.</param>
        /// <returns>IList VendorSpending objects.</returns>
        private IList<VendorSpending> GetVendorSpending(string userID)
        {
            IList<VendorSpending> vendorSpending = new List<VendorSpending>();
            foreach (var vendor in _vendorRepository.GetList())
            {
                if (vendor.IsDisplayed)
                {
                    VendorSpending vendorSpendingHolder = new VendorSpending();
                    vendorSpendingHolder.Name = vendor.Name;
                    vendorSpendingHolder.Amount = _vendorRepository.GetAmount(vendor.VendorID, userID);
                    vendorSpending.Add(vendorSpendingHolder);
                }
            }
            return vendorSpending;
        }

        /// <summary>
        /// Gets a list of CategorySpending objects.
        /// </summary>
        /// <param name="userID">String: UserID for which to rerieve CategorySpending objects.</param>
        /// <returns></returns>
        private IList<CategorySpending> GetCategorySpending(string userID)
        {
            IList<CategorySpending> categorySpending = new List<CategorySpending>();
            foreach (var category in _categoryRepository.GetList())
            {
                if (category.IsDisplayed)
                {
                    CategorySpending categorySpendingHolder = new CategorySpending();
                    categorySpendingHolder.Name = category.Name;
                    categorySpendingHolder.Amount = _categoryRepository.GetCategorySpending(category.CategoryID, userID);
                    categorySpending.Add(categorySpendingHolder); 
                }
            }
            return categorySpending;
        }

        //TODO: This exact method is used in the account controller as well. Put in a central location (DRY).
        /// <summary>
        /// Retrieves a list of accounts with associated balances.
        /// </summary>
        /// <param name="userID">String: userID for which to return account balances.</param>
        /// <returns>IList of AccountWithBalance entities.</returns>
        private IList<AccountWithBalance> GetAccountWithBalances(string userID)
        {
            //Get list of accounts
            IList<AccountWithBalance> accountsWithBalances = new List<AccountWithBalance>();
            foreach (var account in _accountRepository.GetList(userID))
            {
                //Set detailed values and get amount
                AccountWithBalance accountWithBalanceHolder = new AccountWithBalance();
                accountWithBalanceHolder.AccountID = account.AccountID;
                accountWithBalanceHolder.Name = account.Name;
                accountWithBalanceHolder.IsAsset = account.IsAsset;
                accountWithBalanceHolder.IsActive = account.IsActive;
                accountWithBalanceHolder.Balance = _accountRepository.GetBalance(account.AccountID, userID, account.IsAsset);
                accountsWithBalances.Add(accountWithBalanceHolder);
            }

            return accountsWithBalances;
        }

        //TODO: This exact method is used in the transaction controller as well. Put in a central location (DRY).
        /// <summary>
        /// Retrieves a list of transactions associated to the specified user.
        /// </summary>
        /// <param name="userID">String: UserID for which to pull a list of transactions.</param>
        /// <returns>IList of Transaction objects.</returns>
        public IList<Transaction> GetTransactionsWithDetails(string userID)
        {
            //Get get a list of transactions to gain access to transaction ids
            IList<Transaction> transactions = new List<Transaction>();
            foreach (var transaction in _transactionRepository.GetList(userID))
            {
                //Get the detailed data for each transaction and add it to the IList of transactions
                transactions.Add(_transactionRepository.Get(transaction.TransactionID, userID, true));
            }

            return transactions;
        }
    }
}