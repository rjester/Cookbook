using Cookbook.SharedKernel;
using Cookbook.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookbook.Core.RecipeAggregate
{
    public partial class Ingredient : BaseEntity, IAggregateRoot
    {
        public Ingredient()
        {
            RecipeIngredients = new HashSet<RecipeIngredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
