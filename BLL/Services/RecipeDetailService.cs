using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RecipeDetailService : BaseService, IRecipeDetailService
    {

        public RecipeDetailService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }


        private async Task<RecipeDetail?> GetFullNotDeletedByIdAsync(int id)
        {
            return await _context.RecipeDetails
                .Where(x => x.Id.Equals(id) && !x.IsDeleted)
                .Include(x => x.Recipe)
                .Include(x => x.Ingredient)
                .Include(x => x.Unit)
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<RecipeDetail>> GetAllFullNotDeletedAsync()
        {
            return await _context.RecipeDetails
               .Where(x => !x.IsDeleted)
               .Include(x => x.Recipe)
               .Include(x => x.Ingredient)
               .Include(x => x.Unit)
               .ToListAsync();
        }

        public async Task<RecipeDetailModel> AddAsync(RecipeDetailModel model)
        {
            if (model is null)
            {
                throw new ArgumentException($"The RecipeDetailModel model is empty", nameof(model));
            }

            if (!IsValid(model))
            {
                throw new ArgumentException($"The RecipeDetailModel is invalid", nameof(model));
            }

            var entity = _mapper.Map<RecipeDetail>(model);

            await _context.RecipeDetails.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeDetailModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetFullNotDeletedByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"The RecipeDetail with id ({id}) was not found.");
            }

            _context.RecipeDetails.Delete(entity, true);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipeDetailModel>> GetAllAsync()
        {
            return _mapper.Map<List<RecipeDetailModel>>(await GetAllFullNotDeletedAsync());
        }

        public async Task<RecipeDetailModel> GetByIdAsync(int id)
        {
            return _mapper.Map<RecipeDetailModel>(await GetFullNotDeletedByIdAsync(id));
        }

        public bool IsValid(RecipeDetailModel model)
        {
            return true;
        }

        public async Task<RecipeDetailModel> UpdateAsync(int id, RecipeDetailModel model)
        {
            var existingEntity = await _context.RecipeDetails.GetNotDeletedByIdAsync(id);

            if (existingEntity is null)
            {
                throw new NotFoundException($"The RecipeDetail with id ({id}) was not found.");
            }

            var newModel = _mapper.Map<RecipeDetail>(model);

            existingEntity.IngredientId = newModel.IngredientId ?? existingEntity.IngredientId;
            existingEntity.UnitId = newModel.UnitId ?? existingEntity.UnitId;
            existingEntity.Quantity = newModel.Quantity;

            _context.RecipeDetails.Update(existingEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeDetailModel>(existingEntity);
        }
    }
}
