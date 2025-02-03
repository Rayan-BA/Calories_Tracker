using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("meal_entries")]
public class MealEntry
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  [BsonElement("meal")]
  public Meal? Meal { get; set; }
  [BsonElement("created_by"), BsonRepresentation(BsonType.ObjectId)]
  public string? CreatedBy { get; set; } // UserId
  [BsonElement("created_at")]
  public DateTime CreatedAt { get; set; } = DateTime.Now;
}
