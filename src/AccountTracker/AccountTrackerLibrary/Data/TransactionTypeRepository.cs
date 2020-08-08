using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Data
{
    public class TransactionTypeRepository : BaseRepository<TransactionType>
    {
        public TransactionTypeRepository(Context context) : base(context)
        {
        }

        //TODO Implement TransType Get
        public override TransactionType Get(int id, bool includeRelatedEntities = true)
        {
            throw new NotImplementedException();
        }

        //TODO Implement TransType get list???
        public override IList<TransactionType> GetList()
        {
            throw new NotImplementedException();
        }

        public override int GetCount()
        {
            throw new NotImplementedException();
        }
    }
}
