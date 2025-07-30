using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain.Services;

public class LoginService : ILoginService
{
    private readonly DbContexto _context;
    private readonly IConfiguration _configuration;

    public LoginService(DbContexto context, IConfiguration configuration)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<bool> Login(LoginDTO loginDTO)
    {
        var qtd = await _context.Users.Where(u => u.Name == loginDTO.Username && u.Password == loginDTO.Password).CountAsync();
        return (qtd > 0);
    }

    public UsuarioLogado LogarUsuario(LoginDTO login)
    {
        var user = _context.Users.FirstOrDefault(u => u.Name == login.Username && u.Password == login.Password);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Role", user.Profile)
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new UsuarioLogado
        {
            Token = tokenHandler.WriteToken(token),
            Role = user.Profile,
            Nome = user.Name
        };
    }

}