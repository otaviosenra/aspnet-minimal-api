using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces;

public interface IUserService
{
    
    Task<List<User>> GetAllUsers(int page = 1, int pageSize = 10);
    Task<User?> GetUserById(int id);
    Task CreateUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
}
