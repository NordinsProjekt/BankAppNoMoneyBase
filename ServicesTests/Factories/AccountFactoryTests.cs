using Entities.Accounts;
using Entities.Types;
using Services.Factories;
using Services.Models;

namespace ServicesTests.Factories;

public class AccountFactoryTests
{

    [Theory]
    [InlineData(typeof(BankAccount), AccountType.BankAccount)]
    [InlineData(typeof(IskAccount), AccountType.IskAccount)]
    [InlineData(typeof(UddevallaAccount), AccountType.UddevallaAccount)]
    [InlineData(typeof(MillionAccount), AccountType.MillionAccount)]
    [InlineData(typeof(GoldAccount), AccountType.GoldAccount)]
    public void AccountFactory_CreateAccount_ShouldReturnExpected(Type expectedAccount, AccountType accountType)
    {
        var accountDetails = new AccountDetails("TestAccount", "111", 1000m, accountType);

        var result = AccountFactory.CreateAccount(accountDetails);

        Assert.IsType(expectedAccount, result);
    }
}
