﻿namespace ReserveSpot {
     public class UpdatePropertyDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public PropertyType? Type { get; set; }
        public string? Location { get; set; }
        public decimal? PricePerHour { get; set; }
        public int? Capacity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UserID { get; set; }
        public string PropertyID { get; set; }
    }
}
