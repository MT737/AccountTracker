using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTrackerConsoleApp.Data;


namespace AccountTrackerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactions = Repository.GetTransactionsList();

            foreach (var transaction in transactions)
            {
                Console.WriteLine($"Transaction Amount: {transaction.Amount} Transaction Account: {transaction.AccountID}");
            }

            Console.WriteLine("meh");
            Console.Read();
        }
    }
}
