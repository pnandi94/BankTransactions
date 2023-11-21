using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hasitotablazat
{
    public class Bank
    {
        private BankHashSet<string, BankAccount> _accounts;

        public Bank(int size = 100, BankHashSet<string, BankAccount>.HashCallback hashFunction = null)
        {
            _accounts = new BankHashSet<string, BankAccount>(size, hashFunction);
        }

        public void Transaction(string from, string to, double amount)
        {
            try
            {
                BankAccount fromAccount = _accounts[from];
                BankAccount toAccount = _accounts[to];

                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);

                Console.WriteLine($"Transaction completed: {from} -> {to}, Amount: {amount}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Transaction failed: {ex.Message}");
            }
        }

        public void RegisterAccount(string accountNumber, double deposit)
        {
            _accounts.Insert(accountNumber, new BankAccount(accountNumber, deposit));
        }

        public IEnumerable<BankAccount> GetAccounts()
        {
            foreach (var accountList in _accounts.GetContents())
            {
                foreach (var account in accountList)
                {
                    yield return account.Content;
                }
            }
        }
    }

}
