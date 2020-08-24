using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace AccountTrackerWebApp.ViewModels
{
    /// <summary>
    /// A general view model.
    /// </summary>
    public class ViewModel
    {
        //TODO: This view model is doing too much.

        //Properties
        public Transaction TransactionOfInterest { get; set; }  
        public Account AccountOfInterest { get; set; }
        public Category CategoryOfInterest { get; set; }
        public Category AbsorptionCategory { get; set; }
        public Vendor VendorOfInterest { get; set; }
        public Vendor AbsorptionVendor { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public IList<AccountWithBalance> AccountsWithBalances { get; set; }
        public IList<CategorySpending> ByCategorySpending { get; set; }
        public IList<VendorSpending> ByVendorSpending { get; set; }
        public SelectList TransactionTypesSelectList { get; set; }
        public SelectList AccountSelectList { get; set; }
        public SelectList CategorySelectList { get; set; }
        public SelectList VendorSelectList { get; set; }


        //BalanceByAccount class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class AccountWithBalance
        {
            public int AccountID { get; set; }
            public string Name { get; set; }
            public decimal Balance { get; set; }   
            public bool IsAsset { get; set; }
            public bool IsActive { get; set; }
        }

        //CategorySpending class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class CategorySpending
        {
            public string Name { get; set; }
            public decimal Amount { get; set; }
        }

        //VendorSpending class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class VendorSpending
        {
            public string Name { get; set; }
            public decimal Amount { get; set; }
        }

        //TODO: Create individual list inits so as to not mass pull information when not necessary.
        //Initialize the select lists.
        public void Init(TransactionTypeRepository transactionTypeRepository, AccountRepository accountRepository, CategoryRepository categoryRepository, VendorRepository vendorRepository)
        {
            TransactionTypesSelectList = new SelectList(transactionTypeRepository.GetList(), "TransactionTypeID", "Name");
            AccountSelectList = new SelectList(accountRepository.GetList(), "AccountID", "Name");
            VendorSelectList = InitVendorSelectList(vendorRepository);
            CategorySelectList = InitCategorySelectList(categoryRepository);
        }

        public SelectList InitCategorySelectList(CategoryRepository categoryRepository)
        {
            return new SelectList(categoryRepository.GetList(), "CategoryID", "Name");
        }

        public SelectList InitVendorSelectList(VendorRepository vendorRepository)
        {
            return new SelectList(vendorRepository.GetList(), "VendorId", "Name");
        }
    }
}