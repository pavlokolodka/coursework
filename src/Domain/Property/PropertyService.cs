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
            Property newProperty = new Property(payload.Name, payload.Description, (PropertyType)payload.Type, payload.Location, payload.ContactPhone, payload.ContactName, payload.PricePerHour, payload.Capacity, (DateTime)payload.StartDate, (DateTime)payload.EndDate, userId);
            return propertyDao.Create(newProperty);
        }

        public Property Update(UpdatePropertyDto payload)
        {
            throw new NotImplementedException();

        }

        public Property Find()
        {
            throw new NotImplementedException();
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
            (payload.EndDate == null || property.EndDate <= payload.EndDate); 

            return propertyDao.FindMany(filter);
        }

        public bool Delete(DeletePropertyDto payload)
        {
            throw new NotImplementedException();
        }
    }
}
