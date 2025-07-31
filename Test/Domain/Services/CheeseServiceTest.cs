

using MinimalApi.Domain.Services;
using MinimalApi.Domain.Enums;
using MinimalApi.Domain.Entities;
using MinimalApi.Infra.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Test.Domain.Services;

[TestClass]
public class CheeseServiceTest
{
    private string RandomString()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new string(stringChars);
    }
    private DbContexto CriarContexto()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }

    [TestMethod]
    public async Task CriarQueijo()
    {
        // Arrange
        var context = CriarContexto();
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Cheeses WHERE Type = 'QueijoTestExample'");

        var cheeseService = new CheeseService(context);

        var cheese = new Cheese();
        cheese.Type = "CheddarExampleTest";
        cheese.Quantity = 10;
        cheese.Price = 5.99m;

        // Act  
        await cheeseService.CreateCheese(cheese);

        // Assert
        var cheeseCreated = await context.Cheeses.FirstOrDefaultAsync(x => x.Type == "CheddarExampleTest");
        Assert.IsNotNull(cheeseCreated);
        Assert.AreEqual("CheddarExampleTest", cheeseCreated.Type);
        Assert.AreEqual(10, cheeseCreated.Quantity);
        Assert.AreEqual(5.99m, cheeseCreated.Price);
    }

    [TestMethod]
    public async Task BuscarQueijoPorId()
    {
        // Arrange
        var context = CriarContexto();

        var cheeseService = new CheeseService(context);

        var cheese = null as Cheese;

        if (!context.Cheeses.Any())
        {
            context.Cheeses.Add(new Cheese
            {
                Id = 1,
                Type = "CheddarExampleTest",
                Quantity = 10,
                Price = 5.99m
            });
            await context.SaveChangesAsync();
        }

        // Act
        cheese = await cheeseService.GetCheeseById(1);

        // Assert
        Assert.IsNotNull(cheese);
        Assert.AreEqual(1, cheese.Id);
    }

    [TestMethod]
    public async Task BuscarTodosOsQueijos()
    {
        // Arrange
        var context = CriarContexto();
        var cheeseService = new CheeseService(context);

        if (!context.Cheeses.Any())
        {
            context.Cheeses.Add(new Cheese
            {
                Id = 1,
                Type = "CheddarExampleTest",
                Quantity = 10,
                Price = 5.99m
            });
            await context.SaveChangesAsync();
        }


        // Act
        var cheeses = await cheeseService.GetAllCheeses();

        // Assert
        Assert.IsNotNull(cheeses);
        Assert.IsTrue(cheeses.Count > 0);
    }

    [TestMethod]
    public async Task AtualizarQueijo()
    {
        // Arrange
        var context = CriarContexto();

        var cheeseService = new CheeseService(context);
        if (!context.Cheeses.Any())
        {
            context.Cheeses.Add(new Cheese
            {
                Id = 1,
                Type = "CheddarExampleTest",
                Quantity = 10,
                Price = 5.99m
            });
            await context.SaveChangesAsync();
        }

        var typeTest = RandomString() + "CheddarExampleTest";
        var quantityTest = new Random().Next(1, 100);
        var priceTest = new Random().Next(1, 100) + 0.99m;

        var cheese = new Cheese
        {
            Id = 1,
            Type = typeTest,
            Quantity = quantityTest,
            Price = priceTest
        };


        // Act
        await cheeseService.UpdateCheese(cheese);

        // Assert
        var cheeseUpdated = await context.Cheeses.FirstOrDefaultAsync(x => x.Id == 1);
        Assert.IsNotNull(cheeseUpdated);
        Assert.AreEqual(typeTest, cheeseUpdated.Type);
        Assert.AreEqual(quantityTest, cheeseUpdated.Quantity);
        Assert.AreEqual(priceTest, cheeseUpdated.Price);
    }

}
