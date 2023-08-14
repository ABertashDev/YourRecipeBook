
namespace DAL.Entities
{
    public class CookingStep : BaseEntity
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }
    }
}
