using api.DTOs.Food;

namespace api.DTOs.Meal;

public class MealDTO
{
  public string? Name { get; set; }
  public string? Description { get; set; }
  public List<FoodDTO>? Foods { get; set; }
}
