using System.ComponentModel.DataAnnotations;

namespace Insecure_Bank.Models.Model
{
    public class Account
    {
        public int AcId { get; set; }

        public int UserId { get; set; }

        [Required]
        public string AccountNumber { get; set; } = string.Empty;


        public decimal Balance { get; set; }

        [Required]
        public required string AccountType { get; set; } //Vadeli,Vadesiz


        // Navigation property
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Transaction> FromTransactions { get; set; } = new List<Transaction>();
        public virtual ICollection<Transaction> ToTransactions { get; set; } = new List<Transaction>();

    }
}

