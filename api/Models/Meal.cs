using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("meals")]
public class Meal
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  [BsonElement("name")]
  public string? Name { get; set; }
  [BsonElement("description")]
  public string? Description { get; set; }
  [BsonElement("foods")]
  public List<Food>? Foods { get; set; }
  [BsonElement("created_by"), BsonRepresentation(BsonType.ObjectId)]
  public string? CreatedBy { get; set; } // UserId
  [BsonElement("created_at")]
  public DateTime CreatedAt { get; set; } = DateTime.Now;
}
