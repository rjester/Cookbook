using Cookbook.Core.RecipeAggregate;

namespace Cookbook.Core.Interfaces
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAll();
        Task<List<Recipe>> GetByTitle(string title);
        Task<Recipe?> GetById(int id);
    }
}
