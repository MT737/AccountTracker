using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        //TODO: Get balance
        public decimal GetBalance(int accountId, bool isAsset)
        {
            //TODO: I really want to simplify this to a single query (there's already enough communications with the DB happening as is).
            decimal balance;

           var paymentTo = Context.Transactions
                .Include(tt => tt.TransactionType)
                .Where(t => t.AccountID == accountId && t.TransactionType.Name == "Payment To")
                .ToList().Sum(t => t.Amount);

            var paymentFrom = Context.Transactions
                .Include(tt => tt.TransactionType)
                .Where(t => t.AccountID == accountId && t.TransactionType.Name == "Payment From")                
                .ToList().Sum(t => t.Amount);

            //Asset balance = payments to less payments from.
            if (isAsset)
            {
                return balance = paymentTo - paymentFrom;
            }
            //Liability balance = payments from - payments to.
            else
            {
                return paymentFrom - paymentTo;
            }
        }
    }
}
