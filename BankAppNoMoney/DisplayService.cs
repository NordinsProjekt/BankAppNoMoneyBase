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
        Console.WriteLine($"Account Name:      {account.AccountName}");
        Console.WriteLine($"Account Number:    {account.AccountNumber}");
        Console.WriteLine($"Interest Rate:     {account.InterestRate}%");
        Console.WriteLine($"Interest for 2025: {account.CalculateInterestForTheYear(2025):C2}");
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
        decimal totalBalance = iskAccount.Balance();

        var securityTransactions = iskAccount.GetSecurityTransactions();
        var stockTransactions = securityTransactions?.OfType<StockTransaction>().ToList() ?? [];
        var mutualFundTransactions = securityTransactions?.OfType<MutualFundTransaction>().ToList() ?? [];

        // Pre-calculate values
        var stockValues = stockTransactions.Select(tx => tx.GetCurrentValue(tx.PurchasePrice)).ToList();
        var fundValues = mutualFundTransactions.Select(tx => tx.GetCurrentValue(tx.PurchasePrice)).ToList();

        decimal stockBalance = stockValues.Sum();
        decimal mutualFundBalance = fundValues.Sum();

        // Calculate dynamic label width
        var labels = new List<string> { "Cash Balance:", "TOTAL BALANCE:" };
        if (stockTransactions.Count > 0)
            labels.AddRange(["  Quantity:", "  Purchase Price:", "  Current Value:", "Total Stocks:"]);
        if (mutualFundTransactions.Count > 0)
            labels.AddRange(["  Quantity:", "  Purchase Price:", "  Current Value:", "Total Mutual Funds:"]);

        int labelWidth = labels.Max(l => l.Length) + 2;

        // Calculate dynamic value width
        var formattedValues = new List<string> { cashBalance.ToString("C2"), totalBalance.ToString("C2") };
        if (stockTransactions.Count > 0)
        {
            formattedValues.Add(stockBalance.ToString("C2"));
            formattedValues.AddRange(stockTransactions.Select(tx => tx.Quantity.ToString()));
            formattedValues.AddRange(stockTransactions.Select(tx => tx.PurchasePrice.ToString("C2")));
            formattedValues.AddRange(stockValues.Select(v => v.ToString("C2")));
        }
        if (mutualFundTransactions.Count > 0)
        {
            formattedValues.Add(mutualFundBalance.ToString("C2"));
            formattedValues.AddRange(mutualFundTransactions.Select(tx => tx.Quantity.ToString()));
            formattedValues.AddRange(mutualFundTransactions.Select(tx => tx.PurchasePrice.ToString("C2")));
            formattedValues.AddRange(fundValues.Select(v => v.ToString("C2")));
        }

        int valueWidth = formattedValues.Max(v => v.Length) + 2;

        string lightSeparator = new string('─', labelWidth + valueWidth);
        string heavySeparator = new string('═', labelWidth + valueWidth);

        Console.WriteLine("BALANCE BREAKDOWN:");
        Console.WriteLine(lightSeparator);
        Console.WriteLine($"{"Cash Balance:".PadRight(labelWidth)}{cashBalance.ToString("C2").PadLeft(valueWidth)}");
        Console.WriteLine();

        if (stockTransactions.Count > 0)
        {
            Console.WriteLine("STOCKS:");
            for (int i = 0; i < stockTransactions.Count; i++)
            {
                var stockTx = stockTransactions[i];
                Console.WriteLine($"{"  Quantity:".PadRight(labelWidth)}{stockTx.Quantity.ToString().PadLeft(valueWidth)}");
                Console.WriteLine($"{"  Purchase Price:".PadRight(labelWidth)}{stockTx.PurchasePrice.ToString("C2").PadLeft(valueWidth)}");
                Console.WriteLine($"{"  Current Value:".PadRight(labelWidth)}{stockValues[i].ToString("C2").PadLeft(valueWidth)}");
                Console.WriteLine();
            }
            Console.WriteLine($"{"Total Stocks:".PadRight(labelWidth)}{stockBalance.ToString("C2").PadLeft(valueWidth)}");
            Console.WriteLine();
        }

        if (mutualFundTransactions.Count > 0)
        {
            Console.WriteLine("MUTUAL FUNDS:");
            for (int i = 0; i < mutualFundTransactions.Count; i++)
            {
                var fundTx = mutualFundTransactions[i];
                Console.WriteLine($"{"  Quantity:".PadRight(labelWidth)}{fundTx.Quantity.ToString().PadLeft(valueWidth)}");
                Console.WriteLine($"{"  Purchase Price:".PadRight(labelWidth)}{fundTx.PurchasePrice.ToString("C2").PadLeft(valueWidth)}");
                Console.WriteLine($"{"  Current Value:".PadRight(labelWidth)}{fundValues[i].ToString("C2").PadLeft(valueWidth)}");
                Console.WriteLine();
            }
            Console.WriteLine($"{"Total Mutual Funds:".PadRight(labelWidth)}{mutualFundBalance.ToString("C2").PadLeft(valueWidth)}");
            Console.WriteLine();
        }

        Console.WriteLine(heavySeparator);
        Console.WriteLine($"{"TOTAL BALANCE:".PadRight(labelWidth)}{totalBalance.ToString("C2").PadLeft(valueWidth)}");
        Console.WriteLine(heavySeparator);
    }
}
