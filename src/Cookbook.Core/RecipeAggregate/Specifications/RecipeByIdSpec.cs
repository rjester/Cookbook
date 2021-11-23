using Ardalis.Specification;

namespace Cookbook.Core.RecipeAggregate.Specifications;

    public class RecipeByIdSpec : Specification<Recipe>, ISingleResultSpecification
    {
    public RecipeByIdSpec(int recipeId)
    {
        Query
            .Where(recipe => recipe.Id == recipeId)
            .Include(recipe => recipe.Steps)
            .Include(recipe => recipe.RecipeIngredients)
            .ThenInclude(r => r.Ingredient);
    }
}

