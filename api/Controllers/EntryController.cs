using api.DTOs.FoodEntry;
using api.DTOs.MealEntry;
using api.Interfaces;
using api.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

  [HttpGet("{type}/{id}")]
  public async Task<IActionResult> GetEntry([FromRoute] string type, [FromRoute] string id)
  {
    if (type == "food")
    {
      var foodEntry = await _foodEntryRepository.GetFoodEntry(id);
      if (foodEntry == null)
        return NotFound("Entry not found");
      return Ok(foodEntry.Adapt<FoodEntryDTO>());
    }
    else if (type == "meal")
    {
      var mealEntry = await _mealEntryRepository.GetMealEntry(id);
      if (mealEntry == null)
        return NotFound("Entry not found");
      return Ok(mealEntry.Adapt<MealEntryDTO>());
    }
    return BadRequest("Invalid entry type");
  }

  [HttpPost("{type}")]
  public async Task<IActionResult> CreateEntry([FromRoute] string type, [FromBody] JsonElement entryDTO)
  {
    if (type == "food")
    {
      var foodEntryDTO = new CreateFoodEntryDTO
      {
        FoodId = entryDTO.GetProperty("foodId").GetString(),
        ServingSize = entryDTO.GetProperty("servingSize").GetInt32(),
        ServingSizeUnit = entryDTO.GetProperty("servingSizeUnit").GetString(),
        CreatedBy = entryDTO.GetProperty("createdBy").GetString(),
      };
      if (foodEntryDTO == null)
        return BadRequest("Invalid data");
      var foodEntry = await _foodEntryRepository.CreateFoodEntry(foodEntryDTO.Adapt<FoodEntry>());
      return CreatedAtAction(nameof(GetEntry), new { type = "food", id = foodEntry.Id }, foodEntryDTO);
    }
    else if (type == "meal")
    {
      var mealEntryDTO = new CreateMealEntryDTO
      {
        MealId = entryDTO.GetProperty("mealId").GetString(),
        CreatedBy = entryDTO.GetProperty("createdBy").GetString(),
      };
      if (mealEntryDTO == null)
        return BadRequest("Invalid data");
      var mealEntry = await _mealEntryRepository.CreateMealEntry(mealEntryDTO.Adapt<MealEntry>());
      return CreatedAtAction(nameof(GetEntry), new { type = "meal", id = mealEntry.Id }, mealEntryDTO);
    }
    return BadRequest("Invalid entry type");
  }

  [HttpPut("{type}/{id}")]
  public async Task<IActionResult> UpdateEntry([FromRoute] string type, [FromRoute] string id, [FromBody] JsonElement entryDTO)
  {
    if (type == "food")
    {
      var foodEntryDTO = new UpdateFoodEntryDTO
      {
        FoodId = entryDTO.GetProperty("foodId").GetString(),
        ServingSize = entryDTO.GetProperty("servingSize").GetInt32(),
        ServingSizeUnit = entryDTO.GetProperty("servingSizeUnit").GetString(),
        CreatedBy = entryDTO.GetProperty("createdBy").GetString(),
      };
      if (foodEntryDTO == null)
        return BadRequest("Invalid data");
      var foodEntry = foodEntryDTO.Adapt<FoodEntry>();
      foodEntry.Id = id;
      var updatedFoodEntry = await _foodEntryRepository.UpdateFoodEntry(id, foodEntry);
      if (updatedFoodEntry == null)
        return NotFound("Food entry not found");
      return Ok(updatedFoodEntry.Adapt<FoodEntryDTO>());
    }
    else if (type == "meal")
    {
      var mealEntryDTO = new UpdateMealEntryDTO
      {
        MealId = entryDTO.GetProperty("mealId").GetString(),
        CreatedBy = entryDTO.GetProperty("createdBy").GetString(),
      };
      if (mealEntryDTO == null)
        return BadRequest("Invalid data");
      var mealEntry = mealEntryDTO.Adapt<MealEntry>();
      mealEntry.Id = id;
      var updatedMealEntry = await _mealEntryRepository.UpdateMealEntry(id, mealEntry);
      if (updatedMealEntry == null)
        return NotFound("Meal entry not found");
      return Ok(updatedMealEntry.Adapt<MealEntryDTO>());
    }
    return BadRequest("Invalid entry type");
  }

  [HttpDelete("{type}/{id}")]
  public async Task<IActionResult> DeleteEntry([FromRoute] string type, [FromRoute] string id)
  {
    if (type == "food")
    {
      var deletedFoodEntry = await _foodEntryRepository.DeleteFoodEntry(id);
      if (deletedFoodEntry == null)
        return NotFound("Food entry not found");
      return Ok(deletedFoodEntry.Adapt<FoodEntryDTO>());
    }
    else if (type == "meal")
    {
      var deletedMealEntry = await _mealEntryRepository.DeleteMealEntry(id);
      if (deletedMealEntry == null)
        return NotFound("Meal entry not found");
      return Ok(deletedMealEntry.Adapt<MealEntryDTO>());
    }
    return BadRequest("Invalid entry type");
  }
}
