using BLL.Models;

namespace BLL.Interfaces
{
    public interface IUnitService : ICrud<UnitModel>, IBaseService<UnitModel>
    {
        Task<UnitModel> GetByAbbreviationAsync(string value);
    }
}
