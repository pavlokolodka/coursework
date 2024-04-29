using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveSpot.Domain;
using System.Diagnostics;
using System.Security.Claims;

namespace Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertyService _propertyService;
        public PropertiesController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Property>> GetAllProperties([FromQuery] FindAllPropertiesDto filter)
        {
            var properties = _propertyService.FindAll(filter);
            return Ok(properties);
        }

        /* [HttpGet]
         [Authorize]
         public ActionResult<Property> GetAllMyProperties()
         {
             return new string[] { "value1", "value2" };
         }


         [HttpGet("{id}")]
         public ActionResult<Property> GetOneProperty(int id)
         {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
             bool isAdmin = Convert.ToBoolean(HttpContext.Items["IsAdmin"]);
             return "value";
         }*/

        [HttpPost]
        [Authorize]
        public ActionResult<Property> CreateProperty([FromBody] CreatePropertyDto dto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string format = "D"; // D represents the format 00000000-0000-0000-0000-000000000000
            if (!Guid.TryParseExact(userId, format, out Guid guid))
            {
                return BadRequest("Invalid Guid format");
            }

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("invalid...");
            }

            var property = _propertyService.Create(guid, dto);    

            return Ok(property);
        }

       /* [HttpPatch("{id}")]
        [Authorize]
        public ActionResult<Property> UpdateProperty(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Property> DeleteProperty(int id)
        {
        }*/
    }
}
