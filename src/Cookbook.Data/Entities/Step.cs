using System.ComponentModel.DataAnnotations;

namespace Cookbook.Data.Entities
{
    public class Step : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
