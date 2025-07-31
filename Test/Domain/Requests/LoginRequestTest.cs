
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi.Domain.DTOs;

namespace Test.Domain.Requests
{
    [TestClass]
    public class LoginRequestTest
    {

        private readonly HttpClient _client;

        public LoginRequestTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task DeveAutenticarLoginComSucesso()
        {
            // Arrange
            var loginDTO = new LoginDTO()
            {
                Username = "admin",
                Password = "123"
            };
            var content = new StringContent(JsonSerializer.Serialize(loginDTO), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/login", content);

            // Assert
            Assert.IsFalse(response.StatusCode > HttpStatusCode.IMUsed || response.StatusCode < HttpStatusCode.OK, "Expected success status code but got: " + response.StatusCode);
        }
    }
}