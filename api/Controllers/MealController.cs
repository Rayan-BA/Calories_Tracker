using api.DTOs.Meal;
using api.Interfaces;
using api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/meals")]
[ApiController]
[Authorize]
public class MealController(IMealRepository mealRepository, IFoodRepository foodRepository) : ControllerBase
{
  private readonly IMealRepository _mealRepository = mealRepository;
  private readonly IFoodRepository _foodRepository = foodRepository;

  [HttpGet]
  public async Task<IActionResult> GetMeals()
  {
    var meals = await _mealRepository.GetMeals();

    if (meals.Count == 0)
      return Ok(Array.Empty<MealDTO>());

    // configure and use mapster with lists later
    var mealsDTO = new List<MealDTO>();
    foreach (var m in meals)
    {
      mealsDTO.Add(m.Adapt<MealDTO>());
    }

    return Ok(mealsDTO);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetMeal([FromRoute] string id)
  {
    var meal = await _mealRepository.GetMeal(id);

    if (meal == null)
      return NotFound("Meal not found");

    return Ok(meal.Adapt<MealDTO>());
  }

  [HttpPost]
  public async Task<IActionResult> CreateMeal([FromBody] CreateMealDTO mealDTO)
  {
    var meal = mealDTO.Adapt<Meal>();

    var createdMeal = await _mealRepository.CreateMeal(meal);

    return CreatedAtAction(nameof(GetMeal), new { id = createdMeal.Id }, createdMeal.Adapt<MealDTO>());
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateMeal([FromRoute] string id, [FromBody] UpdateMealDTO mealDTO)
  {
    var meal = mealDTO.Adapt<Meal>();
    meal.Id = id;

    var updatedMeal = await _mealRepository.UpdateMeal(id, meal);

    if (updatedMeal == null)
      return NotFound("Meal not found");

    return Ok(updatedMeal.Adapt<MealDTO>());
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteMeal([FromRoute] string id)
  {
    var deletedMeal = await _mealRepository.DeleteMeal(id);

    if (deletedMeal == null)
      return NotFound("Meal not found");

    return Ok(deletedMeal.Adapt<MealDTO>());
  }
}
