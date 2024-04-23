using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelifyApi.Entities;
using SelifyApi.Interfaces;
using SelifyApi.Requests;

namespace SelifyApi.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/v1/foods")]
public class FoodsController(IFoodService foodService) : ApiController
{
    private readonly IFoodService _foodService = foodService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetAllFoods()
    {
        var foods = await _foodService.GetAllAsync();

        return Ok(foods);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFood(Guid id)
    {
        var food = await _foodService.GetByIdAsync(id);

        if (food is null)
            return  NotFound("Requested food not found!");

        return Ok(food);
    }

    [HttpPost]
    public async Task<ActionResult> AddSingleFood([FromBody] CreateFoodRequest request)
    {
        var user = HttpContext.User;
        Food food = await _foodService.AddAsync(request, user);

        return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Food>> UpdateSingleFood(Guid id, [FromBody] UpdateFoodRequest request)
    {
        try {
            await _foodService.UpdateAsync(id, request);
            var food = await _foodService.GetByIdAsync(id);
            return Ok(food);
        } catch (KeyNotFoundException ex) {
            return BadRequest(ex.Message);
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSingleFood(Guid id)
    {
        try {
            await _foodService.DeleteAsync(id);
            return NoContent();
        } catch(KeyNotFoundException) {
            return BadRequest("Food with the given ID is not found");
        }
    }
}