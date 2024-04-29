using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Newtonsoft.Json;

namespace ReserveSpot.Domain
{
    public class User : AbstractEntity
    {
        [JsonProperty(PropertyName = "_userCode")]
        private UserCode userCode;

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "IsAdmin is required")]
        [JsonProperty]
        public bool IsAdmin { get; private set; }

        [Required(ErrorMessage = "IsVerified is required")]
        public bool IsVerified { get; set; }

        //public List<Property>? UserProperties { get; set; }
        //public List<Review>? UserReviews { get; set; }
        //public List<Booking>? UserBookings { get; set; }
        public int? UserCode { get => userCode.Code; }
        public User(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;  
            LastName = lastName;
            userCode = new UserCode();  
        }

        public bool ComparePassword(string password) {
            return HashPassword(password).Equals(Password);
        }

        public bool IsValidUserCode()
        {
            return userCode.ValidateUserCode();
        }
        public int GenerateUserCode()
        {
            return userCode.GenerateUserCode(); 
        }

        public void HashPassword()
        {
            Password = HashPassword(Password);
        }
        private string HashPassword(string password)
        {
            string secret = Environment.GetEnvironmentVariable("SECRET_KEY");
           Console.WriteLine("SECRET");
           Console.WriteLine(secret);
            byte[] secretBytes = Encoding.UTF8.GetBytes(secret);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[secretBytes.Length + passwordBytes.Length];
            Buffer.BlockCopy(secretBytes, 0, combinedBytes, 0, secretBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, secretBytes.Length, passwordBytes.Length);

            byte[] hashedBytes = SHA256.HashData(combinedBytes);

            StringBuilder builder = new StringBuilder();
            foreach (byte b in hashedBytes)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
