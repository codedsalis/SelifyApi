using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SelifyApi.Data;
using SelifyApi.Entities;
using SelifyApi.Interfaces;
using SelifyApi.Requests;

namespace SelifyApi.Services;

public class FoodService(
    DataContext context,
    UserManager<User> userManager,
    IUserService userService,
    ILogger<FoodService> logger) : IFoodService
{
    private readonly UserManager<User> _userManager = userManager;

    private readonly ILogger<FoodService> _logger = logger;

    public async Task<Food> AddAsync(CreateFoodRequest request, ClaimsPrincipal userClaim)
    {
        var user = await userService.GetUser(userClaim);

            Food food = new()
            {
                Name = request.Name,
                Price = request.Price,
                UserId = Guid.Parse(user!.Id.ToString()),
            };

            await context.Foods.AddAsync(food);
            await context.SaveChangesAsync();

            return food;
    }

    public async Task DeleteAsync(Guid id)
    {
        var food = await context.Foods.FindAsync(id);

        if (food is null) {
            throw new KeyNotFoundException($"Food with ID {id} not found");
        }

        context.Remove(food);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Food>> GetAllAsync()
    {
        return await context.Foods.Skip(8).Take(9).ToListAsync();
        // return new PaginatedList<T>
    }

    public async Task<Food?> GetByIdAsync(Guid id)
    {
        return await context.Foods.FindAsync(id) ?? null;
    }

    public async Task UpdateAsync(Guid id, UpdateFoodRequest request)
    {
        var existingFood = await context.Foods.FindAsync(id) ?? throw new KeyNotFoundException($"Food with ID {id} not found");

        existingFood.Name = request.Name;
        existingFood.Price = request.Price;
        existingFood.UpdatedAt = DateTime.UtcNow;

         await context.SaveChangesAsync();
    }
}
