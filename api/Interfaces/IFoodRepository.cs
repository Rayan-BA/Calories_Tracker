using api.Models;
using MongoDB.Bson;

namespace api.Interfaces
{
  public interface IFoodRepository
  {
    public Task<List<Food>> GetFoods();
    public Task<Food> GetFood(string id);
    public Task<Food> CreateFood(Food food);
    public Task<Food> UpdateFood(string id, Food food);
    public Task<Food> DeleteFood(string id);
  }
}
