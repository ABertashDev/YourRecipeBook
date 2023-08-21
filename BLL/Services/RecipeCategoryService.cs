using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RecipeCategoryService : BaseService, IRecipeCategoryService
    {

        public RecipeCategoryService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }


        private async Task<RecipeCategory?> GetFullNotDeletedByIdAsync(int id)
        {
            return await _context.RecipeCategories
                .Where(x => x.Id.Equals(id) && !x.IsDeleted)
                .Include(x => x.Recipes)
                    .ThenInclude(y => y.RecipeDetails)
                .Include(x => x.Recipes)
                    .ThenInclude(y => y.CookingSteps)
                .FirstOrDefaultAsync();
        }

        private async Task<IEnumerable<RecipeCategory>> GetAllFullNotDeletedAsync()
        {
            return await _context.RecipeCategories
               .Where(x => !x.IsDeleted)
               .Include(x => x.Recipes)
                   .ThenInclude(y => y.RecipeDetails)
               .Include(x => x.Recipes)
                   .ThenInclude(y => y.CookingSteps)
               .ToListAsync();
        }

        private async Task<RecipeCategory?> GetNotDeletedByNameAsync(string value)
        {
            return await _context.RecipeCategories
                .Where(x => x.Name.Equals(value) && !x.IsDeleted)
                .Include(x => x.Recipes)
                    .ThenInclude(y => y.RecipeDetails)
                .Include(x => x.Recipes)
                    .ThenInclude(y => y.CookingSteps)
                .FirstOrDefaultAsync();
        }

        public async Task<RecipeCategoryModel> AddAsync(RecipeCategoryModel model)
        {
            if (model is null)
            {
                throw new ArgumentException($"The RecipeCategoryModel model is empty", nameof(model));
            }

            if (!IsValid(model))
            {
                throw new ArgumentException($"The RecipeCategoryModel is invalid", nameof(model));
            }

            var entity = _mapper.Map<RecipeCategory>(model);

            await _context.RecipeCategories.AddAsync(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeCategoryModel>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetFullNotDeletedByIdAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"The RecipeCategory with id ({id}) was not found.");
            }

            _context.RecipeCategories.Delete(entity, false);

            foreach (var item in entity.Recipes)
            {
                item.Category = null;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipeCategoryModel>> GetAllAsync()
        {
            return _mapper.Map<List<RecipeCategoryModel>>(await GetAllFullNotDeletedAsync());
        }

        public async Task<RecipeCategoryModel> GetByIdAsync(int id)
        {
            return _mapper.Map<RecipeCategoryModel>(await GetFullNotDeletedByIdAsync(id));
        }

        public async Task<RecipeCategoryModel> GetByNameAsync(string value)
        {
            return _mapper.Map<RecipeCategoryModel>(await GetNotDeletedByNameAsync(value));
        }

        public bool IsValid(RecipeCategoryModel model)
        {
            return true;
        }

        public async Task<RecipeCategoryModel> UpdateAsync(int id, RecipeCategoryModel model)
        {
            var existingEntity = await _context.RecipeCategories.GetNotDeletedByIdAsync(id);

            if (existingEntity is null)
            {
                throw new NotFoundException($"The RecipeCategory with id ({id}) was not found.");
            }

            var newModel = _mapper.Map<RecipeCategory>(model);

            existingEntity.Name = newModel.Name;
            existingEntity.SortOrder = newModel.SortOrder;

            _context.RecipeCategories.Update(existingEntity);

            await _context.SaveChangesAsync();

            return _mapper.Map<RecipeCategoryModel>(existingEntity);
        }
    }
}
