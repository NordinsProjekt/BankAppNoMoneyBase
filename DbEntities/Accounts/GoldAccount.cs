using DbEntities.Base;

namespace DbEntities.Accounts;

public class GoldAccount : AccountBase
{
    public GoldAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 1.0m)
        : base(accountName, accountNumber, startingBalance, interestRate) { }

    public override decimal Balance()
    {
        return bankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
