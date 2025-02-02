using api.Interfaces;
using api.Models;
using api.Services;
using MongoDB.Driver;

namespace api.Repositories;

public class FoodRepository(MongoDBService mongoDBService) : IFoodRepository
{
  private readonly IMongoCollection<Food> _foodsCollection = mongoDBService.Database.GetCollection<Food>("foods");

  public async Task<Food> CreateFood(Food food)
  {
    await _foodsCollection.InsertOneAsync(food);
    return food;
  }

  public async Task<Food> DeleteFood(string id)
  {
    var filter = Builders<Food>.Filter.Eq(f => f.Id, id);
    var food = await _foodsCollection.FindOneAndDeleteAsync(filter);
    return food;
  }

  public async Task<Food> GetFood(string id)
  {
    var filter = Builders<Food>.Filter.Eq(f => f.Id, id);
    var food = await _foodsCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
    return food;
  }

  public async Task<List<Food>> GetFoods()
  {
    var filter = FilterDefinition<Food>.Empty;
    var foods = await _foodsCollection.FindAsync(filter).Result.ToListAsync();
    return foods;
  }

  public async Task<Food> UpdateFood(string id, Food food)
  {
    var filter = Builders<Food>.Filter.Eq(f => f.Id, id);
    var updatedFood = await _foodsCollection.FindOneAndReplaceAsync(filter, food);
    return updatedFood;
  }
}
