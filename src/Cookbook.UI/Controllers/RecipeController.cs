using Cookbook.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;

namespace Cookbook.UI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly CookbookContext _context;

        public RecipeController(ILogger<RecipeController> logger, 
                                CookbookContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetAllRecipes")]
        public IEnumerable<Recipe> GetAll()
        {
            return _context.Recipes
                .Include(x => x.Ingredients)
                .ThenInclude(x => x.Ingredient)
                            .Include(x => x.Steps)
                            .ToList();
        }

        [HttpGet("{id}", Name = "GetRecipe")]
        public Recipe GetById(int id)
        {
            return new Recipe();
        }
    }
}