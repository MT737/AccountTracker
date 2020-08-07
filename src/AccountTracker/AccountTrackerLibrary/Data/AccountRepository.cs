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
            throw new NotImplementedException();
        }

        //TODO Implement Account GetList
        public override IList<Account> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
