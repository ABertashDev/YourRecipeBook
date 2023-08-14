using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Data;

namespace BLL.Services
{
    internal class RecipeDetailService : BaseService, IUnitService
    {

        public RecipeDetailService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<UnitModel> AddAsync(UnitModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UnitModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UnitModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UnitModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public Task<UnitModel> UpdateAsync(UnitModel model)
        {
            throw new NotImplementedException();
        }
    }
}
