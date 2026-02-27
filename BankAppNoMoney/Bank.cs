using BankAppNoMoney.Base;
using BankAppNoMoney.Factories;
using BankAppNoMoney.Menu;
using BankAppNoMoney.Models;
using BankAppNoMoney.Types;

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
        var account = accounts.Skip(2).FirstOrDefault();
        if (account.Id is Guid ac)
        {
            ac.ToByteArray();



        }

        var hej = "2";
        hej.TryParse(out Guid hejGuid);
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
                    CreateAccount(choice);
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

    private void CreateAccount(int selectedoption)
    {
        Console.Clear();
        Console.Write("Namn på kontot: ");
        var accountName = Console.ReadLine();

        Console.Write("Kontonummer: ");
        var accountNumber = Console.ReadLine();

        Console.Write("Hur mycket ska kontot startas med?: ");
        var validatedAmount = decimal.TryParse(Console.ReadLine(),
            out decimal initialAmount);

        var accountDetails = new AccountDetails { AccountName = accountName, AccountNumber = accountNumber, StartingBalance = initialAmount, AccountType = (AccountType)selectedoption };

        var account = AccountFactory.CreateAccount(accountDetails);
        AddAccount(account);
    }
}

public static class Hej
{
    public static void TryParse(string input, out Guid result)
    {
        if (Guid.TryParse(input, out Guid parsedGuid))
        {
            result = parsedGuid;
        }
        else
        {
            result = Guid.Empty;
        }
    }
}
