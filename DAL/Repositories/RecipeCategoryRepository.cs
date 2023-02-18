using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RecipeCategoryRepository : CommonRepository<RecipeCategory>, IRecipeCategoryRepository
    {

        public RecipeCategoryRepository(RecipeBookDbContext context)
            : base(context)
        {
        }

        public async Task<RecipeCategory> GetByNameAsync(string name)
        {
            return await dbContext.Set<RecipeCategory>().FindAsync(name);
        }
    }
}
