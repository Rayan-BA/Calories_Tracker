using api.Models;

namespace api.Interfaces;

public interface IMealEntryRepository
{
  public Task<List<MealEntry>> GetMealEntries();
  public Task<MealEntry> GetMealEntry(string id);
  public Task<MealEntry> CreateMealEntry(MealEntry mealEntry);
  public Task<MealEntry> UpdateMealEntry(string id, MealEntry mealEntry);
  public Task<MealEntry> DeleteMealEntry(string id);
}
