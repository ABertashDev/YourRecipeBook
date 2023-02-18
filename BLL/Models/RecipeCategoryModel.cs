using System.Collections.Generic;

namespace BLL.Models
{
    public class RecipeCategoryModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }

        public ICollection<int> RecipeIds { get; set; }

    }
}
