using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class FoodEntry
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  [BsonElement("food")]
  public Food? Food { get; set; }
  [BsonElement("serving_size")]
  public int ServingSize { get; set; }
  [BsonElement("serving_size_unit")]
  public string? ServingSizeUnit { get; set; }
  [BsonElement("user_id")]
  public string? UserId { get; set; }
  [BsonElement("created_at")]
  public DateTime CreatedAt { get; set; } = DateTime.Now;
}
