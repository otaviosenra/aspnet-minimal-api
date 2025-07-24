using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces;

public interface ICheeseService
{
    List<Cheese> GetAllCheeses(int page = 1, int pageSize = 10);
    Cheese? GetCheeseById(int id);
    void CreateCheese(Cheese cheese);
    void UpdateCheese(Cheese cheese);
    void DeleteCheese(int id);
}
