using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Application.Abstraction.Auth.Models
{
    public class RegisterDto
    {
        [Required]
        public required string DisplayName { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        [RegularExpression(
        "(?=.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$" ,
        ErrorMessage = "Password must have 1 uppercase, 1 lowercase, 1 number, 1 non-alphanumeric, and be between 6-10 characters.")]
        public required string Password { get; set; }


    }
}
