namespace ReserveSpot
{
    public class UserJSONDao : IDao<User>
    {
        public bool Delete(Predicate<User> where)
        {
            throw new NotImplementedException();
        }     

        List<User> IDao<User>.FindMany(Predicate<User> where)
        {
            throw new NotImplementedException();
        }

        User IDao<User>.FindOne(Predicate<User> where)
        {
            throw new NotImplementedException();
        }

        User IDao<User>.Update(Predicate<User> where, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
