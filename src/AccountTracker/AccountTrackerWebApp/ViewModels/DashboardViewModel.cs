using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace AccountTrackerWebApp.ViewModels
{
    /// <summary>
    /// The view model for the "Dashboard" view.
    /// </summary>
    public class DashboardViewModel
    {
        //Properties
        public IList<Transaction> Transactions { get; set; }
        public IList<AccountWithBalance> AccountsWithBalances { get; set; }
        public IList<CategorySpending> ByCategorySpending { get; set; }
        public IList<VendorSpending> ByVendorSpending { get; set; }

        //TODO: Review options for improving this. It feels wasteful/redundant to remake these classes just to add a field.
        //BalanceByAccount class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class AccountWithBalance
        {
            //TODO: Remove commented out properties if they are not going to be used. 
            //public int AccountId { get; set; }
            public string Name { get; set; }
            //public bool IsAsset { get; set; }
            // public bool IsActive { get; set; }
            public decimal Balance { get; set; }        
        }

        //CategorySpending class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class CategorySpending
        {
            //TODO: Remove commented out properties if they are not going to be used.
            //public int CategoryId { get; set; }
            public string Name { get; set; }
            //public bool IsDisplayed { get; set; }
            //public bool IsDefault { get; set; }
            public decimal Amount { get; set; }
        }

        //VendorSpending class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class VendorSpending
        {
            //TODO: Remove commented out properties if they are not going to be used.
            //public int VendorId { get; set; }
            public string Name { get; set; }
            //public bool IsDefault { get; set; }
            //public bool IsDisplayed { get; set; }
            public decimal Amount { get; set; }
        }
    }
}