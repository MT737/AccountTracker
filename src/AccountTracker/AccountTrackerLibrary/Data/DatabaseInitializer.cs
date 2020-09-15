using AccountTrackerLibrary.Models;
using AccountTrackerLibrary.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            //Generate Users
            var userStore = new UserStore<User>(context);
            var userManager = new ApplicationUserManager(userStore);

            var userMark = new User
            {
                UserName = "mark.taylor737@gmail.com",
                Email = "mark.taylor737@gmail.com"
            };
            userManager.Create(userMark, "markpassword");

            var userBob = new User
            {
                UserName = "bob@gmail.com",
                Email = "bob@gmail.com"
            };
            userManager.Create(userBob, "bobpassword");

            //Insert users (necessary to generate the user.Ids required for the account and transaction inserts below).            
            context.SaveChanges();

            //Filling in default categories.
            IList<Category> categories = new List<Category>();
            categories.Add(new Category() { Name = "Shopping", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "Eating out", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "Gas", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "Groceries/Sundries", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "Entertainment", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "Returns/Deposits", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "ATM Withdrawal", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "Other", IsDisplayed = true, IsDefault = true });
            categories.Add(new Category() { Name = "New Account", IsDisplayed = false, IsDefault = true });
            categories.Add(new Category() { Name = "Account Correction", IsDisplayed = false, IsDefault = true });
            categories.Add(new Category() { Name = "Account Transfer", IsDisplayed = false, IsDefault = true });
            context.Categories.AddRange(categories);

            //Filling default vendors
            IList<Vendor> vendors = new List<Vendor>();
            vendors.Add(new Vendor() { Name = "Lowe's", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "Food Lion", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "Marshalls", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "Harris Teeter", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "Amazon", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "Duke Power", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "IMPLAN", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "Armstrong Transportation", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "ABC", IsDefault = true, IsDisplayed = true });
            vendors.Add(new Vendor() { Name = "N/A", IsDefault = true, IsDisplayed = false });
            context.Vendors.AddRange(vendors);

            //Filling default TransTypes
            IList<TransactionType> transTypes = new List<TransactionType>();
            transTypes.Add(new TransactionType() { Name = "Payment To" });
            transTypes.Add(new TransactionType() { Name = "Payment From" });
            context.TransactionTypes.AddRange(transTypes);


            //Filling test accounts
            IList<Account> accounts = new List<Account>();
            accounts.Add(new Account(userBob, "SunTrust", true, true));
            accounts.Add(new Account(userBob, "Visa: Credit Card", true, false));
            accounts.Add(new Account(userBob, "Target: Credit Card", true, false));
            accounts.Add(new Account(userBob, "MrBank: Interest Free Loan", true, false));
            accounts.Add(new Account(userMark, "Bank of America: Checking", true, true));
            accounts.Add(new Account(userMark, "Fifth Third: Credit Card", true, false));
            accounts.Add(new Account(userMark, "Amazon: Credit Card", true, false));
            accounts.Add(new Account(userMark, "Haverty's: Interest Free Loan", true, false));
            context.Accounts.AddRange(accounts);
            
            //Must save the seed data for the primary keys to the DB before inserting transaction data. Otherwise EF will try to insert transaction data before primary data is inserted.            
            context.SaveChanges();

            ////Filling test transactions
            IList<Transaction> transactions = new List<Transaction>();
            transactions.Add(new Transaction(userBob, DateTime.Today, 1, 1, 9, 7, 5282.52M, "Initial Account Balance"));
            transactions.Add(new Transaction(userBob, DateTime.Today, 2, 1, 1, 1, 521.36M, "Lowe's shopping"));
            transactions.Add(new Transaction(userBob, DateTime.Today, 2, 2, 3, 9, 35.00M, "Gas on Amazon card"));
            transactions.Add(new Transaction(userBob, DateTime.Today, 1, 2, 11, 10, 35.00M, "Paying off Amazon with BofA"));
            transactions.Add(new Transaction(userBob, DateTime.Today, 2, 3, 10, 10, 35.00M, "Initial Account Balance"));
            transactions.Add(new Transaction(userBob, DateTime.Today, 2, 4, 10, 10, 35.00M, "Initial Account Balance" ));
            transactions.Add(new Transaction(userMark, DateTime.Today, 1, 1, 9, 7, 5282.52M, "Initial Account Balance"));
            transactions.Add(new Transaction(userMark, DateTime.Today, 2, 1, 1, 1, 521.36M, "Lowe's shopping"));
            transactions.Add(new Transaction(userMark, DateTime.Today, 2, 2, 3, 9, 35.00M, "Gas on Amazon card"));
            transactions.Add(new Transaction(userMark, DateTime.Today, 1, 2, 11, 10, 35.00M, "Paying off Amazon with BofA"));
            transactions.Add(new Transaction(userMark, DateTime.Today, 2, 3, 10, 10, 35.00M, "Initial Account Balance"));
            transactions.Add(new Transaction(userMark, DateTime.Today, 2, 4, 10, 10, 35.00M, "Initial Account Balance"));
            context.Transactions.AddRange(transactions);

            base.Seed(context);
        }
    }
}
