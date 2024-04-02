namespace ReserveSpot
{
    public class Review : AbstractEntity
    {
        public string PropertyID { get; set; }
        public string UserID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        
        public Review(int rating, string comment, string userId, string propertyId)
        {
            throw new NotImplementedException();        
        }
    }
}
