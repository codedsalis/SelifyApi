using Microsoft.EntityFrameworkCore;
using SelifyApi.Data;
using SelifyApi.Entities;
using SelifyApi.Interfaces;
using SelifyApi.Requests;

namespace SelifyApi.Services;

public class FoodService : IFoodService
{
    private DataContext _context;
    public FoodService(DataContext context)
    {
        _context = context;
    }

    public async Task<Food> Add(CreateFoodRequest request)
    {
        Food food = new Food{
            Name = request.Name,
            Price = request.Price,
        };

        _context.Foods.Add(food);
        await _context.SaveChangesAsync();

        return food;
    }

    public async Task Delete(Guid id)
    {
        var food = await _context.Foods.FindAsync(id);

        if (food is null) {
            throw new KeyNotFoundException($"Food with ID {id} not found");
        }

        _context.Remove(food);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Food>> GetAll()
    {
        return await _context.Foods.ToListAsync();
    }

    public async Task<Food?> GetById(Guid id)
    {
        return await _context.Foods.FindAsync(id) ?? null;
    }

    public async Task Update(Guid id, UpdateFoodRequest request)
    {
        var existingFood = await _context.Foods.FindAsync(id) ?? throw new KeyNotFoundException($"Food with ID {id} not found");

        existingFood.Name = request.Name;
        existingFood.Price = request.Price;
        existingFood.UpdatedAt = DateTime.Now;

         await _context.SaveChangesAsync();
    }
}
