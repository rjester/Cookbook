using Cookbook.Core.RecipeAggregate;
using System.ComponentModel.DataAnnotations;

namespace Cookbook.Api.ApiModels
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        [Required]
        public string? Unit { get; set; }

        public static IngredientDTO FromIngredient(RecipeIngredient ingredient)
        {
            return new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Ingredient.Name,
                Quantity = ingredient.Quantity,
                Unit = ingredient.Unit
            };
        }
    }
}
