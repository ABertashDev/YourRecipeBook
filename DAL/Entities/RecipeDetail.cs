
namespace DAL.Entities
{
    public class RecipeDetail : BaseEntity
    {
        public double Quantity { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public int UnitId { get; set; }

        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        public Unit Unit { get; set; }
    }
}
