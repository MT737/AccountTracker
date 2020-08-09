﻿using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Data
{
    /// <summary>
    /// Custom database initializer class used to populate the database with seed data.
    /// </summary>
    internal class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        //Insert seed data for the database here.

        protected override void Seed(Context context)
        {

            //Filling in default categories.
            IList<Category> categories = new List<Category>();
            categories.Add(new Category() { Name = "Shopping", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Eating out", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Gas", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Groceries/Sundries", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Entertainment", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Returns/Deposits", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "ATM Withdrawal", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Other", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "New Account", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Account Correction", IsActive = true, IsDefault = true });
            categories.Add(new Category() { Name = "Account Transfer", IsActive = true, IsDefault = true });
            context.Categories.AddRange(categories);

            //Filling default vendors
            IList<Vendor> vendors = new List<Vendor>();
            vendors.Add(new Vendor() { Name = "Lowe's", IsDefault = true });
            vendors.Add(new Vendor() { Name = "Food Lion", IsDefault = true });
            vendors.Add(new Vendor() { Name = "Marshalls", IsDefault = true });
            vendors.Add(new Vendor() { Name = "Harris Teeter", IsDefault = true });
            vendors.Add(new Vendor() { Name = "Amazon", IsDefault = true });
            vendors.Add(new Vendor() { Name = "Duke Power", IsDefault = true });
            vendors.Add(new Vendor() { Name = "IMPLAN", IsDefault = true });
            vendors.Add(new Vendor() { Name = "Armstrong Transportation", IsDefault = true });
            vendors.Add(new Vendor() { Name = "ABC", IsDefault = true });
            vendors.Add(new Vendor() { Name = "N/A", IsDefault = true });
            context.Vendors.AddRange(vendors);

            //Filling default TransTypes
            IList<TransactionType> transTypes = new List<TransactionType>();
            transTypes.Add(new TransactionType() { Name = "Payment To" });
            transTypes.Add(new TransactionType() { Name = "Payment From" });
            context.TransactionTypes.AddRange(transTypes);


            //Filling test accounts
            IList<Account> accounts = new List<Account>();
            accounts.Add(new Account() { Name = "Bank of America: Checking", IsActive = true, IsAsset = true });
            accounts.Add(new Account() { Name = "Fifth Third: Credit Card", IsActive = true, IsAsset = false });
            accounts.Add(new Account() { Name = "Amazon: Credit Card", IsActive = true, IsAsset = false });
            accounts.Add(new Account() { Name = "Havery's: Interest Free Loan", IsActive = true, IsAsset = false });            
            context.Accounts.AddRange(accounts);
            
            //Must save the seed data for the primary keys to the DB before inserting transaction data. Otherwise EF will try to insert transaction data before primary data is inserted.            
            context.SaveChanges();

            ////Filling test transactions
            IList<Transaction> transactions = new List<Transaction>();
            transactions.Add(new Transaction() { TransactionDate = DateTime.Today, Amount = 5282.52M, TransactionTypeID = 1, AccountID = 1, CategoryID = 9, VendorID = 7, Description = "Initial Account Balance" });
            transactions.Add(new Transaction() { TransactionDate = DateTime.Today, Amount = 521.36M, TransactionTypeID = 2, AccountID = 1, CategoryID = 1, VendorID = 1, Description = "Lowe's shopping" });
            transactions.Add(new Transaction() { TransactionDate = DateTime.Today, Amount = 35.00M, TransactionTypeID = 2, AccountID = 2, CategoryID = 3, VendorID = 9 , Description = "Gas on Amazon card"});
            transactions.Add(new Transaction() { TransactionDate = DateTime.Today, Amount = 35.00M, TransactionTypeID = 1, AccountID = 2, CategoryID = 11, VendorID = 10, Description = "Paying off Amazon with BofA"});
            transactions.Add(new Transaction() { TransactionDate = DateTime.Today, Amount = 35.00M, TransactionTypeID = 2, AccountID = 1, CategoryID = 11, VendorID = 10, Description = "Paying off Amazon with BofA"});
            context.Transactions.AddRange(transactions);

            base.Seed(context);
        }
    }
}
