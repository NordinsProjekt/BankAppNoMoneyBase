using BankAppNoMoney.Base;

namespace BankAppNoMoney.Accounts;

internal class GoldAccount : AccountBase
{
    public GoldAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 1.0m)
        : base(accountName, accountNumber, startingBalance, interestRate) { }

    internal override decimal Balance()
    {
        return bankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
