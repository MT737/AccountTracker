using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AccountTrackerLibrary.Models;

namespace AccountTrackerLibrary.Data
{
    public class TransactionRepository : BaseRepository<Transaction>
    {
        public TransactionRepository(Context context) : base(context)
        {
        }
                
        public Transaction Get(int id, string userID, bool includeRelatedEntities = true)
        {
            var transaction = Context.Transactions.AsQueryable();

            if (includeRelatedEntities)
            {
                transaction = transaction
                    .Include(tt => tt.TransactionType)
                    .Include(a => a.Account)
                    .Include(c => c.Category)
                    .Include(v => v.Vendor);
            }

            return transaction                
                .Where(t => t.TransactionID == id && t.UserID == userID)
                .SingleOrDefault();
        }

        public IList<Transaction> GetList(string userID)
        {
            return Context.Transactions
                .Include(tt => tt.TransactionType)
                .Include(a => a.Account)
                .Where(t => t.UserID == userID)
                .OrderByDescending(t => t.TransactionID)
                .ToList();
        }

        public int GetCount(string userID)
        {
            return Context.Transactions.Where(t => t.UserID == userID).Count();
        }

        public bool UserOwnsTransaction(int id, string userID)
        {
            return Context.Transactions.Where(t => t.TransactionID == id && t.UserID == userID).Any();
        }
    }
}
