using Cookbook.SharedKernel;
using Cookbook.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookbook.Core.RecipeAggregate
{
    public partial class RecipeIngredient : BaseEntity, IAggregateRoot
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = null!;

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Recipe Recipe { get; set; } = null!;
    }
}
