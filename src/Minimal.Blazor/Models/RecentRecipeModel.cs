using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Minimal.Blazor.Models
{
    public class RecentRecipeModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        [Required]
        public string Title { get; set; }
        [JsonPropertyName("slug")]
        public string? Slug { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("photoUrl")]
        public string? PhotoUrl { get; set; }
    }
}
