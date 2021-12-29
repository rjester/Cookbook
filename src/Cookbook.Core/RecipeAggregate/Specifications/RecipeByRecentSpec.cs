using Ardalis.Specification;

namespace Cookbook.Core.RecipeAggregate.Specifications;

public class RecipeByRecentSpec : Specification<Recipe>
{
    public RecipeByRecentSpec()
    {
        Query.OrderByDescending(x => x.Id).Take(10);
    }
}

