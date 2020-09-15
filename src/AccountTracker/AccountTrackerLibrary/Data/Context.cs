using AccountTrackerLibrary.Models;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class Context : IdentityDbContext<User>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public Context() : base("Context")
        {
        }
        

        /// <summary>
        /// Adjusting model build conventions to fit application needs.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Create tables using base model's OnModelCreating method
            base.OnModelCreating(modelBuilder);

            //Turn of One-To-Many Cascade delete. Will handle such tasks manually.
            //TODO: Test if this approach causes issues with the Identity user tables (that is, will I need to manually delete a user from all associated identity tables)
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            
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
