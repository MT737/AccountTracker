using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerConsoleApp.Data
{
    /// <summary>
    /// Repository for testing various database CRUD operations.
    /// </summary>
    public static class Repository
    {
        /// <summary>
        /// Return a DB context.
        /// </summary>
        /// <returns>An instance of the DB Context class.</returns>
        private static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;
        }

        /// <summary>
        /// Return a list of all transactions in the database.
        /// </summary>
        /// <returns>IList of transactions.</returns>
        public static IList<Transaction> GetTransactionsList()
        {
            using (Context context = GetContext())
            {
                return context.Transactions
                    .OrderBy(t => t.TransactionDate)
                    .ToList();
            }
        }
    }
}
