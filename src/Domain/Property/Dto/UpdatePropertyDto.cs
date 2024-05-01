using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
     public class UpdatePropertyDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ContactName { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png)", ErrorMessage = "Please enter a valid image URL.")]
        public string? ImageUrl { get; set; }
        public bool? IsArchived { get; set; }

        [RegularExpression(@"^(\+?3?8)?(0\d{9})$", ErrorMessage = "Invalid Ukrainian phone number")]
        public string? ContactPhone { get; set; }
        public PropertyType? Type { get; set; }
        public string? Location { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "PricePerHour must be greater than 0")]
        public decimal? PricePerHour { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int? Capacity { get; set; }
        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime? StartDate { get; set; }
        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime? EndDate { get; set; }      
    }
}
