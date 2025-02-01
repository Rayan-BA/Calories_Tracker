using api.DTOs.Meal;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace api.Controllers;

[Route("api/meals")]
[ApiController]
//[Authorize] uncomment
public class MealController : ControllerBase
{
  private readonly IMealRepository _mealRepository;
  private readonly IMealFoodRepository _mealFoodRepository;
  private readonly IFoodRepository _foodRepository;

  public MealController(IMealRepository mealRepository, IMealFoodRepository mealFoodRepository, IFoodRepository foodRepository)
  {
    _mealRepository = mealRepository;
    _mealFoodRepository = mealFoodRepository;
    _foodRepository = foodRepository;
  }

  [HttpGet]
  public async Task<IActionResult> GetMeals()
  {
    var meals = await _mealRepository.GetMeals();
    return Ok(meals);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetMeal([FromRoute] string id)
  {
    // a bit confusing but, we are getting MealFood object here for full details
    var mealFood = await _mealFoodRepository.GetMealFood(id);
    
    if (mealFood == null)
      return NotFound("Meal not found");
    
    return Ok(mealFood);
  }

  [HttpPost]
  public async Task<IActionResult> CreateMeal([FromBody] CreateMealDTO mealDTO)
  {
    var meal = new Meal
    {
      Name = mealDTO.Name,
      Description = mealDTO.Description,
    };
    
    meal = await _mealRepository.CreateMeal(meal);

    foreach (var foodId in mealDTO.FoodIds)
    {
      var food = await _foodRepository.GetFood(foodId);
      
      var mealFood = new MealFood
      {
        Meal = meal,
        Food = food
      };
      
      await _mealFoodRepository.CreateMealFood(mealFood);
    }

    return CreatedAtAction(nameof(GetMeal), new { id = meal.Id }, mealDTO);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateMeal([FromRoute] string id, [FromBody] UpdateMealDTO mealDTO)
  {
    var meal = new Meal
    {
      Name = mealDTO.Name,
      Description = mealDTO.Description,
    };

    foreach (var foodId in mealDTO.FoodIds)
    {
      var food = await _foodRepository.GetFood(foodId);

      var mealFood = new MealFood
      {
        Meal = meal,
        Food = food
      };

      await _mealFoodRepository.UpdateMealFood(id, mealFood);
    }

    var updatedMeal = await _mealRepository.UpdateMeal(id, meal);
    
    if (updatedMeal == null)
      return NotFound("Meal not found");
    
    return Ok(updatedMeal);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteMeal([FromRoute] string id)
  {
    var deletedMeal = await _mealRepository.DeleteMeal(id);
    var deleteMealFood = await _mealFoodRepository.DeleteMealFood(id);
    
    if (deletedMeal == null)
      return NotFound("Meal not found");
    
    if (!deleteMealFood)
      return StatusCode(500, "Error occured while deleting MealFoods");

    return Ok(deletedMeal);
  }
}
