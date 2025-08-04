using System.ComponentModel.DataAnnotations;

namespace Insecure_Bank.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Kullanıcı adı")]
        public string Username { get; set; } = string.Empty;

        

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;
        [Required]
        [Compare("Password")]
        [Display(Name = "Şifre Doğrulama")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
