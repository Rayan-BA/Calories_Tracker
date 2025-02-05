using api.DTOs.FoodEntry;
using api.DTOs.MealEntry;
using api.Interfaces;
using api.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/entry")]
[ApiController]
public class EntryController(IFoodEntryRepository foodEntryRepository, IMealEntryRepository mealEntryRepository) : ControllerBase
{
  private readonly IMealEntryRepository _mealEntryRepository = mealEntryRepository;
  private readonly IFoodEntryRepository _foodEntryRepository = foodEntryRepository;

  [HttpGet]
  public async Task<IActionResult> GetEntries()
  {
    var foodEntries = await _foodEntryRepository.GetFoodEntries();
    var mealEntries = await _mealEntryRepository.GetMealEntries();

    if (foodEntries.Count == 0 && mealEntries.Count == 0)
      return Ok(Array.Empty<object>());

    var entriesDTO = new List<object>();

    foreach (var f in foodEntries)
    {
      entriesDTO.Add(f.Adapt<FoodEntryDTO>());
    }

    foreach (var m in mealEntries)
    {
      entriesDTO.Add(m.Adapt<MealEntryDTO>());
    }

    return Ok(entriesDTO);
  }

  [HttpGet("food/{id}")]
  public async Task<IActionResult> GetFoodEntry([FromRoute] string id)
  {
    var foodEntry = await _foodEntryRepository.GetFoodEntry(id);

    if (foodEntry == null)
      return NotFound("Entry not found");

    return Ok(foodEntry.Adapt<FoodEntryDTO>());
  }

  [HttpGet("meal/{id}")]
  public async Task<IActionResult> GetMealEntry([FromRoute] string id)
  {
    var mealEntry = await _mealEntryRepository.GetMealEntry(id);

    if (mealEntry == null)
      return NotFound("Entry not found");

    return Ok(mealEntry.Adapt<MealEntryDTO>());
  }

  [HttpPost("food")]
  public async Task<IActionResult> CreateFoodEntry([FromBody] CreateFoodEntryDTO foodEntryDTO)
  {
    var foodEntry = await _foodEntryRepository.CreateFoodEntry(foodEntryDTO.Adapt<FoodEntry>());
    return CreatedAtAction(nameof(GetFoodEntry), new { id = foodEntry.Id }, foodEntryDTO);
  }

  [HttpPost("meal")]
  public async Task<IActionResult> CreateMealEntry([FromBody] CreateMealEntryDTO mealEntryDTO)
  {
    var mealEntry = await _mealEntryRepository.CreateMealEntry(mealEntryDTO.Adapt<MealEntry>());
    return CreatedAtAction(nameof(GetMealEntry), new { id = mealEntry.Id }, mealEntryDTO);
  }

  [HttpPut("food/{id}")]
  public async Task<IActionResult> UpdateFoodEntry([FromRoute] string id, [FromBody] UpdateFoodEntryDTO foodEntryDTO)
  {
    var foodEntry = foodEntryDTO.Adapt<FoodEntry>();
    foodEntry.Id = id;
    var updatedFoodEntry = await _foodEntryRepository.UpdateFoodEntry(id, foodEntry);

    if (updatedFoodEntry == null)
      return NotFound("Food entry not found");

    return Ok(updatedFoodEntry.Adapt<FoodEntryDTO>());
  }

  [HttpPut("meal/{id}")]
  public async Task<IActionResult> UpdateMealEntry([FromRoute] string id, [FromBody] UpdateMealEntryDTO mealEntryDTO)
  {
    var mealEntry = mealEntryDTO.Adapt<MealEntry>();
    mealEntry.Id = id;
    var updatedMealEntry = await _mealEntryRepository.UpdateMealEntry(id, mealEntry);

    if (updatedMealEntry == null)
      return NotFound("Meal entry not found");

    return Ok(updatedMealEntry.Adapt<MealEntryDTO>());
  }

  [HttpDelete("food/{id}")]
  public async Task<IActionResult> DeleteFoodEntry([FromRoute] string id)
  {
    var deletedFoodEntry = await _foodEntryRepository.DeleteFoodEntry(id);
    
    if (deletedFoodEntry == null)
      return NotFound("Food entry not found");

    return Ok(deletedFoodEntry.Adapt<FoodEntryDTO>());
  }

  [HttpDelete("meal/{id}")]
  public async Task<IActionResult> DeleteMealEntry([FromRoute] string id)
  {
    var deletedMealEntry = await _mealEntryRepository.DeleteMealEntry(id);

    if (deletedMealEntry == null)
      return NotFound("Meal entry not found");

    return Ok(deletedMealEntry.Adapt<MealEntryDTO>());
  }

}
