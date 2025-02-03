using api.Interfaces;
using api.Models;
using api.Services;
using MongoDB.Driver;

namespace api.Repositories;

public class FoodEntryRepository(MongoDBService mongoDBService) : IFoodEntryRepository
{
  private readonly IMongoCollection<FoodEntry> _foodEntries = mongoDBService.Database.GetCollection<FoodEntry>("food_entries");

  public async Task<FoodEntry> CreateFoodEntry(FoodEntry foodEntry)
  {
    await _foodEntries.InsertOneAsync(foodEntry);
    return foodEntry;
  }

  public async Task<FoodEntry> DeleteFoodEntry(string id)
  {
    var filter = Builders<FoodEntry>.Filter.Eq(f => f.Id, id);
    var foodEntry = await _foodEntries.FindOneAndDeleteAsync(filter);
    return foodEntry;
  }

  public async Task<List<FoodEntry>> GetFoodEntries()
  {
    var filter = FilterDefinition<FoodEntry>.Empty;
    var foodEntries = await _foodEntries.FindAsync(filter).Result.ToListAsync();
    return foodEntries;
  }

  public async Task<FoodEntry> GetFoodEntry(string id)
  {
    var filter = Builders<FoodEntry>.Filter.Eq(f => f.Id, id);
    var foodEntry = await _foodEntries.FindAsync(filter).Result.FirstOrDefaultAsync();
    return foodEntry;
  }

  public async Task<FoodEntry> UpdateFoodEntry(string id, FoodEntry foodEntry)
  {
    var filter = Builders<FoodEntry>.Filter.Eq(f => f.Id, id);
    var updatedFoodEntry = await _foodEntries.FindOneAndReplaceAsync(filter, foodEntry);
    return updatedFoodEntry;
  }
}
