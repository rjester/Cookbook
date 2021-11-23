using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Services.Dtos
{
    public class RecipeDto
    {

            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public StepDto[] Steps { get; set; }
            public IngredientDto[] Ingredients { get; set; }
    }

    public class StepDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
    }
}
