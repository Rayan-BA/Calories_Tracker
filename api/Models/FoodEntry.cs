using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("food_entries")]
public class FoodEntry
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  [BsonElement("food_id"), BsonRepresentation(BsonType.ObjectId)]
  public string? FoodId { get; set; }
  [BsonElement("serving_size")]
  public int ServingSize { get; set; }
  [BsonElement("serving_size_unit")]
  public string? ServingSizeUnit { get; set; }
  [BsonElement("created_by"), BsonRepresentation(BsonType.ObjectId)]
  public string? CreatedBy { get; set; } // UserId
  [BsonElement("created_at")]
  public DateTime CreatedAt { get; set; } = DateTime.Now;
}
