using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveSpot.Domain;
using ReserveSpot.Domain.Common;
using System.Data;
using System.Security.Claims;
using Web.Shared.Dto;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;
        private readonly PropertyService _propertyService;
        public BookingsController(BookingService bookingService, PropertyService propertyService)
        {
            _bookingService = bookingService;
            _propertyService = propertyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetAllProperties()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var bookings = _bookingService.FindAll(userId);
            return Ok(bookings);
        }    

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (Validator.StringToGuild(id) == null)
            {
                return BadRequest("Invalid Guid format");
            }

            var booking = _bookingService.Find(new FindOneBookingDto()
            {
                BookingID = id,
                UserID = userId
            });

            if (booking == null) return NotFound("Booking not found");
            
            return Ok(booking);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Property> CreateProperty([FromBody] CreateBookingModel dto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userGuid = Validator.StringToGuild(userId);
            if (userGuid == null)
            {
                return BadRequest("Invalid Guid format");
            }

            var bookedProperty = _propertyService.Find(dto.PropertyID.ToString());

            if (bookedProperty == null) {
                return NotFound("Property not found");
            }

            int totalBookedDays = bookedProperty.CountTotalDays();
            decimal totalPrice = Property.CountTotalPrice(bookedProperty.PricePerHour, totalBookedDays);

            var bookingDto = new CreateBookingDto()
            {
                UserID = (Guid)userGuid,
                PropertyID = dto.PropertyID,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalPrice = totalPrice
            };

            var booking = _bookingService.Create(bookingDto);

            string url = Url.Action("GetBooking", new { id = booking.ID });

            return Created(url, booking);
        }

       /* [HttpPatch("{id}")]
        [Authorize]
        public ActionResult<Property> UpdateProperty(string id, [FromBody] UpdatePropertyDto dto)
        {
            bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (Validator.StringToGuild(id) == null)
            {
                return BadRequest("Invalid Guid format");
            }

            try
            {
                var updatedProperty = _bookingService.Update(userId, id, dto, isAdmin);
                return Ok(updatedProperty);
            }
            catch (Exception ex)
            {
                if (ex is AccessViolationException)
                {
                    return Forbid();
                }

                if (ex is InvalidOperationException)
                {
                    return NotFound();
                }

                return new ObjectResult("Internal Server Error")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }*/

        [HttpDelete("{id}")]
        public ActionResult<Property> DeleteProperty(string id)
        {
            bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (Validator.StringToGuild(id) == null)
            {
                return BadRequest("Invalid Guid format");
            }

            try
            {
                bool isDeleted = _bookingService.Delete(new DeleteBookingDto()
                {
                    BookingID = id,
                    UserID = userId,
                    IsAdmin = isAdmin   
                });

                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex is AccessViolationException)
                {
                    return Forbid();
                }

                if (ex is InvalidOperationException)
                {
                    return NotFound("Booking not found");
                }

                if (ex is DataException)
                {
                    return Conflict("Cannot delete finished booking");
                }

                return new ObjectResult("Internal Server Error")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
