using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace api.Models;

[CollectionName("users")]
public class User : MongoIdentityUser<ObjectId>
{
}
