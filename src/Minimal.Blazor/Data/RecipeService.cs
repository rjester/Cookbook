using Microsoft.AspNetCore.Components;
using Minimal.Blazor.Models;
using Minimal.Core;
using System.Text;
using System.Text.Json;

namespace Minimal.Blazor.Data;

public class RecipeService
{
    private HttpClient _client;
    private NavigationManager _navigationManager;

    public RecipeService(HttpClient client, NavigationManager navigationManager)
    {
        _client = client;
        _navigationManager = navigationManager;
    }

    public async Task<RecipeModel> GetDetail(string slug)
    {
        bool getRecipeError = false;
        RecipeModel recipe = new();
        var requestUrl = _navigationManager.BaseUri + "recipes/" + slug;
        HttpResponseMessage response = await _client.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            recipe = await JsonSerializer.DeserializeAsync
                <RecipeModel>(responseStream);
        }
        else
        {
            getRecipeError = true;
        }

        return recipe;
    }

    public async Task<EditRecipeModel> GetEditDetail(string slug)
    {
        bool getRecipeError = false;
        EditRecipeModel recipe = new();
        var requestUrl = _navigationManager.BaseUri + "recipes/" + slug;
        HttpResponseMessage response = await _client.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            recipe = await JsonSerializer.DeserializeAsync
                <EditRecipeModel>(responseStream);
        }
        else
        {
            getRecipeError = true;
        }

        return recipe;
    }

    public async Task<List<RecipeModel>> GetRecent()
    {
        bool getRecipeError = false;
        List<RecipeModel> recipes = new();
        var requestUrl = _navigationManager.BaseUri + "api/recipes/recent";
        HttpResponseMessage response = await _client.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            recipes = await JsonSerializer.DeserializeAsync
                <List<RecipeModel>>(responseStream);
        }
        else
        {
            getRecipeError = true;
        }

        return recipes;
    }

    public async Task<List<RecipeModel>> Search(string query)
    {
        bool getRecipeError = false;
        List<RecipeModel> recipes = new();
        var requestUrl = _navigationManager.BaseUri + "recipes/search?q=" + query;
        HttpResponseMessage response = await _client.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            recipes = await JsonSerializer.DeserializeAsync
                <List<RecipeModel>>(responseStream);
        }
        else
        {
            getRecipeError = true;
        }

        return recipes;
    }

    public async Task<RecipeModel> AddRecipe(RecipeModel recipe)
    {
        bool getRecipeError = false;

        Recipe data = new Recipe(recipe.Title, recipe.Description, recipe.PhotoUrl,
                                recipe.PrepTime, recipe.CookTime, recipe.ReadyIn);
        SlugHelper slugGenerator = new SlugHelper();

        data.Slug = slugGenerator.GenerateSlug(data.Title);

        foreach (var ing in recipe.Ingredients)
        {
            data.AddIngredient(new Ingredient(ing.Description));
        }

        foreach (var step in recipe.Steps)
        {
            data.AddStep(new Step(step.Description));
        }

        var jsonData = JsonSerializer.Serialize(data);


        var requestUrl = _navigationManager.BaseUri + "recipes/";
        HttpResponseMessage response = await _client.PostAsync(requestUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            recipe = await JsonSerializer.DeserializeAsync
                <RecipeModel>(responseStream);
        }
        else
        {
            getRecipeError = true;
        }

        return recipe;
    }

    public async Task<EditRecipeModel> UpdateRecipe(EditRecipeModel recipe)
    {
        // get existing recipe
        //bool getRecipeError = false;
        var requestUrl = _navigationManager.BaseUri + "recipes/" + recipe.Slug;
        HttpResponseMessage response = await _client.GetAsync(requestUrl);
        Recipe data = new Recipe(recipe.Title, recipe.Description, recipe.PhotoUrl,
                                recipe.PrepTime, recipe.CookTime, recipe.ReadyIn);
        //SlugHelper slugGenerator = new SlugHelper();

        //data.Slug = slugGenerator.GenerateSlug(data.Title);

        //foreach (var ing in recipe.Ingredients)
        //{
        //    data.AddIngredient(new Ingredient(ing.Description));
        //}

        //foreach (var step in recipe.Steps)
        //{
        //    data.AddStep(new Step(step.Description));
        //}

        //var jsonData = JsonSerializer.Serialize(data);


        //var requestUrl = _navigationManager.BaseUri + "recipes/";
        //HttpResponseMessage response = await _client.PostAsync(requestUrl, new StringContent(jsonData, Encoding.UTF8, "application/json"));

        //if (response.IsSuccessStatusCode)
        //{
        //    using var responseStream = await response.Content.ReadAsStreamAsync();
        //    recipe = await JsonSerializer.DeserializeAsync
        //        <RecipeModel>(responseStream);
        //}
        //else
        //{
        //    getRecipeError = true;
        //}

        return recipe;
    }
}
