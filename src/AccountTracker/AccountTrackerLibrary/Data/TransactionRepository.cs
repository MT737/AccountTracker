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
            throw new NotImplementedException();
        }

        public override IList<Transaction> GetList()
        {
            return Context.Transactions
                .Include(tt => tt.TransactionType)
                .Include(a => a.Account)
                .Include(v => v.Vendor)
                .Include(c => c.Category)
                .OrderBy(t => t.TransactionDate)
                .ToList();
        }
    }
}
