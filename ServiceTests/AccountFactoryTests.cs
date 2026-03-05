using Entities.Accounts;
using Entities.Types;
using Services.Factories;
using Services.Models;

namespace ServiceTests;

public class AccountFactoryTests
{
    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnBankAccount()
    {
        //arrange
        var accountDetails = new AccountDetails
        {
            AccountName = "Test Account",
            AccountNumber = "11",
            AccountType = AccountType.BankAccount,
            StartingBalance = 1000m
        };

        //act
        var account = AccountFactory.CreateAccount(accountDetails);

        //assert
        Assert.IsType<BankAccount>(account);
    }

    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnIskAccount()
    {
        //arrange
        var accountDetails = new AccountDetails
        {
            AccountName = "Test Account",
            AccountNumber = "11",
            AccountType = AccountType.IskAccount,
            StartingBalance = 1000m
        };

        //act
        var account = AccountFactory.CreateAccount(accountDetails);

        //assert
        Assert.IsType<IskAccount>(account);
    }

    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnUddevallaAccount()
    {
        //arrange
        var accountDetails = new AccountDetails
        {
            AccountName = "Test Account",
            AccountNumber = "11",
            AccountType = AccountType.UddevallaAccount,
            StartingBalance = 1000m
        };

        //act
        var account = AccountFactory.CreateAccount(accountDetails);

        //assert
        Assert.IsType<UddevallaAccount>(account);
    }

    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnGoldAccount()
    {
        //arrange
        var accountDetails = new AccountDetails
        {
            AccountName = "Test Account",
            AccountNumber = "11",
            AccountType = AccountType.GoldAccount,
            StartingBalance = 1000m
        };

        //act
        var account = AccountFactory.CreateAccount(accountDetails);

        //assert
        Assert.IsType<GoldAccount>(account);
    }

    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnMillAccount()
    {
        //arrange
        var accountDetails = new AccountDetails
        {
            AccountName = "Test Account",
            AccountNumber = "11",
            AccountType = AccountType.MillionAccount,
            StartingBalance = 1000m
        };

        //act
        var account = AccountFactory.CreateAccount(accountDetails);

        //assert
        Assert.IsType<MillionAccount>(account);
    }
}
