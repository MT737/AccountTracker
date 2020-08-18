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
    public class ViewModel
    {
        //Properties
        public IList<Transaction> Transactions { get; set; }
        public IList<AccountWithBalance> AccountsWithBalances { get; set; }
        public IList<CategorySpending> ByCategorySpending { get; set; }
        public IList<VendorSpending> ByVendorSpending { get; set; }

        //BalanceByAccount class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class AccountWithBalance
        {
            public string Name { get; set; }
            public decimal Balance { get; set; }        
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
    }
}