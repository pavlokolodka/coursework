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
        public ActionResult<IEnumerable<Booking>> GetAllUserBookings()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var bookings = _bookingService.FindAllUserBookings(userId);
            return Ok(bookings);
        }    

        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);
            if (Validator.StringToGuild(id) == null)
            {
                return BadRequest("Invalid Guid format");
            }

            try
            {
                var booking = _bookingService.Find(new FindOneBookingDto()
                {
                    BookingID = id,
                    UserID = userId,
                    IsAdmin = isAdmin
                });

                if (booking == null) return NotFound("Booking not found");

                return Ok(booking);
            }
            catch (Exception ex)
            {
                if (ex is AccessViolationException)
                {
                    return Forbid();
                }
              
                return new ObjectResult("Internal Server Error")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Booking> CreateBooking([FromBody] CreateBookingModel dto)
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

            if (bookedProperty.IsArchived == true)
            {
                return Conflict("Cannot book an archived property");
            }

            var registeredBookings = _bookingService.FindAllPropertyBookings(dto.PropertyID.ToString());
            bool canBookProperty = bookedProperty.CanBookProperty(dto.StartDate, dto.EndDate, registeredBookings);

            if (!canBookProperty)
            {
                return Conflict("Cannot book a property for more than the availible time");
            }
            
            var bookingDto = new CreateBookingDto()
            {
                Name = bookedProperty.Name,
                UserID = (Guid)userGuid,
                PropertyID = dto.PropertyID,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                PricePerHour = bookedProperty.PricePerHour
            };

            try
            {
                var booking = _bookingService.Create(bookingDto);
                // save booked time to property as an array to validate user input based on that
               // _propertyService.Update(new UpdatePropertyDto() { });
                string url = Url.Action("GetBooking", new { id = booking.ID });

                return Created(url, booking);
            } catch (Exception ex)
            {
                return new ObjectResult("Internal Server Error")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }           
        }
        /*
                [HttpPatch("{id}")]
                public ActionResult<Property> UpdateBooking(string id, [FromBody] UpdateBookingModel dto)
                {
                    bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);
                    string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                    if (Validator.StringToGuild(id) == null)
                    {
                        return BadRequest("Invalid Guid format");
                    }

                    var reservation = _bookingService.Find(new FindOneBookingDto()
                    {
                        BookingID = dto.BookingID.ToString(),
                        UserID = userId,
                        IsAdmin = isAdmin,
                    });

                    if (reservation == null)
                    {
                        return NotFound("Property not found");
                    }

                    var bookedProperty = _propertyService.Find(reservation.PropertyID.ToString());

                    if (bookedProperty == null)
                    {
                        return NotFound("Property not found");
                    }

                    if (bookedProperty.IsArchived == true)
                    {
                        return Conflict("Cannot book an archived property");
                    }

                    bool canBookProperty = bookedProperty.CanBookProperty(dto.StartDate, dto.EndDate, );

                    if (!canBookProperty)
                    {
                        return Conflict("Cannot book a property for more than the availible time");
                    }

                    try
                    {
                        var updatedProperty = _bookingService.Update(new UpdateBookingDto()
                        {
                            UserID = userId,
                            BookingID = dto.BookingID,  
                            PricePerHour = bookedProperty.PricePerHour,
                            StartDate  = dto.StartDate,
                            EndDate = dto.EndDate,
                            IsAdmin = isAdmin   
                        });

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
                            return NotFound("Booking not found");
                        }

                        if (ex is DataException)
                        {
                            return Conflict("Cannot update finished booking");
                        }

                        return new ObjectResult("Internal Server Error")
                        {
                            StatusCode = StatusCodes.Status500InternalServerError
                        };
                    }
                }*/

        [HttpDelete("{id}")]
        public ActionResult<Booking> DeleteBooking(string id)
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
