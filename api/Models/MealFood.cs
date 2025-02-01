using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("mealFoods")]
public class MealFood
{
  [BsonId]
  [BsonElement("_id")]
  public object? Id { get; set; }
  [BsonElement("meal")]
  public Meal? Meal { get; set; }
  [BsonElement("food")]
  public Food? Food { get; set; }
}
