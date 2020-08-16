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


        //BalanceByAccount class. Leaving it as a member of the DashboardViewModel for now as it's only needed for the dashboard.
        public class AccountWithBalance
        {
            public int AccountId { get; set; }
            public string Name { get; set; }
            public bool IsAsset { get; set; }
            public bool IsActive { get; set; }
            public decimal Balance { get; set; }
        
        }



        //TODO: Spending by category

        //TODO: Spening by Vendor


    }
}