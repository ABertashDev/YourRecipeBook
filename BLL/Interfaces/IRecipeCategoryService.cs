using BLL.Models;

namespace BLL.Interfaces
{
    public interface IRecipeCategoryService : ICrud<RecipeCategoryModel>, IBaseService<RecipeCategoryModel>
    {
        Task<RecipeCategoryModel> GetByNameAsync(string value);
    }
}
