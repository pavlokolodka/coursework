namespace ReserveSpot
{
    public class Property : AbstractEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }
        public string Location { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }
        public decimal PricePerHour { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }   
        public string UserID { get; set; }
        public List<Review>? PropertyReviews { get; set; }
        public List<Booking>? PropertyBooking { get; set; }

        public static int PropetryCount;
        public static decimal CountTotalPrice(decimal pricePerHour, int numberOfDays)
        {
            throw new NotImplementedException();
        }

        public Property(string name, string description, PropertyType type, string location, string contactPhone, string contactName, decimal pricePerHour, int capacity, DateTime startDate, DateTime endDate, string creatorID)
        {
            throw new NotImplementedException();
            
        }             

        public bool Edit(PropertyDetails updateDetail)
        {
            throw new NotImplementedException();
        }
    }

    public class PropertyDetails
    {
        public string? Name { get; set; }
        public string? ContactName { get; set; }
        public string? ContactPhone { get; set; }
        public string? Description { get; set; }
        public PropertyType? Type { get; set; }
        public string? Location { get; set; }
        public decimal? PricePerHour { get; set; }
        public int? Capacity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
