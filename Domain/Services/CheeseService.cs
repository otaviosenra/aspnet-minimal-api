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

   

    public List<Cheese> GetAllCheeses(int page = 1, int pageSize = 10)
    {

        var allCheeses = _context.Cheeses.ToList();

        return allCheeses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    public Cheese? GetCheeseById(int id)
    {
        Cheese? cheese = _context.Cheeses.Where(c => c.Id == id).FirstOrDefault();

        if (cheese == null)
        {
            throw new KeyNotFoundException("Cheese not found");
        }
        return cheese;
    }
    public void UpdateCheese(Cheese cheese)
    {
        Cheese? existingCheese = _context.Cheeses.Where(c => c.Id == cheese.Id).FirstOrDefault();

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
    public void DeleteCheese(int id)
    {
        Cheese? cheese = _context.Cheeses.Where(c => c.Id == id).FirstOrDefault();

        if (cheese != null)
        {
            _context.Cheeses.Remove(cheese);
            _context.SaveChanges();
        }
        else
        {
            throw new KeyNotFoundException("Cheese not found");
        }
    }

    public void CreateCheese(Cheese cheese)
    {
        _context.Cheeses.Add(cheese);
        _context.SaveChanges();
    }
}
