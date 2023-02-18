using System.Collections.Generic;

namespace BLL.Models
{
    public class IngredientModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }

        public ICollection<int> RecipeDetailIds { get; set; }


        public bool IsValid(out string message)
        {

            bool isValid = true;
            message = "";

            if (string.IsNullOrEmpty(Name))
            {
                message = $"Ingredient model with empty name!";
                isValid = false;
            }

            return isValid;
        }

    }
}
