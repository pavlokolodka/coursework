using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ReserveSpot.Domain
{
    public class UserService
    {
        private readonly IDao<User> userDao;

        public delegate void PropertyCreatedEventHandler(User user);
        public event PropertyCreatedEventHandler UserCreated;

        public UserService(IDao<User> dao) {
            userDao = dao;
        }

        public UserDto Create(CreateUserDto payload)
        {
            var newUser = new User(payload.Email, payload.Password, payload.FirstName, payload.LastName);
            newUser.HashPassword();
            var user = userDao.Create(newUser);

            return new UserDto
            {
                ID = user.ID,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                IsVerified = user.IsVerified
            };
        }

        public UserDto Update(string userId, UpdateUserDto payload)
        {
            var user = userDao.FindOne(user => user.ID.ToString() == userId);

            if (user == null)
            {
                return null;
            }

            user.LastName = payload.LastName ?? user.LastName;
            user.FirstName = payload.FirstName ?? user.FirstName;
            user.Password = payload.Password ?? user.Password;

            user.HashPassword();
            var updatedUser = userDao.Update(user => user.ID.ToString() == userId, user);

            return new UserDto
            {
                ID = updatedUser.ID,
                CreatedAt = updatedUser.CreatedAt,
                UpdatedAt = updatedUser.UpdatedAt,
                Email = updatedUser.Email,
                FirstName = user.FirstName,
                LastName = updatedUser.LastName,
                IsAdmin = user.IsAdmin,
                IsVerified = updatedUser.IsVerified
            };

        }

        public IEnumerable<UserDto> Find(Predicate<User> where)
        {
            return userDao.FindMany(where).Select(u => new UserDto
            {
                ID = u.ID,
                CreatedAt = u.CreatedAt,    
                UpdatedAt = u.UpdatedAt,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsAdmin = u.IsAdmin, 
                IsVerified = u.IsVerified  
            }).ToList();
        }

        public UserDto? FindOneById(string userId)
        {
            var user = userDao.FindOne(user => user.ID.ToString() == userId);

            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                ID = user.ID,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                IsVerified = user.IsVerified
            };
        }

        public UserDto? FindOneByEmail(string email)
        {
            var user = userDao.FindOne(user => user.Email == email);
           
            if (user == null)
            {
                return null; 
            }

            return new UserDto
            {
                ID = user.ID,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                IsVerified = user.IsVerified
            };
        }

        public UserDto? FindUserAndComparePassword(string email, string password)
        {
            var user = userDao.FindOne(user => user.Email == email);

            if (user == null)
            {
                return null;
            }

            bool isValidPassword = user.ComparePassword(password);

            if (!isValidPassword)
            {
                return null;
            }

            return new UserDto
            {
                ID = user.ID,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin,
                IsVerified = user.IsVerified
            };
        }
        public bool VerifyUser(string userId)
        {
            var user = userDao.FindOne(user => user.ID.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            user.IsVerified = true;
            userDao.Update(user => user.ID.ToString() == userId, user);
            return true;
        }
        public bool Delete(string userId)
        {
            var user = userDao.FindOne(user => user.ID.ToString() == userId);

            if (user == null)
            {
                return false;
            }

            userDao.Delete(user => user.ID.ToString() == userId);       

            return true;
        }
    }    
}
