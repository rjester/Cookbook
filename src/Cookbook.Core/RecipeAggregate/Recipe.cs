using Cookbook.SharedKernel;
using Cookbook.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookbook.Core.RecipeAggregate
{
    public partial class Recipe : BaseEntity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual ICollection<Step> Steps { get; set; }

        public Recipe()
        {
            //RecipeIngredients = new HashSet<RecipeIngredient>();
            //Steps = new HashSet<Step>();
        }

        public Recipe(string title, string description) : this()
        {
            Title = title;
            Description = description;
        }

        public Recipe(string title, string description, 
                HashSet<Step> steps = null,
                HashSet<RecipeIngredient> ingredients = null) : this()
        {
            Title = title;
            Description = description;
            Steps = steps ?? new HashSet<Step>();
            RecipeIngredients = ingredients ?? new HashSet<RecipeIngredient>();
        }

        public void AddStep(Step step)
        {

        }
    }
}
