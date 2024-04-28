using ReserveSpot.Domain;
using System.Reflection;

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
            AbstractEntity entity = new ConcreteEntity();

            int result = entity.CompareTo(entity);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void CompareTo_ReturnsPositiveWhenGreaterThan()
        {
            AbstractEntity entity1 = new ConcreteEntity();
            AbstractEntity entity2 = new ConcreteEntity();

            PropertyInfo createdAtProperty = typeof(ConcreteEntity).GetProperty("CreatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (createdAtProperty != null && createdAtProperty.CanWrite)
            {
                createdAtProperty.SetValue(entity2, DateTime.Now.AddMinutes(1));
            }

            int result = entity1.CompareTo(entity2);

            Assert.IsTrue(result < 0);
        }

        [TestMethod]
        public void CompareTo_ReturnsNegativeWhenLessThan()
        {
            AbstractEntity entity1 = new ConcreteEntity();
            AbstractEntity entity2 = new ConcreteEntity();

            PropertyInfo createdAtProperty = typeof(ConcreteEntity).GetProperty("CreatedAt", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (createdAtProperty != null && createdAtProperty.CanWrite)
            {
                createdAtProperty.SetValue(entity2, DateTime.Now.AddMinutes(-1));
            }

              int result = entity1.CompareTo(entity2);

               Assert.IsTrue(result > 0);
        }
    }

    public class ConcreteEntity : AbstractEntity
    {
    }
}
