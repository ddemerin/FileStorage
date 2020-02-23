using System;

namespace FileStorage
{
  class Program
  {
    static void Main(string[] args)
    {

      var tracker = new AccountTracker();
      var userName = "";
      tracker.LoadData();

      var isRunning = true;
      var login = true;

      while(login)
      {
      Console.Clear();
      // Greet the user
      Console.WriteLine("Welcome to First Bank of Suncoast Account Manager!");
        // Login or sign up
      Console.WriteLine("(LOGIN) or (SIGNUP)?");
      var input = Console.ReadLine().ToUpper();
      if (input == "LOGIN")
      {
        Console.WriteLine("What is your username?");
        userName = Console.ReadLine().ToUpper();
        tracker.CheckLogin(userName);
        login = false;
        isRunning = true;
      }
      else if (input == "SIGNUP")
          // Sign up leads to setting username (and password)
      {
        Console.WriteLine("What username would you like to use?");
        userName = Console.ReadLine().ToUpper();
        tracker.CheckSignup(userName);
        Console.WriteLine("How much would you like to deposit into your CHECKING account?");
        var checkingAmount = int.Parse(Console.ReadLine());
        Console.WriteLine("How much would you like to deposit into your SAVINGS account?");
        var savingsAmount = int.Parse(Console.ReadLine());
        tracker.AddNewAccount(userName, checkingAmount, savingsAmount);
        login = false;
        isRunning = true;
      }
            // Saves 
          // Login takes in string for username

      }

    
      while (isRunning)
      {
      Console.Clear();
      // Greet the user
      Console.WriteLine("Welcome to First Bank of Suncoast Account Manager!");
      // Menu for choosing an account to access, immediately shows your account balances
      tracker.ViewAccounts(userName);
      Console.WriteLine("\nWhich account would you like to access?\n\n(CHECKING) or (SAVINGS)?\n\nOr would you like to (QUIT)?");
      var which = Console.ReadLine().ToUpper();
      if (which != "CHECKING" && which != "SAVINGS" && which != "QUIT")
      {
        Console.WriteLine("That is not an account option.");
        Console.WriteLine("\nWhich account would you like to access?\n\n(CHECKING) or (SAVINGS)?\n\nOr would you like to (QUIT)?");
      }
      // if == checking
        // Sub-menu for whether depositing, withdrawing, or transferring between accounts
      if (which == "CHECKING")
      {
        Console.Clear();
        tracker.ViewOne(which, userName);
        Console.WriteLine($"\nWhat would you like to do?");
        Console.WriteLine("\n(DEPOSIT), (WITHDRAW), or (TRANSFER)");
        var input = Console.ReadLine().ToUpper();
          if (input != "DEPOSIT" && input != "WITHDRAW" && input != "TRANSFER" && input != "QUIT")
          {
            Console.WriteLine("That is not option for this account.");
            Console.WriteLine("\n(DEPOSIT), (WITHDRAW), or (TRANSFER)");
          }
          else if (input == "DEPOSIT")
          {
          Console.Clear();
          Console.WriteLine("\nHow much would you like to deposit?");
          tracker.Deposit(which, userName);
          Console.WriteLine("\nPress Enter to return to main menu or would you like to (QUIT)?");
          input = Console.ReadLine().ToUpper();
          if ( input == "QUIT")
            {
              isRunning = false;
            } 
          }
        // Withdraw
          else if (input == "WITHDRAW")
          {
          Console.Clear();
          Console.WriteLine("\nHow much would you like to withdraw?");
          tracker.Withdraw(which, userName);
          Console.WriteLine("\nPress Enter to return to main menu or would you like to (QUIT)?");
          input = Console.ReadLine().ToUpper();
          }
          // Transfer
          else if (input == "TRANSFER")
          {
            Console.Clear();
            Console.WriteLine("\nHow much would you like to transfer to SAVINGS?");
            tracker.TransferChecking(which, userName);
            tracker.ViewAccounts(userName);

            Console.WriteLine("\nPress Enter to return to main menu or would you like to (QUIT)?");
            input = Console.ReadLine().ToUpper();
          }
        }
        // if == saving
          // Sub-menu for whether depositing, withdrawing, or transferring between accounts
        if (which == "SAVINGS")
        {
          Console.Clear();
          tracker.ViewOne(which, userName);
          Console.WriteLine($"\nWhat would you like to do?");
          Console.WriteLine("\n\n(DEPOSIT), (WITHDRAW), or (TRANSFER)");
          var input = Console.ReadLine().ToUpper();
          if (input != "DEPOSIT" && input != "WITHDRAW" && input != "TRANSFER" && input != "QUIT")
          {
            Console.WriteLine("That is not option for this account.");
            Console.WriteLine("\n\n(DEPOSIT), (WITHDRAW), or (TRANSFER)");
          }
          // Deposit
          else if (input == "DEPOSIT")
          {
            Console.Clear();
            Console.WriteLine("\nHow much would you like to deposit?");
            tracker.Deposit(which, userName);

            Console.WriteLine("\nPress Enter to return to main menu or would you like to (QUIT)?");
            input = Console.ReadLine().ToUpper();
          }
          else if (input == "TRANSFER")
          {
          Console.Clear();
          Console.WriteLine("\nHow much would you like to transfer to CHECKING?");
          tracker.TransferSavings(which, userName);
          tracker.ViewAccounts(userName);
          Console.WriteLine("\nPress Enter to return to main menu or would you like to (QUIT)?");
          input = Console.ReadLine().ToUpper(); 
          // Depositing, withdrawing, or transferring auto-saves
        // Ability to quit and save any changes made
          }
        }
        if ( which == "QUIT")
        {
          tracker.SaveData();
          isRunning = false;
        } 
      }
    }
  }
}

