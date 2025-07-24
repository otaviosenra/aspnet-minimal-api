using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Infra.Db;

namespace MinimalApi.Domain.Services;

public class CheeseService : ICheeseService
{
    private readonly DbContexto _context;

    public CheeseService(DbContexto context)
    {
        _context = context;
    }


    public async Task<List<Cheese>> GetAllCheeses(int page = 1, int pageSize = 10)
    {

        var allCheeses = await _context.Cheeses.ToListAsync();

        return allCheeses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task<Cheese?> GetCheeseById(int id)
    {
        Cheese? cheese = await _context.Cheeses.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (cheese == null)
        {
            throw new KeyNotFoundException("Cheese not found");
        }
        return cheese;
    }
    public async Task UpdateCheese(Cheese cheese)
    {
        Cheese? existingCheese = await _context.Cheeses.Where(c => c.Id == cheese.Id).FirstOrDefaultAsync();

        if (existingCheese != null)
        {
            existingCheese.Type = cheese.Type;
            existingCheese.Quantity = cheese.Quantity;
            existingCheese.Price = cheese.Price;
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException("Cheese not found");
        }
    }
    public async Task DeleteCheese(int id)
    {
        Cheese? cheese = await _context.Cheeses.Where(c => c.Id == id).FirstOrDefaultAsync();

        if (cheese != null)
        {
            _context.Cheeses.Remove(cheese);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Cheese not found");
        }
    }

    public async Task CreateCheese(Cheese cheese)
    {
        await _context.Cheeses.AddAsync(cheese);
        await _context.SaveChangesAsync();
    }
}
