using Entities.Base;
using Entities.Menu;
using Entities.Types;
using Services;
using Services.Factories;
using Services.Models;

namespace BankAppNoMoney;

public class Bank
{
    private readonly BankService _bankService;
    private BankMenu bankMenu = new BankMenu("Bank Menu", ["Add account", "Remove account", "Show accounts", "Select account", "Exit"]);
    private AccountMenu accountMenu = new("Account Menu", Enum.GetNames(typeof(AccountType)));

    public Bank(BankService bankService)
    {
        _bankService = bankService;
    }

    public async Task AddAccount(AccountBase account)
    {
        await _bankService.AddAccountAsync(account);
    }

    public async Task RemoveAccount(Guid accountId)
    {
        await _bankService.DeleteAccountAsync(accountId);
    }

    private async Task<List<AccountBase>> GetAllAccounts()
    {
        return await _bankService.GetAccountsAsync();
    }

    public async Task ShowBankMenu()
    {
        while (true)
        {
            var selectedOption = bankMenu.ShowMenu();
            int choice;
            switch (selectedOption)
            {
                case 0:
                    choice = accountMenu.ShowMenu();
                    await CreateAccount(choice);
                    break;
                case 1:
                    {
                        choice = new GenericAccountMenu("Delete account", (await GetAccountMenuStrings()).ToArray()).ShowMenu();
                        var account = (await GetAllAccounts()).Skip(choice).First();
                        await RemoveAccount(account.Id);
                        break;
                    }
                case 2:
                    DisplayService.ShowAsTable("..:: Show accounts ::..", await _bankService.GetAccountsAsync());
                    Console.WriteLine("Press a key to continue");
                    Console.ReadKey();
                    break;
                case 3:
                    {
                        choice = new GenericAccountMenu("Select account", (await GetAccountMenuStrings()).ToArray()).ShowMenu();
                        var account = (await _bankService.GetAccountsAsync()).Skip(choice).First();
                        await ShowAccountDetails(account.Id);
                        break;
                    }
                case 4:
                default:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private async Task ShowAccountDetails(Guid id)
    {
        var account = await _bankService.GetAccountByIdAsync(id);
        if (account == null)
        {
            Console.WriteLine("Account not found");
        }
        else
        {
            DisplayService.ShowAccountDetails(account);
            account.ShowMenu(true);
        }

        Console.WriteLine("Press a key to continue");
        Console.ReadKey();
    }

    private async Task<List<string>> GetAccountMenuStrings()
    {
        return (await _bankService.GetAccountsAsync()).Select(x => new string(x.AccountName + " " + x.AccountNumber)).ToList();
    }

    private async Task CreateAccount(int selectedoption)
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
        await AddAccount(account);
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
