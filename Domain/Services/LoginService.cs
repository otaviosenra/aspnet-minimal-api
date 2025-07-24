using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain.Services;

public class LoginService : ILoginService
{
    private readonly DbContexto _context;

    public LoginService(DbContexto context)
    {
        _context = context;
    }

    public async Task<bool> Login(LoginDTO loginDTO)
    {
        var qtd = await _context.Users.Where(u => u.Name == loginDTO.Username && u.Password == loginDTO.Password).CountAsync();
        return (qtd > 0);
    }

    


}