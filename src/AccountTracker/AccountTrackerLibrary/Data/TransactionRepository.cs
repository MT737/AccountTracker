using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AccountTrackerLibrary.Data
{
    public class TransactionRepository : BaseRepository<Transaction>
    {
        public TransactionRepository(Context context) : base(context)
        {
        }

        //TODO Implement Transaction get
        public override Transaction Get(int id, bool includeRelatedEntities = true)
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
                .Where(t => t.TransactionID == id)
                .SingleOrDefault();
        }

        public override IList<Transaction> GetList()
        {
            return Context.Transactions
                .Include(tt => tt.TransactionType)
                .Include(a => a.Account)
                .OrderByDescending(t => t.TransactionID)
                .ToList();
        }

        public override int GetCount()
        {
            return Context.Transactions.Count();
        }
    }
}
