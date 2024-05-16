using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dto
{
    public class AddPropertyModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public PropertyType? Type { get; set; } = null;

        [Required(ErrorMessage = "ImageUrl is required")]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png)", ErrorMessage = "Please enter a valid image URL.")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "ContactPhone is required")]
        [RegularExpression(@"^(\+?3?8)?(0\d{9})$", ErrorMessage = "Invalid Ukrainian phone number")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "ContactName is required")]
        public string ContactName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PricePerNight must be greater than 0")]
        public decimal PricePerNight { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "StartDate is required")]
        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than EndDate, and to be greater or equal to current date")]
        public DateTime? StartDate { get; set; } = null;

        [Required(ErrorMessage = "EndDate is required")]
        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than StartDate")]
        public DateTime? EndDate { get; set; } = null;
    }
}
