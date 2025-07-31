

using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Enums;

namespace Test.Domain.Entities
{
    [TestClass]
    public class CheeseTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {
            // Arrange
            var cheese = new Cheese();
            // Act
            cheese.Id = 1;
            cheese.Type = "Cheddar";
            cheese.Quantity = 10;
            cheese.Price = 5.99m;

            // Assert
            Assert.AreEqual(1, cheese.Id);
            Assert.AreEqual("Cheddar", cheese.Type);
            Assert.AreEqual(10, cheese.Quantity);
            Assert.AreEqual(5.99m, cheese.Price);
        }
    }
}