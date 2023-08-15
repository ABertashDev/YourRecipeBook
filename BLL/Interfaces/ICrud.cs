
namespace BLL.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {

        Task<TModel> GetByIdAsync(int id);

        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> AddAsync(TModel model);

        Task<TModel> UpdateAsync(int id, TModel model);

        Task DeleteAsync(int id);

    }
}
