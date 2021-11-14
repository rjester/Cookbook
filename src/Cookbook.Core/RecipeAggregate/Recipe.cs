using Ardalis.GuardClauses;
using Cookbook.Core.Events;
using Cookbook.SharedKernel;
using Cookbook.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.RecipeAggregate
{
    public class Recipe : BaseEntity, IAggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        private List<Step> _steps = new List<Step>();
        public IEnumerable<Step> Steps => _steps.AsReadOnly();
        
        public Recipe(string title, string description)
        {
            Title = Guard.Against.NullOrEmpty(title, nameof(title));
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }

        public void AddStep(Step newStep)
        {
            Guard.Against.Null(newStep, nameof(newStep));
            _steps.Add(newStep);

            var newStepAddedEvent = new NewStepAddedEvent(this, newStep);
            Events.Add(newStepAddedEvent);
        }

        public void UpdateTitle(string newTitle)
        {
            Title = Guard.Against.NullOrEmpty(newTitle, nameof(newTitle));
        }

        public void UpdateDescription(string newDescription)
        {
            Description = Guard.Against.NullOrEmpty(newDescription, nameof(newDescription));
        }
    }
}
