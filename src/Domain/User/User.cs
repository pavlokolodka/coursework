namespace ReserveSpot
{
    public class User : AbstractEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; }
        public List<Property>? UserProperties { get; set; }
        public List<Review>? UserReviews { get; set; }
        public List<Booking>? UserBookings { get; set; }
        public User(string username, string password, string firstName, string lastName)
        {
            throw new NotImplementedException();           
        }

        public bool ComparePassword(string password) { 
            throw new NotImplementedException();
        }

        private string HashPassword(string password)
        {
            throw new NotImplementedException();
        }
    }
}
