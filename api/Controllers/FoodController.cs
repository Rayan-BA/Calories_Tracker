﻿using api.DTOs.Food;
using api.Interfaces;
using api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/foods")]
[ApiController]
[Authorize]
public class FoodController(IFoodRepository foodRepository) : ControllerBase
{
  private readonly IFoodRepository _foodRepository = foodRepository;

  [HttpGet]
  public async Task<IActionResult> GetFoods()
  {
    var foods = await _foodRepository.GetFoods();

    if (foods.Count == 0)
    {
      return Ok(Array.Empty<FoodDTO>());
    }

    // configure and use mapster with lists later
    var foodsDTO = new List<FoodDTO>();
    foreach(var f in foods)
    {
      foodsDTO.Add(f.Adapt<FoodDTO>());
    }

    return Ok(foodsDTO);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetFood([FromRoute] string id)
  {
    var food = await _foodRepository.GetFood(id);

    if (food == null)
      return NotFound("Food not found");

    return Ok(food.Adapt<FoodDTO>());
  }

  [HttpPost]
  public async Task<IActionResult> CreateFood([FromBody] CreateFoodDTO foodDto)
  {
    var food = await _foodRepository.CreateFood(foodDto.Adapt<Food>());

    return CreatedAtAction(nameof(GetFood), new { id = food.Id }, foodDto);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateFood([FromRoute] string id, [FromBody] FoodDTO foodDTO)
  {
    var food = foodDTO.Adapt<Food>();
    food.Id = id;
    var updatedFood = await _foodRepository.UpdateFood(id, food);

    if (updatedFood == null)
      return NotFound("Food not found");

    return Ok(foodDTO);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteFood([FromRoute] string id)
  {
    var deletedFood = await _foodRepository.DeleteFood(id);

    if (deletedFood == null)
      return NotFound("Food not found");

    return Ok(deletedFood.Adapt<FoodDTO>());
  }
}
