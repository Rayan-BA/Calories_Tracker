using api.DTOs.Meal;

namespace api.DTOs.MealEntry;

public class UpdateMealEntryDTO
{
  public string? MealId { get; set; }
  public string? CreatedBy { get; set; }
}
