using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelifyApi.Data;
using SelifyApi.Dtos;
using SelifyApi.Entities;
using SelifyApi.Interfaces;

namespace SelifyApi.Services;

public class FoodService : IFoodService
{
    private DataContext _context;
    public FoodService(DataContext context)
    {
        _context = context;
    }

    public async Task Add(Food foodDto)
    {
        _context.Add(foodDto);
        await _context.SaveChangesAsync();
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

    public async Task<Food> GetById(Guid id)
    {
        return await _context.Foods.FindAsync(id);
    }

    public async Task Update(Guid id, Food food)
    {
        var existingFood = await _context.Foods.FindAsync(id) ?? throw new KeyNotFoundException($"Food with ID {id} not found");

        existingFood.Name = food.Name;
        existingFood.Price = food.Price;
        existingFood.UpdatedAt = DateTime.Now;

         await _context.SaveChangesAsync();
    }
}
