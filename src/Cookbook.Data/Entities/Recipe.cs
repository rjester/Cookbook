namespace Cookbook.Data.Entities
{
    public class Recipe : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<Step> Steps { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }
}
