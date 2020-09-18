using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountTrackerLibrary.Models
{
    public class Vendor
    {
        //Properties
        public int VendorID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsDefault { get; set; }

        [Required]
        public bool IsDisplayed { get; set; }

       
        //Navigation Property
        public ICollection<Transaction> Transactions { get; set; }

        //Instantiation of navigation collection.
        public Vendor()
        {
            Transactions = new List<Transaction>();
        }
    }
}
