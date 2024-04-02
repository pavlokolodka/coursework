namespace ReserveSpot
{
    abstract public class Dao<Entity>
    {            
        abstract public Entity FindOne(string id);
        abstract public List<Entity> FindMany(string id);
        abstract public bool Delete(string id);
        abstract public Entity Update(Entity entity);
    }

    public interface IDao<Entity>
    {
        Entity FindOne(Predicate<Entity> where);
        List<Entity> FindMany(Predicate<Entity> where);             
        bool Delete(Predicate<Entity> where);
        Entity Update(Predicate<Entity> where, Entity entity);   
    }
    //  PropertyPredicate<Entity> filter, Dictionary<string, object> filterProperties

    public delegate bool PropertyPredicate<T>(T entityToCompare, Dictionary<string, object> properties);
}
