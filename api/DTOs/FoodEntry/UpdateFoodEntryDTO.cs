using api.DTOs.Food;

namespace api.DTOs.FoodEntry;

public class UpdateFoodEntryDTO
{
  public FoodDTO? Food { get; set; }
  public int ServingSize { get; set; }
  public string? ServingSizeUnit { get; set; }
  public string? CreatedBy { get; set; }
}
