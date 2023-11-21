using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasitotablazat
{
    class Program
    {
        static void Main(string[] args)
        {
            // Tesztelés
            Bank bank = InitializeBank("bank_log.txt");

            // Tranzakciók végrehajtása
            ProcessTransactions("bank_log.txt", bank);

            // Egyenlegek kiírása
            PrintBalances(bank);

            // Example usage of BankHashSet
            BankHashSet<string, int> hashSet = new BankHashSet<string, int>();

            hashSet.Insert("one", 1);
            hashSet.Insert("two", 2);

            Console.WriteLine(hashSet.Find("one")); // Output: 1
            Console.WriteLine(hashSet["two"]);       // Output: 2

            // You can also use hashSet["key"] = value syntax
            hashSet["three"] = 3;
            Console.WriteLine(hashSet["three"]);     // Output: 3
        }

        static Bank InitializeBank(string fileName)
        {
            // Fiókok inicializálása a fájlból
            Bank bank = new Bank();
            List<string> lines = ReadLinesFromFile(fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    string accountNumber = parts[0].Trim();
                    double initialBalance = double.Parse(parts[1].Trim());
                    bank.RegisterAccount(accountNumber, initialBalance);
                }
            }

            return bank;
        }

        static void ProcessTransactions(string fileName, Bank bank)
        {
            // Tranzakciók feldolgozása a fájlból
            List<string> lines = ReadLinesFromFile(fileName);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 3)
                {
                    string fromAccount = parts[0].Trim();
                    string toAccount = parts[1].Trim();
                    double amount = double.Parse(parts[2].Trim());

                    bank.Transaction(fromAccount, toAccount, amount);
                }
            }
        }

        static List<string> ReadLinesFromFile(string fileName)
        {
            // Fájl beolvasása soronként
            List<string> lines = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error reading file: {e.Message}");
            }

            return lines;
        }

        static void PrintBalances(Bank bank)
        {
            // Egyenlegek kiírása
            Console.WriteLine("\nBalances after transactions:");
            foreach (var account in bank.GetAccounts())
            {
                Console.WriteLine($"{account.AccountNumber}: {account.Money}");
            }
        }
    }
}
