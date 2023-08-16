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
            throw new NotImplementedException();
        }
    }
}
