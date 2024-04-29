﻿using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
     public class FindAllPropertiesDto
    {
        public string? Name { get; set; } = null;
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
