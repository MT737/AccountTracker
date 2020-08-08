using System.Collections.Generic;

namespace AccountTrackerLibrary.Data
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected Context Context { get; private set; }

        public BaseRepository(Context context)
        {
            Context = context;
        }

        public abstract TEntity Get(int id, bool includeRelatedEntities = true);

        public abstract IList<TEntity> GetList();

        //TODO: Create CRUD functions

        public abstract int GetCount();

    }
}
