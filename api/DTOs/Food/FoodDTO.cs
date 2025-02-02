using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Food;

public class FoodDTO
{
  public string? Name { get; set; }
  public string? Description { get; set; }
  public string? Brand { get; set; }
  public int ServingSize { get; set; }
  public string? ServingSizeUnit { get; set; }
  public List<KeyValuePair<string, float>>? NutritionalFacts { get; set; }
}
