namespace ReserveSpot.Domain
{
   public interface IDao<Entity>
    {
        Entity? FindOne(Predicate<Entity> where);
        List<Entity> FindMany(Predicate<Entity> where);             
        bool Delete(Predicate<Entity> where);
        Entity Update(Predicate<Entity> where, Entity entity);   
        Entity Create(Entity entity);   
    }
    //  PropertyPredicate<Entity> filter, Dictionary<string, object> filterProperties

    public delegate bool PropertyPredicate<T>(T entityToCompare, Dictionary<string, object> properties);
}
