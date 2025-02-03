using api.Models;

namespace api.Interfaces;

public interface IFoodEntryRepository
{
  public Task<List<FoodEntry>> GetFoodEntries();
  public Task<FoodEntry> GetFoodEntry(string id);
  public Task<FoodEntry> CreateFoodEntry(FoodEntry food);
  public Task<FoodEntry> UpdateFoodEntry(string id, FoodEntry food);
  public Task<FoodEntry> DeleteFoodEntry(string id);
}
