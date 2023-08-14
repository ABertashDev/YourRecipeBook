
namespace BLL.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {

        Task<TModel> GetByIdAsync(int id);

        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> AddAsync(TModel model);

        Task<TModel> UpdateAsync(TModel model);

        Task DeleteAsync(TModel model);

    }
}
