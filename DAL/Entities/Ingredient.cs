using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Ingredient : BaseEntity
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitId { get; set; }

        public Unit Unit { get; set; }
        public ICollection<RecipeDetail> RecipeDetails { get; set; }

    }
}
