using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[BsonIgnoreExtraElements] // to fix: "Element 'ServingSize' does not match any field or property of class api.Models.Food."
[CollectionName("foods")]
public class Food
{
  [BsonId]
  [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  [BsonElement("name")]
  public string? Name { get; set; }
  [BsonElement("description")]
  public string? Description { get; set; }
  [BsonElement("brand")]
  public string? Brand { get; set; }
  [BsonElement("serving_size")]
  public int ServingSize { get; set; }
  [BsonElement("serving_size_unit")]
  public string? ServingSizeUnit { get; set; }
  [BsonElement("nutritional_facts")]
  public List<KeyValuePair<string, float>>? NutritionalFacts { get; set; }
}
