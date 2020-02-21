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

        // method for viewing accounts
        public void ViewAccounts()
        {
        foreach (var account in Accounts)
            {
            Console.WriteLine($"\n{account.Name}: ${account.Amount}");
            }
            var total = Accounts.Sum(sum => sum.Amount);
            Console.WriteLine($"\nTOTAL BALANCE: ${total}");
        }

        // method for view single account
        public void ViewOne(string which)
        {
        var account = Accounts.First(account => account.Name == which).Amount;
        Console.WriteLine($"\nYour {which} account has a balance of ${account}.");
        }

        // method deposit
        public void Deposit(string which)
        {
            var add = int.Parse(Console.ReadLine());
            var deposit = Accounts.First(Account => Account.Name == which).Amount;
            deposit += add;
            Console.WriteLine($"\nYour checking account has a balance of ${deposit}.");
            Accounts.First(Account => Account.Name == which).Amount = deposit;
        }

        // method withdraw from account
        public void Withdraw(string which)
        {
            var remove = int.Parse(Console.ReadLine());
            var withdraw = Accounts.First(Account => Account.Name == which).Amount;
            withdraw -= remove;
            Console.WriteLine($"\nYour checking account has a balance of ${withdraw}.");
            Accounts.First(Account => Account.Name == which).Amount = withdraw;
        }

        // method transfer to checking
        public void TransferChecking(string which)
        {
            var transfer = int.Parse(Console.ReadLine());
            var fromAccount = Accounts.First(Account => Account.Name == which).Amount;
            var toAccount = Accounts.First(Account => Account.Name == "SAVINGS").Amount;
            fromAccount = fromAccount - transfer;
            Accounts.First(Account => Account.Name == "CHECKING").Amount = fromAccount;
            toAccount = toAccount + transfer;
            Accounts.First(Account => Account.Name == "SAVINGS").Amount = toAccount;
        }
        
       // method transfer to savings
        public void TransferSavings(string which)
        {
            var transfer = int.Parse(Console.ReadLine());
            var fromAccount = Accounts.First(Account => Account.Name == which).Amount;
            var toAccount = Accounts.First(Account => Account.Name == "CHECKING").Amount;
            fromAccount = fromAccount - transfer;
            Accounts.First(Account => Account.Name == "SAVINGS").Amount = fromAccount;
            toAccount = toAccount + transfer;
            Accounts.First(Account => Account.Name == "CHECKING").Amount = toAccount;
        }
    }
}