namespace ReserveSpot.Domain
{
    public class ReviewJSONDao : JSONDao<Review>, IDao<Review>
    {
        public ReviewJSONDao() : base(Path.Combine("..", "..", "..", "..", "data", "reviews.json"))
        {
        }

        public Review Create(Review entity)
        {
            ValidateEntity(entity);
            try
            {
                var reviews = LoadEntitites();
                reviews.Add(entity);
                SaveEntities(reviews);
                return entity;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public bool Delete(Predicate<Review> where)
        {
            try
            {
                var reviews = LoadEntitites();
                var deletedReviews = reviews.RemoveAll(where);
                SaveEntities(reviews);
                return deletedReviews > 0;
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public List<Review> FindMany(Predicate<Review> where)
        {
            try
            {
                var reviews = LoadEntitites();
                return reviews.FindAll(where);
            }
            catch (Exception ex)
            {
                return new List<Review>();
            }
        }

        public Review FindOne(Predicate<Review> where)
        {
            try
            {
                var reviews = LoadEntitites();
                return reviews.Find(where);
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        }

        public Review Update(Predicate<Review> where, Review entity)
        {
            ValidateEntity(entity);
            try
            {
                var reviews = LoadEntitites();
                var index = reviews.FindIndex(where);
                if (index != -1)
                {
                    entity.UpdatedAt = DateTime.Now;
                    reviews[index] = entity;
                    SaveEntities(reviews);
                    return entity;
                }

                throw new Exception("Cannot update nonexistent Review");
            }
            catch (Exception ex)
            {
                throw new DaoException(ex.Message);
            }
        } 
    }
}
