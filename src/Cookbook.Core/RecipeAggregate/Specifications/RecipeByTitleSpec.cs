using Ardalis.Specification;

namespace Cookbook.Core.RecipeAggregate.Specifications;

    public class RecipeByTitleSpec : Specification<Recipe>
    {
    public RecipeByTitleSpec(string recipeTitle)
    {
        if (!string.IsNullOrEmpty(recipeTitle))
        {
            Query.Search(x => x.Title, "%" + recipeTitle + "%");
        }
    }
}

