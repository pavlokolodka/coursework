using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata.Ecma335;

namespace ReserveSpot.Domain
{
    public class PropertyService
    {
        private readonly IDao<Property> propertyDao;

        public PropertyService(IDao<Property> dao) {
            propertyDao = dao;  
        }

        public Property Create(Guid userId, CreatePropertyDto payload)
        {
            Property newProperty = new Property(payload.Name, payload.Description, (PropertyType)payload.Type, payload.Location, payload.ContactPhone, payload.ContactName, payload.PricePerHour, payload.Capacity, (DateTime)payload.StartDate, (DateTime)payload.EndDate, payload.ImageUrl, userId); ;
            return propertyDao.Create(newProperty);
        }

        public Property Update(string userId, string propertyId, UpdatePropertyDto payload, bool isAdmin = false)
        {
            var property = propertyDao.FindOne(property => property.ID.ToString() == propertyId);
            
            if (property == null)
            {
                throw new InvalidOperationException("Property not found");
            }

            if (property.UserID.ToString() != userId && !isAdmin)
            {
                throw new AccessViolationException("Cannot edit another user property");
            }

            property.Edit(payload);

            return propertyDao.Update(property => property.ID.ToString() == propertyId, property);
        }

        public List<Property> FindAllByUserId(string userId)
        {
            return propertyDao.FindMany(property => property.UserID.ToString() == userId);
        }
        public Property? Find(string id)
        {
            return propertyDao.FindOne(property => property.ID.ToString() == id);
        }

        public List<Property> FindAll(FindAllPropertiesDto payload)
        {
            Predicate<Property> filter = property =>
            (payload.Name == null || property.Name.Contains(payload.Name, StringComparison.OrdinalIgnoreCase)) &&  
            (payload.Type == null || property.Type == payload.Type) &&          
            (payload.Location == null || property.Location.Contains(payload.Location)) &&  
            (payload.PricePerHour == null || property.PricePerHour <= payload.PricePerHour) && 
            (payload.Capacity == null || property.Capacity >= payload.Capacity) &&  
            (payload.StartDate == null || property.StartDate >= payload.StartDate) &&  
            (payload.EndDate == null || property.EndDate <= payload.EndDate) &&
            property.IsArchived == false; 

            return propertyDao.FindMany(filter);
        }

        public bool Delete(string userId, string id, bool isAdmin = false)
        {
            var property = propertyDao.FindOne(property => property.ID.ToString() == id);

            if (property == null)
            {
                throw new InvalidOperationException("Property not found");
            }

            if (property.UserID.ToString() != userId && !isAdmin)
            {
                throw new AccessViolationException("Cannot delete another user property");
            }
            

            propertyDao.Delete(property => property.ID.ToString() == id);
            return true;
        }
    }
}
