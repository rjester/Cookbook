using Ardalis.GuardClauses;

namespace Cookbook.Core
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Description { get; private set; }

        public Ingredient(string description)
        {
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }
    }
}