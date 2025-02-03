using api.DTOs.Meal;

namespace api.DTOs.MealEntry;

public class UpdateMealEntryDTO
{
  public MealDTO? Meal { get; set; }
  public string? CreatedBy { get; set; }
}
