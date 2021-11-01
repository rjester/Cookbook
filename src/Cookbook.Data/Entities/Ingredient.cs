using System.ComponentModel.DataAnnotations;

namespace Cookbook.Data.Entities
{
    public class Ingredient : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
