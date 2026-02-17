using BankAppNoMoney.Accounts;
using BankAppNoMoney.Base;
using BankAppNoMoney.Transactions;

namespace BankAppNoMoney;

internal static class DisplayService
{
    internal static void ShowAsTable(string title, List<AccountBase> accounts)
    {
        Console.Clear();

        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts to display.");
            return;
        }

        // Calculate dynamic column widths
        int nameWidth = "Account Name".Length;
        int accountNumberWidth = "Account Number".Length;
        int balanceWidth = "Balance".Length;

        foreach (var account in accounts)
        {
            nameWidth = Math.Max(nameWidth, account.AccountName.Length);
            accountNumberWidth = Math.Max(accountNumberWidth, account.AccountNumber.Length);
            balanceWidth = Math.Max(balanceWidth, account.Balance().ToString("C2").Length);
        }

        // Add padding
        nameWidth += 2;
        accountNumberWidth += 2;
        balanceWidth += 2;

        // Create header
        string header = $"{("Account Name").PadRight(nameWidth)}{("Account Number").PadRight(accountNumberWidth)}{("Balance").PadLeft(balanceWidth)}";
        string separator = new string('-', nameWidth + accountNumberWidth + balanceWidth + 2);

        // Display table
        Console.WriteLine(separator);
        Console.WriteLine(header);
        Console.WriteLine(separator);

        // Display each account
        foreach (var account in accounts)
        {
            string name = account.AccountName;
            string accountNumber = account.AccountNumber;
            string balance = account.Balance().ToString("C2");

            Console.WriteLine($"{name.PadRight(nameWidth)}{accountNumber.PadRight(accountNumberWidth)}{balance.PadLeft(balanceWidth)}");
        }

        Console.WriteLine(separator);
    }

    internal static void ShowAccountDetails(AccountBase account)
    {
        Console.Clear();

        // Display basic account information
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine($"Account Name:     {account.AccountName}");
        Console.WriteLine($"Account Number:   {account.AccountNumber}");
        Console.WriteLine($"Interest Rate:    {account.InterestRate}%");
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine();

        // If it's an ISK account, show detailed breakdown
        if (account is IskAccount iskAccount)
        {
            ShowIskAccountDetails(iskAccount);
        }
        else
        {
            Console.WriteLine($"Total Balance:    {account.Balance():C2}");
        }

        Console.WriteLine();
    }

    private static void ShowIskAccountDetails(IskAccount iskAccount)
    {
        decimal cashBalance = iskAccount.GetCashBalance();
        decimal stockBalance = 0;
        decimal mutualFundBalance = 0;

        Console.WriteLine("BALANCE BREAKDOWN:");
        Console.WriteLine("───────────────────────────────────────────────────────");
        Console.WriteLine($"Cash Balance:     {cashBalance,15:C2}");
        Console.WriteLine();

        var securityTransactions = iskAccount.GetSecurityTransactions();

        if (securityTransactions != null && securityTransactions.Count > 0)
        {
            // Display Stocks
            var stockTransactions = securityTransactions.OfType<StockTransaction>().ToList();
            if (stockTransactions.Count > 0)
            {
                Console.WriteLine("STOCKS:");
                foreach (var stockTx in stockTransactions)
                {
                    decimal value = stockTx.GetCurrentValue(stockTx.PurchasePrice);
                    stockBalance += value;
                    Console.WriteLine($"    Quantity:       {stockTx.Quantity,10}");
                    Console.WriteLine($"    Purchase Price: {stockTx.PurchasePrice,10:C2}");
                    Console.WriteLine($"    Current Value:  {value,10:C2}");
                    Console.WriteLine();
                }
                Console.WriteLine($"Total Stocks:     {stockBalance,15:C2}");
                Console.WriteLine();
            }

            // Display Mutual Funds
            var mutualFundTransactions = securityTransactions.OfType<MutualFundTransaction>().ToList();
            if (mutualFundTransactions.Count > 0)
            {
                Console.WriteLine("MUTUAL FUNDS:");
                foreach (var fundTx in mutualFundTransactions)
                {
                    decimal value = fundTx.GetCurrentValue(fundTx.PurchasePrice);
                    mutualFundBalance += value;
                    Console.WriteLine($"    Quantity:       {fundTx.Quantity,10}");
                    Console.WriteLine($"    Purchase Price: {fundTx.PurchasePrice,10:C2}");
                    Console.WriteLine($"    Current Value:  {value,10:C2}");
                    Console.WriteLine();
                }
                Console.WriteLine($"Total Mutual Funds: {mutualFundBalance,13:C2}");
                Console.WriteLine();
            }
        }

        // Display total
        decimal totalBalance = iskAccount.Balance();
        Console.WriteLine("═══════════════════════════════════════════════════════");
        Console.WriteLine($"TOTAL BALANCE:    {totalBalance,15:C2}");
        Console.WriteLine("═══════════════════════════════════════════════════════");
    }
}
