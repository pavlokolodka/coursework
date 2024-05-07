﻿using ReserveSpot;
using System.ComponentModel.DataAnnotations;

namespace ReserveSpot.Domain
{
    public class CreateBookingDto
    {
        public Guid PropertyID { get; set; }
        public Guid UserID { get; set; }

        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime StartDate { get; set; }

        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime EndDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PricePerHour must be greater than 0")]
        public decimal PricePerHour { get; set; }
    }
}
