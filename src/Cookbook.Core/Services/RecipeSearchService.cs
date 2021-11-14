using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Ardalis.Result;
using Cookbook.Core.Interfaces;
using Cookbook.Core.RecipeAggregate;
using Cookbook.Core.RecipeAggregate.Specifications;
using Cookbook.SharedKernel.Interfaces;

namespace Cookbook.Core.Services;

    public class RecipeSearchService : IRecipeSearchService 
    {
    private readonly IRepository<Recipe> _repository;

    public RecipeSearchService(IRepository<Recipe> repository)
    {
        _repository = repository;
    }

    //public async Task<Result<List<Recipe>>> GetAllIncompleteItemsAsync(int recipeId, string searchString)
    //{
    //    if (string.IsNullOrEmpty(searchString))
    //    {
    //        var errors = new List<ValidationError>();
    //        errors.Add(new ValidationError()
    //        {
    //            Identifier = nameof(searchString),
    //            ErrorMessage = $"{nameof(searchString)} is required."
    //        });
    //        return Result<List<Recipe>>.Invalid(errors);
    //    }

    //    var projectSpec = new RecipeByIdSpec(recipeId);
    //    var project = await _repository.GetBySpecAsync(projectSpec);

    //    // TODO: Optionally use Ardalis.GuardClauses Guard.Against.NotFound and catch
    //    if (project == null) return Result<List<Recipe>>.NotFound();

    //    //var incompleteSpec = new IncompleteItemsSearchSpec(searchString);

    //    try
    //    {
    //        //var items = incompleteSpec.Evaluate(project.Steps).ToList();

    //        //return new Result<List<Recipe>>(items);
    //        return new Result<List<Recipe>>();
    //    }
    //    catch (Exception ex)
    //    {
    //        // TODO: Log details here
    //        return Result<List<Recipe>>.Error(new[] { ex.Message });
    //    }
    //}

    //public async Task<Result<Recipe>> GetNextIncompleteItemAsync(int recipeId)
    //{
    //    var projectSpec = new ProjectByIdWithItemsSpec(recipeId);
    //    var project = await _repository.GetBySpecAsync(projectSpec);
    //    if (project == null)
    //    {
    //        return Result<Recipe>.NotFound();
    //    }

    //    var incompleteSpec = new IncompleteItemsSpec();

    //    var items = incompleteSpec.Evaluate(project.Items).ToList();

    //    if (!items.Any())
    //    {
    //        return Result<Recipe>.NotFound();
    //    }

    //    return new Result<Recipe>(items.First());
    //}
}

