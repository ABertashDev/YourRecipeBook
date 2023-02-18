using System.Collections.Generic;

namespace BLL.Models
{
    public class RecipeModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<int> RecipeDetailIds { get; set; }
        public ICollection<int> CookingStepIds { get; set; }

    }
}
