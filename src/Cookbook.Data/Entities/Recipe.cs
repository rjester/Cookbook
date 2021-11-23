using System.ComponentModel.DataAnnotations;

namespace Cookbook.Data.Entities
{
    public class Recipe : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public IList<Step> Steps { get; set; }
        public IList<RecipeIngredient> Ingredients { get; set; }

        public Recipe()
        {
            Steps = new List<Step>();
            Ingredients = new List<RecipeIngredient>();
        }
    }
}
