using SelifyApi.Entities;

namespace SelifyApi.Interfaces;

public interface IFoodService
{
  Task<IEnumerable<Food>> GetAll();
    Task<Food> GetById(Guid id);
    Task Add(Food food);
    Task Update(Guid id, Food food);
    Task Delete(Guid id);
}