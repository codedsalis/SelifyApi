using System.Security.Claims;
using SelifyApi.Entities;
using SelifyApi.Requests;

namespace SelifyApi.Interfaces;

public interface IFoodService
{
  Task<IEnumerable<Food>> GetAllAsync();
    Task<Food?> GetByIdAsync(Guid id);
    Task<Food> AddAsync(CreateFoodRequest food, ClaimsPrincipal user);
    Task UpdateAsync(Guid id, UpdateFoodRequest request);
    Task DeleteAsync(Guid id);
}