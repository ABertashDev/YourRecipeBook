using DAL.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class RecipeCategory : BaseEntity
    {

        public string Name { get; set; }
        public int SortOrder { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

    }
}
