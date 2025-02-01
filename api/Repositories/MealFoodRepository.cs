using api.Interfaces;
using api.Models;
using api.Services;
using MongoDB.Driver;

namespace api.Repositories;

public class MealFoodRepository : IMealFoodRepository
{
  private readonly IMongoCollection<MealFood> _mealFoodCollection;

  public MealFoodRepository(MongoDBService mongoDBService)
  {
    _mealFoodCollection = mongoDBService.Database.GetCollection<MealFood>("mealFoods");
  }

  public async Task<MealFood> CreateMealFood(MealFood mealFood)
  {
    await _mealFoodCollection.InsertOneAsync(mealFood);
    return mealFood;
  }

  public async Task<bool> DeleteMealFood(string id)
  {
    var filter = Builders<MealFood>.Filter.Eq(mf => mf.Meal.Id, id);
    var mealFood = await _mealFoodCollection.DeleteManyAsync(filter);
    return mealFood.IsAcknowledged;
  }

  public Task<MealFood> GetMealFood(string id)
  {
    var filter = Builders<MealFood>.Filter.Eq(mf => mf.Meal.Id, id);
    var mealFood = _mealFoodCollection.FindAsync(filter).Result.FirstOrDefaultAsync();
    return mealFood;
  }

  public Task<List<MealFood>> GetMealFoods()
  {
    var filter = FilterDefinition<MealFood>.Empty;
    var mealFoods = _mealFoodCollection.FindAsync(filter).Result.ToListAsync();
    return mealFoods;
  }

  public Task<MealFood> UpdateMealFood(string id, MealFood mealFood)
  {
    var filter = Builders<MealFood>.Filter.Eq(mf => mf.Meal.Id, id);
    var updatedMealFood = _mealFoodCollection.FindOneAndReplaceAsync(filter, mealFood);
    return updatedMealFood;
  }
}
