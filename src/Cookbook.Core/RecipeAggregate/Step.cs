using Cookbook.SharedKernel;
using Cookbook.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Cookbook.Core.RecipeAggregate
{
    public partial class Step : BaseEntity, IAggregateRoot
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public int? RecipeId { get; set; }

        public virtual Recipe? Recipe { get; set; }
    }
}
