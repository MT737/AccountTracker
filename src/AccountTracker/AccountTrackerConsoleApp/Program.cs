using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTrackerConsoleApp.Helpers;
using AccountTrackerLibrary;
using AccountTrackerLibrary.Data;
using AccountTrackerLibrary.Models;

namespace AccountTrackerConsoleApp
{
    class Program
    {
        //Various commands that can be performed.        
        const string CommandAdd = "a";
        const string CommandEdit = "e";
        const string CommandDelete = "d";
        const string CommandQuit = "q";
        const string CommandReturn = "r";
        const string CommandYes = "y";
        const string CommandB = "b";
        const string CommandC = "c";
        const string CommandG = "g";
        const string CommandM = "m";
        const string CommandS = "s";
        const string CommandT = "t";
        const string CommandU = "u";
        const string CommandV = "v";
        const string CommandW = "w";
        const string CommandX = "x";
        const string CommandZ = "z";

        static void Main(string[] args)
        {         
            //Default to the transactions list.
            string command = "welcome";

            while (command != CommandQuit)
            {
                switch (command)
                {
                    case CommandT:
                        ViewTransactions();
                        command = CommandM;
                        continue;
                    case CommandU:
                        //ConsoleHelper.ListAccounts();
                        break;
                    case CommandC:
                        //ConsoleHelper.ListCategories();
                        break;
                    case CommandV:
                        //ConsoleHelper.ListVendors();
                        break;
                    case "welcome":
                        //ConsoleHelper.Output("Welcome to the AccountTracker console app.");
                        break;
                    case CommandM:
                        //ConsoleHelper.ClearOutput();
                        break;
                    default: 
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();                
                ConsoleHelper.OutputLine("Please select a command from the following options:\r\n t - view transactions\r\n u - view accounts\r\n c - view categories\r\n v - view vendors" +
                    "\r\n q - to quite", false);

                //Get command from the user.
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }
        }

        public static void ViewTransactions()
        {
            string command = CommandT;
            var transactionIds = new List<int>();
            
            while (command != CommandM)
            {
                switch (command)
                {
                    case CommandT:
                        ConsoleHelper.ListTransactions(transactionIds);
                        break;
                    case CommandAdd:
                        AddTransaction();
                        command = CommandT;
                        continue;
                    default:
                        if (AttempDisplayTransaction(transactionIds, command))
                        {
                            command = CommandT;
                            continue;
                        }
                        else
                        {
                            ConsoleHelper.OutputLine("Sorry, that command was not understood.");
                        }                        
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                var transactionCount = ConsoleHelper.GetListCount("Transaction");
                string message = "";
                if (transactionCount > 0)
                {
                    message = $"Select transaction from 1-{transactionCount} or ";
                }
                ConsoleHelper.Output($"{message}a - add transaction, m - return to detailed screen");
                
                //Get the command from the user.
                command = ConsoleHelper.ReadInput("Enter a command: ", forceLowerCase: true);
            }
        }

        public static void AddTransaction()
        {
            var transaction = new Transaction();
            var haltProcess = false;
            int? inputId;

            while (!haltProcess)
            {
                //Date
                DateTime? transDate = GetTransactionDate();
                if (transDate == null)
                {
                    haltProcess = true;
                    continue;
                }
                transaction.TransactionDate = transDate.Value;

                //TransactionType
                inputId = GetTransactionTypeID();
                if (inputId == null)
                {
                    haltProcess = true;
                    continue;
                }
                transaction.TransactionTypeID = inputId.Value;

                //Account
                inputId = GetAccountID();
                if (inputId == null)
                {
                    haltProcess = true;
                    continue;
                }                
                transaction.AccountID = inputId.Value;

                //Amount
                decimal? inputAmount = GetAmount();
                if (inputAmount == null)
                {
                    haltProcess = true;
                    continue;
                }
                transaction.Amount = inputAmount.Value;

                //Category
                inputId = GetCategoryID();
                if (inputId == null)
                {
                    haltProcess = true;
                    continue;
                }
                transaction.CategoryID = inputId.Value;

                //Vendor
                inputId = GetVendorID();
                if(inputId == null)
                {
                    haltProcess = true;
                    continue;
                }                
                transaction.VendorID = inputId.Value;

                //Description
                transaction.Description = GetDescription();

                //Add Transaction
                ConsoleHelper.AddTransaction(transaction);
                haltProcess = true;
            }
        }

        /// <summary>
        /// Prompts the user to enter a date and validates the input.
        /// </summary>
        /// <returns>Returns a DateTime object with a date value if the user provides a valid date value or null if the user cancels.</returns>
        private static DateTime? GetTransactionDate()
        {
            string command = "get value";

            while (true)
            {
                switch (command)
                {
                    case CommandReturn:
                        return null;
                    case "get value":
                        ConsoleHelper.ClearOutput();
                        ConsoleHelper.Output("DATE");
                        break;
                    default:
                        ConsoleHelper.ClearOutput();
                        ConsoleHelper.OutputLine("Sorry, that input was not valid. Try again.");
                        break;
                }

                //Request user input
                ConsoleHelper.OutputLine("Enter a date or enter r - to return to transactions list.");
                ConsoleHelper.OutputLine("Date format example - 10/22/2018", false);

                //Confirm input
                command = ConsoleHelper.ReadInput("Date", true);
                DateTime inputtedData;
                DateTime.TryParse(command, out inputtedData);
                if (inputtedData != DateTime.MinValue)
                {
                    return inputtedData.Date;
                }
            }            
        }

        //TODO: Add XML
        private static int? GetTransactionTypeID()
        {
            string command = "get value";
            List<int> transactionTypeIds = new List<int>();

            while (true)
            {
                switch (command)
                {
                    case CommandReturn:
                        return null;
                    case "get value":
                        ConsoleHelper.ListTransactionTypes(transactionTypeIds);
                        break;
                    default:
                        if (InputConfirmed(command, transactionTypeIds))
                        {
                            return transactionTypeIds[int.Parse(command) - 1];
                        }
                        else
                        {                      
                            ConsoleHelper.ListTransactionTypes(transactionTypeIds);
                            ConsoleHelper.OutputLine("Sorry, that input was not valid. Try again.");
                        }
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                var transactionTypeCount = ConsoleHelper.GetListCount("TransactionType");
                string message = "";
                if (transactionTypeCount > 0)
                {
                    message = $"Select 1-{transactionTypeCount} or ";
                }
                ConsoleHelper.Output($"{message}r - return to detailed screen");

                //Get the command from the user.
                command = ConsoleHelper.ReadInput("Enter a command: ", forceLowerCase: true);
            }
        }

        //TODO: XML documentation
        private static int? GetAccountID()
        {
            string command = "get value";            
            List<int> accountIds = new List<int>();

            while (true)
            {
                switch (command)
                {
                    case CommandReturn:
                        return null;
                    case "get value":
                        ConsoleHelper.ListAccounts(accountIds);
                        break;
                    default:
                        if (InputConfirmed(command, accountIds))
                        {
                            return accountIds[int.Parse(command) - 1];
                        }
                        else
                        {
                            ConsoleHelper.ListAccounts(accountIds);
                            ConsoleHelper.OutputLine("Sorry, that input was not valid. Try again.");
                        }
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                var accountCount = ConsoleHelper.GetListCount("Account");
                string message = "";
                if (accountCount > 0)
                {
                    message = $"Select 1-{accountCount} or ";
                }
                ConsoleHelper.Output($"{message}r - return to detailed screen");

                //Get the command from the user.
                command = ConsoleHelper.ReadInput("Enter a command: ", forceLowerCase: true);
            }
        }

        //TODO: XML Documenation
        private static decimal? GetAmount()
        {
            string command = "get value";

            while (true)
            {
                switch (command)
                {
                    case CommandReturn:
                        return null;
                    case "get value":
                        ConsoleHelper.ClearOutput();
                        break;
                    default:
                        if (InputConfirmed(command))
                        {
                            return decimal.Parse(command);
                        }
                        else
                        {
                            ConsoleHelper.OutputLine("Sorry, that input was not valid. Try again.");
                        }
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("AMOUNT:");
                ConsoleHelper.Output("Commands: ");
                string message = "Enter a transaction amount or ";
                ConsoleHelper.Output($"{message}r - return to detailed screen");

                //Get the command from the user.
                command = ConsoleHelper.ReadInput("Command: ", forceLowerCase: true);
            }
        }

        //TODO: XML Documentation
        private static int? GetCategoryID()
        {
            string command = "get value";
            List<int> categoryIds = new List<int>();

            while (true)
            {
                switch (command)
                {
                    case CommandReturn:
                        return null;
                    case "get value":
                        ConsoleHelper.ListCategories(categoryIds);
                        break;
                    default:
                        if (InputConfirmed(command, categoryIds))
                        {
                            return categoryIds[int.Parse(command) - 1];
                        }
                        else
                        {
                            ConsoleHelper.ListCategories(categoryIds);
                            ConsoleHelper.OutputLine("Sorry, that input was not valid. Try again.");
                        }
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                var categoryCount = ConsoleHelper.GetListCount("Category");
                string message = "";
                if (categoryCount > 0)
                {
                    message = $"Select 1-{categoryCount} or ";
                }
                ConsoleHelper.Output($"{message}r - return to detailed screen");

                //Get the command from the user.
                command = ConsoleHelper.ReadInput("Enter a command: ", forceLowerCase: true);
            }
        }

        //TODO: XML Documentation
        private static int? GetVendorID()
        {
            string command = "get value";
            List<int> vendorIds = new List<int>();

            while (true)
            {
                switch (command)
                {
                    case CommandReturn:
                        return null;
                    case "get value":
                        ConsoleHelper.ListVendors(vendorIds);
                        break;
                    default:
                        if (InputConfirmed(command, vendorIds))
                        {
                            return vendorIds[int.Parse(command) - 1];
                        }
                        else
                        {
                            ConsoleHelper.ListCategories(vendorIds);
                            ConsoleHelper.OutputLine("Sorry, that input was not valid. Try again.");
                        }
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                var categoryCount = ConsoleHelper.GetListCount("Vendor");
                string message = "";
                if (categoryCount > 0)
                {
                    message = $"Select 1-{categoryCount} or ";
                }
                ConsoleHelper.Output($"{message}r - return to detailed screen");

                //Get the command from the user.
                command = ConsoleHelper.ReadInput("Enter a command: ", forceLowerCase: true);
            }
        }

        //TODO: XML Documentation
        private static string GetDescription()
        {
            ConsoleHelper.ClearOutput();
            ConsoleHelper.Output("Enter a description (this can be left blank).");
            
            //Get the command from the user.
            return ConsoleHelper.ReadInput("Description: ", forceLowerCase: false);            
        }

        //TODO: XML Documentation
        public static bool AttempDisplayTransaction(List<int> transactionIds, string command)
        {
            bool successful = false;
            int? transactionID = null;

            //Check that list of Ids is not null.
            if (transactionIds != null)
            {
                //Attempt to parse the command to a line number.
                int lineNumber = 0;
                int.TryParse(command, out lineNumber);

                //If the line number is within the range, then pull that transaction.
                if (lineNumber > 0 && lineNumber <= transactionIds.Count)
                {
                    transactionID = transactionIds[lineNumber - 1];
                    successful = true;
                }
            }

            //If transactionID exists, then display the transaction in detail.
            if (transactionID != null)
            {
                DisplayTransaction(transactionID.Value);
            }

            return successful;
        }

        //TODO: Complete Implementation
        public static void DisplayTransaction(int transactionID)
        {
            string command = CommandT;
            while (command != CommandReturn)
            {
                switch (command)
                {
                    case CommandT:
                        ConsoleHelper.ListTransactionDetails(transactionID);
                        break;
                    case CommandEdit:
                        EditTransaction(transactionID);
                        command = CommandT;
                        continue;
                    case CommandDelete:
                        DeleteTransaction(transactionID);
                        command = CommandReturn;
                        continue;
                    default:
                        ConsoleHelper.OutputLine("Sorry, but the command was not understood.");
                        break;
                }

                //List available commands
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                ConsoleHelper.OutputLine("e - edit, d - delete, r - return", false);

                //Get command from user.
                command = ConsoleHelper.ReadInput("Enter a command: ", true);
            }
        }

        //TODO: Implement
        private static void EditTransaction(int transactionId)
        {
            string command = CommandT;
            int? id;
            Transaction transaction = ConsoleHelper.GetTransaction(transactionId);
           
            while (command != CommandReturn)
            {
                switch (command)
                {
                    case CommandT:
                        ConsoleHelper.ListTransactionDetails(transaction);
                        break;
                    case CommandG:
                        //Edit transaction date
                        DateTime? date = GetTransactionDate();
                        if (date != null)
                        {
                            transaction.TransactionDate = date.Value;
                        }
                        command = CommandT;
                        continue;
                    case CommandZ:
                        //Edit transaction type                        
                        id = GetTransactionTypeID();
                        if (id !=null)
                        {
                            transaction.TransactionTypeID = id.Value;
                        }
                        command = CommandT;
                        continue;
                    case CommandX:
                        //Edit account
                        id = GetAccountID();
                        if (id != null)
                        {
                            transaction.AccountID = id.Value;
                        }
                        command = CommandT;
                        continue;
                    case CommandC:
                        //Edit category
                        id = GetCategoryID();
                        if (id != null)
                        {
                            transaction.CategoryID = id.Value;
                        }
                        command = CommandT;
                        continue;
                    case CommandB:
                        //Edit amount
                        decimal? amount = GetAmount();
                        if (amount != null)
                        {
                            transaction.Amount = amount.Value;
                        }
                        command = CommandT;
                        continue;
                    case CommandV:
                        //Edit vendor
                        id = GetVendorID();
                        if (id != null)
                        {
                            transaction.VendorID = id.Value;
                        }
                        command = CommandT;
                        continue;
                    case CommandW:
                        //Edit description
                        //TODO: current method doesn't provide an opportunity to back out. Add that!
                        transaction.Description = GetDescription();
                        command = CommandT;
                        continue;
                    case CommandS:
                        //Save changes to the DB
                        ConsoleHelper.EditTransaction(transaction);
                        command = CommandReturn;
                        continue;
                    case CommandReturn:
                        //Return;
                        command = CommandReturn;
                        continue;
                    default:
                        ConsoleHelper.OutputLine("Sorry, but the command was not understood.");
                        break;
                }

                //List available commands
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                ConsoleHelper.OutputLine("d - edit date, z - edit transaction type, x - edit account, c - edit category, b - edit amount, v - edit vendor, w - edit description, s - save changes, r - cancel", false);

                //Get command from user.
                command = ConsoleHelper.ReadInput("Enter a command to edit an entry, save changes, or cancel: ", true);
            }
        }

        //TODO: XML Documentation
        private static void DeleteTransaction(int transactionId)
        {
            string command = "Confirm";
            while (command != CommandReturn)
            {
                switch (command)
                {
                    case "Confirm":
                        break;
                    case CommandYes:
                        ConsoleHelper.DeleteTransaction(transactionId);
                        ConsoleHelper.OutputLine("Transaction deleted. Press any button to continue.", true);
                        Console.ReadLine();
                        command = CommandReturn;
                        continue;
                    default:            //if response is anything but 'y'.
                        command = CommandReturn;
                        continue;
                }

                //Get user confirmation.
                ConsoleHelper.OutputLine("Are you sure you want to delete this transaction? This action cannot be undone.", true);
                command = ConsoleHelper.ReadInput("Press 'y' to confirm or any other key to cancel.", true);
            }
        }

        //TODO: XML Documentation
        public static bool InputConfirmed(string command, List<int> ids)
        {
            var successful = false;

            //Check that the list of Ids is not null.
            if (ids != null)
            {
                //Attempt to parse the command to a line number.
                int lineNumber = 0;
                int.TryParse(command, out lineNumber);

                //If the line number is within the range, then input confirmed.
                if (lineNumber > 0 && lineNumber <= ids.Count)
                {
                    successful = true;
                }
            }

            if (ids == null)
            {
                ConsoleHelper.OutputLine("Error! Id list is null. Close program and troubleshoot.", true);
                Environment.Exit(0); //Not a true 0 code for environment exit, but a message has been provided and the console app is not for production release.
            }

            return successful;
        }

        //TODO: XML Documentation
        public static bool InputConfirmed(string input)
        {
            bool successful = false;

            //Check if the input is not null.
            if (input != null)
            {
                //Attempt to parse input as decimal
                decimal.TryParse(input, out decimal value);

                //Ensure the value parsed and is positive.
                if (value > 0)
                {
                    successful = true;
                }
            }

            return successful;
        }

    }
}
