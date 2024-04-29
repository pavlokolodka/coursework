using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ReserveSpot.Domain {
     public class UpdateUserDto 
     {
        [StringLength(100, ErrorMessage = "The password must be at least 8 characters long.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,100}$", ErrorMessage = "The password must contain at least one lowercase letter, one uppercase letter, and one number.")]
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
