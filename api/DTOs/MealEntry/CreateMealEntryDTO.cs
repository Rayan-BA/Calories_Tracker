using api.DTOs.Meal;

namespace api.DTOs.MealEntry;

public class CreateMealEntryDTO
{
  public MealDTO? Meal { get; set; }
  public string? CreatedBy { get; set; }
}
