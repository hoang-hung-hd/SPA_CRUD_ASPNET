using System.ComponentModel.DataAnnotations;

namespace useCookieAuth.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(20), MinLength(5, ErrorMessage ="Password at least 5 chararacters")]
        public string Password { get; set; }

    }
}
