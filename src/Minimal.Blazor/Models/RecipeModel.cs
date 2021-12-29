using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Minimal.Blazor.Models
{
    public class RecipeModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        [Required]
        public string Title { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("photoUrl")]
        public string? PhotoUrl { get; set; }
        [JsonPropertyName("prepTime")]
        public string PrepTime { get; set; }
        [JsonPropertyName("cookTime")]
        public string? CookTime { get; set; }
        [JsonPropertyName("readyIn")]
        public string ReadyIn { get; set; }

        [JsonPropertyName("steps")]
        public ICollection<StepModel> Steps { get; set; }
        [JsonPropertyName("ingredients")]
        public ICollection<IngredientModel> Ingredients { get; set; }

        public RecipeModel()
        {
            Ingredients = new List<IngredientModel>();
            Steps = new List<StepModel>();
        }
    }

    public class IngredientModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class StepModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
