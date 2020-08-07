using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .OrderBy(t => t.TransactionDate)
                .ToList();
        }
    }
}
