using System.ComponentModel.DataAnnotations;

namespace Insecure_Bank.Models.Model
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public required string Username { get; set; } = string.Empty;//NullReferenceException hatalarının önüne geçmek için kullandım

        [Required]
        public required string Password { get; set; } = string.Empty;

        public required string Email { get; set; } // SQL'deki REAL tipi C#'da double veya decimal olabilir, string de bir seçenektir.
        //XSS zaafiyeti için bio ekledim.
        public string? Bio { get; set; }






        // Navigation property
        public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
        public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}

