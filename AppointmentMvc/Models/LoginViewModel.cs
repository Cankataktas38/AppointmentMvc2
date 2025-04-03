using System.ComponentModel.DataAnnotations;

namespace AppointmentMvc.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Posta")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string? Password { get; set; }
    }
}
