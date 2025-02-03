#pragma warning disable CS8602

using api.DTOs.Food;
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
      for (var i = 0; i < m.Foods.Count; i++)
      {
        m.Foods[i].Adapt<FoodDTO>();
      }
      
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
    var foodDTOs = new List<FoodDTO>();
    var foods = new List<Food>();
    foreach (var foodId in mealDTO.FoodIds)
    {
      var food = await _foodRepository.GetFood(foodId);
      foods.Add(food);
      foodDTOs.Add(food.Adapt<FoodDTO>());
    }

    var meal = mealDTO.Adapt<Meal>();
    meal.Foods = foods;

    var createdMeal = await _mealRepository.CreateMeal(meal);
    
    return CreatedAtAction(nameof(GetMeal), new { id = createdMeal.Id }, createdMeal.Adapt<MealDTO>());
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateMeal([FromRoute] string id, [FromBody] UpdateMealDTO mealDTO)
  {
    var foodDTOs = new List<FoodDTO>();
    var foods = new List<Food>();
    foreach (var foodId in mealDTO.FoodIds)
    {
      var food = await _foodRepository.GetFood(foodId);
      foods.Add(food);
      foodDTOs.Add(food.Adapt<FoodDTO>());
    }

    var meal = mealDTO.Adapt<Meal>();
    meal.Id = id;
    meal.Foods = foods;

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
