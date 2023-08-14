using System.Collections.Generic;

namespace DAL.Entities
{
    public class Unit : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
