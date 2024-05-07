namespace Web.Shared.Dto
{
    public class CreateBookingModel
    {
        public Guid PropertyID { get; set; }
      
        [StartDateLessThanEndDate(ErrorMessage = "StartDate must be less than or equal to EndDate")]
        public DateTime StartDate { get; set; }

        [EndDateGreaterThanStartDate(ErrorMessage = "EndDate must be greater than or equal to StartDate")]
        public DateTime EndDate { get; set; }
    }
}
