using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {

        Task<TModel> GetByIdAsync(int id);

        Task<IEnumerable<TModel>> GetAllAsync();

        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);
       
        Task DeleteAsync(TModel model);

    }
}
