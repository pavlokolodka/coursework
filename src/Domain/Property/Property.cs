using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
    public class Property : AbstractEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
     
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "ImageUrl is required")]
        [RegularExpression(@"(http(s?):)([/|.|\w|\s|-])*\.(?:jpg|jpeg|gif|png)", ErrorMessage = "Please enter a valid image URL.")]
        public string ImageUrl { get; set; }

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

        [Range(1, int.MaxValue, ErrorMessage = "PricePerHour must be greater than 0")]
        public decimal PricePerHour { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0")]
        public int Capacity { get; set; }

        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime StartDate { get; set; }

        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime EndDate { get; set; }

      /*  public DateTime BookedStartDate { get; set; }

        public DateTime BookedEndDate { get; set; }*/
                
        public bool IsArchived { get; set; } = false;

        [Required(ErrorMessage = "UserID is required")]
        public Guid UserID { get; set; }
               
        public static int PropetryCount;     

        public Property(string name, string description, PropertyType type, string location, string contactPhone, string contactName, decimal pricePerHour, int capacity, DateTime startDate, DateTime endDate, string imageUrl, Guid creatorID)
        {
            Name = name;
            Description = description;
            Type = type;
            Location = location;
            ContactPhone = contactPhone;
            ContactName = contactName;
            PricePerHour = pricePerHour;
            Capacity = capacity;
            StartDate = startDate;
            EndDate = endDate;
            ImageUrl = imageUrl;
            UserID = creatorID;      
        }             

        public void Edit(UpdatePropertyDto updateDetail)
        {
            Name = updateDetail.Name ?? Name;
            Description = updateDetail.Description ?? Description;
            Type = updateDetail.Type ?? Type;
            Location = updateDetail.Location ?? Location;
            ContactPhone = updateDetail.ContactPhone ?? ContactPhone;
            ContactName = updateDetail.ContactName ?? ContactName;
            PricePerHour = updateDetail.PricePerHour ?? PricePerHour;
            Capacity = updateDetail.Capacity ?? Capacity;
            ImageUrl = updateDetail.ImageUrl ?? ImageUrl;
            IsArchived = updateDetail.IsArchived ?? IsArchived;

            StartDate = updateDetail.StartDate ?? StartDate;
            EndDate = updateDetail.EndDate ?? EndDate;          
        }    

     /*   public bool CanBookProperty(DateTime? startDate, DateTime? endDate)
        {
            startDate = startDate ?? StartDate;  
            endDate = endDate ?? EndDate;
            
            if (startDate >= StartDate || endDate <= EndDate) return false;

            return true;
        }*/

        public bool CanBookProperty(DateTime desiredStartDate, DateTime desiredEndDate, List<Booking> bookings)
        {
            if (desiredStartDate < StartDate || desiredEndDate > EndDate) return false;

            if (bookings.Count == 0) return true;         
            
            foreach (Booking booking in bookings) {               
                
                // # = start/end of booking
                // - = free/booked time
                // | = availible time
                    
                if (
                    // |#--START--#---|
                    (desiredStartDate <= booking.EndDate && desiredStartDate >= booking.StartDate) ||
                    // |--#--END---#--|
                    (desiredEndDate >= booking.StartDate && desiredEndDate <= booking.EndDate) ||
                    // |START-#--#-END|
                    (desiredStartDate <= booking.StartDate && desiredEndDate >= booking.EndDate)

                    )
                {
                    return false; 
                }             
            }

            return true;     
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
