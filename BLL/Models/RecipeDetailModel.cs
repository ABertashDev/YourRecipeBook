

namespace BLL.Models
{
    public class RecipeDetailModel
    {

        public int Id { get; set; }
        public double Quantity { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }

    }
}
