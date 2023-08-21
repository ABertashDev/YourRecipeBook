using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using Bogus.Bson;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class UnitService : BaseService, IUnitService
    {

        public UnitService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        private async Task<Unit?> GetFullNotDeletedByIdAsync(int id)
        {
            return await _context.Units
                .Where(x => x.Id.Equals(id) && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<Unit>> GetAllFullNotDeletedAsync()
        {
            return await _context.Units.Where(x => !x.IsDeleted).ToListAsync();
        }

        private async Task<Unit?> GetNotDeletedByAbbreviationAsync(string value)
        {
            return await _context.Units
                .Where(x => x.Abbreviation.Equals(value.Trim()) && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<UnitModel> AddAsync(UnitModel model)
        {
            if (model is null)
            {
                throw new ArgumentException($"The UnitModel model is empty", nameof(model));
            }

            if (!IsValid(model))
            {
                throw new ArgumentException($"The UnitModel is invalid", nameof(model));
            }

            var entity = _mapper.Map<Unit>(model);

            await _context.Units.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<UnitModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetFullNotDeletedByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"The Unit with id ({id}) was not found.");
            }

            _context.Units.Delete(entity, false);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UnitModel>> GetAllAsync()
        {
            return _mapper.Map<List<UnitModel>>(await GetAllFullNotDeletedAsync());
        }

        public async Task<UnitModel> GetByIdAsync(int id)
        {
            return _mapper.Map<UnitModel>(await GetFullNotDeletedByIdAsync(id));
        }

        public async Task<UnitModel> GetByAbbreviationAsync(string value)
        {
            return _mapper.Map<UnitModel>(await GetNotDeletedByAbbreviationAsync(value));
        }

        public bool IsValid(UnitModel model)
        {
            return true;
        }

        public async Task<UnitModel> UpdateAsync(int id, UnitModel model)
        {
            var existingEntity = await _context.Units.GetNotDeletedByIdAsync(id);

            if (existingEntity is null)
            {
                throw new NotFoundException($"The Unit with id ({id}) was not found.");
            }

            var newModel = _mapper.Map<Unit>(model);

            existingEntity.Name = newModel.Name;
            existingEntity.Abbreviation = newModel.Abbreviation;

            _context.Units.Update(existingEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<UnitModel>(existingEntity);
        }
    }
}
