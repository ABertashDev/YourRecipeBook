using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Data;

namespace BLL.Services
{
    public class RecipeService : BaseService, IRecipeService
    {

        public RecipeService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<RecipeModel> AddAsync(RecipeModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(RecipeModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RecipeModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeModel> UpdateAsync(RecipeModel model)
        {
            throw new NotImplementedException();
        }
    }
}
