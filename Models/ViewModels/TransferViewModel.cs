using System.ComponentModel.DataAnnotations;

namespace Insecure_Bank.Models.ViewModels
{
    public class TransferViewModel
    {
        [Required]
        [Display(Name = "From Account")]
        public int FromAccountId { get; set; }

        [Required]
        [Display(Name = "To Account")]
        public required string ToAccountNumber { get; set; }
        [Required]
        [Display(Name = "Miktar")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Miktar 0'dan büyük olmalıdır")]
        public decimal Amount { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; } = string.Empty;
    }
}
