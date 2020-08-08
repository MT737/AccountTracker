using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTrackerLibrary.Data;

namespace AccountTrackerConsoleApp.Helpers
{
    public static class ConsoleHelper
    {        
        static Context GetContext()
        {
            var context = new Context();
            context.Database.Log = (message) => Debug.WriteLine(message);
            return context;               
        }

        public static void ListTransactions()
        {
            using (var context = GetContext())
            {
                var _transactionsRepository = new TransactionRepository(context);
                var transactions = _transactionsRepository.GetList();
                if (transactions.Count > 0)
                {
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"{transaction.TransactionType.Name} {transaction.Account.Name}. Amount: {transaction.Amount}. Category: {transaction.Category.Name} " +
                            $"Vendor: {transaction.Vendor.Name}. Transaction date: {transaction.TransactionDate}  Description: {transaction.Description}");
                    }
                }
                else
                {
                    Console.WriteLine($"No transactions found.");
                }
            }
        }

        public static void OutputBlankLine()
        {
            Console.WriteLine();
        }

        public static void Output(string message)
        {
            Console.WriteLine(message);
        }

        public static void OutputLine(string message, bool outputBlankLineBeforMessage = true)
        {
            if(outputBlankLineBeforMessage)
            {
                Console.WriteLine();
            }
            Console.WriteLine(message);
        }

        public static string ReadInput(string prompt, bool forceLowerCase = false)
        {
            Console.WriteLine();
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            return forceLowerCase ? input.ToLower() : input;
        }

        public static int GetTransactionCount()
        {
            using(var context = GetContext())
            {
                var _transactionsRepository = new TransactionRepository(context);
                var transactions = _transactionsRepository.GetList();

                return _transactionsRepository.GetCount();
            }            
        }
    }
}
