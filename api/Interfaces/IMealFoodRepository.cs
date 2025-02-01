using api.Models;

namespace api.Interfaces;

public interface IMealFoodRepository
{
  public Task<List<MealFood>> GetMealFoods();
  public Task<MealFood> GetMealFood(string id);
  public Task<MealFood> CreateMealFood(MealFood mealFood);
  public Task<MealFood> UpdateMealFood(string id, MealFood mealFood);
  public Task<bool> DeleteMealFood(string id);
}
