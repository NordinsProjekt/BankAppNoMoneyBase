using BankAppNoMoney.Accounts;
using BankAppNoMoney.Base;
using BankAppNoMoney.Transactions;

namespace BankAppNoMoney;

internal static class SeedDataService
{
    internal static List<AccountBase> GenerateAccounts()
    {
        var accounts = new List<AccountBase>();

        accounts.AddRange(CreateBankAccounts());
        accounts.AddRange(CreateUddevallaAccounts());
        accounts.AddRange(CreateIskAccounts());

        return accounts;
    }

    private static List<BankAccount> CreateBankAccounts()
    {
        var bankAccount1 = new BankAccount("Checking Account", "1001-2345", 5000m);
        bankAccount1.Deposit(1500m);
        bankAccount1.Withdraw(300m);
        bankAccount1.Deposit(750m);
        bankAccount1.Withdraw(200m);

        var bankAccount2 = new BankAccount("Savings Account", "1001-6789", 10000m);
        bankAccount2.Deposit(2000m);
        bankAccount2.Deposit(1000m);
        bankAccount2.Withdraw(500m);

        return [bankAccount1, bankAccount2];
    }

    private static List<UddevallaAccount> CreateUddevallaAccounts()
    {
        var uddevallaAccount1 = new UddevallaAccount("Uddevalla Premium", "2001-1111", 15000m);
        uddevallaAccount1.Deposit(5000m);
        uddevallaAccount1.Withdraw(2000m);
        uddevallaAccount1.Deposit(1500m);
        uddevallaAccount1.Withdraw(800m);
        uddevallaAccount1.Deposit(3000m);

        var uddevallaAccount2 = new UddevallaAccount("Uddevalla Basic", "2001-2222", 8000m);
        uddevallaAccount2.Deposit(1200m);
        uddevallaAccount2.Withdraw(600m);
        uddevallaAccount2.Deposit(2500m);

        return [uddevallaAccount1, uddevallaAccount2];
    }

    private static List<IskAccount> CreateIskAccounts()
    {
        var iskAccount1 = CreateIskAccount1();
        var iskAccount2 = CreateIskAccount2();

        return [iskAccount1, iskAccount2];
    }

    private static IskAccount CreateIskAccount1()
    {
        var iskAccount = new IskAccount("ISK Investment Plus", "3001-1234", 25000m);

        AddStocksToIskAccount1(iskAccount);
        AddMutualFundsToIskAccount1(iskAccount);

        iskAccount.Deposit(3000m);
        iskAccount.Withdraw(1500m);

        return iskAccount;
    }

    private static void AddStocksToIskAccount1(IskAccount iskAccount)
    {
        var appleStockTransaction = new StockTransaction
        {
            Quantity = 50,
            PurchasePrice = 150m,
            PurchaseDate = DateTime.Now.AddMonths(-6)
        };
        iskAccount.AddSecurityTransaction(appleStockTransaction);

        var microsoftStockTransaction = new StockTransaction
        {
            Quantity = 30,
            PurchasePrice = 280m,
            PurchaseDate = DateTime.Now.AddMonths(-3)
        };
        iskAccount.AddSecurityTransaction(microsoftStockTransaction);
    }

    private static void AddMutualFundsToIskAccount1(IskAccount iskAccount)
    {
        var vanguardFundTransaction = new MutualFundTransaction
        {
            Quantity = 100,
            PurchasePrice = 95m,
            PurchaseDate = DateTime.Now.AddMonths(-8)
        };
        iskAccount.AddSecurityTransaction(vanguardFundTransaction);
    }

    private static IskAccount CreateIskAccount2()
    {
        var iskAccount = new IskAccount("ISK Growth Portfolio", "3001-5678", 30000m);

        AddStocksToIskAccount2(iskAccount);
        AddMutualFundsToIskAccount2(iskAccount);

        iskAccount.Deposit(5000m);
        iskAccount.Withdraw(2000m);
        iskAccount.Deposit(1000m);

        return iskAccount;
    }

    private static void AddStocksToIskAccount2(IskAccount iskAccount)
    {
        var teslaStockTransaction = new StockTransaction
        {
            Quantity = 20,
            PurchasePrice = 220m,
            PurchaseDate = DateTime.Now.AddMonths(-4)
        };
        iskAccount.AddSecurityTransaction(teslaStockTransaction);

        var nvidiaStockTransaction = new StockTransaction
        {
            Quantity = 40,
            PurchasePrice = 450m,
            PurchaseDate = DateTime.Now.AddMonths(-2)
        };
        iskAccount.AddSecurityTransaction(nvidiaStockTransaction);
    }

    private static void AddMutualFundsToIskAccount2(IskAccount iskAccount)
    {
        var fidelityFundTransaction = new MutualFundTransaction
        {
            Quantity = 150,
            PurchasePrice = 120m,
            PurchaseDate = DateTime.Now.AddMonths(-10)
        };

        iskAccount.AddSecurityTransaction(fidelityFundTransaction);

        var schwabFundTransaction = new MutualFundTransaction
        {
            Quantity = 80,
            PurchasePrice = 55m,
            PurchaseDate = DateTime.Now.AddMonths(-5)
        };

        iskAccount.AddSecurityTransaction(schwabFundTransaction);
    }
}
