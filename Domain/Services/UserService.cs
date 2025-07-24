using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain.Services;

public class UserService : IUserService
{
    private readonly DbContexto _context;

    public UserService(DbContexto context)
    {
        _context = context;
    }

    public async Task<bool> Login(LoginDTO loginDTO)
    {
        var qtd = await _context.Users.Where(u => u.Name == loginDTO.Username && u.Password == loginDTO.Password).CountAsync();
        return (qtd > 0);
    }

    


}