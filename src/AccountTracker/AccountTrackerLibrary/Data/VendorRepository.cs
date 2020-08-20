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

        //TODO Implement Vendor get
        public override Vendor Get(int id, bool includeRelatedEntities = true)
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

        //TODO Implement Vendor list
        public override IList<Vendor> GetList()
        {
            return Context.Vendors
                .OrderBy(v => v.Name)
                .ToList();
        }
        
        public override int GetCount()
        {
            return Context.Vendors.Count();
        }

        public decimal GetAmount(int vendorID)
        {
            return Context.Transactions
                .Where(t => t.VendorID == vendorID)
                .ToList().Sum(t => t.Amount);
        }

        public int GetID(string name)
        {
            return Context.Vendors
                .Where(v => v.Name == name)
                .SingleOrDefault().VendorID;
        }
    }
}
