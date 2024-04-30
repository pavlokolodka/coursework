namespace ReserveSpot.Domain
{
    public class PropertyJSONDao : JSONDao<Property>, IDao<Property>
    {
        public PropertyJSONDao() : base(Path.Combine("..", "..", "..", "..", "data", "properties.json"))
        {
        }

        public Property Create(Property entity)
        {
            ValidateEntity(entity);
            try
            {
                var properties = LoadEntitites();
                properties.Add(entity);
                SaveEntities(properties);
                return entity;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public bool Delete(Predicate<Property> where)
        {
            try
            {
                var properties = LoadEntitites();
                var deletedProperties = properties.RemoveAll(where);
                SaveEntities(properties);
                return deletedProperties > 0;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public List<Property> FindMany(Predicate<Property> where)
        {
            try
            {
                var properties = LoadEntitites();
                return properties.FindAll(where);
            }
            catch (Exception ex)
            {
                return new List<Property>();
            }
        }

        public Property FindOne(Predicate<Property> where)
        {
            try
            {
                var properties = LoadEntitites();
                return properties.Find(where);
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public Property Update(Predicate<Property> where, Property entity)
        {
            ValidateEntity(entity);
            try
            {
                var properties = LoadEntitites();
                var index = properties.FindIndex(where);
                if (index != -1)
                {
                    entity.UpdatedAt = DateTime.Now;
                    properties[index] = entity;
                    SaveEntities(properties);
                    return entity;
                }

                throw new Exception("Cannot update nonexistent Property");
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }
    }
}
