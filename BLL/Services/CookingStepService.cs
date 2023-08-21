using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class CookingStepService : BaseService, ICookingStepService
    {
        public CookingStepService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }


        private async Task<CookingStep?> GetFullNotDeletedByIdAsync(int id)
        {
            return await _context.CookingSteps
                .Where(x => x.Id.Equals(id) && !x.IsDeleted)
                .Include(x => x.Recipe)
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<CookingStep>> GetAllFullNotDeletedAsync()
        {
            return await _context.CookingSteps
               .Where(x => !x.IsDeleted)
               .Include(x => x.Recipe)
               .ToListAsync();
        }

        public async Task<CookingStepModel> AddAsync(CookingStepModel model)
        {
            if (model is null)
            {
                throw new ArgumentException($"The CookingStepModel model is empty", nameof(model));
            }

            if (!IsValid(model))
            {
                throw new ArgumentException($"The CookingStepModel is invalid", nameof(model));
            }

            var entity = _mapper.Map<CookingStep>(model);

            await _context.CookingSteps.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CookingStepModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetFullNotDeletedByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"The CookingStep with id ({id}) was not found.");
            }

            _context.CookingSteps.Delete(entity, true);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CookingStepModel>> GetAllAsync()
        {
            return _mapper.Map<List<CookingStepModel>>(await GetAllFullNotDeletedAsync());
        }

        public async Task<CookingStepModel> GetByIdAsync(int id)
        {
            return _mapper.Map<CookingStepModel>(await GetFullNotDeletedByIdAsync(id));
        }

        public bool IsValid(CookingStepModel model)
        {
            return true;
        }

        public async Task<CookingStepModel> UpdateAsync(int id, CookingStepModel model)
        {
            var existingEntity = await _context.CookingSteps.GetNotDeletedByIdAsync(id);

            if (existingEntity is null)
            {
                throw new NotFoundException($"The CookingStep with id ({id}) was not found.");
            }

            var newModel = _mapper.Map<CookingStep>(model);

            existingEntity.Number = newModel.Number;
            existingEntity.Name = newModel.Name;
            existingEntity.Description = newModel.Description;
            existingEntity.Photo = newModel.Photo;

            _context.CookingSteps.Update(existingEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<CookingStepModel>(existingEntity);
        }
    }
}
