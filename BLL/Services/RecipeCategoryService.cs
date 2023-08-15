using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Data;

namespace BLL.Services
{
    internal class RecipeCategoryService : BaseService, IRecipeCategoryService
    {

        public RecipeCategoryService(RecipeBookDbContext context, IMapper mapper)
            : base(context, mapper)
        {

        }

        public Task<RecipeCategoryModel> AddAsync(RecipeCategoryModel model)
        {
            throw new NotImplementedException();
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

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public Task<RecipeCategoryModel> UpdateAsync(RecipeCategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
