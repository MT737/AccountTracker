using AccountTrackerLibrary.Models;
using System.Collections.Generic;
using System.Data.Entity;

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

            base.Seed(context);
        }
    }
}
