using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("mealFoods")]
public class MealFood
{
  //[BsonId]
  //[BsonElement("_id")]
  //public object? Id { get; set; } = new { MealId = string.Empty, FoodId = string.Empty };
  [BsonElement("meal")]
  public Meal? Meal { get; set; }
  [BsonElement("food")]
  public Food? Food { get; set; }
}
