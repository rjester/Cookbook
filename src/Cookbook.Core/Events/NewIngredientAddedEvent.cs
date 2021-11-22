using Cookbook.Core.RecipeAggregate;
using Cookbook.SharedKernel;

namespace Cookbook.Core.Events
{
    public class NewIngredientAddedEvent : BaseDomainEvent
    {
        public RecipeIngredient NewIngredient { get; set; }
        public Recipe Recipe { get; set; }

        public NewIngredientAddedEvent(Recipe recipe,
            RecipeIngredient newIngredient)
        {
            Recipe = recipe;
            NewIngredient = newIngredient;
        }
    }
}
