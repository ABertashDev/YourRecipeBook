using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Data;

namespace BLL.Services
{
    public class RecipeCategoryService : BaseService, IRecipeCategoryService
    {

        public RecipeCategoryService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }

        public Task<RecipeCategoryModel> AddAsync(RecipeCategoryModel model)
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

        public Task DeleteAsync(RecipeCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecipeCategoryModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeCategoryModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipeCategoryModel> GetByNameAsync(string value)
        {
            return _mapper.Map<RecipeCategoryModel>(await GetNotDeletedByNameAsync(value));
        }

        public bool IsValid(RecipeCategoryModel model)
        {
            throw new NotImplementedException();
        }

        public Task<RecipeCategoryModel> UpdateAsync(RecipeCategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
