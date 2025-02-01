using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("meals")]
public class Meal
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
  public string? Id { get; set; }
  [BsonElement("name")]
  public string? Name { get; set; }
  [BsonElement("description")]
  public string? Description { get; set; }
}
