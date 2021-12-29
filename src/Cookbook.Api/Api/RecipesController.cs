using Cookbook.Api.ApiModels;
using Cookbook.Core.RecipeAggregate;
using Cookbook.Core.RecipeAggregate.Specifications;
using Cookbook.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Api.Api
{
    public class RecipesController : BaseApiController
    {
        private readonly IRepository<Recipe> _repo;
        private readonly IRepository<Ingredient> _ingredientRepo;

        public RecipesController(IRepository<Recipe> repo, 
                                IRepository<Ingredient> ingredientRepo)
        {
            _repo = repo;
            _ingredientRepo = ingredientRepo;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var recipeDTOs = (await _repo.ListAsync())
                .Select(recipe => new RecipeDTO
                (
                    id: recipe.Id,
                    title: recipe.Title,
                    description: recipe.Description
                ))
                .ToList();

            return Ok(recipeDTOs);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var recipeSpec = new RecipeByIdSpec(id);
            var recipe = await _repo.GetBySpecAsync(recipeSpec);
            if (recipe == null)
            {
                return NotFound();
            }

            var result = new RecipeDTO
            (
                id: recipe.Id,
                title: recipe.Title,
                description: recipe.Description,
                steps: new List<StepDTO>
                (
                    recipe.Steps.Select(i => StepDTO.FromStep(i)).ToList()
                ),
                ingredients: new List<IngredientDTO>
                (
                    recipe.RecipeIngredients.Select(ri => IngredientDTO.FromIngredient(ri)).ToList()
                )
            );

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetByTitle(string q)
        {
            var recipeSpec = new RecipeByTitleSpec(q);

            var recipeDTOs = (await _repo.ListAsync(recipeSpec))
                .Select(recipe => new RecipeDTO
                (
                    id: recipe.Id,
                    title: recipe.Title,
                    description: recipe.Description
                ))
                .ToList();

            return Ok(recipeDTOs);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecent()
        {
            var recipeSpec = new RecipeByRecentSpec();

            var recipeDTOs = (await _repo.ListAsync(recipeSpec))
                .Select(recipe => new RecipeDTO
                (
                    id: recipe.Id,
                    title: recipe.Title,
                    description: recipe.Description
                ))
                .ToList();

            return Ok(recipeDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRecipeDTO request)
        {
            var steps = request.Steps.Select(x => new Step
            {
                Description = x.Description
            });

            var ids = request.Ingredients.Select(y => y.Id).ToArray();

            var existingIngredients = request.Ingredients.Where(y => y.Id > 0);
            var newIngredients = request.Ingredients.Where(y => y.Id <= 0);
            var newRecipe = new Recipe(request.Title, request.Description);

            foreach (var step in steps)
            {
                newRecipe.AddStep(step);
            }

            foreach (var item in newIngredients)
            {
                newRecipe.AddIngredient(
                    new RecipeIngredient
                    {
                        Quantity = item.Quantity,
                        Unit = item.Unit,
                        Ingredient = new Ingredient
                        {
                            Name = item.Name
                        }
                    });
            }

            foreach (var item in existingIngredients)
            {
                newRecipe.AddIngredient(
                    new RecipeIngredient
                    {
                        Quantity = item.Quantity,
                        Unit = item.Unit,
                        IngredientId = item.Id
                    });
            }

            var createdRecipe = await _repo.AddAsync(newRecipe);

            var result = new RecipeDTO
            (
                id: createdRecipe.Id,
                title: createdRecipe.Title,
                description: createdRecipe.Description
                //steps: createdRecipe.Steps.Select(x => x.From
            );
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EditRecipeDTO request)
        {
            var existingRecipe = await _repo.GetByIdAsync(request.Id);
            existingRecipe.Title = request.Title;
            existingRecipe.Description = request.Description;
            var newRecipe = new Recipe(request.Title, request.Description);

            await _repo.UpdateAsync(existingRecipe);

            var result = new RecipeDTO
            (
                id: existingRecipe.Id,
                title: existingRecipe.Title,
                description: existingRecipe.Description
            );
            return Ok(result);
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRecipe = await _repo.GetByIdAsync(id);
            if (existingRecipe is null)
            {
                return NotFound();
            }
            await _repo.DeleteAsync(existingRecipe);

            return Ok();
        }
    }
}
