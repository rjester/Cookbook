using Cookbook.Data.Entities;
using Cookbook.Services;
using Cookbook.Services.Dtos;
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
        private readonly IRecipeService _svc;

        public RecipeController(ILogger<RecipeController> logger, 
                                IRecipeService svc)
        {
            _logger = logger;
            _svc = svc;
        }

        [HttpGet(Name = "GetAllRecipes")]
        public IActionResult GetAll()
        {
            return Ok(_svc.GetAll());
        }

        [HttpGet("{id}", Name = "GetRecipe")]
        public IActionResult GetById(int id)
        {
            var result = _svc.GetById(id);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}