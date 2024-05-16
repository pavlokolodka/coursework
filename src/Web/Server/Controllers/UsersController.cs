using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveSpot.Domain;
using System.Security.Claims;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly BookingService _bookingService;
        private readonly PropertyService _propertyService;
        public UsersController(UserService userService, BookingService bookingService, PropertyService propertyService) {
            _userService = userService;
            _bookingService = bookingService;
            _propertyService = propertyService;
        }
   
        [HttpGet]
        [Authorize]
        public ActionResult<UserDto> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);

            if (isAdmin == false) { 
                return Forbid();
            }

            var users = _userService.Find(user => user.IsAdmin != true);
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<UserDto> Get(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);
                       
            if (isAdmin == false && !userId.Equals(id))
            {
                return Forbid();
            }

            var foundUser = _userService.FindOneById(id);

            if (foundUser == null)
            {
                return NotFound();
            }

            return Ok(foundUser);
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<UserDto> Me()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var user = _userService.FindOneById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public ActionResult<UserDto> UpdateUser(string id, [FromBody] UpdateUserDto payload)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;        
           
            if (!userId.Equals(id))
            {
                return new ObjectResult("Cannot update another user")
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }

            var updatedUser = _userService.Update(id, payload);          

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);

            if (isAdmin == false && !userId.Equals(id))
            {
                return Forbid();
            }

            var userBookings = _bookingService.FindAllUserBookings(id);

            if (userBookings.Count > 0) {
                return Conflict("Cannot delete user with active bookings");
            }

            _userService.Delete(id);
            _propertyService.ArchiveAllUserProperties(id);
           
            return NoContent();
        }
    }    
}
