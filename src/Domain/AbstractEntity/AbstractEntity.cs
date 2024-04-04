namespace ReserveSpot
{
    public abstract class AbstractEntity : IComparable<AbstractEntity>
    {
        public string ID { get; set; }
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
            Guid uuid = Guid.NewGuid();
            string uuidString = uuid.ToString();
            this.ID = uuidString;
        }

        public int CompareTo(AbstractEntity? other)
        {
            if (other == null) return 1;        
            return CreatedAt.CompareTo(other.CreatedAt);
        }
    }
}

