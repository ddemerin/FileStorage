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
            using (var reader = new StreamReader("accounts.csv"))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {    
                Accounts = csvReader.GetRecords<Account>().ToList();
            }
        }

        // method for saving data
        public void SaveData()
        {
            using (var writer = new StreamWriter("accounts.csv"))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(Accounts);
                writer.Flush();
                }
        } 

        // method for adding users
        public void AddNewAccount(string userName, int checkingAmount, int savingsAmount)
        {
            var checkingAccount = new Account()
            {
                UserName = userName,
                Name = "CHECKING",
                Amount = checkingAmount
            };
            var savingsAccount = new Account()
            {
                UserName = userName,
                Name = "SAVINGS",
                Amount = savingsAmount
            };
            Accounts.Add(checkingAccount);
            Accounts.Add(savingsAccount);
            SaveData();
        }

        public void CheckSignup(string userName)
        {
            var checkUser = Accounts.Any(account => account.UserName == userName);
            if (checkUser == true)
            {
                Console.WriteLine("That username already exists. Please choose another one.");
                userName = Console.ReadLine().ToUpper();
            }
        }
        public void CheckLogin(string userName)
        {
            var login = new Program();
            var checkUser = Accounts.Any(account => account.UserName == userName);
            if (checkUser == false)
            {
                Console.WriteLine("That username doesn't not exist. Try again.");
                userName = Console.ReadLine().ToUpper();
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