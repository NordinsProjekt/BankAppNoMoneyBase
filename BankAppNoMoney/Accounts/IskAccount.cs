using BankAppNoMoney.Base;
using BankAppNoMoney.Transactions;

namespace BankAppNoMoney.Accounts;

internal class IskAccount : AccountBase
{
    public IskAccount()
    {
    }

    public IskAccount(string accountName, string accountNumber, decimal startingBalance, decimal interestRate = 0.5m)
        : base(accountName, accountNumber, startingBalance, interestRate)
    {
    }

    protected List<SecurityTransactionBase> securityTransactions = new List<SecurityTransactionBase>();

    internal override decimal Balance()
    {
        var bankTransaction = bankTransactions.Sum(x => x.Amount);

        var fundTransaction = securityTransactions
            .OfType<MutualFundTransaction>()
            .Sum(x => x.GetCurrentValue(x.PurchasePrice));

        var stockTransaction = securityTransactions.OfType<StockTransaction>().Sum(x => x.GetCurrentValue(x.PurchasePrice));

        return bankTransaction + fundTransaction + stockTransaction + StartingBalance;
    }

    internal decimal GetCashBalance()
    {
        return StartingBalance + bankTransactions.Sum(x => x.Amount);
    }

    internal List<SecurityTransactionBase> GetSecurityTransactions()
    {
        return securityTransactions.ToList();
    }

    internal void AddSecurityTransaction(SecurityTransactionBase transaction)
    {
        securityTransactions.Add(transaction);
    }
}
