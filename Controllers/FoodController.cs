using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelifyApi.Data;
using SelifyApi.Dtos;
using SelifyApi.Entities;
using SelifyApi.Interfaces;
using SelifyApi.Services;

namespace SelifyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodController : ControllerBase
{
    private readonly IFoodService _foodService;

    public FoodController(IFoodService foodService)
    {
        _foodService = foodService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Food>>> GetAllFoods()
    {
        var foods = await _foodService.GetAll();

        return Ok(foods);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Food>> GetFood(Guid id)
    {
        var food = await _foodService.GetById(id);

        if (food is null)
            return  NotFound("Requested food not found!");

        return Ok(food);
    }

    [HttpPost]
    public async Task<ActionResult> AddSingleFood([FromBody] Food foodDto)
    {
        await _foodService.Add(foodDto);

        return CreatedAtAction(nameof(GetFood), new { id = foodDto.Id }, foodDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Food>> UpdateSingleFood(Guid id, [FromBody] Food request)
    {
        try {
            await _foodService.Update(id, request);
        } catch (KeyNotFoundException ex) {
            return BadRequest(ex.Message);
        }

        var food = await _foodService.GetById(id);

        return food;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSingleFood(Guid id)
    {
        try {
            await _foodService.Delete(id);
            return NoContent();
        } catch(KeyNotFoundException) {
            return BadRequest("Food with the given ID is not found");
        }
    }
}