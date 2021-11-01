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
        public IEnumerable<Step> Steps { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
