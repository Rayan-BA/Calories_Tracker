using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Food;

public class CreateFoodDTO
{
  [Required(ErrorMessage = "Name required")]
  public string? Name { get; set; }
  [MaxLength(500, ErrorMessage = "Description too long")]
  public string? Description { get; set; }
  [MaxLength(50, ErrorMessage = "Brand too long")]
  public string? Brand { get; set; }
  [Required(ErrorMessage = "Serving size required")]
  [Range(0, int.MaxValue, ErrorMessage = "Serving size must be greater than 0")]
  public int ServingSize { get; set; }
  [Required(ErrorMessage = "Serving size unit required")]
  [MaxLength(10, ErrorMessage = "Serving size unit too long")]
  public string? ServingSizeUnit { get; set; }
  public List<KeyValuePair<string, float>>? NutritionalFacts { get; set; }
}
