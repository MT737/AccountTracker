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
        public TransactionType Get(int id, bool includeRelatedEntities = true)
        {
            var transactionType = Context.TransactionTypes.AsQueryable();

            if (includeRelatedEntities)
            {
                throw new NotImplementedException(); 
            }

            return transactionType
                .Where(tt => tt.TransactionTypeID == id)
                .SingleOrDefault();
        }

        public IList<TransactionType> GetList()
        {
            return Context.TransactionTypes
                .OrderBy(t => t.TransactionTypeID)
                .ToList();
        }

        public int GetCount()
        {
            return Context.TransactionTypes.Count();
        }

        public int GetID(string name)
        {
            return Context.TransactionTypes
                .Where(tt => tt.Name == name)
                .SingleOrDefault().TransactionTypeID;
        }
    }
}
