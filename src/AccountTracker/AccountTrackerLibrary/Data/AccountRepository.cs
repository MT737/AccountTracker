using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Data
{
    public class AccountRepository : BaseRepository<Account>
    {
        public AccountRepository(Context context) : base(context)
        {
        }

        //TODO Implement Account Get
        public override Account Get(int id, bool includeRelatedEntities = true)
        {
            var account = Context.Accounts.AsQueryable();

            if (includeRelatedEntities)
            {
                throw new NotImplementedException();
            }

            return account
                .Where(a => a.AccountID == id)
                .SingleOrDefault();
        }

        //TODO Implement Account GetList
        public override IList<Account> GetList()
        {
            return Context.Accounts
                .OrderBy(a => a.Name)
                .ToList();
        }

        public override int GetCount()
        {
            return Context.Accounts.Count();
        }
    }
}
