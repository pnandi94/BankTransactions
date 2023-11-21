using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasitotablazat
{
    public class BankAccount
    {
        public string AccountNumber { get; }
        public double Money { get; private set; }

        public BankAccount(string accountNumber, double initialBalance)
        {
            AccountNumber = accountNumber;
            Money = initialBalance;
        }

        public void Deposit(double amount)
        {
            Money += amount;
        }

        public void Withdraw(double amount)
        {
            if (amount > Money)
            {
                Console.WriteLine($"Insufficient funds for withdrawal from account {AccountNumber}.");
            }
            else
            {
                Money -= amount;
            }
        }
    }
}
