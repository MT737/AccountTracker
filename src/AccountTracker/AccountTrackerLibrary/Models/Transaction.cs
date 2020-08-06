using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary
{
    public class Transaction
    {
        //Properties
        public int TransactionID { get; set; }

        public DateTime TransactionDate { get; set; }

        public int TransactionTypeID { get; set; }

        public int AccountID { get; set; }

        public int CategoryID { get; set; }

        public decimal Amount { get; set; }

        public int VendorID { get; set; }

        public string Description { get; set; }

        //Navigation Properties
        public TransType TransactionType { get; set; }
        public Account Account { get; set; }
        public Category Category { get; set; }
        public Vendor Vendor { get; set; }
    }
}
