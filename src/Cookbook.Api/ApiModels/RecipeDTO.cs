using System.Text.Json.Serialization;

namespace Cookbook.Api.ApiModels
{
    public class RecipeDTO : CreateRecipeDTO
    {
        public RecipeDTO(int id, string title, string description, 
                            List<StepDTO>? steps = null, 
                            List<IngredientDTO>? ingredients = null) : 
                                base(title, description, steps, ingredients)
        {
            Id = id;
        }

        public int Id { get; set; }
    }

    public class CreateRecipeDTO
    {
        public CreateRecipeDTO(string title, string description, 
                                List<StepDTO>? steps = null,
                                List<IngredientDTO>? ingredients = null)
        {
            Title = title;
            Description = description;
            Steps = steps ?? new List<StepDTO>();
            Ingredients = ingredients ?? new List<IngredientDTO>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public List<StepDTO> Steps { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
    }

    public class EditRecipeDTO
    {
        public EditRecipeDTO(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
