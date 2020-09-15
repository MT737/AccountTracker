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

        public Vendor Get(int id, bool includeRelatedEntities = true)
        {
            var vendor = Context.Vendors.AsQueryable();

            if (includeRelatedEntities)
            {
                throw new NotImplementedException(); 
            }

            return vendor
                .Where(v => v.VendorID == id)
                .SingleOrDefault();
        }

        public IList<Vendor> GetList()
        {
            return Context.Vendors
                .OrderBy(v => v.Name)
                .ToList();
        }
        
        public int GetCount()
        {
            return Context.Vendors.Count();
        }

        public decimal GetAmount(int vendorID, string userID)
        {
            return Context.Transactions
                .Where(t => t.VendorID == vendorID && t.UserID == userID)
                .ToList().Sum(t => t.Amount);
        }

        public bool NameExists(Vendor vendor)
        {
            return Context.Vendors
                .Where(v => v.Name.ToLower() == vendor.Name.ToLower() && v.VendorID != vendor.VendorID)
                .Any();
        }

        public int GetID(string name)
        {
            return Context.Vendors
                .Where(v => v.Name == name)
                .SingleOrDefault().VendorID;
        }

        public void Absorption(int absorbedID, int absorbingID)
        {
            //TODO: this works for a small database, but for large scale, this method should be updated to perform a bulk update.
            IQueryable<Transaction> vendorsToUpdate = Context.Transactions
                .Where(v => v.VendorID == absorbedID);

            foreach (Transaction transaction in vendorsToUpdate)
            {
                transaction.VendorID = absorbingID;
            }
            Context.SaveChanges();
        }
    }
}
