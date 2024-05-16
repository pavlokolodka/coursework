using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveSpot.Domain;
using ReserveSpot.Domain.Common;
using System.Security.Claims;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _propertyService;
        private readonly BookingService _bookingService;
        public PropertiesController(PropertyService propertyService, BookingService bookingService)
        {
            _propertyService = propertyService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Property>> GetAllProperties([FromQuery] FindAllPropertiesDto filter)
        {
            var properties = _propertyService.FindAll(filter);
            return Ok(properties);
        }

        [HttpGet("my")]
        [Authorize]
        public ActionResult<IEnumerable<Property>> GetAllMyProperties()
        {
           string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
         
            var properties = _propertyService.FindAllByUserId(userId);
            return Ok(properties);
        }        

        [HttpGet("{id}")]
        public ActionResult<Property> GetProperty(string id)
        {
            if (Validator.StringToGuild(id) == null)
            {
                return BadRequest("Invalid Guid format");
            }

            var property = _propertyService.Find(id);
            if (property == null) return NotFound("Property not found");
            return Ok(property);
        }

        [HttpGet("{id}/available-days")]
        public ActionResult<List<DateTime>> GetPropertyAvailableDays(string id)
        {
            if (Validator.StringToGuild(id) == null)
            {
                return BadRequest("Invalid Guid format");
            }

            var property = _propertyService.Find(id);
            if (property == null) return NotFound("Property not found");

            var bookings = _bookingService.FindAllPropertyBookings(id);
           
            HashSet<DateTime> bookedDates = new HashSet<DateTime>();
        
            foreach (var booking in bookings)
            {
                for (DateTime date = booking.StartDate.Date; date <= booking.EndDate.Date; date = date.AddDays(1))
                {
                    bookedDates.Add(date);
                }
            }

            List<DateTime> availableDates = new List<DateTime>();
            for (DateTime date = property.StartDate.Date; date <= property.EndDate.Date; date = date.AddDays(1))
            {
                if (!bookedDates.Contains(date))
                {
                    availableDates.Add(date);
                }
            }

            return Ok(availableDates);
        }


        [HttpPost]
        [Authorize]
        public ActionResult<Property> CreateProperty([FromBody] CreatePropertyDto dto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var guid = Validator.StringToGuild(userId);
            if (guid == null)
            {
                return BadRequest("Invalid Guid format");
            }

            var maybeSameProperty = _propertyService.FindAll(new FindAllPropertiesDto()
            {
                Name = dto.Name,
                Location = dto.Location,
            });

            if (maybeSameProperty.Any())
            {
                return Conflict("The same property has already been registered");
            }

            var property = _propertyService.Create((Guid)guid, dto);

            string url = Url.Action("GetProperty", new { id = property.ID });

            return Created(url, property);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public ActionResult<Property> UpdateProperty(string id, [FromBody] UpdatePropertyDto dto)
        {
            bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
         
            if (Validator.StringToGuild(id) == null)
            {
                return BadRequest("Invalid Guid format");
            }

            var maybeSameProperty = _propertyService.FindAll(new FindAllPropertiesDto()
            {
                Name = dto.Name,
                Location = dto.Location,
            });

            if (maybeSameProperty.Count > 1 || maybeSameProperty.Count == 1 && maybeSameProperty.First().ID.ToString() != id)
            {
                return Conflict("The same property has already been registered");
            }


            try
            {
                var updatedProperty = _propertyService.Update(userId, id, dto, isAdmin);
                return Ok(updatedProperty);
            } catch (Exception ex)
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
        }

        [HttpDelete("{id}")]
        [Authorize]
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
                bool isDeleted = _propertyService.Delete(userId, id, isAdmin);
                return NoContent();
            } catch(Exception ex)
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
            
        }
    }
}
