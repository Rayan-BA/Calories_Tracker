using api.Interfaces;
using api.Models;
using api.Services;
using MongoDB.Driver;

namespace api.Repositories;

public class MealEntryRepository(MongoDBService mongoDBService) : IMealEntryRepository
{
  private readonly IMongoCollection<MealEntry> _mealEntriesCollection = mongoDBService.Database.GetCollection<MealEntry>("meal_entries");

  public async Task<MealEntry> CreateMealEntry(MealEntry mealEntry)
  {
    await _mealEntriesCollection.InsertOneAsync(mealEntry);
    return mealEntry;
  }

  public async Task<MealEntry> DeleteMealEntry(string id)
  {
    var filter = Builders<MealEntry>.Filter.Eq(m => m.Id, id);
    var mealEntry = await _mealEntriesCollection.FindOneAndDeleteAsync(filter);
    return mealEntry;
  }

  public async Task<List<MealEntry>> GetMealEntries()
  {
    var filter = FilterDefinition<MealEntry>.Empty;
    var mealEntries = await _mealEntriesCollection.FindAsync(filter).Result.ToListAsync();
    return mealEntries;
  }

  public async Task<MealEntry> GetMealEntry(string id)
  {
    var filter = Builders<MealEntry>.Filter.Eq(m => m.Id, id);
    var mealEntry = await _mealEntriesCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
    return mealEntry;
  }

  public async Task<MealEntry> UpdateMealEntry(string id, MealEntry mealEntry)
  {
    var filter = Builders<MealEntry>.Filter.Eq(m => m.Id, id);
    var updatedMealEntry = await _mealEntriesCollection.FindOneAndReplaceAsync(filter, mealEntry);
    return updatedMealEntry;
  }
}
