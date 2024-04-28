namespace ReserveSpot.Domain
{
     public class CreateReviewDto 
     {
        public string PropertyID { get; set; }
        public string UserID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
     }
}
