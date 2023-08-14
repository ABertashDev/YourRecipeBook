
namespace BLL.Models
{
    public class CookingStepModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int RecipeId { get; set; }
    }
}
