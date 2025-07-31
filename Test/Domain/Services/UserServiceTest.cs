

using MinimalApi.Domain.Services;
using MinimalApi.Domain.Enums;
using MinimalApi.Domain.Entities;
using MinimalApi.Infra.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Test.Domain.Services;

[TestClass]
public class UserServiceTest
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
    public async Task CriarUsuario()
    {
        // Arrange
        var context = CriarContexto();
        await context.Database.ExecuteSqlRawAsync("DELETE FROM Users WHERE Name = 'HoraciusTesteExample'");

        var userService = new UserService(context);

        var user = new User();
        user.Name = "HoraciusTesteExample";
        user.Email = "hor@cio.com.br";
        user.Password = "horacio123";
        user.Profile = ProfileType.USER.ToString();

        // Act  
        await userService.CreateUser(user);

        // Assert
        var userCreated = await context.Users.FirstOrDefaultAsync(x => x.Name == "HoraciusTesteExample");
        Assert.IsNotNull(userCreated);
        Assert.AreEqual("HoraciusTesteExample", userCreated.Name);
        Assert.AreEqual("hor@cio.com.br", userCreated.Email);
        Assert.AreEqual("horacio123", userCreated.Password);
        Assert.AreEqual(ProfileType.USER.ToString(), userCreated.Profile);
    }

    [TestMethod]
    public async Task BuscarUsuarioPorId()
    {
        // Arrange
        var context = CriarContexto();

        var userService = new UserService(context);

        var user = null as User;

        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Id = 1,
                Name = "HoraciusTesteExample",
                Email = "hor@cio.com.br",
                Password = "horacio123",
                Profile = ProfileType.USER.ToString()
            });
            await context.SaveChangesAsync();
        }

        // Act
        user = await userService.GetUserById(1);

        // Assert
        Assert.IsNotNull(user);
        Assert.AreEqual(1, user.Id);
    }

    [TestMethod]
    public async Task BuscarTodosOsUsuarios()
    {
        // Arrange
        var context = CriarContexto();
        var userService = new UserService(context);

        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Id = 1,
                Name = "HoraciusTesteExample",
                Email = "hor@cio.com.br",
                Password = "horacio123",
                Profile = ProfileType.USER.ToString()
            });
            await context.SaveChangesAsync();
        }


        // Act
        var users = await userService.GetAllUsers();

        // Assert
        Assert.IsNotNull(users);
        Assert.IsTrue(users.Count > 0);
    }

    [TestMethod]
    public async Task AtualizarUsuario()
    {
        // Arrange
        var context = CriarContexto();

        var userService = new UserService(context);
        if (!context.Users.Any())
        {
            context.Users.Add(new User
            {
                Id = 1,
                Name = "HoraciusTesteExample",
                Email = "hor@cio.com.br",
                Password = "horacio123",
                Profile = ProfileType.USER.ToString()
            });
            await context.SaveChangesAsync();
        }

        var emailTest = RandomString() + "greathor@cius.com.br";
        var user = new User
        {
            Id = 1,
            Name = "admin",
            Email = emailTest,
            Password = "123",
            Profile = ProfileType.ADMIN.ToString()
        };


        // Act
        await userService.UpdateUser(user);

        // Assert
        var userUpdated = await context.Users.FirstOrDefaultAsync(x => x.Id == 1);
        Assert.IsNotNull(userUpdated);
        Assert.AreEqual(emailTest, userUpdated.Email);
        Assert.AreEqual(ProfileType.ADMIN.ToString(), userUpdated.Profile);
    }

}
