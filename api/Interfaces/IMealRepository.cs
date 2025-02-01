using api.Models;

namespace api.Interfaces;

public interface IMealRepository
{
  public Task<List<Meal>> GetMeals();
  public Task<Meal> GetMeal(string id);
  public Task<Meal> CreateMeal(Meal meal);
  public Task<Meal> UpdateMeal(string id, Meal meal);
  public Task<Meal> DeleteMeal(string id);
}
