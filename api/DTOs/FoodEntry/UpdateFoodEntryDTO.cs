﻿using api.DTOs.Food;

namespace api.DTOs.FoodEntry;

public class UpdateFoodEntryDTO
{
  public string? FoodId { get; set; }
  public int ServingSize { get; set; }
  public string? ServingSizeUnit { get; set; }
  public string? CreatedBy { get; set; }
}
