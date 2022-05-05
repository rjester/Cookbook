using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Minimal.Blazor.Models
{
    public class EditRecipeModel
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
        public string Steps { get; set; }
        [JsonPropertyName("ingredients")]
        public string Ingredients { get; set; }
    }
}
