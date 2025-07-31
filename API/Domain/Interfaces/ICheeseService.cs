using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces;

public interface ICheeseService
{
    Task<List<Cheese>> GetAllCheeses(int page = 1, int pageSize = 10);
    Task<Cheese?> GetCheeseById(int id);
    Task CreateCheese(Cheese cheese);
    Task UpdateCheese(Cheese cheese);
    Task DeleteCheese(int id);
}
