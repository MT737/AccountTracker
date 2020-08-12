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
            var category = Context.Categories.AsQueryable();

            if (includeRelatedEntities)
            {
                throw new NotImplementedException(); 
            }

            return category
                .Where(c => c.CategoryID == id)
                .SingleOrDefault();
        }

        //TODO Implement Category Get list
        public override IList<Category> GetList()
        {
            return Context.Categories
                .OrderBy(c => c.Name)
                .ToList();
        }

        public override int GetCount()
        {
            return Context.Categories.Count();
        }
    }
}
