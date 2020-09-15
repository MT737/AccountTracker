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

        public  Category Get(int id, bool includeRelatedEntities = true)
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

        public IList<Category> GetList()
        {
            return Context.Categories
                .OrderBy(c => c.Name)
                .ToList();
        }

        public int GetCount()
        {
            return Context.Categories.Count();
        }

        public bool NameExists(Category category)
        {
            return Context.Categories
                .Where(c => c.Name.ToLower() == category.Name.ToLower() && c.CategoryID != category.CategoryID)
                .Any();
        }

        public decimal GetCategorySpending(int categoryID, string userID)
        {
            return Context.Transactions
                .Where(t => t.CategoryID == categoryID && t.UserID == userID)
                .ToList().Sum(t => t.Amount);
        }

        public int GetID(string name)
        {
            return Context.Categories
                .Where(c => c.Name == name)
                .SingleOrDefault().CategoryID;
        }

        public void Absorption(int absorbedID, int absorbingID)
        {
            //TODO: this works for a small database, but for large scale, this method should be updated to perform a bulk update.
            //TODO: this is dangerous considering any user could change categories. Should make all of these tables user owned.
            IQueryable<Transaction> catsToUpdate = Context.Transactions
                .Where(c => c.CategoryID == absorbedID);

            foreach (Transaction transaction in catsToUpdate)
            {
                transaction.CategoryID = absorbingID;                
            }
            Context.SaveChanges();
        }
    }
}
