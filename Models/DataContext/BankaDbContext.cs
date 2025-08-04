using Insecure_Bank.Models.Model;
using Microsoft.EntityFrameworkCore;

namespace Insecure_Bank.Models.DataContext
{
    public class BankaDbContext : DbContext
    {
        public BankaDbContext(DbContextOptions<BankaDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Password).IsRequired().HasMaxLength(255);

                entity.Property(u => u.Email).HasMaxLength(100);
                entity.Property(u => u.Bio).HasMaxLength(1000);
                entity.HasIndex(u => u.Username).IsUnique();
            });

            // Account Configuration
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.AcId);
                entity.Property(a => a.AccountNumber).IsRequired().HasMaxLength(20);
                entity.Property(a => a.Balance).HasColumnType("decimal(18,2)");
                entity.Property(a => a.AccountType).HasMaxLength(20);
                entity.HasIndex(a => a.AccountNumber).IsUnique();

                entity.HasOne(a => a.User)
                      .WithMany(u => u.Accounts)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Transaction Configuration
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.TrId);
                entity.Property(t => t.Amount).HasColumnType("decimal(18,2)");
                entity.Property(t => t.Description).HasMaxLength(500);
                entity.Property(t => t.TransactionType).HasMaxLength(20);

                entity.HasOne(t => t.FromAccount)
                      .WithMany(a => a.FromTransactions)
                      .HasForeignKey(t => t.FromAccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.ToAccount)
                      .WithMany(a => a.ToTransactions)
                      .HasForeignKey(t => t.ToAccountId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.User)
                      .WithMany(u => u.Transactions)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


        }
    }
}
