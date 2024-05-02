using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Web.Shared.Dto
{
	public class PropertyModel
	{
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Type is required")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PropertyType Type { get; set; }

		[Required(ErrorMessage = "Location is required")]
		public string Location { get; set; }

		[Required(ErrorMessage = "ContactPhone is required")]
		[RegularExpression(@"^(\+?3?8)?(0\d{9})$", ErrorMessage = "Invalid Ukrainian phone number")]
		public string ContactPhone { get; set; }

		[Required(ErrorMessage = "ContactName is required")]
		public string ContactName { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png)", ErrorMessage = "Please enter a valid image URL.")]
        public string ImageUrl { get; set; }

        public bool IsArchived { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PricePerHour must be greater than 0")]
		public decimal PricePerHour { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
		public int Capacity { get; set; }

		[StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
		public DateTime StartDate { get; set; }

		[EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
		public DateTime EndDate { get; set; }

		[Required(ErrorMessage = "UserID is required")]
		public Guid UserID { get; set; }

		public Guid ID { get; set; }	

		public static int PropetryCount;
		public static decimal CountTotalPrice(decimal pricePerHour, int numberOfDays)
		{
			return pricePerHour * 24 * numberOfDays;
		}
	}
    public enum PropertyType
    {
        Apartment,
        House,
        Hotel,
        Condo,
        Villa,
        Cottage
    }
}
