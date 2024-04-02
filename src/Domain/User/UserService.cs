namespace ReserveSpot
{
    public class UserService
    {
        private readonly IDao<User> userDao;

        public delegate void PropertyCreatedEventHandler(User user);
        public event PropertyCreatedEventHandler UserCreated;

        public UserService(IDao<User> dao) {
            userDao = dao;
        }

        public User Create(CreateUserDto payload)
        {
            throw new NotImplementedException();
        }

        public User Update(UpdateUserDto payload)
        {
            throw new NotImplementedException();

        }

        public User Find(string userId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string userId)
        {
            throw new NotImplementedException();
        }
    }    
}
