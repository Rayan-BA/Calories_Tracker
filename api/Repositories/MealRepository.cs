using api.Interfaces;
using api.Models;
using api.Services;
using MongoDB.Driver;

namespace api.Repositories;

public class MealRepository(MongoDBService mongoDBService) : IMealRepository
{
  private readonly IMongoCollection<Meal> _mealsCollection = mongoDBService.Database.GetCollection<Meal>("meals");

  public async Task<Meal> CreateMeal(Meal meal)
  {
    await _mealsCollection.InsertOneAsync(meal);
    return meal;
  }

  public async Task<Meal> DeleteMeal(string id)
  {
    var filter = Builders<Meal>.Filter.Eq(m => m.Id, id);
    var meal = await _mealsCollection.FindOneAndDeleteAsync(filter);
    return meal;
  }

  public async Task<Meal> GetMeal(string id)
  {
    var filter = Builders<Meal>.Filter.Eq(m => m.Id, id);
    var meal = await _mealsCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
    return meal;
  }

  public async Task<List<Meal>> GetMeals()
  {
    var filter = FilterDefinition<Meal>.Empty;
    var meals = await _mealsCollection.FindAsync(filter).Result.ToListAsync();
    return meals;
  }

  public async Task<Meal> UpdateMeal(string id, Meal meal)
  {
    var filter = Builders<Meal>.Filter.Eq(m => m.Id, id);
    var updatedMeal = await _mealsCollection.FindOneAndReplaceAsync(filter, meal);
    return meal;
  }
}
