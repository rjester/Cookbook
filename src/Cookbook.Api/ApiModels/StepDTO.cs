using Cookbook.Core.RecipeAggregate;
using System.ComponentModel.DataAnnotations;

namespace Cookbook.Api.ApiModels
{
    public class StepDTO
    {
        public int Id { get; set; }
        [Required]
        public string? Description { get; set; }

        public static StepDTO FromStep(Step step)
        {
            return new StepDTO
            {
                Id = step.Id,
                Description = step.Description
            };
        }
    }
}
