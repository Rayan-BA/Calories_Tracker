using api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace api.Services;

public class MongoDBService
{
  private readonly IConfiguration _configuration;
  private readonly IMongoDatabase _database;
  private readonly string _connectionString;
  private readonly string? _databaseName;

  public MongoDBService(IConfiguration configuration)
  {
    _configuration = configuration;
    var mongodbConfig = _configuration.GetSection("MongoDB");
    _connectionString = mongodbConfig.GetValue<string>("ConnectionString");
    _databaseName = mongodbConfig.GetValue<string>("Database");
    var mongoUrl = MongoUrl.Create(_connectionString);
    var mongoClient = new MongoClient(mongoUrl);
    _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
  }

  public IMongoDatabase Database => _database;
  public string ConnectionString => _connectionString;
  public string? DatabaseName => _databaseName;
}
