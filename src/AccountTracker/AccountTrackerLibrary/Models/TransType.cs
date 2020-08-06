using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Models
{
    public class TransType
    {
        //Properties
        public int TransTypeID { get; set; }
        public string Name { get; set; }

        //Navigation Property
        public ICollection<Transaction> Transactions { get; set; }

        //Instantiation of navigation collection.
        public TransType()
        {
            Transactions = new List<Transaction>();
        }

    }
}
