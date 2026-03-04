using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class MillionAccount : AccountBase
{
    public MillionAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 1.0m)
        : base(accountName, accountNumber, startingBalance, interestRate) { }

    internal override decimal Balance()
    {
        return bankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
