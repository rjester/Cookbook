using Cookbook.Core.RecipeAggregate;
using Cookbook.SharedKernel;

namespace Cookbook.Core.Events
{
    public class NewStepAddedEvent : BaseDomainEvent
    {
        public Step NewStep { get; set; }
        public Recipe Recipe { get; set; }

        public NewStepAddedEvent(Recipe recipe,
            Step newStep)
        {
            Recipe = recipe;
            NewStep = newStep;
        }
    }
}
