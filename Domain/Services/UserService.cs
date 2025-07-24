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

    public bool Login(LoginDTO loginDTO)
    {
        var qtd = _context.Users.Where(u => u.Name == loginDTO.Username && u.Password == loginDTO.Password).Count();
        return (qtd > 0);
    }

    


}