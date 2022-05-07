using Ardalis.GuardClauses;

namespace Cookbook.Core
{
    public class Step
    {
        public int Id { get; set; }
        public string Description { get; private set; }

        public Step(string description)
        {
            Description = Guard.Against.NullOrEmpty(description, nameof(description));
        }
    }
}
