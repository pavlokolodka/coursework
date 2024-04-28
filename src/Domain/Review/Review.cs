using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
    public class Review : AbstractEntity
    {
        [Required(ErrorMessage = "PropertyID is required")]
        public Guid PropertyID { get; set; }        
        [Required(ErrorMessage = "UserID is required")]
        public Guid UserID { get; set; }
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
        public int Rating { get; set; }
        [StringLength(300, ErrorMessage = "Comment must be a string with a maximum length of 300")] 
        public string Comment { get; set; }
        
        public Review(int rating, string comment, Guid userId, Guid propertyId)
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
