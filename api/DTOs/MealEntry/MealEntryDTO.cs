using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using api.DTOs.Meal;

namespace api.DTOs.MealEntry;

public class MealEntryDTO
{
  public string? MealId { get; set; }
  public string? CreatedBy { get; set; }
}
