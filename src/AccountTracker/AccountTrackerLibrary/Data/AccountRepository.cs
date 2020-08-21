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

        /// <summary>
        /// Retrieves a record of a single account. 
        /// </summary>
        /// <param name="id">Int: the AccountID of the account to be retrieved.</param>
        /// <param name="includeRelatedEntities">Bool: determination to pull associated entities. Not currently relevant to Accounts and defaults to false.</param>
        /// <returns></returns>
        public override Account Get(int id, bool includeRelatedEntities = false)
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

        /// <summary>
        /// Retrieves a list of accounts that exist in the database.
        /// </summary>
        /// <returns>Returns and IList of Account entities.</returns>
        public override IList<Account> GetList()
        {
            return Context.Accounts
                .OrderBy(a => a.Name)
                .ToList();
        }

        public int GetID(string name)
        {
            return Context.Accounts
                .Where(a => a.Name == name)
                .SingleOrDefault().AccountID;
        }

        /// <summary>
        /// Gets a count of accounts in the database.
        /// </summary>
        /// <returns>Returns an integer representing the count of accounts in the database.</returns>
        public override int GetCount()
        {
            return Context.Accounts.Count();
        }

        /// <summary>
        /// Calculates an account balance based on transactions in the database.
        /// </summary>
        /// <param name="accountId">Int: AccountID of the account balance to be calculated.</param>
        /// <param name="isAsset">Bool: IsAsset classification of the account balance to be calculated.</param>
        /// <returns></returns>
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

            ////Asset balance = payments to less payments from.
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

        public bool NameExists(Account account)
        {
           return Context.Accounts
                .Where(a => a.Name.ToLower() == account.Name.ToLower() && a.AccountID != account.AccountID)
                .Any();
        }
    }
}
