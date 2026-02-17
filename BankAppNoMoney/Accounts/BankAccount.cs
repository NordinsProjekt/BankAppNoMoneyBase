using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class BankAccount : AccountBase
{
    public BankAccount()
    {
    }

    public BankAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 1.0m) : base(accountName, accountNumber, startingBalance, interestRate)
    {
    }

    internal override decimal Balance()
    {
        var t = bankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}

