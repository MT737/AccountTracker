﻿using AccountTrackerLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace AccountTrackerLibrary.Data
{
    public class TransactionTypeRepository : BaseRepository<TransactionType>
    {
        public TransactionTypeRepository(Context context) : base(context)
        {
        }

        /// <summary>
        /// Returns a list of transactiontype entities.
        /// </summary>
        /// <returns>IList TransactionType entities.</returns>
        public IList<TransactionType> GetList()
        {
            return Context.TransactionTypes
                .OrderBy(t => t.TransactionTypeID)
                .ToList();
        }

        /// <summary>
        /// Retrieves the transactionID associated to the passed transaction type name.
        /// </summary>
        /// <param name="name">String: Name of the transaction type.</param>
        /// <returns>Int: Transaciton Type ID associated to the passed name.</returns>
        public int GetID(string name)
        {
            return Context.TransactionTypes
                .Where(tt => tt.Name == name)
                .SingleOrDefault().TransactionTypeID;
        }
    }
}
