

using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Enums;

namespace Test.Domain.Entities
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestarGetSetPropriedades()
        {
            // Arrange
            var user = new User();
            // Act
            user.Name = "Horacio";
            user.Email = "hor@cio.com.br";
            user.Password = "horacio123";
            user.Profile = ProfileType.USER.ToString(); //  "ADMIN", "USER"

            // Assert
            Assert.AreEqual("Horacio", user.Name);
            Assert.AreEqual("hor@cio.com.br", user.Email);
            Assert.AreEqual("horacio123", user.Password);
            Assert.AreEqual(ProfileType.USER.ToString(), user.Profile);
        }
    }
}