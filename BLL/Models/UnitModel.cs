using System.Collections.Generic;

namespace DAL.Entities
{
    public class UnitModel
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<int> IngredientIds { get; set; }
        public ICollection<int> RecipeDetailIds { get; set; }

    }
}
