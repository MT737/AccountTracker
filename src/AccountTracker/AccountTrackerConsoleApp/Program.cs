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
                Console.WriteLine($"Transaction Amount: {transaction.Amount} Transaction Account: {transaction.AccountID}");
            }

            Console.WriteLine("meh");
            Console.Read();
        }
    }
}
