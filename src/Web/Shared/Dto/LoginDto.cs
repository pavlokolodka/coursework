using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dto
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The password must be at least 8 characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,100}$", ErrorMessage = "The password must contain at least one lowercase letter, one uppercase letter, and one number.")]
        public string Password { get; set; }
    }
}
