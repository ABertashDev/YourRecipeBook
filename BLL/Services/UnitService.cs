using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using Bogus.Bson;
using DAL.Data;

namespace BLL.Services
{
    public class UnitService : BaseService, IUnitService
    {

        public UnitService(RecipeBookDbContext context, IMapper mapper)
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


        private async Task<Unit?> GetNotDeletedByAbbreviationAsync(string value)
        {
            return await _context.Units
                .Where(x => x.Abbreviation.Equals(value.Trim()) && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<UnitModel> AddAsync(UnitModel model)
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

        public async Task<UnitModel> GetByAbbreviationAsync(string value)
        {
            return _mapper.Map<UnitModel>(await GetNotDeletedByAbbreviationAsync(value));
        }

        public bool IsValid(UnitModel model)
        {
            throw new NotImplementedException();
        }
    }
}
