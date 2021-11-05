using Microsoft.EntityFrameworkCore;

namespace Cookbook.Data.Entities
{
    public class CookbookContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer("Server=.;Database=Cookbook1;Trusted_Connection=True;");
    }
}
