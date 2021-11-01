namespace Cookbook.Data.Entities
{
    public class Step : BaseEntity
    {
        public string Description { get; set; }
        public Recipe Recipe { get; set; }
    }
}
