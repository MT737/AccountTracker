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
    class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        //Insert seed data for the database here.

        protected override void Seed(Context context)
        {
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
