using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    internal class RecipeService : BaseService, IRecipeService
    {

        public RecipeService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }


        private async Task<Recipe?> GetFullNotDeletedByIdAsync(int id)
        {
            return await _context.Recipes
                .Where(x => x.Id.Equals(id) && !x.IsDeleted)
                .Include(x => x.Category)
                .Include(x => x.CookingSteps)
                .Include(x => x.RecipeDetails)
                    .ThenInclude(y => y.Ingredient)
                .Include(x => x.RecipeDetails)
                    .ThenInclude(y => y.Unit)
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<Recipe>> GetAllFullNotDeletedAsync()
        {
            return await _context.Recipes
               .Where(x => !x.IsDeleted)
               .Include(x => x.Category)
               .Include(x => x.CookingSteps)
               .Include(x => x.RecipeDetails)
                   .ThenInclude(y => y.Ingredient)
               .Include(x => x.RecipeDetails)
                   .ThenInclude(y => y.Unit)
               .ToListAsync();
        }

        public async Task<RecipeModel> AddAsync(RecipeModel model)
        {
            if (model is null)
            {
                throw new ArgumentException($"The RecipeModel model is empty", nameof(model));
            }

            if (!IsValid(model))
            {
                throw new ArgumentException($"The RecipeModel is invalid", nameof(model));
            }

            var entity = _mapper.Map<Recipe>(model);

            await _context.Recipes.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetFullNotDeletedByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"The Recipe with id ({id}) was not found.");
            }

            _context.Recipes.Delete(entity, false);

            foreach (var item in entity.RecipeDetails)
            {
                _context.RecipeDetails.Delete(item, false);
            }

            foreach (var item in entity.CookingSteps)
            {
                _context.CookingSteps.Delete(item, false);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipeModel>> GetAllAsync()
        {
            return _mapper.Map<List<RecipeModel>>(await GetAllFullNotDeletedAsync());
        }

        public async Task<RecipeModel> GetByIdAsync(int id)
        {
            return _mapper.Map<RecipeModel>(await GetFullNotDeletedByIdAsync(id));
        }

        public bool IsValid(RecipeModel model)
        {
            return true;
        }

        public async Task<RecipeModel> UpdateAsync(int id, RecipeModel model)
        {
            var existingEntity = await _context.Recipes.GetNotDeletedByIdAsync(id);

            if (existingEntity is null)
            {
                throw new NotFoundException($"The Recipe with id ({id}) was not found.");
            }

            var newModel = _mapper.Map<Recipe>(model);

            existingEntity.Name = newModel.Name;
            existingEntity.Description = newModel.Description;
            existingEntity.CategoryId = newModel.CategoryId ?? existingEntity.CategoryId;

            _context.Recipes.Update(existingEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeModel>(existingEntity);
        }
    }
}