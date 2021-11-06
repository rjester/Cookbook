using Cookbook.Data.Entities;
using Cookbook.Services.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Cookbook.Services
{
    public interface IRecipeService
    {
        IEnumerable<RecipeDto> GetAll();
        RecipeDto GetById(int id);
        RecipeDto Add(RecipeDto newRecipe);
        RecipeDto Update(int id, RecipeDto recipe);
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

        public RecipeDto Add(RecipeDto newRecipe)
        {
            var ingredients = new List<RecipeIngredient>();
            // find existing ingredients
            foreach (var ing in newRecipe.Ingredients)
            {
                var x = _context.RecipeIngredients.Include(x => x.Ingredient).FirstOrDefault(x => x.Ingredient.Name == ing.Name);
                if (x != null)
                {
                    ingredients.Add(new RecipeIngredient
                    {
                        Ingredient = x.Ingredient,
                        Quantity = (decimal)ing.Quantity,
                        Unit = ing.Unit
                    });
                }
                else
                {
                    ingredients.Add(new RecipeIngredient
                    {
                        Ingredient = new Ingredient
                        {
                            Name = ing.Name
                        },
                        Quantity = (decimal)ing.Quantity,
                        Unit = ing.Unit
                    });
                }
            }

            var entity = new Recipe
            {
                Description = newRecipe.Description,
                Title = newRecipe.Title,
                Ingredients = ingredients,
                Steps = newRecipe.Steps.Select(x => new Step
                {
                    Description = x.Description
                }).ToList()
            };

            _context.Recipes.Add(entity);
            _context.SaveChanges();
            return GetById(entity.Id);
        }

        public RecipeDto Update(int id, RecipeDto recipe)
        {
            var ingredients = new List<RecipeIngredient>();
            // find existing ingredients
            foreach (var ing in recipe.Ingredients)
            {
                var x = _context.RecipeIngredients.Include(x => x.Ingredient).FirstOrDefault(x => x.Ingredient.Name == ing.Name);
                if (x != null)
                {
                    ingredients.Add(new RecipeIngredient
                    {
                        Ingredient = x.Ingredient,
                        Quantity = (decimal)ing.Quantity,
                        Unit = ing.Unit
                    });
                }
                else
                {
                    ingredients.Add(new RecipeIngredient
                    {
                        Ingredient = new Ingredient
                        {
                            Name = ing.Name
                        },
                        Quantity = (decimal)ing.Quantity,
                        Unit = ing.Unit
                    });
                }
            }

            var entity = new Recipe
            {
                Id = recipe.Id,
                Description = recipe.Description,
                Title = recipe.Title,
                Ingredients = ingredients,
                Steps = recipe.Steps.Select(x => new Step
                {
                    Description = x.Description
                }).ToList()
            };
            _context.Update(entity);
            _context.SaveChanges();
            return GetById(id);
        }
    }
}