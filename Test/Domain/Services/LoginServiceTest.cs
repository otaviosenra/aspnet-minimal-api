

using MinimalApi.Domain.Services;
using MinimalApi.Infra.Db;
using Microsoft.Extensions.Configuration;
using MinimalApi.Domain.DTOs;

namespace Test.Domain.Services;

[TestClass]
public class LoginServiceTest
{
    private DbContexto CriarContexto()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }

    private IConfigurationRoot GetConfiguration()
    {
        return new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
       
    }


    [TestMethod]
    public void TestarLoginComSucesso()
    {
        // Arrange
        var context = CriarContexto();
        IConfigurationRoot configuration = GetConfiguration();

        var loginService = new LoginService(context, configuration);

        var login = new LoginDTO()
        {
            Username = "admin",
            Password = "123"
        };

        // Act  
        UsuarioLogado usuarioLogado = loginService.LogarUsuario(login);

        // Assert
        Assert.IsNotNull(usuarioLogado);
        Assert.IsFalse(string.IsNullOrWhiteSpace(usuarioLogado.Token)); 
        Assert.AreEqual("admin", usuarioLogado.Nome); 
    }

}
