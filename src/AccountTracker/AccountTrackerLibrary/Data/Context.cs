using AccountTrackerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Data
{
    /// <summary>
    /// Entity Framework context class.
    /// </summary>
    public class Context : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransTypes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }


        //DBInitializer for this context class is set in the config file. As no further constructor specifications required, no consructor is present.        


        /// <summary>
        /// Adjusting model build conventions to fit application needs.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Removing the pluralizing table name convention 
            // so our table names will use our entity class singular names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Using the fluent API to configure the precision and scale
            // for Transactions.Amount.
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasPrecision(16, 2);
        }
    }
}
