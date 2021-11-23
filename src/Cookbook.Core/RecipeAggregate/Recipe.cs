using Cookbook.Core.Events;
using Cookbook.SharedKernel;
using Cookbook.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookbook.Core.RecipeAggregate
{
    public partial class Recipe : BaseEntity, IAggregateRoot
    {
        private List<Step> _steps = new List<Step>();
        private List<RecipeIngredient> _recipeIngredients = new List<RecipeIngredient>();
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        //public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        //public virtual ICollection<Step> Steps { get; set; }
        public IEnumerable<Step> Steps => _steps.AsReadOnly();
        public IEnumerable<RecipeIngredient> RecipeIngredients => _recipeIngredients.AsReadOnly();

        public Recipe()
        {
        }

        public Recipe(string title, string description) : this()
        {
            Title = title;
            Description = description;
        }

        public void AddStep(Step newStep)
        {
            _steps.Add(newStep);

            var newStepAddedEvent = new NewStepAddedEvent(this, newStep);
            Events.Add(newStepAddedEvent);
        }

        public void AddIngredient(RecipeIngredient newIngredient)
        {
            _recipeIngredients.Add(newIngredient);

            var newIngredientAddedEvent = new NewIngredientAddedEvent(this, newIngredient);
            Events.Add(newIngredientAddedEvent);
        }
    }
}
