using api.DTOs.Meal;

namespace api.DTOs.MealEntry;

public class CreateMealEntryDTO
{
  public string? MealId { get; set; }
  public string? CreatedBy { get; set; }
}
