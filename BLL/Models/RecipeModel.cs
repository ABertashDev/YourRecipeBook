
namespace BLL.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<RecipeDetailModel> RecipeDetails { get; set; }
        public IEnumerable<CookingStepModel> CookingSteps { get; set; }
    }
}
