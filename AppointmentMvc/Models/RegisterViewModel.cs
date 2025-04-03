using System.ComponentModel.DataAnnotations;

namespace AppointmentMvc.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ad Soyad")]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Posta")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        [Display(Name = "Şifre tekrar")]
        public string? ConfirmPassword { get; set; }
    }
}
