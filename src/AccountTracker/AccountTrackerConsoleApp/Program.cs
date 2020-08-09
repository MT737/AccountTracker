using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTrackerConsoleApp.Helpers;
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
                        ConsoleHelper.ListAccounts();
                        break;
                    case CommandListCategories:
                        ConsoleHelper.ListCategories();
                        break;
                    case CommandListVendors:
                        ConsoleHelper.ListVendors();
                        break;
                    case "welcome":
                        ConsoleHelper.Output("Welcome to the AccountTracker console app.");
                        break;
                    case CommandMainScreen:
                        ConsoleHelper.ClearOutput();
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
                        //TODO: Implement add transaction.
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
                var transactionCount = ConsoleHelper.GetTransactionCount();
                if (transactionCount > 0)
                {
                    ConsoleHelper.Output($"Enter a number 1-{transactionCount} for a detialed view,");
                }
                ConsoleHelper.OutputLine("m - return to detailed screen", false);
                
                //Get the command from the user.
                command = ConsoleHelper.ReadInput("Enter a command: ", forceLowerCase: true);
            }
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

    }
}
