using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Data;

namespace BLL.Services
{
    public class IngredientService : BaseService, IIngredientService
    {

        public IngredientService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<IngredientModel> AddAsync(IngredientModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IngredientModel model)
        {
            throw new NotImplementedException();
        }

        private async Task<Ingredient?> GetNotDeletedByNameAsync(string value)
        {
            return await _context.Ingredients
                .Where(x => x.Name.Equals(value) && !x.IsDeleted)
                .Include(x => x.Unit)
                .FirstOrDefaultAsync();
        }

        public Task<IEnumerable<IngredientModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IngredientModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public Task<IngredientModel> UpdateAsync(IngredientModel model)
        {
            return _mapper.Map<IngredientModel>(await GetFullNotDeletedByIdAsync(id));
        }

        public async Task<IngredientModel> GetByNameAsync(string value)
        {
            return _mapper.Map<IngredientModel>(await GetNotDeletedByNameAsync(value));
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
