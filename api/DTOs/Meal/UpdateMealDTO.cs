using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Meal;

public class UpdateMealDTO
{
  [Required(ErrorMessage = "Name required")]
  public string? Name { get; set; }
  [MaxLength(500, ErrorMessage = "Description too long")]
  public string? Description { get; set; }
  [MinLength(1, ErrorMessage = "At least one food required")]
  public List<string>? FoodIds { get; set; }
}
