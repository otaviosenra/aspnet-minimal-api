using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
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

    

    public async Task<List<User>> GetAllUsers(int page = 1, int pageSize = 10){

        List<User> allUsers = await _context.Users.ToListAsync();
        return allUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
    public async Task<User?> GetUserById(int id){
        User? user = await _context.Users.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new KeyNotFoundException("User not found");
        }
        return user;
    }
    public async Task CreateUser(User user){

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateUser(User user){

        User? existingUser = await _context.Users.Where(c => c.Id == user.Id).FirstOrDefaultAsync();

        if (existingUser != null)
        {
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Profile = user.Profile;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("User not found");
        }
    }
    public async Task DeleteUser(int id){

        User? user = await _context.Users.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("User not found");
        }
    }
    


}