using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountTrackerLibrary.Models
{
    public class Account
    {
        //Properties
        public int AccountID { get; set; }
        
        public User User { get; set; }
        
        [Required]
        public string UserID { get; set; }

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

        /// <summary>
        /// Constructor for creating accounts.
        /// </summary>
        /// <param name="user">The user for the account.</param>
        /// <param name="accountID">The account ID.</param>
        /// <param name="name">The name of the account.</param>
        /// <param name="isAsset">Bool representing the classification (asset or liability) of the account.</param>
        /// <param name="isActive">Bool representing the active status of the account.</param>
        public Account(User user, string name, bool isAsset, bool isActive)
        {
            UserID = user.Id;
            Name = name;
            IsAsset = isAsset;
            IsActive = isActive;
        }
    }
}
