using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTrackerLibrary;
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

        //TODO: XML
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

        public static void ListTransactionDetails(int transactionId)
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

        public static void ListTransactionDetails(Transaction transaction)
        {
            ClearOutput();
            OutputLine("DETAILED TRANSACTION INFORMATION");
            
            using (var context = GetContext())
            {
                OutputLine($"Transaction number:\t\t{transaction.TransactionID}");
                OutputLine($"Transaction Date:\t\t{transaction.TransactionDate}", outputBlankLineBeforMessage: false);
                OutputLine($"Ammount:\t\t\t{transaction.Amount}", outputBlankLineBeforMessage: false);
                OutputLine($"Description:\t\t\t{transaction.Description}", outputBlankLineBeforMessage: false);

                var transactionTypeRepository = new TransactionTypeRepository(context);                
                OutputLine($"Transaction Type:\t\t{transactionTypeRepository.Get(transaction.TransactionTypeID, false).Name}", outputBlankLineBeforMessage: false);

                var accountRepository = new AccountRepository(context);
                OutputLine($"Account:\t\t\t{accountRepository.Get(transaction.AccountID, false).Name}", outputBlankLineBeforMessage: false);

                var categoryRepository = new CategoryRepository(context);
                OutputLine($"Category:\t\t\t{categoryRepository.Get(transaction.CategoryID, false).Name}", outputBlankLineBeforMessage: false);

                var vendorRepository = new VendorRepository(context);
                OutputLine($"Vendor:\t\t\t\t{vendorRepository.Get(transaction.VendorID, false).Name}", outputBlankLineBeforMessage: false);
            }
        }

        public static Transaction GetTransaction(int transactionId)
        {
            using (var context = GetContext())
            {
                var transactionRepository = new TransactionRepository(context);
                return transactionRepository.Get(transactionId, includeRelatedEntities: false); 
            }
        }

        public static void AddTransaction(Transaction transaction)
        {
            using (Context context = GetContext())
            {
                var transactionRepository = new TransactionRepository(context);
                transactionRepository.Add(transaction);
            }
        }

        public static void DeleteTransaction(int transactionId)
        {
            using (Context context = GetContext())
            {
                var transactionRepository = new TransactionRepository(context);
                transactionRepository.Delete(transactionId);
            }
        }

        public static void EditTransaction(Transaction transaction)
        {
            using (Context context = GetContext())
            {
                var transactionRepository = new TransactionRepository(context);
                transactionRepository.Update(transaction);
            }
        }

        public static void ListAccounts(List<int> accountIds)
        {
            ClearOutput();
            accountIds.Clear();         //Clearing the list to account for instances in which the list has to be recalled due to a bad user input.

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

        public static void ListCategories(List<int> categoryIds)
        {
            ClearOutput();
            categoryIds.Clear();         //Clearing the list to account for instances in which the list has to be recalled due to a bad user input.
            OutputLine("CATEGORIES:");

            using (var context = GetContext())
            {
                var categoryRepository = new CategoryRepository(context);
                var categories = categoryRepository.GetList();
                if (categories.Count > 0)
                {                    
                    foreach (var category in categories)
                    {
                        categoryIds.Add(category.CategoryID);
                        Console.WriteLine($"{categories.IndexOf(category) + 1}) {category.Name}");
                    }
                } 
            }
        }

        public static void ListTransactionTypes(List<int> transactionTypeIds)
        {
            ClearOutput();
            transactionTypeIds.Clear();         //Clearing the list to account for instances in which the list has to be recalled due to a bad user input.
            OutputLine("TRANSACTION TYPE:");
            using (var context = GetContext())
            {
                var transactionTypesRepository = new TransactionTypeRepository(context);
                var transactionTypes = transactionTypesRepository.GetList();
                if (transactionTypes.Count > 0)
                {
                    foreach (var transactionType in transactionTypes)
                    {
                        transactionTypeIds.Add(transactionType.TransactionTypeID);                        
                        Console.WriteLine($"{transactionTypes.IndexOf(transactionType) + 1}) {transactionType.Name}");
                    }
                }
            }
        }

        public static void ListVendors(List<int> vendorIds)
        {
            ClearOutput();
            vendorIds.Clear();         //Clearing the list to account for instances in which the list has to be recalled due to a bad user input.
            OutputLine("VENDORS:");

            using (var context = GetContext())
            {
                var vendorRepository = new VendorRepository(context);
                var vendors = vendorRepository.GetList();
                if (vendors.Count > 0)
                {
                    foreach (var vendor in vendors)
                    {
                        vendorIds.Add(vendor.VendorID);
                        Console.WriteLine($"{vendors.IndexOf(vendor) + 1}) {vendor.Name}");
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
