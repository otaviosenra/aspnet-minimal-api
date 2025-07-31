
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi.Domain.DTOs;

namespace Test.Domain.Requests
{
    [TestClass]
    public class CheeseRequestTest
    {
        private readonly HttpClient _client;

        public CheeseRequestTest()
        {
            var factory = new WebApplicationFactory<Program>();
            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task UnauthorizedGetCheeses()
        {

            // Act
            var response = await _client.GetAsync("/api/cheese");

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized, "Expected Unauthorized status code but got: " + response.StatusCode);
        }

        [TestMethod]
        public async Task UnauthorizedGetCheeseByIdEqualsOne()
        {
            // Arrange
            int cheeseId = 1;

            // Act
            var response = await _client.GetAsync($"/api/cheese/{cheeseId}");

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized, "Expected Unauthorized status code but got: " + response.StatusCode);
        }


        [TestMethod]
        public async Task UnauthorizedCreateCheese()
        {
            // Arrange
            var cheeseDTO = new CheeseDTO()
            {
                Type = "CheddarExampleTest",
                Quantity = 10,
                Price = 5.99m
            };
            var content = new StringContent(JsonSerializer.Serialize(cheeseDTO), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/cheese", content);

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized, "Expected Unauthorized status code but got: " + response.StatusCode);
        }

        [TestMethod]
        public async Task UnauthorizedUpdateCheese()
        {
            // Arrange
            int cheeseId = 1;
            var cheeseDTO = new CheeseDTO()
            {
                Type = "UpdatedCheese",
                Quantity = 20,
                Price = 10.99m
            };
            var content = new StringContent(JsonSerializer.Serialize(cheeseDTO), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/cheese/{cheeseId}", content);

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Unauthorized, "Expected Unauthorized status code but got: " + response.StatusCode);
        }
    }

    
}