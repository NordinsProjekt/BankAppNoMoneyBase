using BankAppNoMoney.Base;

namespace BankAppNoMoney;

internal class Bank
{
    private List<AccountBase> accounts = new List<AccountBase>();
    private BankMenu bankMenu = new BankMenu("Bank Menu", new string[]
    {
        "Add account",
        "Remove account",
        "Show accounts",
        "Exit"
    });

    internal void AddAccount(AccountBase account)
    {
        accounts.Add(account);
    }

    internal void RemoveAccount(Guid accountId)
    {
        var account = accounts.FirstOrDefault(x => x.Id == accountId);
        if (account != null)
        {
            accounts.Remove(account);
        }
    }

    internal List<AccountBase> GetAccounts()
    {
        return accounts;
    }

    internal void ShowBankMenu()
    {
        var selectedOption = bankMenu.ShowMenu();
        switch (selectedOption)
        {
            case 0:

                break;
            case 1:
                Console.WriteLine("Remove account selected");
                break;
            case 2:
                Console.WriteLine("Show accounts selected");
                break;
            case 3:
                Console.WriteLine("Exit selected");
                return;
            default:
                break;
        }
    }

    private AccountBase CreateAccount(int selectedoption)
    {
        while (true)
        {

        }

        Console.Clear();
        Console.Write("Hur mycket ska kontot startas med?: ");
        _ = decimal.TryParse(Console.ReadLine(), out decimal initialAmount);

        Console.Write("Vilken ränta ska kontot ha?: ");
        _ = decimal.TryParse(Console.ReadLine(), out decimal initialInterest);

        var bankAccountName = Console.ReadLine();

        account.AccountName = bankAccountName;
        account.InterestRate = initialInterest;



        if (decimal.TryParse(Console.ReadLine(), out decimal initialInterest))
    }
