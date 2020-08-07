using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public int TransactionTypeID { get; set; }

        [Required]
        public int AccountID { get; set; }

        [Required]
        public int CategoryID { get; set; }
        
        public int VendorID { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        //Navigation Properties
        public TransactionType TransactionType { get; set; }
        public Account Account { get; set; }
        public Category Category { get; set; }
        public Vendor Vendor { get; set; }
    }
}
