namespace ReserveSpot
{
    public abstract class AbstractEntity : IComparable<AbstractEntity>
    {
        public string ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public AbstractEntity()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            GenerateId();
        }

        private void GenerateId()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(AbstractEntity? other)
        {
            throw new NotImplementedException();
        }
    }
}

