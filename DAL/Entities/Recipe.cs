using System.Collections.Generic;

namespace DAL.Entities
{
    public class Recipe : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }

        public RecipeCategory? Category { get; set; }
        public ICollection<RecipeDetail> RecipeDetails { get; set; }
        public ICollection<CookingStep> CookingSteps { get; set; }
    }
}
