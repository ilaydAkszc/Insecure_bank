using System.ComponentModel.DataAnnotations;

namespace Insecure_Bank.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display (Name ="Usename")]
        public string Username { get; set; }= string.Empty; 
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty; //kullanıcının yönlendirileceği hedef sayfa URL'si





    }
}
