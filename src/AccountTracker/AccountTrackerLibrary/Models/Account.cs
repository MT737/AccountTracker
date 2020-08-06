using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Models
{
    public class Account
    {
        //Properties
        public int AccountID { get; set; }

        public string Name { get; set; }

        public bool IsAsset { get; set; }

        public bool IsActive { get; set; }


        //Navigation Property
        public ICollection<Transaction> Transactions { get; set; }

        //Instantiation of navigation collection.
        public Account()
        {
            Transactions = new List<Transaction>();
        }
    }
}
