using BankAppNoMoney.Accounts;
using BankAppNoMoney.Base;

namespace BankAppNoMoney;

internal class Bank
{
    private List<AccountBase> accounts = new List<AccountBase>();
    private BankMenu bankMenu = new BankMenu("Bank Menu", ["Add account", "Remove account", "Show accounts", "Exit"]);
    private AccountMenu accountMenu = new("Account Menu", ["Bank account", "ISK account", "Uddevalla account"]);

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
                var choice = accountMenu.ShowMenu();
                AddAccount(CreateAccount(choice));
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
        Console.Clear();
        Console.Write("Namn på kontot: ");
        var accountName = Console.ReadLine();

        Console.Write("Kontonummer: ");
        var accountNumber = Console.ReadLine();

        Console.Write("Hur mycket ska kontot startas med?: ");
        _ = decimal.TryParse(Console.ReadLine(), out decimal initialAmount);

        Console.Write("Vilken ränta ska kontot ha?: ");
        _ = decimal.TryParse(Console.ReadLine(), out decimal initialInterest);

        var bankAccountName = Console.ReadLine();

        switch (selectedoption)
        {
            case 0:
                return new BankAccount(accountName ?? "", accountNumber ?? "", initialAmount, initialInterest);
            case 1:
                return new IskAccount(accountName ?? "", accountNumber ?? "", initialAmount, initialInterest);
            case 2:
                return new UddevallaAccount(accountName ?? "", accountNumber ?? "", initialAmount, initialInterest);
            default: throw new NotImplementedException();
        }
    }
}
