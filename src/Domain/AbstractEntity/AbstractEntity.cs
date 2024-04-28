namespace ReserveSpot.Domain
{
    public abstract class AbstractEntity : IComparable<AbstractEntity>
    {
        public Guid ID { get; set; }
        public DateTime CreatedAt { get; internal set; }
        public DateTime UpdatedAt { get; set; }

        public AbstractEntity()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            GenerateId();
        }

        private void GenerateId()
        {
            Guid guid = Guid.NewGuid();
            ID = guid;
        }

        public int CompareTo(AbstractEntity? other)
        {
            if (other == null) return 1;        
            return CreatedAt.CompareTo(other.CreatedAt);
        }
    }
}

