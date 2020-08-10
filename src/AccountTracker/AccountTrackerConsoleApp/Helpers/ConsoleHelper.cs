using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;

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

        public static void ListTransactions(List<int> transactionIds)
        {
            ClearOutput();
            transactionIds.Clear();
            
            OutputLine("TRANSACTIONS:");

            using (var context = GetContext())
            {
                var transactionsRepository = new TransactionRepository(context);
                var transactions = transactionsRepository.GetList();
                if (transactions.Count > 0)
                {
                    foreach (var transaction in transactions)
                    {
                        transactionIds.Add(transaction.TransactionID);
                        Console.WriteLine($"{transactions.IndexOf(transaction) + 1}) {transaction.TransactionType.Name} {transaction.Account.Name}.\r\n" +
                            $"\tAmount: {transaction.Amount}.\r\n\tTransaction date: {transaction.TransactionDate}");
                    }
                }
                else
                {
                    Console.WriteLine("No transactions found.");
                }
            }
        }

        public static void GetDetailTransaction(int transactionId)
        {
            ClearOutput();
            OutputLine("DETAILED TRANSACTION INFORMATION");

            using (var context = GetContext())
            {
                var transactionRepository = new TransactionRepository(context);
                var transaction = transactionRepository.Get(transactionId);

                OutputLine($"Transaction number:\t\t{transaction.TransactionID}");
                OutputLine($"Transaction Date:\t\t{transaction.TransactionDate}", outputBlankLineBeforMessage: false);
                OutputLine($"Transaction Type:\t\t{transaction.TransactionType.Name}", outputBlankLineBeforMessage: false);
                OutputLine($"Account:\t\t\t{transaction.Account.Name}", outputBlankLineBeforMessage: false);
                OutputLine($"Ammount:\t\t\t{transaction.Amount}", outputBlankLineBeforMessage: false);
                OutputLine($"Category:\t\t\t{transaction.Category.Name}", outputBlankLineBeforMessage: false);
                OutputLine($"Vendor:\t\t\t\t{transaction.Vendor.Name}", outputBlankLineBeforMessage: false);
                OutputLine($"Description:\t\t\t{transaction.Description}", outputBlankLineBeforMessage: false);
            }
        }

        public static void ListAccounts(List<int> accountIds)
        {
            ClearOutput();
            OutputLine("ACCOUNTS:");

            using (var context = GetContext())
            {
                var accountRepository = new AccountRepository(context);
                var accounts = accountRepository.GetList();
                if (accounts.Count > 0)
                {
                    foreach (var account in accounts)
                    {
                        accountIds.Add(account.AccountID);
                        Console.WriteLine($"{accounts.IndexOf(account) +1}) {account.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("No accounts found.");
                }
            }
        }

        public static void ListCategories()
        {
            ClearOutput();
            OutputLine("CATEGORIES:");

            using (var context = GetContext())
            {
                var categoryRepository = new CategoryRepository(context);
                var categories = categoryRepository.GetList();
                if (categories.Count > 0)
                {
                    var categoryCount = 0;
                    foreach (var category in categories)
                    {
                        categoryCount++;
                        Console.WriteLine($"{categoryCount}) {category.Name}");
                    }
                } 
            }
        }

        public static void ListTransactionTypes()
        {
            ClearOutput();

            using (var context = GetContext())
            {
                var transactionTypesRepository = new TransactionTypeRepository(context);
                var transactionTypes = transactionTypesRepository.GetList();
                if (transactionTypes.Count > 0)
                {
                    var transactionTypeCount = 0;
                    foreach (var transactionType in transactionTypes)
                    {
                        transactionTypeCount++;
                        Console.WriteLine($"{transactionTypeCount} {transactionType.Name}");
                    }
                }
            }
        }

        public static void ListVendors()
        {
            ClearOutput();
            OutputLine("VENDORS:");

            using (var context = GetContext())
            {
                var vendorRepository = new VendorRepository(context);
                var vendors = vendorRepository.GetList();
                if (vendors.Count > 0)
                {
                    var vendorCount = 0;
                    foreach (var vendor in vendors)
                    {
                        vendorCount++;
                        Console.WriteLine($"{vendorCount}) {vendor.Name}");
                    }
                }
            }
        }


        public static void OutputBlankLine()
        {
            Console.WriteLine();
        }

        public static void ClearOutput()
        {
            Console.Clear();
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


        //TODO: XML
        public static int? GetListCount(string listName)
        {
            using (var context = GetContext())
            {
                switch (listName)
                {
                    case "Transaction":
                        var transactionRepository = new TransactionRepository(context);
                        return transactionRepository.GetCount();
                    case "TransactionType":
                        var transactionTypeRepository = new TransactionTypeRepository(context);
                        return transactionTypeRepository.GetCount();
                    case "Account":
                        var accountRepository = new AccountRepository(context);
                        return accountRepository.GetCount();
                    case "Vendor":
                        var vendorRepository = new VendorRepository(context);
                        return vendorRepository.GetCount();
                    case "Category":
                        var categoryRepository = new CategoryRepository(context);
                        return categoryRepository.GetCount();
                    default:
                        ConsoleHelper.OutputLine("Error! Passed ListName not found. Close the program and troubleshoot.", true);
                        Console.ReadLine();
                        Environment.Exit(0); //Not a true 0 code for environment exit, but a message has been provided and the console app is not for production release.
                        return null;        //Only inlcuding a return null in order to satisfy Visual Studio.
                }
            }
        }
    }
}
