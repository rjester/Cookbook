using Cookbook.Core.Interfaces;
using Cookbook.Core.RecipeAggregate;
using Cookbook.Core.RecipeAggregate.Specifications;
using Cookbook.Core.RecipeAggregate.Specifications.Filters;
using Cookbook.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _recipeRepo;

        public RecipeService(IRepository<Recipe> recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        public Task<List<Recipe>> GetAll()
        {
            return _recipeRepo.ListAsync(new RecipeSpec(new RecipeFilter { 
                LoadChildren = true
            }));
        }

        public Task<Recipe?> GetById(int id)
        {
            var idSpec = new RecipeByIdSpec(id);
            return _recipeRepo.GetBySpecAsync(idSpec);
        }

        public Task<List<Recipe>> GetByTitle(string title)
        {
            var titleSpec = new RecipeByTitleSpec(title);
            return _recipeRepo.ListAsync(titleSpec);
        }
    }
}
