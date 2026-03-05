using Entities.Accounts;
using Entities.Base;
using Entities.Security;
using Entities.Transactions;
using Microsoft.EntityFrameworkCore;

namespace EFCore;

public class BankContext : DbContext
{
    public BankContext() { }

    public BankContext(DbContextOptions<BankContext> options) : base(options) { }

    public DbSet<AccountBase> Accounts { get; set; }
    public DbSet<BankTransaction> BankTransactions { get; set; }
    public DbSet<SecurityTransactionBase> SecurityTransactions { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<MutualFund> MutualFunds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=BankAppNoMoney;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountBase>()
            .HasDiscriminator<string>("AccountType")
            .HasValue<BankAccount>(nameof(BankAccount))
            .HasValue<GoldAccount>(nameof(GoldAccount))
            .HasValue<MillionAccount>(nameof(MillionAccount))
            .HasValue<UddevallaAccount>(nameof(UddevallaAccount))
            .HasValue<IskAccount>(nameof(IskAccount));

        modelBuilder.Entity<AccountBase>()
            .Property<decimal>("StartingBalance");

        modelBuilder.Entity<AccountBase>()
            .HasMany<BankTransaction>("bankTransactions")
            .WithOne()
            .HasForeignKey("AccountId");

        modelBuilder.Entity<BankTransaction>()
            .Property<Guid>("Id");
        modelBuilder.Entity<BankTransaction>()
            .HasKey("Id");

        modelBuilder.Entity<SecurityTransactionBase>()
            .HasDiscriminator<string>("TransactionType")
            .HasValue<StockTransaction>(nameof(StockTransaction))
            .HasValue<MutualFundTransaction>(nameof(MutualFundTransaction));

        modelBuilder.Entity<IskAccount>()
            .HasMany<SecurityTransactionBase>("securityTransactions")
            .WithOne()
            .HasForeignKey("IskAccountId");

        modelBuilder.Entity<SecurityBase>()
            .Property<string>("Symbol");
        modelBuilder.Entity<SecurityBase>()
            .Property<string>("Name");

        base.OnModelCreating(modelBuilder);
    }
}
