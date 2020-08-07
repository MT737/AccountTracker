using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTrackerLibrary.Data;

namespace AccountTrackerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new Context();
            var transactionRepo = new TransactionRepository(context);
            var translist = transactionRepo.GetList();

            foreach (var transaction in translist)
            {
                Console.WriteLine($"{transaction.TransactionType.Name} {transaction.Account.Name}. Amount: {transaction.Amount}. Category: {transaction.Category.Name} " +
                    $"Vendor: {transaction.Vendor.Name}. Transaction date: {transaction.TransactionDate}  Description: {transaction.Description}");
            }

            Console.Read();
        }
    }
}
