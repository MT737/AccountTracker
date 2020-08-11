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

namespace AccountTrackerConsoleApp
{
    class Program
    {
        //Various commands that can be performed.        
        const string CommandListTransactions = "t";
        const string CommandListAccounts = "u";
        const string CommandListCategories = "c";
        const string CommandListVendors = "v";
        const string CommandMainScreen = "m";
        const string CommandAdd = "a";
        const string CommandEdit = "e";
        const string CommandDelete = "d";
        const string CommandQuit = "q";
        const string CommandReturn = "r";

        static void Main(string[] args)
        {         
            //Default to the transactions list.
            string command = "welcome";

            while (command != CommandQuit)
            {
                switch (command)
                {
                    case CommandListTransactions:
                        ViewTransactions();
                        command = CommandMainScreen;
                        continue;
                    case CommandListAccounts:
                        //ConsoleHelper.ListAccounts();
                        break;
                    case CommandListCategories:
                        //ConsoleHelper.ListCategories();
                        break;
                    case CommandListVendors:
                        //ConsoleHelper.ListVendors();
                        break;
                    case "welcome":
                        //ConsoleHelper.Output("Welcome to the AccountTracker console app.");
                        break;
                    case CommandMainScreen:
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
            string command = CommandListTransactions;
            var transactionIds = new List<int>();
            
            while (command != CommandMainScreen)
            {
                switch (command)
                {
                    case CommandListTransactions:
                        ConsoleHelper.ListTransactions(transactionIds);
                        break;
                    case CommandAdd:
                        AddTransaction();
                        command = CommandListTransactions;
                        continue;
                    default:
                        if (AttempDisplayTransaction(transactionIds, command))
                        {
                            command = CommandListTransactions;
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
            //pull inputs from user
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

                //Get TransactionType ID by listing type options for the user
                inputId = GetTransactionTypeID();
                if (inputId == null)
                {
                    haltProcess = true;
                    continue;
                }
                transaction.TransactionTypeID = inputId.Value;

                //Get Account by listing type options for the user
                inputId = GetAccountID();
                if (inputId == null)
                {
                    haltProcess = true;
                    continue;
                }                
                transaction.AccountID = inputId.Value;

                //Get amount (ensure the value is currency)
                transaction.Amount = GetAmount();

                //Get Category by listing type options for the user
                transaction.CategoryID = GetCategoryID();

                //Get Vendor by listing type options for the user
                transaction.VendorID = GetVendorID();

                //Get description (ensure the text is within bounds) Test EF's sql injection protection by trying to use drop table
                transaction.Description = GetDescription(); 
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

        //TODO: Implement
        private static decimal GetAmount()
        {
            throw new NotImplementedException();
        }

        //TODO: Implement
        private static string GetDescription()
        {
            throw new NotImplementedException();
        }

        //TODO: Implement
        private static int GetVendorID()
        {
            throw new NotImplementedException();
        }

        //TODO: Implement
        private static int GetCategoryID()
        {
            throw new NotImplementedException();
        }




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

        public static void DisplayTransaction(int transactionID)
        {
            string command = CommandListTransactions;
            while (command != CommandReturn)
            {
                switch (command)
                {
                    case CommandListTransactions:
                        ConsoleHelper.GetDetailTransaction(transactionID);
                        break;
                    case CommandEdit:
                        //TODO: Edit transaction
                        command = CommandListTransactions;
                        continue;
                    case CommandDelete:
                        //TODO: Delete transaction. Use an if statement with a boolean method to confirm deletion. When deletion complete, pass the return command. If return false, relist the transaction details.
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

    }
}
