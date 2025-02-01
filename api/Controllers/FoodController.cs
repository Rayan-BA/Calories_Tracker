using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/foods")]
[ApiController]
//[Authorize] uncomment
public class FoodController : ControllerBase
{
  private readonly IFoodRepository _foodRepository;

  public FoodController(IFoodRepository foodRepository)
  {
    _foodRepository = foodRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetFoods()
  {
    var foods = await _foodRepository.GetFoods();

    return Ok(foods);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetFood([FromRoute] string id)
  {
    var food = await _foodRepository.GetFood(id);

    if (food == null)
      return NotFound("Food not found");

    return Ok(food);
  }

  [HttpPost]
  public async Task<IActionResult> CreateFood([FromBody] Food food)
  {
    await _foodRepository.CreateFood(food);

    return CreatedAtAction(nameof(GetFood), new { id = food.Id }, food);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateFood([FromRoute] string id, [FromBody] Food food)
  {
    var updatedFood = await _foodRepository.UpdateFood(id, food);

    if (updatedFood == null)
      return NotFound("Food not found");

    return Ok(updatedFood);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteFood([FromRoute] string id)
  {
    var deletedFood = await _foodRepository.DeleteFood(id);

    if (deletedFood == null)
      return NotFound("Food not found");

    return Ok(deletedFood);
  }
}
