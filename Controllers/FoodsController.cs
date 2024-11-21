using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelifyApi.Entities;
using SelifyApi.Interfaces;
using SelifyApi.Requests;
using SelifyApi.Responses;

namespace SelifyApi.Controllers;

[Route("api/v1/foods")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FoodsController(IFoodService foodService) : ApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(UnauthorizedResult))]
    public async Task<ActionResult<SelifyResponse<IEnumerable<Food>>>> GetAllFoods()
    {
        var foods = await foodService.GetAllAsync();

        return Ok(new SelifyResponse<IEnumerable<Food>>(
            foods, "success", "All foods have been returned"
        ));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SelifyResponse<Food>>> GetFood(Guid id)
    {
        var food = await foodService.GetByIdAsync(id);

        if (food is null)
            return NotFound("Requested food not found!");

        return Ok(new SelifyResponse<Food>(
            food, "success", "Food details successfully fetched"
        ));
    }

    [HttpPost]
    public async Task<ActionResult> AddSingleFood([FromBody] CreateFoodRequest request)
    {
        var user = HttpContext.User;
        Food food = await foodService.AddAsync(request, user);

        return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Food>> UpdateSingleFood(Guid id, [FromBody] UpdateFoodRequest request)
    {
        try
        {
            await foodService.UpdateAsync(id, request);
            var food = await foodService.GetByIdAsync(id);
            return Ok(food);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteSingleFood(Guid id)
    {
        try
        {
            await foodService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return BadRequest("Food with the given ID is not found");
        }
    }
}