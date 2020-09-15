using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Models
{
    public class Transaction
    {
        //Properties   
        public int TransactionID { get; set; }
        
        public User User { get; set; }
        
        [Required]
        public string UserID { get; set; }

        [Required, Display(Name = "Date")]        
        public DateTime TransactionDate { get; set; }

        [Required, Display(Name = "Transaction Type")]
        public int TransactionTypeID { get; set; }

        [Required, Display(Name = "Account")]
        public int AccountID { get; set; }

        [Required, Display(Name = "Category")]
        public int CategoryID { get; set; }
        
        [Required, Display(Name = "Vendor")]
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

        /// <summary>
        /// Default blank constructor to allow for empty placeholder constructors.
        /// </summary>
        public Transaction()
        { }

        /// <summary>
        /// Constructor for creating transactions.
        /// </summary>
        /// <param name="user">User for the transaction.</param>
        /// <param name="transactionDate">Date of the transaction.</param>
        /// <param name="transactionTypeID">Type of transaction "Payment To" or "Payment From".</param>
        /// <param name="accountID">Account associated to the transaction.</param>
        /// <param name="categoryID">Transaction category</param>
        /// <param name="vendorID">Transaction vendor</param>
        /// <param name="amount">Transaction amount</param>
        /// <param name="description">Transaction description</param>
        public Transaction(User user, DateTime transactionDate, int transactionTypeID, int accountID, int categoryID, int vendorID, decimal amount, string description)
        {
            UserID = user.Id;
            TransactionDate = transactionDate;
            TransactionTypeID = transactionTypeID;
            AccountID = accountID;
            CategoryID = categoryID;
            VendorID = vendorID;
            Amount = amount;
            Description = description;
        }
    }
}
