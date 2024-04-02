namespace ReserveSpot
{
    public class ReviewService
    {
        private readonly IDao<Review> reviewDao;

        public ReviewService(IDao<Review> dao) {
            reviewDao = dao;
        }

        public Review Create(CreateReviewDto payload)
        {
            throw new NotImplementedException();
        }

        public Review Update(UpdateReviewDto payload)
        {
            throw new NotImplementedException();

        }

        public Review Find(string reviewId)
        {
            throw new NotImplementedException();
        }

        public List<Review> FindAll(FindAllReviewsDto payload)
        {
            throw new NotImplementedException();
        }

        public bool Delete(DeleteReviewDto payload)
        {
            throw new NotImplementedException();
        }
    }
}
