using api.Models;

namespace api.Interfaces;

public interface IFoodRepository
{
  public Task<List<Food>> GetFoods();
  public Task<Food> GetFood(string id);
  public Task<Food> CreateFood(Food foodEntry);
  public Task<Food> UpdateFood(string id, Food foodEntry);
  public Task<Food> DeleteFood(string id);
}
