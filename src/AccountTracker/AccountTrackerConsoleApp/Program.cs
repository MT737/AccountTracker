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
        const string CommandListAccounts = "a";
        const string CommandListCategories = "c";
        const string CommandListVendors = "v";
        const string CommandQuit = "q";

        static void Main(string[] args)
        {         
            //Default to the transactions list.
            string command = CommandListTransactions;

            while (command != CommandQuit)
            {
                switch (command)
                {
                    case CommandListTransactions:
                        //TODO: call helper method to pull transactions?
                        ConsoleHelper.ListTransactions();
                        break;
                    default: //TODO: Call helper method to pull transactions? Going to have the transactions list as the default.
                        ConsoleHelper.ListTransactions();
                        break;
                }

                //List the available commands.
                ConsoleHelper.OutputBlankLine();
                ConsoleHelper.Output("Commands: ");
                var transactionCount = ConsoleHelper.GetTransactionCount();
                if (transactionCount > 0)
                {
                    ConsoleHelper.Output($"Enter a number 1-{transactionCount},");
                }
                ConsoleHelper.OutputLine("putcommandshere. Just hit t for now or q to quite", false);

                //Get command from the user.
                command = ConsoleHelper.ReadInput("Enter a Command: ", true);
            }
        }
    }
}
