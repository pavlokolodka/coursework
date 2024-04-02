namespace ReserveSpot {
     public class UpdateReviewDto 
     {
        public string ReviewID { get; set; }
        public string UserID { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
     }
}
