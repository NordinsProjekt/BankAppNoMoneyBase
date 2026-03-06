using Entities.Transactions;

namespace Entities.Base;

public abstract class AccountBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    private decimal startingBalance = 0;

    protected decimal StartingBalance
    {
        get
        {
            return startingBalance;
        }
        set
        {
            if (value < 0)
            {
                startingBalance = 0;
            }
            else
            {
                startingBalance = value;
            }
        }
    }

    public string AccountName { get; set; } = "";
    public string AccountNumber { get; set; } = "";
    public decimal InterestRate { get; set; } = 0;

    protected List<BankTransaction> bankTransactions = new List<BankTransaction>();

    public AccountBase() { }

    public AccountBase(string accountName, string accountNumber, decimal startingBalance, decimal interestRate)
    {
        AccountName = accountName;
        AccountNumber = accountNumber;
        StartingBalance = startingBalance;
        InterestRate = interestRate;
    }

    public abstract decimal Balance();

    public virtual void Deposit(decimal amount, DateTime? dateTime = null)
    {
        if (!ValidateDepositInput(amount)) return;

        AddTransaction(amount, dateTime);
    }

    public decimal CalculateInterestForTheYear(int year)
    {
        var validatedDate = DateTime.TryParse($"{year}-01-01", out DateTime parsedDate);
        if (!validatedDate) return -1;

        var balance = GetStartBalance(year);
        var interestForAccount = 0m;

        var yearTransactions = GetYearTransactionsByDay(year);
        var tempDateTime = DateOnly.FromDateTime(parsedDate);

        foreach (var transaction in yearTransactions)
        {
            var interval = transaction.Key.DayNumber - tempDateTime.DayNumber;
            interestForAccount += CalculatePeriodInterest(balance, interval);
            balance += transaction.Sum(x => x.Amount);
            tempDateTime = transaction.Key;
        }

        var lastToEndOfYear = DateOnly.Parse($"{year}-12-31").DayNumber - tempDateTime.DayNumber + 1;
        interestForAccount += CalculatePeriodInterest(balance, lastToEndOfYear);

        return interestForAccount;
    }

    private decimal GetStartBalance(int year)
    {
        return StartingBalance + bankTransactions
            .Where(x => x.TransactionalDate.Year < year)
            .Sum(x => x.Amount);
    }

    private IEnumerable<IGrouping<DateOnly, BankTransaction>> GetYearTransactionsByDay(int year)
    {
        return bankTransactions
        .Where(x => x.TransactionalDate.Year == year)
        .OrderBy(x => x.TransactionalDate)
        .GroupBy(x => DateOnly.FromDateTime(x.TransactionalDate));
    }

    private decimal CalculatePeriodInterest(decimal balance, int days)
    {
        return balance * (InterestRate / 100) / 365 * days;
    }

    public virtual void Withdraw(decimal amount)
    {
        if (!ValidateWithdrawInput(amount)) return;

        AddTransaction(-amount);
    }

    private bool ValidateDepositInput(decimal amount)
    {
        if (amount < 0)
        {
            Console.WriteLine("Deposit value cant be negative");
            return false;
        }

        if (decimal.MaxValue < amount)
        {
            Console.WriteLine("Deposit value too big");
            return false;
        }

        return true;
    }

    private bool ValidateWithdrawInput(decimal amount)
    {
        if (Balance() <= amount)
        {
            Console.WriteLine("Not enough money to withdraw");
            return false;
        }

        if (decimal.MaxValue < amount)
        {
            Console.WriteLine("Withdraw value too big");
            return false;
        }

        return true;
    }

    private void AddTransaction(decimal amount, DateTime? dateTime = null)
    {
        var transaction = new BankTransaction
        {
            Amount = amount,
            TransactionalDate = dateTime ?? DateTime.Now
        };

        bankTransactions.Add(transaction);
    }
}
