using System.Text;
using System.Security.Cryptography;

namespace ReserveSpot
{
    public class User : AbstractEntity
    {
        private UserCode userCode;
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; private set; }
        public bool IsVerified { get; set; }
        public List<Property>? UserProperties { get; set; }
        public List<Review>? UserReviews { get; set; }
        public List<Booking>? UserBookings { get; set; }
        public int? UserCode { get => userCode.Code; }
        public User(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;  
            LastName = lastName;            
        }

        public bool ComparePassword(string password) {
            return HashPassword(password).Equals(Password);
        }

        public bool IsValidUserCode()
        {
            userCode ??= new UserCode();
            return userCode.ValidateUserCode();
        }
        public int GenerateUserCode()
        {
            userCode ??= new UserCode(); 
          
            return userCode.GenerateUserCode(); 
        }

        public void HashPassword()
        {
            Password = HashPassword(Password);
        }
        private string HashPassword(string password)
        {
            string secret = Environment.GetEnvironmentVariable("SECRET_KEY");
           
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
