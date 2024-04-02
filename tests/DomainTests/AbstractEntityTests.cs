using ReserveSpot;

namespace DomainTests
{
    [TestClass]
    public class AbstractEntityTests
    {
        [TestMethod]
        public void Constructor_InitializesProperties()
        {
            AbstractEntity entity = new ConcreteEntity();

            Assert.IsNotNull(entity.ID);
            Assert.IsTrue(entity.CreatedAt <= DateTime.Now && entity.CreatedAt > DateTime.Now.AddSeconds(-1));
            Assert.IsTrue(entity.UpdatedAt <= DateTime.Now && entity.UpdatedAt > DateTime.Now.AddSeconds(-1));
        }

        [TestMethod]
        public void CompareTo_ReturnsZeroWhenEqual()
        {
            AbstractEntity entity1 = new ConcreteEntity();
            AbstractEntity entity2 = new ConcreteEntity();

            int result = entity1.CompareTo(entity2);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CompareTo_ReturnsPositiveWhenGreaterThan()
        {
            AbstractEntity entity1 = new ConcreteEntity();
            AbstractEntity entity2 = new ConcreteEntity();

            entity1.CreatedAt = DateTime.Now.AddMinutes(-1);
            int result = entity1.CompareTo(entity2);

            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void CompareTo_ReturnsNegativeWhenLessThan()
        {
            AbstractEntity entity1 = new ConcreteEntity();
            AbstractEntity entity2 = new ConcreteEntity();

            entity2.CreatedAt = DateTime.Now.AddMinutes(-1);
            int result = entity1.CompareTo(entity2);

            Assert.IsTrue(result < 0);
        }
    }

    public class ConcreteEntity : AbstractEntity
    {
    }
}
