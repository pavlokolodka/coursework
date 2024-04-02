namespace ReserveSpot
{
    public class PropertyService
    {
        private readonly IDao<Property> propertyDao;

        public PropertyService(IDao<Property> dao) {
            propertyDao = dao;  
        }

        public Property Create(CreatePropertyDto payload)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool Delete(DeletePropertyDto payload)
        {
            throw new NotImplementedException();
        }
    }
}
