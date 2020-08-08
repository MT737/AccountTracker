using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Data
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(Context context) : base(context)
        {
        }

        //TODO Implement Category Get
        public override Category Get(int id, bool includeRelatedEntities = true)
        {
            throw new NotImplementedException();
        }

        //TODO Implement Category Get list
        public override IList<Category> GetList()
        {
            throw new NotImplementedException();
        }

        public override int GetCount()
        {
            throw new NotImplementedException();
        }
    }
}
