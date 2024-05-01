using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Shared.Dto.Property
{
    public class UpdatePropertyModel
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public string? ContactName { get; set; } = null;

        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png)", ErrorMessage = "Please enter a valid image URL.")]
        public string? ImageUrl { get; set; } = null;

        public bool IsArchived { get; set; } = false;

        [RegularExpression(@"^(\+?3?8)?(0\d{9})$", ErrorMessage = "Invalid Ukrainian phone number")]
        public string? ContactPhone { get; set; } = null;
        public PropertyType? Type { get; set; } = null;
        public string? Location { get; set; } = null;
      
        [Range(1, int.MaxValue, ErrorMessage = "PricePerHour must be greater than 0")]
        public decimal? PricePerHour { get; set; } = null;
        
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int? Capacity { get; set; } = null;
        
        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime? StartDate { get; set; } = null;
        
        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime? EndDate { get; set; } = null;
    }
}
