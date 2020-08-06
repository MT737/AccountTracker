using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Models
{
    public class Vendor
    {
        //Properties
        public int VendorID { get; set; }

        public string Name { get; set; }

        public bool IsDefault { get; set; }

        //Navigation Property
        public ICollection<Transaction> Transactions { get; set; }

        //Instantiation of navigation collection.
        public Vendor()
        {
            Transactions = new List<Transaction>();
        }
    }
}
