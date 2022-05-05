using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal.Blazor.Data;
using Minimal.Blazor.Models;
using Minimal.Infrastructure.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Minimal.Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly RecipeService1 _svc;

        public RecipesController(AppDbContext context, RecipeService1 svc)
        {
            _context = context;
            _svc = svc;
        }

        [HttpGet]
        public async Task<IEnumerable<RecipeSummary>> Get()
        {
            var t = await _context.Recipes
                                .Select(x => new RecipeSummary
                                {
                                    Id = x.Id,
                                    Description = x.Description,
                                    Title = x.Title,
                                    Slug = x.Slug,
                                    PhotoUrl = x.PhotoUrl
                                })
                                .ToListAsync();

            return t;
        }

        [HttpGet("recent")]
        public async Task<IList<RecentRecipeModel>> GetRecent()
        {
            var t = await _svc.GetRecent();
            return t;
        }

        [HttpGet]
        [Route("{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var t = await _svc.GetDetail(slug);

            if (t == null)
            {
                return NotFound();
            }

            return Ok(t);
        }

        //[HttpGet("edit/{slug}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetEdit(string slug)
        //{
        //    var t = await _svc.GetEditDetail(slug);

        //    if (t == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(t);
        //}

        [HttpPost]
        public async Task<IActionResult> Post(RecipeModel recipe)
        {
            var t = await _svc.AddRecipe(recipe);

            return CreatedAtAction(nameof(GetBySlug), new { slug = recipe.Slug }, recipe);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
