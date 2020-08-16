using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IList<Account> Accounts { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<Vendor> Vendors { get; set; }
    }
}