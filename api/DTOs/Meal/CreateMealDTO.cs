using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Meal;

public class CreateMealDTO
{
  [Required(ErrorMessage = "Name required")]
  public string? Name { get; set; }
  public string? Description { get; set; }
  [MinLength(1, ErrorMessage = "At least one food required")]
  public List<string>? FoodIds { get; set; }
}
