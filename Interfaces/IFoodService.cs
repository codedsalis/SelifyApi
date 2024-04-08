using SelifyApi.Dtos;
using SelifyApi.Entities;
using SelifyApi.Requests;

namespace SelifyApi.Interfaces;

public interface IFoodService
{
  Task<IEnumerable<Food>> GetAll();
    Task<Food?> GetById(Guid id);
    Task<Food> Add(CreateFoodRequest food);
    Task Update(Guid id, UpdateFoodRequest request);
    Task Delete(Guid id);
}