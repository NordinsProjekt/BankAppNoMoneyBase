using BankAppNoMoney.Accounts;

namespace BankTests.Bases;

public class AccountBaseTests
{
    [Theory]
    [InlineData(1000, 1, 2023, 10)] // 1000 @ 1% for a full year = 10
    [InlineData(0, 1, 2023, 0)]  // zero balance = no interest
    [InlineData(1000, 0, 2023, 0)]  // zero rate = no interest
    [InlineData(-500, 1, 2023, 0)]  // negative starting balance is clamped to 0
    [InlineData(10000, 1, 2025, 100)]
    public void AccountBase_CalculateInterestForTheYear_ShouldReturnExpected(
        decimal startingBalance, decimal interestRate, int year, decimal expected)
    {
        var account = new BankAccount("Test", "1234", startingBalance, interestRate);

        var result = account.CalculateInterestForTheYear(year);

        Assert.Equal(expected, Math.Round(result, 2));
    }

    [Fact]
    public void AccountBase_CalculateInterestForTheYear_InvalidYear_ShouldReturnMinusOne()
    {
        var account = new BankAccount("Test", "1234", 1000, 1);

        var result = account.CalculateInterestForTheYear(-1);

        Assert.Equal(-1, result);
    }

    [Theory]
    [InlineData(0, 1, 2025, 100)]
    public void AccountBase_CalculateInterestForTheYear_DepositYearBefore_ShouldReturnExpected(
        decimal startingBalance, decimal interestRate, int year, decimal expected)
    {
        var account = new BankAccount("Test", "1234", startingBalance, interestRate);
        account.Deposit(10000, DateTime.Parse("2024-04-30"));

        var result = account.CalculateInterestForTheYear(year);

        Assert.Equal(expected, Math.Round(result, 2));
    }

    [Theory]
    [InlineData(10000, 1, 2025, 150)]
    public void AccountBase_CalculateInterestForTheYear_DepositMiddleOfYear_ShouldReturnExpected(
    decimal startingBalance, decimal interestRate, int year, decimal expected)
    {
        var account = new BankAccount("Test", "1234", startingBalance, interestRate);
        account.Deposit(10000, DateTime.Parse($"{year}-01-01").AddDays(182));

        var result = account.CalculateInterestForTheYear(year);

        Assert.Equal(expected, Math.Round(result, 0));
    }
}
