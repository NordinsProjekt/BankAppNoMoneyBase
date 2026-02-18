using BankAppNoMoney.Menu;
using BankAppNoMoney.Transactions;

namespace BankAppNoMoney.Base;

internal abstract class AccountBase
{
    protected GenericAccountMenu menu = new("Account functions", ["Deposit", "Withdraw", "Cancel"]);
    internal Guid Id { get; set; } = Guid.NewGuid();
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

    internal string AccountName { get; set; } = "";
    internal string AccountNumber { get; set; } = "";
    internal decimal InterestRate { get; set; } = 0;

    protected List<BankTransaction> bankTransactions = new List<BankTransaction>();

    internal AccountBase() { }

    internal AccountBase(string accountName, string accountNumber, decimal startingBalance, decimal interestRate)
    {
        AccountName = accountName;
        AccountNumber = accountNumber;
        StartingBalance = startingBalance;
        InterestRate = interestRate;
    }

    internal abstract decimal Balance();

    internal virtual void Deposit(decimal amount)
    {
        if (!ValidateDepositInput(amount)) return;

        AddTransaction(amount);
    }

    internal virtual void Withdraw(decimal amount)
    {
        if (!ValidateWithdrawInput(amount)) return;

        AddTransaction(-amount);
    }

    internal virtual void ShowMenu(bool useCursorPos = false)
    {
        var option = menu.ShowMenu(useCursorPos);

        switch (option)
        {
            case 0:
                {
                    Console.Write("How much to deposit?: ");
                    var validatedAmount = decimal.TryParse(Console.ReadLine(), out decimal amount);
                    Deposit(validatedAmount ? amount : 0);

                    break;
                }
            case 1:
                {
                    Console.Write("How much to withdraw?: ");
                    var validatedAmount = decimal.TryParse(Console.ReadLine(), out decimal amount);
                    Withdraw(validatedAmount ? amount : 0);

                    break;
                }
            case 2: break;
        }
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

    private void AddTransaction(decimal amount)
    {
        var t = new BankTransaction
        {
            Amount = amount,
            TransactionalDate = DateTime.Now
        };

        bankTransactions.Add(t);
    }
}
