using Cookbook.Core;
using Cookbook.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Infrastructure.Services
{
    public interface IRecipeService
    {
        Recipe GetDetail(string slug);
    }

    public class RecipeService : IRecipeService
    {
        private readonly AppDbContext _context;

        public RecipeService(AppDbContext context)
        {
            _context = context;
        }

        public Recipe GetDetail(string slug)
        {
            var result = _context.Recipes.FirstOrDefault(x => x.Slug == slug);

            return result;
        }
    }
}
