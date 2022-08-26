using System.ComponentModel.DataAnnotations;

namespace useCookieAuth.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20), MinLength(5, ErrorMessage = "Password at least 5 chararacters")]
        public string Password { get; set; }
    }
}
