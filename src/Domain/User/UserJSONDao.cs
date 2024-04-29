namespace ReserveSpot.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.IO;
    //using System.Text.Json;
    
    public class UserJSONDao : JSONDao<User>, IDao<User>
    {
        public UserJSONDao(): base(Path.Combine("..", "..", "..", "..", "data", "users.json"))
        {
        }

        public User Create(User entity)
        {
            ValidateEntity(entity);
            try
            {
                var users = LoadEntitites();
                users.Add(entity);
                SaveEntities(users);
                return entity;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }


        public bool Delete(Predicate<User> where)
        {
            try
            {
                var users = LoadEntitites();
                var deletedUsers = users.RemoveAll(where);
                SaveEntities(users);
                return deletedUsers > 0;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public List<User> FindMany(Predicate<User> where)
        {
            try
            {
                var users = LoadEntitites();
                return users.FindAll(where);
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        public User? FindOne(Predicate<User> where)
        {
            try
            {
                var users = LoadEntitites();
                return users.Find(where);
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public User Update(Predicate<User> where, User entity)
        {
            ValidateEntity(entity);
            try
            {
                var users = LoadEntitites();
                var index = users.FindIndex(where);
                if (index != -1)
                {
                    entity.UpdatedAt = DateTime.Now;
                    users[index] = entity;
                    SaveEntities(users);
                    return entity;
                }
                
                throw new Exception("Cannot update nonexistent User");
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }
    }

}
