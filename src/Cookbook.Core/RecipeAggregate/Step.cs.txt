using Ardalis.GuardClauses;
using Cookbook.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Core.RecipeAggregate
{
    public class Step : BaseEntity
    {
        public string Description { get; private set; }

        public Step(string description)
        {
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }

        public void UpdateDescription(string newDescription)
        {
            Description = Guard.Against.NullOrEmpty(newDescription, nameof(newDescription));
        }
    }
}
