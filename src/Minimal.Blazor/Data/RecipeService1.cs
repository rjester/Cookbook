using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Minimal.Blazor.Models;
using Minimal.Core;
using Minimal.Infrastructure.Data;
using System.Text;
using System.Text.Json;

namespace Minimal.Blazor.Data;

public class RecipeService1
{
    private readonly AppDbContext _context;
    public RecipeService1(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RecipeModel> GetDetail(string slug)
    {
        var result = await _context.Recipes
                    .Select(x => new RecipeModel
                    {
                        Slug = x.Slug,
                        CookTime = x.CookTime,
                        Description = x.Description,
                        Id = x.Id,
                        PhotoUrl = x.PhotoUrl,
                        PrepTime = x.PrepTime,
                        ReadyIn = x.ReadyIn,
                        Title = x.Title,
                        Ingredients = x.Ingredients.Select(i => new IngredientModel
                        {
                            Id = i.Id,
                            Description = i.Description
                        }).ToList(),
                        Steps = x.Steps.Select(s => new StepModel
                        {
                            Id = s.Id,
                            Description = s.Description
                        }).ToList()
                    })
                    .SingleOrDefaultAsync(x => x.Slug == slug);

        return result;
    }

    public async Task<List<RecentRecipeModel>> GetRecent()
    {
        var result = await _context.Recipes.OrderByDescending(x => x.Id).Take(10)
                            .Select(x => new RecentRecipeModel
                            {
                                Id = x.Id,
                                Description = x.Description,
                                Title = x.Title,
                                Slug = x.Slug,
                                PhotoUrl = x.PhotoUrl
                            })
                        .ToListAsync();

        return result;
    }

    //public async Task<List<RecipeModel>> Search(string query)
    //{
    //    bool getRecipeError = false;
    //    List<RecipeModel> recipes = new();
    //    var requestUrl = _navigationManager.BaseUri + "recipes/search?q=" + query;
    //    HttpResponseMessage response = await _client.GetAsync(requestUrl);

    //    if (response.IsSuccessStatusCode)
    //    {
    //        using var responseStream = await response.Content.ReadAsStreamAsync();
    //        recipes = await JsonSerializer.DeserializeAsync
    //            <List<RecipeModel>>(responseStream);
    //    }
    //    else
    //    {
    //        getRecipeError = true;
    //    }

    //    return recipes;
    //}

    //public async Task<RecipeModel> AddRecipe(RecipeModel recipe)
    //{
    //    bool getRecipeError = false;

    //    Recipe data = new Recipe(recipe.Title, recipe.Description, recipe.PhotoUrl,
    //                            recipe.PrepTime, recipe.CookTime, recipe.ReadyIn);
    //    SlugHelper slugGenerator = new SlugHelper();

    //    data.Slug = slugGenerator.GenerateSlug(data.Title);

    //    foreach (var ing in recipe.Ingredients)
    //    {
    //        data.AddIngredient(new Ingredient(ing.Description));
    //    }

    //    foreach (var step in recipe.Steps)
    //    {
    //        data.AddStep(new Step(step.Description));
    //    }

    //    var jsonData = JsonSerializer.Serialize(data);


    //    var requestUrl = _navigationManager.BaseUri + "recipes/";
    //    HttpResponseMessage response = await _client.PostAsync(requestUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

    //    if (response.IsSuccessStatusCode)
    //    {
    //        using var responseStream = await response.Content.ReadAsStreamAsync();
    //        recipe = await JsonSerializer.DeserializeAsync
    //            <RecipeModel>(responseStream);
    //    }
    //    else
    //    {
    //        getRecipeError = true;
    //    }

    //    return recipe;
    //}

    //public async Task<EditRecipeModel> UpdateRecipe(EditRecipeModel recipe)
    //{
    //    // get existing recipe
    //    //bool getRecipeError = false;
    //    var requestUrl = _navigationManager.BaseUri + "recipes/" + recipe.Slug;
    //    HttpResponseMessage response = await _client.GetAsync(requestUrl);
    //    Recipe data = new Recipe(recipe.Title, recipe.Description, recipe.PhotoUrl,
    //                            recipe.PrepTime, recipe.CookTime, recipe.ReadyIn);
    //    //SlugHelper slugGenerator = new SlugHelper();

    //    //data.Slug = slugGenerator.GenerateSlug(data.Title);

    //    //foreach (var ing in recipe.Ingredients)
    //    //{
    //    //    data.AddIngredient(new Ingredient(ing.Description));
    //    //}

    //    //foreach (var step in recipe.Steps)
    //    //{
    //    //    data.AddStep(new Step(step.Description));
    //    //}

    //    //var jsonData = JsonSerializer.Serialize(data);


    //    //var requestUrl = _navigationManager.BaseUri + "recipes/";
    //    //HttpResponseMessage response = await _client.PostAsync(requestUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

    //    //if (response.IsSuccessStatusCode)
    //    //{
    //    //    using var responseStream = await response.Content.ReadAsStreamAsync();
    //    //    recipe = await JsonSerializer.DeserializeAsync
    //    //        <RecipeModel>(responseStream);
    //    //}
    //    //else
    //    //{
    //    //    getRecipeError = true;
    //    //}

    //    return recipe;
    //}

    public async Task<RecipeModel> AddRecipe(RecipeModel recipe)
    {
        Recipe data = new Recipe(recipe.Title, recipe.Description, recipe.PhotoUrl,
                                recipe.PrepTime, recipe.CookTime, recipe.ReadyIn);
        SlugHelper slugGenerator = new SlugHelper();

        data.Slug = slugGenerator.GenerateSlug(data.Title);

        var collisions = await _context.Recipes.Where(x => x.Slug.Contains(recipe.Slug) && x.Id != recipe.Id).Select(x => x.Slug).ToListAsync();
        if (collisions?.Count() > 0)
        {
            data.Slug += "-" + collisions.Count().ToString();
        }

        foreach (var ing in recipe.Ingredients)
        {
            data.AddIngredient(new Ingredient(ing.Description));
        }

        foreach (var step in recipe.Steps)
        {
            data.AddStep(new Step(step.Description));
        }

        _context.Recipes.Add(data);
        await _context.SaveChangesAsync();

        recipe.Id = data.Id;

        return recipe;
    }
}
