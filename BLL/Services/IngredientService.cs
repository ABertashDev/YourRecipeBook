using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class IngredientService : BaseService, IIngredientService
    {

        public IngredientService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }


        private async Task<Ingredient?> GetFullNotDeletedByIdAsync(int id)
        {
            return await _context.Ingredients
                .Where(x => x.Id.Equals(id) && !x.IsDeleted)
                .Include(x => x.Unit)
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<Ingredient>> GetAllFullNotDeletedAsync()
        {
            return await _context.Ingredients
               .Where(x => !x.IsDeleted)
               .Include(x => x.Unit)
               .ToListAsync();
        }

        public async Task<IngredientModel> AddAsync(IngredientModel model)
        {
            if (model is null)
            {
                throw new ArgumentException($"The IngredientModel model is empty", nameof(model));
            }

            if (!IsValid(model))
            {
                throw new ArgumentException($"The IngredientModel is invalid", nameof(model));
            }

            var entity = _mapper.Map<Ingredient>(model);

            await _context.Ingredients.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<IngredientModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetFullNotDeletedByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"The Ingredient with id ({id}) was not found.");
            }

            _context.Ingredients.Delete(entity, false);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IngredientModel>> GetAllAsync()
        {
            return _mapper.Map<List<IngredientModel>>(await GetAllFullNotDeletedAsync());
        }

        public async Task<IngredientModel> GetByIdAsync(int id)
        {
            return _mapper.Map<IngredientModel>(await GetFullNotDeletedByIdAsync(id));
        }

        public bool IsValid(IngredientModel model)
        {
            return true;
        }

        public async Task<IngredientModel> UpdateAsync(int id, IngredientModel model)
        {
            var existingEntity = await _context.Ingredients.GetNotDeletedByIdAsync(id);

            if (existingEntity is null)
            {
                throw new NotFoundException($"The Ingredient with id ({id}) was not found.");
            }

            var newModel = _mapper.Map<Ingredient>(model);

            existingEntity.Name = newModel.Name;
            existingEntity.Description = newModel.Description;
            existingEntity.UnitId = newModel.UnitId ?? existingEntity.UnitId;

            _context.Ingredients.Update(existingEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<IngredientModel>(existingEntity);
        }
    }
}
