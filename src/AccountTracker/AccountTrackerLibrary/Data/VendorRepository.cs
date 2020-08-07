using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Data
{
    public class VendorRepository : BaseRepository<Vendor>
    {
        public VendorRepository(Context context) : base(context)
        {

        }

        //TODO Implement Vendor get
        public override Vendor Get(int id, bool includeRelatedEntities = true)
        {
            throw new NotImplementedException();
        }

        //TODO Implement Vendor list
        public override IList<Vendor> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
