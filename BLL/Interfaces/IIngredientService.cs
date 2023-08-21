using BLL.Models;

namespace BLL.Interfaces
{
    public interface IIngredientService : ICrud<IngredientModel>, IBaseService<IngredientModel>
    {
        Task<IngredientModel> GetByNameAsync(string value);
    }
}
