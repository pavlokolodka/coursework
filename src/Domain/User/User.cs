namespace ReserveSpot
{
    public class User : AbstractEntity
    {
        private UserCode userCode;
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; }
        public bool IsVerified { get; set; }
        public List<Property>? UserProperties { get; set; }
        public List<Review>? UserReviews { get; set; }
        public List<Booking>? UserBookings { get; set; }
        public int UserCode { get => userCode.Code; set { userCode.Code = value; } }
        public User(string username, string password, string firstName, string lastName)
        {
            throw new NotImplementedException();           
        }

        public bool ComparePassword(string password) { 
            throw new NotImplementedException();
        }

        public void IsValidUserCode()
        {
            throw new NotImplementedException();
        }
        public void GenerateUserCode()
        {
            throw new NotImplementedException();
        }


        private string HashPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
