using BankAppNoMoney.Accounts;
using BankAppNoMoney.Base;
using BankAppNoMoney.Menu;

namespace BankAppNoMoney;

internal class Bank
{
    private List<AccountBase> accounts = new List<AccountBase>();
    private BankMenu bankMenu = new BankMenu("Bank Menu", ["Add account", "Remove account", "Show accounts", "Select account", "Exit"]);
    private AccountMenu accountMenu = new("Account Menu", ["Bank account", "ISK account", "Uddevalla account"]);

    internal Bank()
    {
#if DEBUG
        accounts = SeedDataService.GenerateAccounts();
#endif
    }

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
        while (true)
        {
            var selectedOption = bankMenu.ShowMenu();
            int choice;
            switch (selectedOption)
            {
                case 0:
                    choice = accountMenu.ShowMenu();
                    AddAccount(CreateAccount(choice));
                    break;
                case 1:
                    {
                        choice = new GenericAccountMenu("Delete account", GetAccountMenuStrings().ToArray()).ShowMenu();
                        var account = accounts.Skip(choice).First();
                        RemoveAccount(account.Id);
                        break;
                    }
                case 2:
                    DisplayService.ShowAsTable("..:: Show accounts ::..", accounts);
                    Console.WriteLine("Press a key to continue");
                    Console.ReadKey();
                    break;
                case 3:
                    {
                        choice = new GenericAccountMenu("Select account", GetAccountMenuStrings().ToArray()).ShowMenu();
                        var account = accounts.Skip(choice).First();
                        ShowAccountDetails(account.Id);
                        break;
                    }
                case 4:
                default:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void ShowAccountDetails(Guid id)
    {
        var account = accounts.First(a => a.Id == id);
        DisplayService.ShowAccountDetails(account);
        account.ShowMenu(true);
        Console.WriteLine("Press a key to continue");
        Console.ReadKey();
    }

    private List<string> GetAccountMenuStrings()
    {
        return accounts.Select(x => new string(x.AccountName + " " + x.AccountNumber)).ToList();
    }

    private AccountBase CreateAccount(int selectedoption)
    {
        Console.Clear();
        Console.Write("Namn på kontot: ");
        var accountName = Console.ReadLine();

        Console.Write("Kontonummer: ");
        var accountNumber = Console.ReadLine();

        Console.Write("Hur mycket ska kontot startas med?: ");
        var validatedAmount = decimal.TryParse(Console.ReadLine(), out decimal initialAmount);

        return selectedoption switch
        {
            0 => new BankAccount(accountName ?? $"Defaultname{accounts.Count + 1}", accountNumber ?? $"Acc{accounts.Count + 1}", validatedAmount ? initialAmount : 0m),
            1 => new IskAccount(accountName ?? $"Defaultname{accounts.Count + 1}", accountNumber ?? $"Acc{accounts.Count + 1}", validatedAmount ? initialAmount : 0m),
            2 => new UddevallaAccount(accountName ?? $"Defaultname{accounts.Count + 1}", accountNumber ?? $"Acc{accounts.Count + 1}", validatedAmount ? initialAmount : 0m),
            _ => throw new NotImplementedException(),
        };
    }
}
