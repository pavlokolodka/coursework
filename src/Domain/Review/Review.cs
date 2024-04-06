using System.ComponentModel.DataAnnotations;

namespace ReserveSpot
{
    public class Review : AbstractEntity
    {
        [Required(ErrorMessage = "PropertyID is required")]
        [RegularExpression("^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", ErrorMessage = "PropertyID must be a UUID")]
        public string PropertyID { get; set; }        
        [Required(ErrorMessage = "UserID is required")]
        [RegularExpression("^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$", ErrorMessage = "UserID must be a UUID")]
        public string UserID { get; set; }
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
        public int Rating { get; set; }
        [StringLength(300, ErrorMessage = "Comment must be a string with a maximum length of 300")] 
        public string Comment { get; set; }
        
        public Review(int rating, string comment, string userId, string propertyId)
        {
            Rating = rating;
            Comment = comment;
            UserID = userId;    
            PropertyID = propertyId;
        }

        public void Edit(int? rating, string? comment)
        {
            if (rating != null)
            {
                Rating = (int)rating;
            }

            if (comment != null)
            {
                Comment = comment;
            }
        }
    }
}
