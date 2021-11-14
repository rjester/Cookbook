using Ardalis.Specification;
using Cookbook.Core.RecipeAggregate.Specifications.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.RecipeAggregate.Specifications
{
    public class RecipeSpec : Specification<Recipe>
    {
        public RecipeSpec(RecipeFilter filter)
        {
            Query.OrderBy(x => x.Id);

            if (filter.LoadChildren)
            {
                Query.Include(x => x.Steps);
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                Query.Where(x => x.Title == filter.Title);
            }

            if (filter.Id.HasValue)
            {
                Query.Where(x => x.Id == filter.Id);
            }
        }
    }
}
