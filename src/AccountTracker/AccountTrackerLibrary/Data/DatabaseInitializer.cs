using AccountTrackerLibrary.Models;
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
    internal class DatabaseInitializer : DropCreateDatabaseAlways<Context>
    {
        //Insert seed data for the database here.

        protected override void Seed(Context context)
        {

            //Filling in default categories.

            var shopping = new Category()
            {
                Name = "Shopping",
                IsDefault = true,
                IsActive = true
            };
            context.Categories.Add(shopping);

            var eatingOut = new Category()
            {
                Name = "Eating Out",
                IsDefault = true,
               IsActive = true
            };
            context.Categories.Add(eatingOut);

            var gas = new Category()
            {
                Name = "Gas",
                IsDefault = true,
                IsActive = true
            };
            context.Categories.Add(gas);

            //Filling default vendors
            var vendor = new Vendor()
            {
                Name = "Lowe's",
                IsDefault = true
            };
            context.Vendors.Add(vendor);


            //Filling test account
            var account = new Account()
            {
                Name = "Bank of America: Checking",
                IsAsset = true,
                IsActive = true
            };
            context.Accounts.Add(account);

            //Filling test transactions
            var transaction1 = new Transaction()
            {
                TransactionDate = DateTime.Now,
                Amount = 112.52M,
                TransactionTypeID = 1,
                AccountID = 1,
                CategoryID = 1
            };

            context.Transactions.Add(transaction1);
            context.SaveChanges();
        }
    }
}
