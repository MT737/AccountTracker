using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Data
{
    public class TransTypeRepository : BaseRepository<TransType>
    {
        public TransTypeRepository(Context context) : base(context)
        {
        }

        //TODO Implement TransType Get
        public override TransType Get(int id, bool includeRelatedEntities = true)
        {
            throw new NotImplementedException();
        }

        //TODO Implement TransType get list???
        public override IList<TransType> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
