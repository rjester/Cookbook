namespace Cookbook.Data.Entities
{
    public class RecipeIngredient : BaseEntity
    {
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
    }
}
