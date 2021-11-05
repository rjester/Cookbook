using Cookbook.Data.Entities;
using Cookbook.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Services
{
    public interface IRecipeService 
    {
        IEnumerable<RecipeDto> GetAll();
        RecipeDto GetById(int id);
    }

    public class RecipeService : IRecipeService
    {
        private readonly CookbookContext _context;
        public RecipeService(CookbookContext context)
        {
            _context = context;
        }

        public IEnumerable<RecipeDto> GetAll() 
        { 
            var result = _context.Recipes
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Ingredient)
                            .Include(x => x.Steps)
                            .Select(x => new RecipeDto { 
                                Id = x.Id,
                                Description = x.Description,
                                Title = x.Title,
                                Steps = x.Steps.Select(s => new StepDto { 
                                                    Id = s.Id,
                                                    Description = s.Description
                                        }).ToArray(),
                                Ingredients = x.Ingredients.Select(i => new IngredientDto
                                {
                                    Id = i.Id,
                                    Name = i.Ingredient.Name,
                                    Quantity = (float)i.Quantity,
                                    Unit = i.Unit
                                }).ToArray()
                            })
                            .ToList();

            return result;
        }

        public RecipeDto GetById(int id)
        {
            var result = _context.Recipes
                        .Include(x => x.Ingredients)
                            .ThenInclude(x => x.Ingredient)
                        .Include(x => x.Steps)
                    .Where(x => x.Id == id)
                    .Select(x => new RecipeDto
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Title = x.Title,
                        Steps = x.Steps.Select(s => new StepDto
                        {
                            Id = s.Id,
                            Description = s.Description
                        }).ToArray(),
                        Ingredients = x.Ingredients.Select(i => new IngredientDto
                        {
                            Id = i.Id,
                            Name = i.Ingredient.Name,
                            Quantity = (float)i.Quantity,
                            Unit = i.Unit
                        }).ToArray()
                    }).FirstOrDefault();

            return result;
        }
    }
}