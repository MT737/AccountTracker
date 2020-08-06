using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTrackerLibrary.Models
{
    public class Account
    {
        //Properties
        public int AccountID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsAsset { get; set; }

        [Required]
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
