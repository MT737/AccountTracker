using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Models
{
    public class TransType
    {
        //Properties
        public int TransTypeID { get; set; }
        
        [Required, StringLength(100)]
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
