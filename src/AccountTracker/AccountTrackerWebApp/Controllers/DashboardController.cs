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
    }
}