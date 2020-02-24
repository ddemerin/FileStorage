using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using CsvHelper;

namespace FileStorage
{
    public class AccountTracker
    {
        public List<Account> Accounts { get; set; } = new List<Account>();
        
        // method for loading data
        public void LoadData()
        {
        // "using" statement fixes c# bug that doesn't allow saves while StreamReader\CsvReader is still open
            // calls StreamReader to look into accounts for load
            using (var reader = new StreamReader("accounts.csv"))
            // calls CsvReader to eliminate special characters when reading from .csv file
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {    
                // applies the records in .csv file to Account List.
                Accounts = csvReader.GetRecords<Account>().ToList();
            }
        }

        // method for saving data
        public void SaveData()
        {
        // "using" statement fixes c# bug that doesn't allow saves while StreamReader\CsvReader is still open
            // calls StreamReader to look into accounts for save   
            using (var writer = new StreamWriter("accounts.csv"))
            // calls CsvReader to eliminate special characters when reading from .csv file
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(Accounts);
                writer.Flush();
                }
        } 

        // method for adding users
        public void AddNewAccount(string userName, string password, int checkingAmount, int savingsAmount)
        {
            var checkingAccount = new Account()
            {
                UserName = userName,
                Password = password,
                Name = "CHECKING",
                Amount = checkingAmount
            };
            var savingsAccount = new Account()
            {
                UserName = userName,
                Password = password,
                Name = "SAVINGS",
                Amount = savingsAmount
            };
            Accounts.Add(checkingAccount);
            Accounts.Add(savingsAccount);
            SaveData();
        }

        public string CheckSignup(string userName)
        {
            var checkUser = Accounts.Any(account => account.UserName == userName);
            if (checkUser == true)
            {
                Console.WriteLine("That username already exists. Please choose another one.");
                userName = Console.ReadLine().ToUpper();
            }
            return userName;
        }
        public string CheckLogin(string userName)
        {
            var loginCheck = true;
            var login = new Program();
            
            while (loginCheck)
            {
            var checkUser = Accounts.Any(account => account.UserName == userName);
            if (checkUser == true)
            {
                Console.WriteLine($"\nWelcome {userName}!\n");
                loginCheck = false;
            }
            else
            {
                Console.WriteLine("\nThat username doesn't not exist. Try again.");
                userName = Console.ReadLine().ToUpper();
            }
            }
            return userName;
        }
        public void CheckPassword(string userName, string password)
        {
            var login = new Program();
            var loggingIn = true;
            while (loggingIn)
            {
            password = Console.ReadLine();
            var checkUser = Accounts.First(account => account.UserName == userName).UserName; 
            var correctPass = Accounts.First(account => account.Password == password).Password;
            if (password == correctPass && userName == checkUser)
            {
                loggingIn = false;
            }
            else
            {
                Console.WriteLine("\nThat is not the correct password. Try again.");
                password = Console.ReadLine();
            }
            }
        }

        // method for viewing accounts
        public void ViewAccounts(string userName)
        {
            var signedIn = Accounts.Where(account => account.UserName == userName);
            foreach (var account in signedIn)
            {
            Console.WriteLine($"\n{account.Name}: ${account.Amount}");
            }
            var total = signedIn.Sum(sum => sum.Amount);
            Console.WriteLine($"\nTOTAL BALANCE: ${total}");
        }

        // method for view single account
        public void ViewOne(string which, string userName)
        {   
            var signedIn = Accounts.Where(account => account.UserName == userName);
            var account = signedIn.First(account => account.Name == which).Amount;
            Console.WriteLine($"\nYour {which} account has a balance of ${account}.");
        }

        // method deposit
        public void Deposit(string which, string userName)
        {
            var add = int.Parse(Console.ReadLine());
            var signedIn = Accounts.Where(account => account.UserName == userName);
            var deposit = signedIn.First(Account => Account.Name == which).Amount;
            deposit += add;
            Console.WriteLine($"\nYour checking account has a balance of ${deposit}.");
            Accounts.First(Account => Account.Name == which).Amount = deposit;
            SaveData();
        }

        // method withdraw from account
        public void Withdraw(string which, string userName)
        {
            var remove = int.Parse(Console.ReadLine());
            var signedIn = Accounts.Where(account => account.UserName == userName);
            var withdraw = signedIn.First(Account => Account.Name == which).Amount;
            withdraw -= remove;
            Console.WriteLine($"\nYour checking account has a balance of ${withdraw}.");
            Accounts.First(Account => Account.Name == which).Amount = withdraw;
            SaveData();
        }

        // method transfer to checking
        public void TransferChecking(string which, string userName)
        {
            var transfer = int.Parse(Console.ReadLine());
            var signedIn = Accounts.Where(account => account.UserName == userName);
            var fromAccount = signedIn.First(Account => Account.Name == which).Amount;
            var toAccount = signedIn.First(Account => Account.Name == "SAVINGS").Amount;
            fromAccount = fromAccount - transfer;
            Accounts.First(Account => Account.Name == "CHECKING").Amount = fromAccount;
            toAccount = toAccount + transfer;
            Accounts.First(Account => Account.Name == "SAVINGS").Amount = toAccount;
            SaveData();
        }
        
       // method transfer to savings
        public void TransferSavings(string which, string userName)
        {
            var transfer = int.Parse(Console.ReadLine());
            var signedIn = Accounts.Where(account => account.UserName == userName);
            var fromAccount = signedIn.First(Account => Account.Name == which).Amount;
            var toAccount = signedIn.First(Account => Account.Name == "CHECKING").Amount;
            fromAccount = fromAccount - transfer;
            Accounts.First(Account => Account.Name == "SAVINGS").Amount = fromAccount;
            toAccount = toAccount + transfer;
            Accounts.First(Account => Account.Name == "CHECKING").Amount = toAccount;
            SaveData();
        }
    }
}